using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;

namespace lazylog
{
    class dm_exec_query_statsTypeA : BaseSender
    {
        
        public dm_exec_query_statsTypeA(string CurrentTableName, string BaseTableName, DateTime ProbeTime) 
            : base(CurrentTableName, BaseTableName, ProbeTime)
        {
            list_dm_exec_query_stats_data_typeA = new List<dm_exec_query_stats_typeA>();
            list_dm_exec_query_stats_statement_TypeA = new List<dm_exec_query_stats_statement_TypeA>();
        }

        List<dm_exec_query_stats_typeA> list_dm_exec_query_stats_data_typeA;
        List<dm_exec_query_stats_statement_TypeA> list_dm_exec_query_stats_statement_TypeA;
        
        string GetDataStatementQuery = string.Empty; 

        protected override string GetData()
        {
            try
            {
                list_dm_exec_query_stats_data_typeA.Clear();
                using (SqlConnection conn = new SqlConnection(config.GetConnectionString(InitialCatalog.Repository)))
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        //cmd.CommandText = string.Format(GetDataQuery, CurrentTableName);
                        cmd.CommandText = GetDataQuery;
                        cmd.Parameters.Add("@ProbeTime", SqlDbType.DateTime).Value = Convert.ToDateTime(ProbeTime.ToString("yyyy-MM-dd HH:mm:ss.000"));
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                list_dm_exec_query_stats_data_typeA.Add(
                                    new dm_exec_query_stats_typeA
                                    {
                                        ip = LocalIp,
                                        port = LocalPort,
                                        probe_time = ProbeTime.ToString("yyyy-MM-dd HH:mm:ss.000"),
                                        execution_count = config.DatabaseValue<long>(reader["execution_count"]),
                                        total_worker_time = config.DatabaseValue<long>(reader["total_worker_time"]),
                                        total_logical_reads = config.DatabaseValue<long>(reader["total_logical_reads"]),
                                        total_physical_reads = config.DatabaseValue<long>(reader["total_physical_reads"]),
                                        total_logical_writes = config.DatabaseValue<long>(reader["total_logical_writes"]),
                                        total_elapsed_time = config.DatabaseValue<long>(reader["total_elapsed_time"]),
                                        total_grant_kb = config.DatabaseValue<long>(reader["total_grant_kb"]),
                                        sql_handle = config.DatabaseValue<string>(reader["sql_handle"]),
                                        statement_start_offset = config.DatabaseValue<int>(reader["statement_start_offset"]),
                                        statement_end_offset = config.DatabaseValue<int>(reader["statement_end_offset"]),
                                        plan_handle = config.DatabaseValue<string>(reader["plan_handle"]),
                                        query_hash = config.DatabaseValue<string>(reader["query_hash"]),
                                        query_plan_hash = config.DatabaseValue<string>(reader["query_plan_hash"])
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

            var json = JsonConvert.SerializeObject(list_dm_exec_query_stats_data_typeA, settings);
            return json;
        }


        private string GetStatementData()
        {
            try
            {
                list_dm_exec_query_stats_statement_TypeA.Clear();
                using (SqlConnection conn = new SqlConnection(config.GetConnectionString(InitialCatalog.Repository)))
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = GetDataStatementQuery;
                        cmd.Parameters.Add("@ProbeTime", SqlDbType.DateTime).Value = Convert.ToDateTime(ProbeTime.ToString("yyyy-MM-dd HH:mm:ss.000"));
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                list_dm_exec_query_stats_statement_TypeA.Add(
                                    new dm_exec_query_stats_statement_TypeA
                                    {
                                        ip = LocalIp,
                                        port = LocalPort,
                                        probe_time = ProbeTime.ToString("yyyy-MM-dd HH:mm:ss.000"),
                                        query_hash = config.DatabaseValue<string>(reader["query_hash"]),
                                        query_statement = config.DatabaseValue<string>(reader["query_statement"])
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

            var json = JsonConvert.SerializeObject(list_dm_exec_query_stats_statement_TypeA, settings);
            return json;

        }



        internal override void SendData()
        {
            try
            {
                SendToGeneralRepository(GetData(), "/api/sqlmon/dm_exec_query_stats");
                SendToGeneralRepository(GetStatementData(), "/api/sqlmon/dm_exec_query_stats_statement");

                list_dm_exec_query_stats_data_typeA = null;
                list_dm_exec_query_stats_statement_TypeA = null;
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
	a.[probe_time]
	, a.[execution_count] 
	, a.[total_worker_time] 
	, a.[total_logical_reads] 
	, a.[total_physical_reads] 
	, a.[total_logical_writes] 
	, a.[total_elapsed_time] 
	, a.[total_grant_kb] 
	, convert(varchar(max), a.[sql_handle], 1) sql_handle
	, a.[statement_start_offset]
	, a.[statement_end_offset]
	, convert(varchar(max), a.[plan_handle], 1) plan_handle
	, convert(varchar(max), a.[query_hash], 1) query_hash
	, convert(varchar(max), a.[query_plan_hash] , 1) query_plan_hash
from [dbo].[view_dm_exec_query_stats_summary] a 
	join [dbo].[dm_exec_query_stats_statement] b 
	on a.query_hash = b.query_hash 
where a.probe_time = @ProbeTime
";

                GetDataStatementQuery = @"
select 
    convert(varchar(max), query_hash, 1) query_hash
	, query_statement
from [dbo].[dm_exec_query_stats_statement]
where last_access_time = @ProbeTime 
";
                EndPointUrl = config.GetValue(Category.Sqlmon, Key.SqlmonWebApiCallEndPointUrl);
            }
            catch (Exception ex)
            {
                log.Error(string.Format("{0}, {1}", ex.Message, ex.StackTrace));
            }
        }
    }
}
