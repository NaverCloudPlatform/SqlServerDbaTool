using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;
using CsLib; 

namespace lazylog
{
    class dm_os_workersTypeB : BaseSender
    {
        public dm_os_workersTypeB(string CurrentTableName, string BaseTableName, DateTime ProbeTime) 
            : base(CurrentTableName, BaseTableName, ProbeTime) { }
        private string ClaUserKey = string.Empty;
        private string ClaLogTypes = string.Empty;

        protected override void Initialization()
        {
            try
            {
                ClaUserKey = "MSSQL" + config.GetValue(Category.CLA, Key.UserKey);
                ClaLogTypes = config.GetValue(Category.CLA, Key.LogTypes);
                EndPointUrl = config.GetValue(Category.CLA, Key.URL);
                MachineName = (config.GetValue(Category.GLOBAL, Key.hostname) == "") ? Environment.MachineName : config.GetValue(Category.GLOBAL, Key.hostname);
                LocalIp = (config.GetValue(Category.GLOBAL, Key.private_ip) == "") ? LocalIp : config.GetValue(Category.GLOBAL, Key.private_ip);
                GetLatestResultsInJsonQuery = @"
SELECT
@clauserkey                                           AS [key],
@logtypes                                             AS [type],
CONVERT(varchar(32), DATEADD(mi, DATEDIFF(mi, GETDATE(), GETUTCDATE()), probe_time), 126)+'Z'   AS [@timestamp],
@subject                                              AS subject,
lower(@hostname)                                      AS hostname,
@private_ip                                           AS ip,
datediff(second,'1970-01-01', DATEADD(mi, DATEDIFF(mi, GETDATE(), GETUTCDATE()), probe_time))  AS probe_time,
session_limit                                         AS session_limit,
current_session_cnt                                   AS current_session_cnt,
max_worker_thread                                     AS max_worker_thread,
current_worker_cnt                                    AS current_worker_cnt,
quantum_used                                          AS quantum_used,
state                                                 AS state,
rtrim(last_wait_type)                                 AS last_wait_type,
session_id                                            AS session_id,
rtrim(original_login_name)                            AS original_login_name,
rtrim(host_name)                                      AS host_name,
rtrim(program_name)                                   AS program_name,
rtrim(command)                                        AS command,
cpu_time                                              AS cpu_time,
total_elapsed_time                                    AS total_elapsed_time,
reads                                                 AS reads,
writes                                                AS writes,
logical_reads                                         AS logical_reads,
CONVERT(varchar(max),nullif(query_hash,0x),1)         AS query_hash,
CONVERT(varchar(max),nullif(query_plan_hash,0x),1)    AS query_plan_hash,
CONVERT(varchar(max),nullif(sql_handle,0x),1)         AS sql_handle,
database_id                                           AS database_id,
blocking_session_id                                   AS blocking_session_id,
open_transaction_count                                AS open_transaction_count,
percent_complete                                      AS percent_complete,
transaction_isolation_level                           AS transaction_isolation_level,
COALESCE(query_text, '')                              AS message
FROM [dbo].[view_dm_os_workers]
WHERE query_text IS NOT NULL 
--    and original_login_name not like '%rdsadmin' 
--    and original_login_name not like 'NT Service%'
and probe_time = @ProbeTime
FOR JSON PATH, INCLUDE_NULL_VALUES option(recompile) ; 
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
                SendToCla(GetData());
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
                        //cmd.CommandText = string.Format(GetLatestResultsInJsonQuery, CurrentTableName, ProbeTime.ToString("yyyy-MM-dd HH:mm:ss.000"));
                        cmd.CommandText = GetLatestResultsInJsonQuery;
                        cmd.Parameters.Add("@hostname", SqlDbType.NVarChar, 100).Value = this.MachineName;
                        cmd.Parameters.Add("@clauserkey", SqlDbType.NVarChar, 100).Value = this.ClaUserKey;
                        cmd.Parameters.Add("@logtypes", SqlDbType.NVarChar, 100).Value = this.ClaLogTypes;
                        cmd.Parameters.Add("@subject", SqlDbType.NVarChar, 100).Value = BaseTableName;
                        cmd.Parameters.Add("@private_ip", SqlDbType.NVarChar, 100).Value = this.LocalIp;
                        cmd.Parameters.Add("@ProbeTime", SqlDbType.DateTime).Value = Convert.ToDateTime(ProbeTime.ToString("yyyy-MM-dd HH:mm:ss.000"));
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                //single 관련 에러 없는지 볼것 
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
                    string response = new SoaCall().WebApiCallCla(
                    EndPointUrl,
                    json,
                    BaseTableName
                    );
                    //string responseString = await response;
                    log.Warn(response);
                }
            }
            catch (Exception ex)
            {
                log.Error(string.Format("{0}, {1}", ex.Message, ex.StackTrace));
            }
        }
    }
}
