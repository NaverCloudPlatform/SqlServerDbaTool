using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Newtonsoft.Json;
using System.Net.Http;

using System.Net;
using System.IO;

namespace lazylog
{
    class sp_lock2TypeA : BaseSender
    {
        public sp_lock2TypeA(string CurrentTableName, string BaseTableName, DateTime ProbeTime) 
            : base(CurrentTableName, BaseTableName,  ProbeTime)
        {
            list_sp_loc2_TypeA = new List<sp_lock2_TypeA>();
        }

        List<sp_lock2_TypeA> list_sp_loc2_TypeA;

        protected override string GetData()
        {
            try
            {
                list_sp_loc2_TypeA.Clear();
                using (SqlConnection conn = new SqlConnection(config.GetConnectionString(InitialCatalog.Repository)))
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = GetDataQuery;
                        cmd.Parameters.Add("@ProbeTime", SqlDbType.DateTime).Value = Convert.ToDateTime(ProbeTime.ToString("yyyy-MM-dd HH:mm:ss.000"));
                        SqlDataReader reader = cmd.ExecuteReader();
                        
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                list_sp_loc2_TypeA.Add(
                                    new sp_lock2_TypeA
                                    {
                                        ip = LocalIp,
                                        port = LocalPort,
                                        probe_time = ProbeTime.ToString("yyyy-MM-dd HH:mm:ss.000"),
                                        hh_mm_ss     = config.DatabaseValue<string>(reader["hh_mm_ss"]),
                                        wait_sec     = config.DatabaseValue<long>(reader["wait_sec"]),
                                        locktree     = config.DatabaseValue<string>(reader["locktree"]).Trim(),
                                        spid         = config.DatabaseValue<int>(reader["spid"]),
                                        kpid         = config.DatabaseValue<int>(reader["kpid"]),
                                        blocked      = config.DatabaseValue<int>(reader["blocked"]),
                                        waittype     = config.DatabaseValue<string>(reader["waittype"]),
                                        waittime     = config.DatabaseValue<long>(reader["waittime"]),
                                        lastwaittype = config.DatabaseValue<string>(reader["lastwaittype"]).Trim(),
                                        waitresource = config.DatabaseValue<string>(reader["waitresource"]).Trim(),
                                        dbid         = config.DatabaseValue<int>(reader["dbid"]),
                                        uid          = config.DatabaseValue<int>(reader["uid"]),
                                        cpu          = config.DatabaseValue<long>(reader["cpu"]),
                                        physical_io  = config.DatabaseValue<long>(reader["physical_io"]),
                                        memusage     = config.DatabaseValue<long>(reader["memusage"]),
                                        login_time   = config.DatabaseValue<DateTime>(reader["login_time"]),
                                        last_batch   = config.DatabaseValue<DateTime>(reader["last_batch"]),
                                        ecid         = config.DatabaseValue<int>(reader["ecid"]),
                                        open_tran    = config.DatabaseValue<int>(reader["open_tran"]),
                                        status       = config.DatabaseValue<string>(reader["status"]).Trim(),
                                        sid          = config.DatabaseValue<string>(reader["sid"]).Trim(),
                                        hostname     = config.DatabaseValue<string>(reader["hostname"]).Trim(),
                                        program_name = config.DatabaseValue<string>(reader["program_name"]).Trim(),
                                        hostprocess  = config.DatabaseValue<string>(reader["hostprocess"]).Trim(),
                                        cmd          = config.DatabaseValue<string>(reader["cmd"]).Trim(),
                                        nt_domain    = config.DatabaseValue<string>(reader["nt_domain"]).Trim(),
                                        nt_username  = config.DatabaseValue<string>(reader["nt_username"]).Trim(),
                                        net_address  = config.DatabaseValue<string>(reader["net_address"]).Trim(),
                                        net_library  = config.DatabaseValue<string>(reader["net_library"]).Trim(),
                                        loginame     = config.DatabaseValue<string>(reader["loginame"]).Trim(),
                                        context_info = config.DatabaseValue<string>(reader["context_info"]).Trim(),
                                        sql_handle   = config.DatabaseValue<string>(reader["sql_handle"]).Trim(),
                                        stmt_start   = config.DatabaseValue<int>(reader["stmt_start"]),
                                        stmt_end     = config.DatabaseValue<int>(reader["stmt_end"]),
                                        request_id   = config.DatabaseValue<int>(reader["request_id"]),
                                        number       = config.DatabaseValue<int>(reader["number"]),
                                        encrypted    = config.DatabaseValue<string>(reader["encrypted"]).Trim(),
                                        text         = config.DatabaseValue<string>(reader["text"])
                                    });
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
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            var json = JsonConvert.SerializeObject(list_sp_loc2_TypeA, settings);
            return json;
        }

        internal override void SendData()
        {
            try
            {
                SendToGeneralRepository(GetData(), "/api/sqlmon/sp_lock2");
                list_sp_loc2_TypeA = null;
            }
            catch (Exception ex)
            {
                log.Error(string.Format("{0}, {1}", ex.Message, ex.StackTrace));
            }
        }

        protected override void Initialization()
        {
            try
            {
                GetDataQuery = @"
select 
	  convert(varchar(30), probe_time, 121) probe_time 
	, hh_mm_ss
	, wait_sec
	, locktree
	, spid
	, kpid
	, blocked
	, convert(varchar(max), waittype, 1) waittype
	, waittime
	, lastwaittype lastwaittype 
	, waitresource
	, dbid
	, uid
	, cpu
	, physical_io
	, memusage
	, login_time
	, last_batch
	, ecid
	, open_tran
	, status
	, convert(varchar(max), sid, 1 ) sid
	, hostname
	, program_name
	, hostprocess
	, cmd
	, nt_domain
	, nt_username
	, net_address
	, net_library
	, loginame
	, convert(varchar(max),context_info,1) context_info
	, convert(varchar(max),sql_handle,1) sql_handle
	, stmt_start
	, stmt_end
	, request_id
	, objectid
	, number
	, encrypted
	, text
from [dbo].[view_sp_lock2]
where probe_time = @ProbeTime";

                EndPointUrl = config.GetValue(Category.Sqlmon, Key.SqlmonWebApiCallEndPointUrl);
            }
            catch (Exception ex)
            {
                log.Error(string.Format("{0}, {1}", ex.Message, ex.StackTrace));
            }
        }
    }
}