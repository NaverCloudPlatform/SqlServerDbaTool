using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Newtonsoft.Json;
using System.Net.Http;
using CsLib; 
using System.Net;
using System.IO;

namespace lazylog
{
    class sp_lock2TypeB : BaseSender
    {
        public sp_lock2TypeB(string CurrentTableName, string BaseTableName, DateTime ProbeTime)
            : base(CurrentTableName, BaseTableName, ProbeTime)
        { }

        private string ClaUserKey = string.Empty;
        private string ClaLogTypes = string.Empty;

        protected override void Initialization()
        {
            try
            {
                EndPointUrl = config.GetValue(Category.CLA, Key.URL);
                ClaUserKey = "MSSQL" + config.GetValue(Category.CLA, Key.UserKey);
                ClaLogTypes = config.GetValue(Category.CLA, Key.LogTypes);
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
	wait_sec                                              AS wait_sec,
	rtrim(locktree)                                       AS locktree,
	spid                                                  AS spid,
	kpid                                                  AS kpid,
	rtrim(lastwaittype)                                   AS lastwaittype,
	rtrim(waitresource)                                   AS waitresource,
	rtrim(program_name)                                   AS program_name,
	rtrim(loginame)                                       AS loginame,
	CONVERT(varchar(max),nullif(sql_handle,0x),1)         AS sql_handle,
	stmt_start                                            AS stmt_start,
	stmt_end                                              AS stmt_end,
	COALESCE(Text, '')                                    AS message
FROM [dbo].[view_sp_lock2]
where probe_time = @ProbeTime
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
            StringBuilder sb = new StringBuilder();
            try
            {
                
                using (SqlConnection conn = new SqlConnection(config.GetConnectionString(InitialCatalog.Repository)))
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = string.Format(GetLatestResultsInJsonQuery, CurrentTableName);
                        cmd.Parameters.Add("@clauserkey", SqlDbType.NVarChar, 100).Value = this.ClaUserKey;
                        cmd.Parameters.Add("@logtypes", SqlDbType.NVarChar, 100).Value = this.ClaLogTypes;
                        cmd.Parameters.Add("@hostname", SqlDbType.NVarChar, 100).Value = this.MachineName; 
                        cmd.Parameters.Add("@subject", SqlDbType.NVarChar, 100).Value = BaseTableName;
                        cmd.Parameters.Add("@private_ip", SqlDbType.NVarChar, 100).Value = this.LocalIp;
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
                    string response = new SoaCall().WebApiCallCla(
                    EndPointUrl,
                    json,
                    BaseTableName
                    );
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
