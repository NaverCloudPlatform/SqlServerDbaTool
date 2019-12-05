using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using CsLib; 

namespace lazylog
{
    class dm_exec_query_statsTypeB : BaseSender
    {
        public dm_exec_query_statsTypeB(string CurrentTableName, string BaseTableName, DateTime ProbeTime)
            : base(CurrentTableName, BaseTableName, ProbeTime)
        { }

        string GetStatementResultsInJsonQuery = string.Empty;
        private string ClaUserKey = string.Empty;
        private string ClaLogTypes = string.Empty;

        protected override void Initialization()
        {
            try
            {
                EndPointUrl = config.GetValue(Category.NPOT, Key.URL);
                ClaUserKey = "MSSQL" + config.GetValue(Category.CLA, Key.UserKey);
                ClaLogTypes = config.GetValue(Category.CLA, Key.LogTypes);

                MachineName = (config.GetValue(Category.GLOBAL, Key.hostname) == "") ? Environment.MachineName : config.GetValue(Category.GLOBAL, Key.hostname);
                LocalIp = (config.GetValue(Category.GLOBAL, Key.private_ip) == "") ? LocalIp : config.GetValue(Category.GLOBAL, Key.private_ip);
                //            GetLatestResultsInJsonQuery = @"
                //set nocount on 
                //set transaction isolation level read uncommitted 

                //SELECT
                //    metric                                                AS metric,
                //    datediff(second,'1970-01-01', DATEADD(mi, DATEDIFF(mi, GETDATE(), GETUTCDATE()), probe_time)) AS timestamp,
                //    value                                                 AS value,
                //    lower(@hostname)                                    AS 'tags.hostname',
                //    @private_ip                                           AS 'tags.ip',
                //	@ncp_mbr_no                                           AS 'tags.ncp_mbr_no',
                //	@ncp_group_no                                         AS 'tags.ncp_group_no',
                //    CONVERT(varchar(max), nullif(query_hash, 0x), 1)      AS 'tags.query_hash'
                //FROM 
                //	(
                //	select 
                //		[probe_time]
                //		, [execution_count] as d_execution_count
                //		, [total_worker_time] d_total_worker_time 
                //		, [total_logical_reads] d_total_logical_reads
                //		, [total_physical_reads] d_total_physical_reads
                //		, [total_logical_writes] d_total_logical_writes
                //		, [total_elapsed_time] d_total_elapsed_time
                //		, [total_grant_kb] d_total_grant_kb
                //		, [sql_handle]
                //		, [statement_start_offset]
                //		, [statement_end_offset]
                //		, [plan_handle]
                //		, [query_hash]
                //		, [query_plan_hash] 
                //	from [dbo].[view_dm_exec_query_stats_summary]
                //	where probe_time = @ProbeTime
                //	) a
                //    UNPIVOT(value for metric in (
                //        d_execution_count,
                //        d_total_worker_time,
                //        d_total_logical_reads,
                //        d_total_physical_reads,
                //        d_total_logical_writes,
                //        d_total_elapsed_time,
                //        d_total_grant_kb
                //    )) AS UNPVT
                //FOR JSON PATH option(recompile);
                //";

                GetLatestResultsInJsonQuery = @"
set nocount on 
set transaction isolation level read uncommitted 

SELECT
    metric                                                AS metric,
    datediff(second,'1970-01-01', DATEADD(mi, DATEDIFF(mi, GETDATE(), GETUTCDATE()), probe_time)) AS timestamp,
    value                                                 AS value,
    lower(@hostname)                                    AS 'tags.hostname',
    @private_ip                                           AS 'tags.ip',
	@ncp_mbr_no                                           AS 'tags.ncp_mbr_no',
	@ncp_group_no                                         AS 'tags.ncp_group_no',
    CONVERT(varchar(max), nullif(query_hash, 0x), 1)      AS 'tags.query_hash'
FROM 
	(
	select 
		[probe_time]
		, [execution_count] as d_execution_count
		, [total_worker_time] d_total_worker_time 
		, [total_logical_reads] d_total_logical_reads
		, [total_physical_reads] d_total_physical_reads
		, [total_logical_writes] d_total_logical_writes
		, [total_elapsed_time] d_total_elapsed_time
		, [total_grant_kb] d_total_grant_kb
		, [sql_handle]
		, [statement_start_offset]
		, [statement_end_offset]
		, [plan_handle]
		, a.[query_hash]
		, [query_plan_hash] 
	from [dbo].[view_dm_exec_query_stats_summary] a
		inner join [dbo].[dm_exec_query_stats_statement] b 
		on a.query_hash = b.query_hash 
	where a.probe_time = @ProbeTime
	) a
    UNPIVOT(value for metric in (
        d_execution_count,
        d_total_worker_time,
        d_total_logical_reads,
        d_total_physical_reads,
        d_total_logical_writes,
        d_total_elapsed_time,
        d_total_grant_kb
    )) AS UNPVT
FOR JSON PATH option(recompile);
";


                GetStatementResultsInJsonQuery = @"
SELECT
    @clauserkey                                           AS [key],
    @logtypes                                             AS [type],
    CONVERT(varchar(32), DATEADD(mi, DATEDIFF(mi, GETDATE(), GETUTCDATE()), last_access_time), 126)+'Z'   AS [@timestamp],
    @subject                                              AS subject,
	lower(@Machinename)                                    AS hostname,
	@private_ip                                           AS ip,
    datediff(second,'1970-01-01', DATEADD(mi, DATEDIFF(mi, GETDATE(), GETUTCDATE()), last_access_time))  AS probe_time,
	CONVERT(varchar(max),nullif(query_hash,0x),1)         AS query_hash,
	COALESCE(query_statement, '')                         AS message
FROM [dm_exec_query_stats_statement]
where last_access_time = @ProbeTime
FOR JSON PATH, INCLUDE_NULL_VALUES option(recompile)
";
            }
            catch (Exception ex)
            {
                log.Error(string.Format("{0}, {1}", ex.Message, ex.StackTrace));
            }
        }

        internal override void SendData()
        {
            try
            {
                SendToNpot(GetData());
                SendToCla(GetStatementData());
            }
            catch (Exception ex)
            {
                log.Error(string.Format("{0}, {1}", ex.Message, ex.StackTrace));
            }
        }

        protected override string GetData()
        {
            // WebApi 로 데이터 전송 
            StringBuilder sb = new StringBuilder();
            try
            {
                
                using (SqlConnection conn = new SqlConnection(config.GetConnectionString(InitialCatalog.Repository)))
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        //cmd.CommandText = string.Format(GetLatestResultsInJsonQuery, CurrentTableName );
                        cmd.CommandText = GetLatestResultsInJsonQuery;
                        cmd.Parameters.Add("@hostname", SqlDbType.NVarChar, 100).Value = this.MachineName;
                        cmd.Parameters.Add("@private_ip", SqlDbType.NVarChar, 100).Value = this.LocalIp; 
                        cmd.Parameters.Add("@ncp_mbr_no", SqlDbType.NVarChar, 100).Value = config.GetValue(Category.NPOT, Key.ncp_mbr_no);
                        cmd.Parameters.Add("@ncp_group_no", SqlDbType.NVarChar, 100).Value = config.GetValue(Category.NPOT, Key.ncp_group_no);
                        cmd.Parameters.Add("@ProbeTime", SqlDbType.DateTime).Value = Convert.ToDateTime(ProbeTime.ToString("yyyy-MM-dd HH:mm:ss.000"));
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                sb.Append(config.DatabaseValue<string>(reader[0]));
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
            return sb.ToString();
        }

        private string GetStatementData()
        {
            // WebApi 로 데이터 전송 
            StringBuilder sb = new StringBuilder();
            try
            {
                
                using (SqlConnection conn = new SqlConnection(config.GetConnectionString(InitialCatalog.Repository)))
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = GetStatementResultsInJsonQuery;
                        cmd.Parameters.Add("@Machinename", SqlDbType.NVarChar, 100).Value = this.MachineName;
                        cmd.Parameters.Add("@private_ip", SqlDbType.NVarChar, 100).Value = this.LocalIp;
                        cmd.Parameters.Add("@logtypes", SqlDbType.NVarChar, 100).Value = this.ClaLogTypes;
                        cmd.Parameters.Add("@clauserkey", SqlDbType.NVarChar, 100).Value = this.ClaUserKey;
                        cmd.Parameters.Add("@subject", SqlDbType.NVarChar, 100).Value = "dm_exec_query_stats2";
                        cmd.Parameters.Add("@ProbeTime", SqlDbType.DateTime).Value = Convert.ToDateTime(ProbeTime.ToString("yyyy-MM-dd HH:mm:ss.000"));
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                sb.Append(config.DatabaseValue<string>(reader[0]));
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
            return sb.ToString();
        }

        private void SendToCla(string json)
        {
            try
            {
                if (json.Trim() != "")
                {

                    log.Warn("SendToCla");
                    string response = new CsLib.SoaCall().WebApiCallCla(
                    config.GetValue(Category.CLA, Key.URL),
                    json,
                    "dm_exec_query_stats2"
                    );
                    log.Warn(response);
                }
            }
            catch (Exception ex)
            {
                log.Error(string.Format("{0}, {1}", ex.Message, ex.StackTrace));
            }
        }
        
        private async void SendToNpot(string json)
        {
            try
            {
                if (json.Trim() != "")
                {
                    log.Warn("SendToNpot");
                    Task<string> response = new SoaCall().WebApiCallNpot(
                    EndPointUrl,
                    json,
                    config.GetValue(Category.NPOT, Key.AuthInfo) 
                    );
                    string responseString = await response;
                    log.Warn(responseString);
                }
            }
            catch (Exception ex)
            {
                log.Error(string.Format("{0}, {1}", ex.Message, ex.StackTrace));
            }
        }


    }
}
