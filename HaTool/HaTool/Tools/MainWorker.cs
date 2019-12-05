using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Data;
using System.Windows.Forms;
using CsLib;

namespace HaTool.Tools
{
    class MainWorker
    {

        ThreadCallBackMethod CallbackMethodName { get; set; }

        public string Ip { get; set; }
        public string Port { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
        public string Catalog { get; set; }
        public int ConnectionTimeoutSec { get; set; }
        public int CommandTimeoutSec { get; set; }
        public string Querys { get; set; }
        public string ConnectionString { get; set; }
        public int ErrorCnt { get; set; }
        public StringBuilder sbResultAll { get; set; }
        public bool CancelRequested { get; set; } = false;
        public int recordCount = 0;
        private DateTime startTime;
        private DateTime endTime;
        private string ColumnDelimiter { get; set; }

        SqlCommand cmd;

        public MainWorker(
            ThreadCallBackMethod methodName
            , string ip
            , string port
            , string userId
            , string password
            , string catalog
            , int connectionTimeoutSec
            , int commandTimeoutSec
            , string querys
            , string columnDelimiter
        )
        {
            CallbackMethodName = methodName;
            Ip = ip;
            Port = port;
            UserId = userId;
            Password = password;
            Catalog = catalog;
            ConnectionTimeoutSec = connectionTimeoutSec;
            CommandTimeoutSec = commandTimeoutSec;
            Querys = querys;
            ConnectionString = SetConnectionString(Catalog);
            sbResultAll = new StringBuilder();
            ColumnDelimiter = columnDelimiter;
        }

        private string SetConnectionString(string initialCatalog, int connectionTimeout = 5)
        {
            return new SqlConnectionStringBuilder
            {
                DataSource = Ip + "," + Port,
                UserID = UserId,
                Password = Password,
                InitialCatalog = initialCatalog,
                ConnectTimeout = ConnectionTimeoutSec
            }.ConnectionString;
        }

        public void CancelWork()
        {
            CancelRequested = true;
            sbResultAll.Append("-->>---------------------------------\r\n");
            sbResultAll.Append("\r\nCancel requested time : ");
            sbResultAll.Append(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            sbResultAll.Append("\r\n---------------------------------<<--\r\n\r\n");

            if (cmd != null)
            {
                cmd.Cancel();
            }
        }

        public void DoWork(object theadPoolStatus)
        {
            bool bReturn = false;
            ThreadPoolStatus status = theadPoolStatus as ThreadPoolStatus;
            if (status == null) return;
            Debug.WriteLine("WorkQueue Ready!");

            // 한번에 실행하려고
            status.stop.WaitOne();

            // 실행
            bReturn = QueryExecuter(Querys, CommandTimeoutSec);

            // 콜백
            CallbackMethodName(Ip, Port, Catalog, bReturn, ErrorCnt, sbResultAll);

            // 완료 시그널 다음 Thread 고~
            status.signal.Set();
        }


        private bool QueryExecuter(string listStringQuery, int commandTimeout = 30)
        {
            bool bReturn = false;
            sbResultAll.Clear();
            List<string> querys = TranString.ReadQuery(listStringQuery);
            startTime = DateTime.Now;
            
            foreach (var query in querys)
            {
                try
                {
                    if (CancelRequested)
                        break;

                    sbResultAll.Append("-->>---------------------------------");
                    sbResultAll.Append(query + Environment.NewLine);
                    sbResultAll.Append("---------------------------------<<--" + Environment.NewLine + Environment.NewLine);
                    if (query.Trim().ToUpper().StartsWith("USE"))
                    {
                        string[] database = query.Trim().Split(new[] { Environment.NewLine, " " }, StringSplitOptions.None);
                        ConnectionString = SetConnectionString(database[1]);

                    }
                    string result = string.Empty;


                    using (SqlConnection conn = new SqlConnection(ConnectionString))
                    {
                        conn.Open();
                        conn.InfoMessage += Conn_InfoMessage; // message hook (like backup message) 


                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.StatementCompleted += Cmd_StatementCompleted; // retrive row count

                            cmd.CommandType = CommandType.Text;

                            //// execute only estimation plan text
                            //cmd.CommandText = "SET SHOWPLAN_ALL ON";
                            //cmd.ExecuteNonQuery();

                            //// execute only estimation plan xml
                            //cmd.CommandText = "SET SHOWPLAN_XML ON";
                            //cmd.ExecuteNonQuery();
                                                       
                            cmd.CommandText = query;
                            SqlDataReader reader = cmd.ExecuteReader();

                            do
                            {
                                StringBuilder sb = new StringBuilder();
                                string Header = string.Empty;
                                string Line = string.Empty;
                                DataTable schemaTable = reader.GetSchemaTable();
                                if (schemaTable != null)
                                {
                                    try
                                    {
                                        foreach (DataRow row in schemaTable.Rows)
                                        {
                                            foreach (DataColumn column in schemaTable.Columns)
                                            {
                                                if (column.ColumnName == "ColumnName")
                                                {
                                                    Header = Header + row[column] + ColumnDelimiter;
                                                }
                                            }
                                        }
                                        Header = Header + Environment.NewLine;
//                                        Line = Line + Environment.NewLine;
                                        sb.Append(Header);
                                        sb.Append(Line);

                                        while (reader.Read())
                                        {
                                            for (int i = 0; i < reader.FieldCount; i++)
                                            {
                                                if (reader.GetValue(i).ToString() == "System.Byte[]")
                                                    sb.Append("0x" + BitConverter.ToString((byte[])reader.GetValue(i)).Replace("-", ""));
                                                else
                                                    sb.Append(reader.GetValue(i).ToString());
                                                sb.Append(ColumnDelimiter);
                                            }
                                            sb.Append(Environment.NewLine);
                                        }

                                    }
                                    catch (SqlException ex)
                                    {
                                        ErrorCnt++;
                                        sbResultAll.Append("-->>---------------------------------" + Environment.NewLine);
                                        sbResultAll.Append("--SQL Exception" + Environment.NewLine);
                                        sbResultAll.Append("-->>---------------------------------" + Environment.NewLine);
                                        for (int i = 0; i < ex.Errors.Count; i++)
                                        {
                                            sbResultAll.Append("Inner SqlException No #" + i + Environment.NewLine +
                                            "Message: " + ex.Errors[i].Message + Environment.NewLine +
                                            "Source: " + ex.Errors[i].Source + Environment.NewLine +
                                            "Procedure: " + ex.Errors[i].Procedure + Environment.NewLine);
                                        }
                                    }
                                    finally
                                    {
                                        sb.Append(Environment.NewLine);
                                        sbResultAll.Append(sb);
                                        sbResultAll.Append(string.Format("({0} {1} affected)" + Environment.NewLine + Environment.NewLine, recordCount, (recordCount == 1) ? "row" : "rows"));
                                    }
                                }
                                else
                                {
                                    string[] Query = query.Trim().Split(new[] { Environment.NewLine, " " }, StringSplitOptions.None);
                                    if (
                                        Query[0].Equals("update", StringComparison.OrdinalIgnoreCase)
                                        || Query[0].Equals("insert", StringComparison.OrdinalIgnoreCase)
                                        || Query[0].Equals("delete", StringComparison.OrdinalIgnoreCase)
                                        || Query[1].Equals("update", StringComparison.OrdinalIgnoreCase)
                                        || Query[1].Equals("insert", StringComparison.OrdinalIgnoreCase)
                                        || Query[1].Equals("delete", StringComparison.OrdinalIgnoreCase)
                                        )
                                        sbResultAll.Append(string.Format("({0} {1} affected)" + Environment.NewLine + Environment.NewLine, recordCount, (recordCount == 1) ? "row" : "rows"));
                                    else
                                        sbResultAll.Append(string.Format("Commands completed successfully." + Environment.NewLine + Environment.NewLine));
                                }

                            } while (reader.NextResult());
                        }
                        conn.Close();
                        bReturn = true;
                    }
                }

                catch (SqlException ex)
                {
                    ErrorCnt++;
                    sbResultAll.Append("-->>---------------------------------" + Environment.NewLine);
                    sbResultAll.Append("--SQL Exception" + Environment.NewLine);
                    sbResultAll.Append("-->>---------------------------------" + Environment.NewLine);

                    for (int i = 0; i < ex.Errors.Count; i++)
                    {
                        sbResultAll.Append("SqlException No #" + i + Environment.NewLine +
                        "Message: " + ex.Errors[i].Message + Environment.NewLine +
                        "Source: " + ex.Errors[i].Source + Environment.NewLine +
                        "Procedure: " + ex.Errors[i].Procedure + Environment.NewLine);
                    }

                    sbResultAll.Append("---------------------------------<<--" + Environment.NewLine + Environment.NewLine);

                    bReturn = false;
                }
                catch (Exception ex)
                {
                    ErrorCnt++;
                    sbResultAll.Append("-->>---------------------------------" + Environment.NewLine);
                    sbResultAll.Append("--Exception" + Environment.NewLine);
                    sbResultAll.Append("-->>---------------------------------" + Environment.NewLine);
                    sbResultAll.Append(ex.Message);
                    sbResultAll.Append("---------------------------------<<--" + Environment.NewLine + Environment.NewLine);
                    bReturn = false;
                }
            }

            //sbResultAll.Append(Environment.NewLine + "-->>---------------------------------" + Environment.NewLine);
            endTime = DateTime.Now;
            //sbResultAll.Append("EndTime : " + endTime.ToString("yyyy-MM-dd HH:mm:ss.fff") + Environment.NewLine);
            sbResultAll.Append("---------------------------------<<--" + Environment.NewLine + Environment.NewLine);
            sbResultAll.Append(Environment.NewLine + "-->>---------------------------------" + Environment.NewLine);
            sbResultAll.Append("StartTime : " + startTime.ToString("yyyy-MM-dd HH:mm:ss.fff") + Environment.NewLine);
            sbResultAll.Append("EndTime : " + endTime.ToString("yyyy-MM-dd HH:mm:ss.fff") + Environment.NewLine);
            TimeSpan diff = endTime - startTime;
            string formatted = string.Format(
                                   "TotalExecutionTime : {0} days, {1} hours, {2} minutes, {3} seconds, {4} miliseconds",
                                   diff.Days,
                                   diff.Hours,
                                   diff.Minutes,
                                   diff.Seconds,
                                   diff.Milliseconds
                                   );
            sbResultAll.Append(formatted + Environment.NewLine);
            sbResultAll.Append("---------------------------------<<--" + Environment.NewLine + Environment.NewLine);

            return bReturn;
        }

        private void Cmd_StatementCompleted(object sender, StatementCompletedEventArgs e)
        {
            recordCount = e.RecordCount;
        }

        private void Conn_InfoMessage(object sender, SqlInfoMessageEventArgs e)
        {
            sbResultAll.Append(e.Message + Environment.NewLine);
        }
    }
}
