using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Newtonsoft.Json;

namespace lazylog
{
    class dm_os_workersTypeA : BaseSender
    {
        public dm_os_workersTypeA(string CurrentTableName, string BaseTableName, DateTime ProbeTime) 
            : base(CurrentTableName, BaseTableName, ProbeTime)
        {
            list_dm_os_workers_data_TypeA = new List<dm_os_workers_data_TypeA>();
        }

        List<dm_os_workers_data_TypeA> list_dm_os_workers_data_TypeA;

        protected override string GetData()
        {
            try
            {
                list_dm_os_workers_data_TypeA.Clear();
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
                                list_dm_os_workers_data_TypeA.Add(
                                    new dm_os_workers_data_TypeA
                                    {
                                        ip = LocalIp,
                                        port = LocalPort,
                                        probe_time = ProbeTime.ToString("yyyy-MM-dd HH:mm:ss.000"),
                                        session_limit = config.DatabaseValue<int>(reader["session_limit"]),
                                        current_session_cnt = config.DatabaseValue<int>(reader["current_session_cnt"]),
                                        max_worker_thread = config.DatabaseValue<int>(reader["max_worker_thread"]),
                                        current_worker_cnt = config.DatabaseValue<int>(reader["current_worker_cnt"]),
                                        scheduler_id = config.DatabaseValue<int>(reader["scheduler_id"]),
                                        quantum_used = config.DatabaseValue<long>(reader["quantum_used"]),
                                        is_preemptive = config.DatabaseValue<bool>(reader["is_preemptive"]).ToString(),
                                        context_switch_count = config.DatabaseValue<long>(reader["context_switch_count"]),
                                        state = config.DatabaseValue<string>(reader["state"]),
                                        last_wait_type = config.DatabaseValue<string>(reader["last_wait_type"]),
                                        processor_group = config.DatabaseValue<short>(reader["processor_group"]),
                                        tasks_processed_count = config.DatabaseValue<int>(reader["tasks_processed_count"]),
                                        task_address = config.DatabaseValue<string>(reader["task_address"]),
                                        session_id = config.DatabaseValue<int>(reader["session_id"]),
                                        original_login_name = config.DatabaseValue<string>(reader["original_login_name"]),
                                        host_name = config.DatabaseValue<string>(reader["host_name"]),
                                        program_name = config.DatabaseValue<string>(reader["program_name"]),
                                        command = config.DatabaseValue<string>(reader["command"]),
                                        cpu_time = config.DatabaseValue<long>(reader["cpu_time"]),
                                        total_elapsed_time = config.DatabaseValue<long>(reader["total_elapsed_time"]),
                                        reads = config.DatabaseValue<long>(reader["reads"]),
                                        writes = config.DatabaseValue<long>(reader["writes"]),
                                        logical_reads = config.DatabaseValue<long>(reader["logical_reads"]),
                                        query_hash = config.DatabaseValue<string>(reader["query_hash"]),
                                        sql_handle = config.DatabaseValue<string>(reader["sql_handle"]),
                                        statement_start_offset = config.DatabaseValue<int>(reader["statement_start_offset"]),
                                        statement_end_offset = config.DatabaseValue<int>(reader["statement_end_offset"]),
                                        database_id = config.DatabaseValue<short>(reader["database_id"]),
                                        blocking_session_id = config.DatabaseValue<int>(reader["blocking_session_id"]),
                                        open_transaction_count = config.DatabaseValue<int>(reader["open_transaction_count"]),
                                        percent_complete = config.DatabaseValue<Single>(reader["percent_complete"]),
                                        transaction_isolation_level = config.DatabaseValue<short>(reader["transaction_isolation_level"]),
                                        query_plan_hash = config.DatabaseValue<string>(reader["query_plan_hash"]),
                                        plan_handle = config.DatabaseValue<string>(reader["plan_handle"]),
                                        query_text = config.DatabaseValue<string>(reader["query_text"]),
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

            var json = JsonConvert.SerializeObject(list_dm_os_workers_data_TypeA, settings);
            return json;
        }


        protected override void Initialization()
        {
            try
            {
                GetDataQuery = @"
select
	  probe_time
	, session_limit
	, current_session_cnt
	, max_worker_thread
	, current_worker_cnt
	, scheduler_id
	, quantum_used
	, is_preemptive
	, context_switch_count
	, state
	, last_wait_type
	, processor_group
	, tasks_processed_count
	, convert(varchar(max), task_address, 1) task_address
	, session_id
	, original_login_name
	, host_name
	, program_name
	, command
	, cpu_time
	, total_elapsed_time
	, reads
	, writes
	, logical_reads
	, convert(varchar(max), query_hash, 1) query_hash
	, convert(varchar(max), sql_handle, 1) sql_handle
	, statement_start_offset
	, statement_end_offset
	, database_id
	, blocking_session_id
	, open_transaction_count
	, percent_complete
	, transaction_isolation_level
	, convert(varchar(max), query_plan_hash, 1) query_plan_hash
	, convert(varchar(max), plan_handle, 1) plan_handle
	, convert(varchar(max), query_text, 1) query_text
from [dbo].[view_dm_os_workers]
where probe_time = @ProbeTime
";
                EndPointUrl = config.GetValue(Category.Sqlmon, Key.SqlmonWebApiCallEndPointUrl);
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
                SendToGeneralRepository(GetData(), "/api/sqlmon/dm_os_workers");
                list_dm_os_workers_data_TypeA = null;
            }
            catch (Exception ex)
            {
                log.Error(string.Format("{0}, {1}", ex.Message, ex.StackTrace));
            }
        }
    }
}
