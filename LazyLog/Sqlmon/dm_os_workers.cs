using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogClient;
using System.Threading;
using System.Data.SqlClient;
using System.Data;

namespace lazylog
{
    class dm_os_workers : BaseSqlmon<dm_os_workers_data>
    {
        public dm_os_workers() : base()
        {
            BaseTableName = this.GetType().Name;
        }

        internal override void Start()
        {
            Initialization(); 
            buffer = new List<dm_os_workers_data>();
            IsRunning = true;

            while (IsRunning)
            {
                try
                {
                    DateTime endTime = DateTime.Now.Add(new TimeSpan(0, 0, RunIntervalSec));  // 3초마다 동작 

                    if (RunIntervalSec == 0)
                    {
                        IsRunning = false;
                        log.Warn("sqlmon (dm_os_workers) can't start because ProbeIntervalSec is 0");
                        break;
                    }
                    else
                    {
                        ProbeTime = DateTime.Now;
                        Purge();
                        GetData();
                        LocalSaveData(CurrentTableName);
                        //LocalSaveData(CurrentViewName);

                        // webapi 에 쏘기 
                        if ((InternalRepositorySaveCnt % WebApiIntervalModValue) == 0)
                        {
                            RemoteSaveData();
                            InternalRepositorySaveCnt = 0;
                        }

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

        protected override void GetData()
        {
            // 데이터를 구한다. 
            log.Warn(string.Format("{0} GetData started", BaseTableName));
            try
            {
                string probe_time = ProbeTime.ToString("yyyy-MM-dd HH:mm:ss.000");
                using (SqlConnection conn = new SqlConnection(config.GetConnectionString(InitialCatalog.Repository)))
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = GetDataQuery;
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                buffer.Add(new dm_os_workers_data
                                {
                                    probe_time = Convert.ToDateTime(probe_time),

                                    session_limit = config.DatabaseValue<int>(reader["session_limit"]),
                                    current_session_cnt = config.DatabaseValue<int>(reader["current_session_cnt"]),
                                    max_worker_thread = config.DatabaseValue<int>(reader["max_worker_thread"]),
                                    current_worker_cnt = config.DatabaseValue<int>(reader["current_worker_cnt"]),
                                    scheduler_id = config.DatabaseValue<int>(reader["scheduler_id"]),
                                    quantum_used = config.DatabaseValue<long>(reader["quantum_used"]),

                                    is_preemptive = config.DatabaseValue<bool>(reader["is_preemptive"]),
                                    context_switch_count = config.DatabaseValue<int>(reader["context_switch_count"]),
                                    state = config.DatabaseValue<string>(reader["state"]),
                                    last_wait_type = config.DatabaseValue<string>(reader["last_wait_type"]),
                                    processor_group = config.DatabaseValue<short>(reader["processor_group"]),

                                    tasks_processed_count = config.DatabaseValue<int>(reader["tasks_processed_count"]),
                                    task_address = config.DatabaseValue<byte[]>(reader["task_address"]),
                                    session_id = config.DatabaseValue<short>(reader["session_id"]),
                                    original_login_name = config.DatabaseValue<string>(reader["original_login_name"]),
                                    host_name = config.DatabaseValue<string>(reader["host_name"]),

                                    program_name = config.DatabaseValue<string>(reader["program_name"]),
                                    command = config.DatabaseValue<string>(reader["command"]),
                                    cpu_time = config.DatabaseValue<int>(reader["cpu_time"]),
                                    total_elapsed_time = config.DatabaseValue<int>(reader["total_elapsed_time"]),
                                    reads = config.DatabaseValue<long>(reader["reads"]),

                                    writes = config.DatabaseValue<long>(reader["writes"]),
                                    logical_reads = config.DatabaseValue<long>(reader["logical_reads"]),
                                    query_hash = config.DatabaseValue<byte[]>(reader["query_hash"]),
                                    sql_handle = config.DatabaseValue<byte[]>(reader["sql_handle"]),
                                    statement_start_offset = config.DatabaseValue<int>(reader["statement_start_offset"]),

                                    statement_end_offset = config.DatabaseValue<int>(reader["statement_end_offset"]),
                                    database_id = config.DatabaseValue<short>(reader["database_id"]),
                                    blocking_session_id = config.DatabaseValue<short>(reader["blocking_session_id"]),
                                    open_transaction_count = config.DatabaseValue<int>(reader["open_transaction_count"]),
                                    percent_complete = config.DatabaseValue<Single>(reader["percent_complete"]),

                                    transaction_isolation_level = config.DatabaseValue<short>(reader["transaction_isolation_level"]),
                                    query_plan_hash = config.DatabaseValue<byte[]>(reader["query_plan_hash"]),
                                    plan_handle = config.DatabaseValue<byte[]>(reader["plan_handle"]),
                                    query_text = config.DatabaseValue<string>(reader["query_text"])

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
        }

        protected override void RemoteSaveData()
        {
            try
            {
                string SenderType = config.GetValue(Category.Sender, Key.Type);

                if (!SenderType.Equals("No", StringComparison.OrdinalIgnoreCase))
                {
                    BaseSender sender;
                    if (SenderType.Equals("A", StringComparison.OrdinalIgnoreCase))
                        sender = new dm_os_workersTypeA(CurrentTableName, BaseTableName, ProbeTime);
                    else
                        sender = new dm_os_workersTypeB(CurrentTableName, BaseTableName, ProbeTime);
                    sender.SendData();
                }
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
                TableSlideMin = Convert.ToInt32(config.GetValue(Category.Sqlmon, Key.dm_os_workers_TableSlideMin));
                WebApiIntervalModValue = Convert.ToInt32(config.GetValue(Category.Sqlmon, Key.dm_os_workers_WebApiIntervalModValue));
                RunIntervalSec = Convert.ToInt32(config.GetValue(Category.Sqlmon, Key.dm_os_workers_ProbeIntervalSec));
                WebApiIntervalModValue = Convert.ToInt32(config.GetValue(Category.Sqlmon, Key.dm_os_workers_WebApiIntervalModValue));
                RemainTableCnt = Convert.ToInt32(config.GetValue(Category.Sqlmon, Key.dm_os_workers_RemainTableCnt));

                GetDataQuery = @"
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED 
declare @host_name nvarchar(100), @servicename nvarchar(100)
select @host_name = host_name(), @servicename = @@servicename 


declare @session_limit int 
declare @current_worker_cnt int
declare @session_current int 
declare @max_worker_threads int 
select @session_limit = cast(case when value_in_use = 0 then 32767 else value_in_use end as int) from sys.configurations with (nolock) where name = 'user connections' option(recompile)
select @max_worker_threads = max_workers_count from sys.dm_os_sys_info with (nolock) option(recompile)
select @session_current = count(*) from sys.dm_exec_sessions  with (nolock) option(recompile)
select @current_worker_cnt = count(*) from sys.dm_os_workers  with (nolock) option(recompile)
select 
@session_limit session_limit
, @session_current current_session_cnt 
, @max_worker_threads max_worker_thread
, @current_worker_cnt current_worker_cnt
, s.scheduler_id
, quantum_used
, is_preemptive
, context_switch_count
, state
, w.last_wait_type
, processor_group
, tasks_processed_count
, w.task_address
, t.session_id 
, se.original_login_name
, se.host_name
, se.program_name 
, r.command 
, r.cpu_time
, r.total_elapsed_time
, r.reads
, r.writes
, r.logical_reads 
, r.query_hash
, p.sql_handle

, r.statement_start_offset
, r.statement_end_offset
, r.database_id
, r.blocking_session_id
, r.open_transaction_count
, r.percent_complete
, r.transaction_isolation_level
, r.query_plan_hash
, r.plan_handle
, substring(q.text,1, 2000) query_text 

, @host_name host_name
, @servicename servicename 

from sys.dm_os_workers w with (nolock)
join sys.dm_os_schedulers s with (nolock)
on w.scheduler_address = s.scheduler_address 
left join sys.dm_os_tasks t with (nolock)
on t.task_address = w.task_address
left join sys.dm_exec_sessions se with (nolock)
on t.session_id = se.session_id
left join sys.dm_exec_requests r with (nolock)
on se.session_id = r.session_id
outer apply sys.dm_exec_sql_text (r.sql_handle) q
left join sys.sysprocesses p with (nolock)
on r.session_id = p.spid 
where r.command is not null
and t.session_id > 50
and t.session_id <> @@spid
and original_login_name not like '%rdsadmin%' and  original_login_name not like 'NT Service%' 
option (recompile)
";



                TableGenQuery = @"
CREATE TABLE [dbo].[dm_os_workers_{0}](
	[idx]                           [bigint] IDENTITY(1,1) NOT NULL,
	[probe_time]                    [datetime] NULL,
	[session_limit]                 [int] NULL,
	[current_session_cnt]           [int] NULL,
	[max_worker_thread]             [int] NULL,
	[current_worker_cnt]            [int] NULL,
	[scheduler_id]                  [int] NULL,
	[quantum_used]                  [bigint] NULL,
	[is_preemptive]                 [bit] NULL,
	[context_switch_count]          [bigint] NULL,
	[state]                         [nvarchar](60) NULL,
	[last_wait_type]                [nvarchar](100) NULL,
	[processor_group]               [smallint] NULL,
	[tasks_processed_count]         [int] NULL,
	[task_address]                  [varbinary](8) NULL,
	[session_id]                    [int] NULL,
	[original_login_name]           [nvarchar](128) NULL,
	[host_name]                     [nvarchar](128) NULL,
	[program_name]                  [nvarchar](128) NULL,
	[command]                       [nvarchar](32) NULL,
	[cpu_time]                      [bigint] NULL,
	[total_elapsed_time]            [bigint] NULL,
	[reads]                         [bigint] NULL,
	[writes]                        [bigint] NULL,
	[logical_reads]                 [bigint] NULL,
	[query_hash]                    [binary](8) NULL,
	[sql_handle]                    [varbinary](64) NULL,
	[statement_start_offset]        [int] NULL,
	[statement_end_offset]          [int] NULL,
	[database_id]                   [smallint] NULL,
	[blocking_session_id]           [int] NULL,
	[open_transaction_count]        [int] NULL,
	[percent_complete]              [real] NULL,
	[transaction_isolation_level]   [smallint] NULL,
	[query_plan_hash]               [binary](8) NULL,
	[plan_handle]                   [varbinary](64) NULL,
	[query_text]                    [nvarchar](2000) NULL
) ON [PRIMARY]
GO

CREATE CLUSTERED INDEX [cl_dm_os_workers_{0}] ON [dbo].[dm_os_workers_{0}]
(
	[probe_time] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO


declare @dropViewQuery varchar(8000)
declare @createViewQuery varchar(8000)

set @dropViewQuery
=
'
	drop view view_dm_os_workers
';

set @createViewQuery 
=
'
	create view view_dm_os_workers
	as 
	select 
		  [idx]                          
		, [probe_time]                   
		, [session_limit]                
		, [current_session_cnt]          
		, [max_worker_thread]            
		, [current_worker_cnt]           
		, [scheduler_id]                 
		, [quantum_used]                 
		, [is_preemptive]                
		, [context_switch_count]         
		, [state]                        
		, [last_wait_type]               
		, [processor_group]              
		, [tasks_processed_count]        
		, [task_address]                 
		, [session_id]                   
		, [original_login_name]          
		, [host_name]                    
		, [program_name]                 
		, [command]                      
		, [cpu_time]                     
		, [total_elapsed_time]           
		, [reads]                        
		, [writes]                       
		, [logical_reads]                
		, [query_hash]                   
		, [sql_handle]                   
		, [statement_start_offset]       
		, [statement_end_offset]         
		, [database_id]                  
		, [blocking_session_id]          
		, [open_transaction_count]       
		, [percent_complete]             
		, [transaction_isolation_level]  
		, [query_plan_hash]              
		, [plan_handle]                  
		, [query_text]                   
	from dm_os_workers_{0}
';

begin try
    if exists (select * from INFORMATION_SCHEMA.VIEWS where table_name = 'view_dm_os_workers')
    begin
        exec (@dropViewQuery) 
    end 
end try 
begin catch 
	RAISERROR(N'lazylog : drop view error %s', 16, 1, @dropViewQuery) WITH LOG	
end catch 

begin try
	exec (@createViewQuery)
end try 
begin catch 
	RAISERROR(N'lazylog : create view error %s', 16, 1, @createViewQuery) WITH LOG	
end catch 

";
            }
            catch (Exception ex)
            {
                log.Error(string.Format("{0}, {1}", ex.Message, ex.StackTrace));
            }
        }
    }
}
