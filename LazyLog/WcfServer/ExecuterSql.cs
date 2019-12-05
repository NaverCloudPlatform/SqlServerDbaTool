using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using CsLib;
using Newtonsoft;
using Newtonsoft.Json;
using System.Threading;
using System.Data.SqlClient;
using System.Data;

namespace lazylog
{
    class ExecuterSql
    {
        // sync and async sql process....using pipe connection ....
        Config config = Config.Instance;
        string logFilename = string.Empty;
        static readonly string logDirectory = "ExecuteLogSql";
        static string logDirectoryFullname = string.Empty;
        string logFileFullname = string.Empty;
        string cmdType = string.Empty;
        StringBuilder sbResultAll = new StringBuilder();
        WcfRestServer.WcfResponse wcfResponse = new WcfRestServer.WcfResponse();
        int errorCnt = 0;
        int recordCount = 0;
        string connectionString = string.Empty;
        string syncAsync = string.Empty; 

        public ExecuterSql(string cmdType)
        {
            this.cmdType = cmdType;
            logFilename = DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + Guid.NewGuid() + ".log";
            logDirectoryFullname = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, logDirectory);
            logFileFullname = Path.Combine(logDirectoryFullname, logFilename);
            LogDirectoryCheckAndCreate();
        }

        private void LogDirectoryCheckAndCreate()
        {
            if (!Directory.Exists(logDirectoryFullname))
                Directory.CreateDirectory(logDirectoryFullname);
            Common.FileDeleteFromDate(logDirectoryFullname, "*.log", new TimeSpan(24 * 7, 0, 0));
        }


        public WcfRestServer.WcfResponse Execute(string cmdText)
        {
            try
            {
                if (cmdType == null || cmdType.Trim().Equals(""))
                {
                    wcfResponse.IsSuccess = false;
                    wcfResponse.ResultMessage = "";
                    wcfResponse.ErrorMessage = "cmdType is empty";
                    return wcfResponse;
                }
                if (cmdText == null || cmdText.Trim().Equals(""))
                {
                    wcfResponse.IsSuccess = false;
                    wcfResponse.ResultMessage = "";
                    wcfResponse.ErrorMessage = "cmdText is empty";
                    return wcfResponse;
                }

                Common.FileLogWriteLine(logFileFullname, $"cmdType : {cmdType}");
                Common.FileLogWriteLine(logFileFullname, $"cmdText : {cmdText}");

                if (cmdType.Equals("TypeSql", StringComparison.OrdinalIgnoreCase))
                {
                    var typeSql = JsonConvert.DeserializeObject<TypeSql>(cmdText);
                    if (typeSql.SyncAsync == null || typeSql.SyncAsync.Trim().Equals(""))
                    {
                        wcfResponse.IsSuccess = false;
                        wcfResponse.ResultMessage = "";
                        wcfResponse.ErrorMessage = "SyncAsync is Empty";
                        return wcfResponse;
                    }
                    if (typeSql.ConnectionTimeout == null || typeSql.ConnectionTimeout.Trim().Equals(""))
                    {
                        wcfResponse.IsSuccess = false;
                        wcfResponse.ResultMessage = "";
                        wcfResponse.ErrorMessage = "ConnectionTimeout is Empty";
                        return wcfResponse;
                    }
                    if (typeSql.CommandTimeout == null || typeSql.CommandTimeout.Trim().Equals(""))
                    {
                        wcfResponse.IsSuccess = false;
                        wcfResponse.ResultMessage = "";
                        wcfResponse.ErrorMessage = "CmdTimeout is Empty";
                        return wcfResponse;
                    }
                    if (typeSql.Database == null || typeSql.Database.Trim().Equals(""))
                    {
                        wcfResponse.IsSuccess = false;
                        wcfResponse.ResultMessage = "";
                        wcfResponse.ErrorMessage = "Database is Empty";
                        return wcfResponse;
                    }
                    if (typeSql.Querys == null || typeSql.Querys.Trim().Equals(""))
                    {
                        wcfResponse.IsSuccess = false;
                        wcfResponse.ResultMessage = "";
                        wcfResponse.ErrorMessage = "Querys is Empty";
                        return wcfResponse;
                    }
                    if (typeSql.QueryEchoYN == null || typeSql.QueryEchoYN.Trim().Equals(""))
                    {
                        typeSql.QueryEchoYN = "N";
                    }
                    if (typeSql.CountYN == null || typeSql.CountYN.Trim().Equals(""))
                    {
                        typeSql.CountYN = "N";
                    }
                    if (typeSql.HeaderYN == null || typeSql.HeaderYN.Trim().Equals(""))
                    {
                        typeSql.HeaderYN = "N";
                    }

                    if (!Int32.TryParse(typeSql.ConnectionTimeout, out int connectionTimeout))
                    {
                        wcfResponse.IsSuccess = false;
                        wcfResponse.ResultMessage = "";
                        wcfResponse.ErrorMessage = "ConnectionTimeout TryParse Error";
                        return wcfResponse;
                    }
                    if (!Int32.TryParse(typeSql.CommandTimeout, out int commandTimeout))
                    {
                        wcfResponse.IsSuccess = false;
                        wcfResponse.ResultMessage = "";
                        wcfResponse.ErrorMessage = "CommandTimeout TryParse Error";
                        return wcfResponse;
                    }

                    connectionString = SetConnectionString(typeSql.Database, connectionTimeout);
                    syncAsync = typeSql.SyncAsync;
                    if (typeSql.SyncAsync.Equals("Sync", StringComparison.OrdinalIgnoreCase))
                    {
                        bool isSuccess = QueryExecuter(typeSql.Querys, connectionTimeout, commandTimeout, typeSql.QueryEchoYN);
                        wcfResponse.IsSuccess = isSuccess;
                        wcfResponse.ResultMessage = sbResultAll.ToString();
                        wcfResponse.ErrorMessage = "";
                        return wcfResponse;
                    }
                    else if (typeSql.SyncAsync.Equals("Async", StringComparison.OrdinalIgnoreCase))
                    {
                        Thread thread = new Thread(() => QueryExecuter(typeSql.Querys, connectionTimeout, commandTimeout, typeSql.QueryEchoYN));
                        thread.Start();

                        wcfResponse.IsSuccess = true;
                        wcfResponse.ResultMessage = logFileFullname;
                        wcfResponse.ErrorMessage = "";

                        return wcfResponse;
                        //thread.Join();
                        //Common.FileLogWriteLine(logFileFullname, sbResultAll.ToString());
                        //Common.FileLogWriteLine(logFileFullname, "rest async cmd completed");
                    }
                    else
                    {
                        wcfResponse.IsSuccess = false;
                        wcfResponse.ResultMessage = "";
                        wcfResponse.ErrorMessage = "unknown sync type";
                        return wcfResponse;
                    }
                }

                wcfResponse.IsSuccess = true;
                if (wcfResponse.ResultMessage == null || wcfResponse.ResultMessage.Trim().Equals(""))
                    wcfResponse.ResultMessage = logFileFullname;
            }
            catch (Exception ex)
            {
                wcfResponse.IsSuccess = false;
                wcfResponse.ErrorMessage = ex.Message; // exception message 
            }

            Common.FileLogWriteLine(logFileFullname, $"wcfResponse : {JsonConvert.SerializeObject(wcfResponse)}");
            return wcfResponse;
        }


        bool QueryExecuter(string listStringQuery, int connectionTimeout = 5, int commandTimeout = 30, string queryEchoYN="N", string countYN="N", string headerYN = "N")
        {
            bool bReturn = false;
            sbResultAll.Clear();
            
            List<string> querys = TranString.ReadQuery(listStringQuery);
            if (queryEchoYN.Equals("Y", StringComparison.OrdinalIgnoreCase))
            {
                sbResultAll.Append("-->>---------------------------------" + Environment.NewLine);
                sbResultAll.Append(DateTime.Now + Environment.NewLine);
                sbResultAll.Append("---------------------------------<<--" + Environment.NewLine + Environment.NewLine);
            }
            foreach (var query in querys)
            {
                try
                {
                    if (queryEchoYN.Equals("Y", StringComparison.OrdinalIgnoreCase))
                    {
                        sbResultAll.Append("-->>---------------------------------");
                        sbResultAll.Append(query + Environment.NewLine);
                        sbResultAll.Append("---------------------------------<<--" + Environment.NewLine + Environment.NewLine);
                    }
                    if (query.Trim().ToUpper().StartsWith("USE"))
                    {
                        string[] database = query.Trim().Split(new[] { Environment.NewLine, " " }, StringSplitOptions.None);
                        connectionString = SetConnectionString(database[1], connectionTimeout);
                        //comboBoxDatabase.InvokeIfRequired(s =>
                        //{
                        //    s.Text = "";
                        //    s.SelectedText = database[1].ToString();
                        //});
                    }
                    string result = string.Empty;
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        conn.InfoMessage += Conn_InfoMessage; // message hook (like backup message) 

                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.StatementCompleted += Cmd_StatementCompleted; // retrive row count
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = query;
                            cmd.CommandTimeout = commandTimeout;
                            SqlDataReader reader = cmd.ExecuteReader();
                            int recordsAffected = reader.RecordsAffected;

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
                                        if (headerYN.Equals("Y", StringComparison.OrdinalIgnoreCase))
                                        {
                                            foreach (DataRow row in schemaTable.Rows)
                                            {
                                                foreach (DataColumn column in schemaTable.Columns)
                                                {
                                                    if (column.ColumnName == "ColumnName")
                                                    {
                                                        Header = Header + row[column] + "\t";
                                                        Line = Line + "------- ";
                                                    }
                                                }
                                            }
                                            Header = Header + Environment.NewLine;
                                            Line = Line + Environment.NewLine;
                                            sb.Append(Header);
                                            sb.Append(Line);
                                        }

                                        while (reader.Read())
                                        {
                                            for (int i = 0; i < reader.FieldCount; i++)
                                            {
                                                if (reader.GetValue(i).ToString() == "System.Byte[]")
                                                    sb.Append("0x" + BitConverter.ToString((byte[])reader.GetValue(i)).Replace("-", ""));
                                                else
                                                    sb.Append(reader.GetValue(i).ToString());
                                                sb.Append("\t");
                                            }
                                            sb.Append(Environment.NewLine);
                                        }

                                    }
                                    catch (SqlException ex)
                                    {
                                        errorCnt++;
                                        sbResultAll.Append("-->>---------------------------------" + Environment.NewLine);
                                        sbResultAll.Append("--sql exception info : " + Environment.NewLine);
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
                                        if (countYN.Equals("Y", StringComparison.OrdinalIgnoreCase))
                                        {
                                            sbResultAll.Append(string.Format("({0} {1} affected)" + Environment.NewLine + Environment.NewLine, recordCount, (recordCount == 1) ? "row" : "rows"));
                                        }
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
                                reader.NextResult();
                            } while (reader.HasRows);
                        }
                        conn.Close();
                        bReturn = true;
                    }
                }

                catch (SqlException ex)
                {
                    errorCnt++;
                    sbResultAll.Append("-->>---------------------------------" + Environment.NewLine);
                    sbResultAll.Append("--sql exception info : " + Environment.NewLine);
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
                    errorCnt++;
                    sbResultAll.Append("-->>---------------------------------" + Environment.NewLine);
                    sbResultAll.Append("--exception info : " + Environment.NewLine);
                    sbResultAll.Append("-->>---------------------------------" + Environment.NewLine);
                    sbResultAll.Append(ex.Message);
                    sbResultAll.Append("---------------------------------<<--" + Environment.NewLine + Environment.NewLine);

                    bReturn = false;
                }
                finally
                {
                    if (syncAsync.Equals("Async", StringComparison.OrdinalIgnoreCase))
                        Common.FileLogWriteLine(logFileFullname, sbResultAll.ToString());

                }
            }

            if (syncAsync.Equals("Async", StringComparison.OrdinalIgnoreCase))
                Common.FileLogWriteLine(logFileFullname, "async cmd completed");

            return bReturn;
        }

        string SetConnectionString(string initialCatalog, int connectionTimeout = 5)
        {
            return new SqlConnectionStringBuilder
            {
                DataSource = LogClient.Config.Instance.GetValue(LogClient.Category.Repository, LogClient.Key.SqlDataSource),
                UserID = LogClient.Config.Instance.GetValue(LogClient.Category.Repository, LogClient.Key.SqlId),
                Password = config.DecryptedPassword,
                InitialCatalog = initialCatalog,
                ConnectTimeout = connectionTimeout
            }.ConnectionString;
        }

        void Conn_InfoMessage(object sender, SqlInfoMessageEventArgs e)
        {
            if (syncAsync.Equals("Async", StringComparison.OrdinalIgnoreCase))
                Common.FileLogWriteLine(logFileFullname, e.Message);
            else
                sbResultAll.Append(e.Message + Environment.NewLine);
        }

        void Cmd_StatementCompleted(object sender, StatementCompletedEventArgs e)
        {
            recordCount = e.RecordCount;
        }
    }
}
