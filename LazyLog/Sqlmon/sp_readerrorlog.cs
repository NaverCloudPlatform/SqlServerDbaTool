using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogClient;
using System.Threading;
using System.Data.SqlClient;
using System.Data;
using CsLib;

namespace lazylog
{
    class sp_readerrorlog : BaseSqlmon<sp_readerrorlog_data>
    {
        // LogDate, ProcessInfo or ErrorLevel, Text 
        Dictionary<Tuple<DateTime, string, string>, string> xpLogTableBuffer;
        Dictionary<Tuple<DateTime, string, string>, string> xpLogNewBuffer;
        DateTime maxDateTime;
        public sp_readerrorlog() : base()
        {
            BaseTableName = this.GetType().Name;
            xpLogTableBuffer = new Dictionary<Tuple<DateTime, string, string>, string>();
            xpLogNewBuffer = new Dictionary<Tuple<DateTime, string, string>, string>();
            maxDateTime = DateTime.Now.Add(new TimeSpan(-1, 0, 0));
        }

        internal override void Start()
        {
            Initialization();
            IsRunning = true;
            while (IsRunning)
            {
                try
                {
                    DateTime endTime = DateTime.Now.Add(new TimeSpan(0, 0, RunIntervalSec));  // 3초마다 동작 

                    if (RunIntervalSec == 0)
                    {
                        IsRunning = false;
                        log.Warn("sqlmon (sp_readerrorlog) can't start because ProbeIntervalSec is 0");
                        break;
                    }
                    else
                    {
                        Purge();
                        LoadLastSentData();
                        GetData();
                        RemoteSaveData();
                        LocalSaveData(CurrentTableName);

                        while (DateTime.Now < endTime)
                        {
                            if (!IsRunning) break;
                            Thread.Sleep(200);
                        }
                    }
                }
                catch (Exception ex)
                {
                    log.Error(string.Format("{0}, {1}", ex.Message, ex.StackTrace));
                }
            }
        }

        protected override void Purge()
        {
            buffer.Clear(); // 
            xpLogTableBuffer.Clear(); // 마지막 전송한 데이터 읽은것
            xpLogNewBuffer.Clear();  // 현재 xp_readerrorlog 로 읽은것

            // 테이블 없으면 만들어
            if (!GetCurrentTableName(BaseTableName, out CurrentTableName))
            {
                try
                {
                    Common.QueryExecuter(
                        config.GetConnectionString(InitialCatalog.Repository)
                        , string.Format(TableGenQuery, BaseTableName, "ProcessInfo"));
                }
                catch (Exception ex)
                {
                    log.Error(string.Format("{0}, {1}", ex.Message, ex.StackTrace));
                }
                finally
                {
                    GetCurrentTableName(BaseTableName, out CurrentTableName);
                }
            }
        }

        protected override bool GetCurrentTableName(string BaseTableName, out string CurrentTableName)
        {
            bool bReturn = false;
            string TableName = string.Empty;
            try
            {
                using (SqlConnection conn = new SqlConnection(config.GetConnectionString(InitialCatalog.Repository)))
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = @"
select top 1 TABLE_NAME
from INFORMATION_SCHEMA.TABLES
where TABLE_NAME like @BaseTableName+'SendHist' 
order by TABLE_NAME desc ";
                        cmd.Parameters.Add("@BaseTableName", SqlDbType.NVarChar, 100).Value = BaseTableName;

                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                TableName = config.DatabaseValue<string>(reader["TABLE_NAME"]);
                            }
                            bReturn = true;
                        }
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                log.Error(string.Format("{0}, {1}", ex.Message, ex.StackTrace));
                TableName = "";
            }

            CurrentTableName = TableName;
            return bReturn;
        }
        
        private void LoadLastSentData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(config.GetConnectionString(InitialCatalog.Repository)))
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = string.Format(@"
select 
	  LogDate
	, ProcessInfo
	, Text
from [dbo].[{0}SendHist]
", BaseTableName);
                        cmd.Parameters.Add("@BaseTableName", SqlDbType.NVarChar, 100).Value = BaseTableName;

                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                xpLogTableBuffer.Add(new Tuple<DateTime, string, string>
                                (
                                    config.DatabaseValue<DateTime>(reader["LogDate"]),
                                    config.DatabaseValue<string>(reader["ProcessInfo"]),
                                    config.DatabaseValue<string>(reader["Text"])
                                ), "");
                            }
                        }
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                log.Error(string.Format("{0}, {1}", ex.Message, ex.StackTrace));
            }
        }

        protected override void GetData()
        {
            // 데이터를 구한다. 
            
            log.Warn(string.Format("{0} GetData started", BaseTableName));
            try
            {
                if (xpLogTableBuffer.Count > 0)
                {
                    maxDateTime = xpLogTableBuffer.Max(x => x.Key.Item1);
                }

                using (SqlConnection conn = new SqlConnection(config.GetConnectionString(InitialCatalog.Repository)))
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = GetDataQuery;
                        cmd.Parameters.Add("@LogStartTime", SqlDbType.DateTime).Value = maxDateTime;
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                try
                                {
                                    if (!xpLogNewBuffer.ContainsKey(new Tuple<DateTime, string, string>
                                        (
                                            config.DatabaseValue<DateTime>(reader["LogDate"]),
                                            config.DatabaseValue<string>(reader["ProcessInfo"]),
                                            config.DatabaseValue<string>(reader["Text"])
                                        )))
                                    xpLogNewBuffer.Add(new Tuple<DateTime, string, string>
                                        (
                                            config.DatabaseValue<DateTime>(reader["LogDate"]),
                                            config.DatabaseValue<string>(reader["ProcessInfo"]),
                                            config.DatabaseValue<string>(reader["Text"])
                                        ), "");
                                }
                                catch (Exception ex)
                                {
                                    log.Error(string.Format("{0}, {1}", ex.Message, ex.StackTrace));
                                } 
                            }
                        }
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                log.Error(string.Format("{0}, {1}", ex.Message, ex.StackTrace));
            }
        }

        protected override void RemoteSaveData()
        {
            try
            {
                string SenderType = config.GetValue(Category.Sender, Key.Type);

                if (xpLogNewBuffer.Count > 0)
                {
                    RemoveOverlappedData();

                    if (xpLogNewBuffer.Count > 0)
                    {

                        if (!SenderType.Equals("No", StringComparison.OrdinalIgnoreCase))
                        {
                            BaseSender sender;
                            if (SenderType.Equals("A", StringComparison.OrdinalIgnoreCase))
                                sender = new sp_readerrorlogTypeA(xpLogNewBuffer);
                            else
                                sender = new sp_readerrorlogTypeB(xpLogNewBuffer);

                            sender.SendData();
                        }

                        // 보내고 끝 

                        maxDateTime = xpLogNewBuffer.Max(x => x.Key.Item1);

                        RemoveDataXpLogNewBufferLessThanMaxDateTime(maxDateTime);

                        // bulk 용 버퍼에 넣음 
                        foreach (var a in xpLogNewBuffer)
                        {
                            buffer.Add(new sp_readerrorlog_data
                            {
                                LogDate = a.Key.Item1,
                                ProcessInfo = a.Key.Item2,
                                Text = a.Key.Item3
                            });
                        }

                        Common.QueryExecuter(
                            config.GetConnectionString(InitialCatalog.Repository)
                            , string.Format(@"truncate table {0}SendHist", BaseTableName));
                    }
                }
                else
                    log.Warn("noData");
            } 
            catch (Exception ex)
            {
                log.Error(string.Format("{0}, {1}", ex.Message, ex.StackTrace));
            }
            
        }
        
        private void RemoveDataXpLogNewBufferLessThanMaxDateTime(DateTime maxDateDateTime)
        {
            try
            {
                Dictionary<Tuple<DateTime, string, string>, string> toRemove = new Dictionary<Tuple<DateTime, string, string>, string>();
                foreach (var a in xpLogNewBuffer)
                {
                    if (a.Key.Item1 < maxDateDateTime)
                        toRemove.Add(a.Key, a.Value);
                }

                foreach (var a in toRemove)
                {
                    xpLogNewBuffer.Remove(a.Key);
                }
            }
            catch (Exception ex)
            {
                log.Error(string.Format("{0}, {1}", ex.Message, ex.StackTrace));
            }
        }

        private void RemoveOverlappedData()
        {
            try
            {
                foreach (var a in xpLogTableBuffer)
                {
                    xpLogNewBuffer.Remove(a.Key);
                }
            }
            catch (Exception ex ) {
                log.Error(string.Format("{0}, {1}", ex.Message, ex.StackTrace));
            }
        }
        
        protected override void Initialization()
        {
            try
            {
                RunIntervalSec = Convert.ToInt32(config.GetValue(Category.Sqlmon, Key.sp_readerrorlog_ProbeIntervalSec));

                GetDataQuery = @"
set nocount on 
set transaction isolation level read uncommitted 
exec xp_readerrorlog 0, 1, null, null, @LogStartTime, null, 'asc'
";

                TableGenQuery = @"
create table {0}SendHist 
(LogDate datetime
, {1} nvarchar(100)
, Text nvarchar(3000)
)
go

create index cl_{0}SendHist on {0}SendHist (LogDate)
go
";
            }
            catch (Exception ex)
            {
                log.Error(string.Format("{0}, {1}", ex.Message, ex.StackTrace));
            }
            
        }
    }
}
