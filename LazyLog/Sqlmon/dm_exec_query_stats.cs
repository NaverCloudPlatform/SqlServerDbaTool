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
    class dm_exec_query_stats : BaseSqlmon<dm_exec_query_stats_data>
    {
        string BaseSummaryTableName = string.Empty;
        string CurrentSummaryTableName = string.Empty;
        string PreviousTableName = string.Empty;
        string SummaryTableGenQuery = string.Empty;
        string MakeViewQuery = string.Empty;
        string LocalSaveDeltaQuery = string.Empty;
        string dm_exec_query_stats_statement_TableGenQuery = string.Empty;
        //string dm_exec_query_stats_statement_last_access_limit = string.Empty;
        string GetQueryStatementQuery = string.Empty;
        int SummaryTableRemainCnt = 0;
        string RemovePlan = string.Empty;

        public dm_exec_query_stats() : base()
        {
            try
            {
                BaseTableName = GetType().Name;
                BaseSummaryTableName = "dm_exec_query_stats_summary";
                CurrentViewName = "view_" + BaseSummaryTableName;
                SummaryTableGenQuery = @"
CREATE TABLE [dbo].[dm_exec_query_stats_summary_{0}](
	probe_time              datetime not null, 
	[execution_count]       [bigint] NOT NULL,
	[total_worker_time]     [bigint] NOT NULL,
	[total_logical_reads]   [bigint] NOT NULL,
	[total_physical_reads]  [bigint] NOT NULL,
	[total_logical_writes]  [bigint] NOT NULL,
	[total_elapsed_time]    [bigint] NOT NULL,
	[total_grant_kb]        [bigint] NULL,
	[sql_handle]            [varbinary](64) NOT NULL,
	[statement_start_offset] [int] NOT NULL,
	[statement_end_offset]  [int] NOT NULL,
	[plan_handle]           [varbinary](64) NOT NULL,
	[query_hash]            [binary](8) NULL,
	[query_plan_hash]       [binary](8) NULL
) ON [PRIMARY]
GO

SET ANSI_PADDING ON
GO

CREATE CLUSTERED INDEX [cl_dm_exec_query_stats_summary_{0}] ON [dbo].[dm_exec_query_stats_summary_{0}]
(
	[probe_time] ASC, 
	[query_hash] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO


declare @dropViewQuery varchar(8000)
declare @createViewQuery varchar(8000)

set @dropViewQuery
=
'
	drop view view_dm_exec_query_stats_summary
';

set @createViewQuery 
=
'
	create view view_dm_exec_query_stats_summary
	as 
	select 
        probe_time              
        , [execution_count]       
        , [total_worker_time]     
        , [total_logical_reads]   
        , [total_physical_reads]  
        , [total_logical_writes]  
        , [total_elapsed_time]    
        , [total_grant_kb]        
        , [sql_handle]            
        , [statement_start_offset]
        , [statement_end_offset]  
        , [plan_handle]           
        , [query_hash]            
        , [query_plan_hash]       
	from dm_exec_query_stats_summary_{0}
';

begin try
    if exists (select * from INFORMATION_SCHEMA.VIEWS where table_name = 'view_dm_exec_query_stats_summary')
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

                // 0 stats_summary
                // 1 PreviousTableName 
                // 2 CurrentTableName
                LocalSaveDeltaQuery = @"
insert into [view_dm_exec_query_stats_summary]
(
	  probe_time
	, execution_count
	, total_worker_time		
	, total_logical_reads	
	, total_physical_reads	
	, total_logical_writes	
	, total_elapsed_time	
	, total_grant_kb		
	, sql_handle            
	, statement_start_offset
	, statement_end_offset  
	, plan_handle			
	, query_hash
	, query_plan_hash     
)
select top (100)
	  @probe_time probe_time 
	, case when execution_count		 <= 0 then 1 else execution_count		 end execution_count		
	, case when total_worker_time	 <= 0 then 0 else total_worker_time		 end total_worker_time	
	, case when total_logical_reads	 <= 0 then 0 else total_logical_reads	 end total_logical_reads	
	, case when total_physical_reads <= 0 then 0 else total_physical_reads	 end total_physical_reads
	, case when total_logical_writes <= 0 then 0 else total_logical_writes	 end total_logical_writes
	, case when total_elapsed_time	 <= 0 then 0 else total_elapsed_time	 end total_elapsed_time	
	, case when total_grant_kb		 <= 0 then 0 else total_grant_kb		 end total_grant_kb		
	, sql_handle            
	, statement_start_offset
	, statement_end_offset  
	, plan_handle			
	, query_hash
	, query_plan_hash     
from 
	( 
	select 
		  new.execution_count			- old.execution_count       as execution_count		
		, new.total_worker_time			- old.total_worker_time	    as total_worker_time		
		, new.total_logical_reads		- old.total_logical_reads	as total_logical_reads	
		, new.total_physical_reads		- old.total_physical_reads	as total_physical_reads	
		, new.total_logical_writes		- old.total_logical_writes  as total_logical_writes	
		, new.total_elapsed_time		- old.total_elapsed_time	as total_elapsed_time	
		, new.total_grant_kb			- old.total_grant_kb		as total_grant_kb		
		, new.sql_handle            								as sql_handle            
		, new.statement_start_offset								as statement_start_offset
		, new.statement_end_offset  								as statement_end_offset  
		, new.plan_handle											as plan_handle			
		, new.query_hash											as query_hash
		, new.query_plan_hash     									as query_plan_hash     
	from 
		( 
		select 
			  sum(execution_count		  ) execution_count		  
			, sum(total_worker_time	      ) total_worker_time	  
			, sum(total_logical_reads	  ) total_logical_reads	  
			, sum(total_physical_reads	  ) total_physical_reads  
			, sum(total_logical_writes    ) total_logical_writes  
			, sum(total_elapsed_time	  ) total_elapsed_time	  
			, sum(total_grant_kb		  ) total_grant_kb		  
			, max(sql_handle              ) sql_handle            
			, min(statement_start_offset  )	statement_start_offset
			, max(statement_end_offset    )	statement_end_offset  
			, max(plan_handle			  )	plan_handle			  
			,     query_hash
			, max(query_plan_hash         ) query_plan_hash
		from 
			[dbo].[view_dm_exec_query_stats_new]
		group by query_hash
		) new
		left join 
		( 
		select
			  sum(execution_count		  ) execution_count		  
			, sum(total_worker_time	      ) total_worker_time	  
			, sum(total_logical_reads	  ) total_logical_reads	  
			, sum(total_physical_reads	  ) total_physical_reads  
			, sum(total_logical_writes    ) total_logical_writes  
			, sum(total_elapsed_time	  ) total_elapsed_time	  
			, sum(total_grant_kb		  ) total_grant_kb		  
			, max(sql_handle              ) sql_handle            
			, min(statement_start_offset  )	statement_start_offset
			, max(statement_end_offset    )	statement_end_offset  
			, max(plan_handle			  )	plan_handle			  
			,     query_hash
			, max(query_plan_hash         ) query_plan_hash	 
		from 
			[dbo].[view_dm_exec_query_stats_old]
		group by query_hash
		) old
		on new.query_hash = old.query_hash
	) a 
where total_worker_time > 0 
order by total_worker_time desc 
option (recompile) 
";


                RemovePlan = @"

set nocount on 
set transaction isolation level read uncommitted 

declare @freeproccache_target table
(
idx int identity(1,1), 
plan_handle varbinary (64)
) 

declare @maxIdx int , @plan_handle  varchar(100), @sql_handle  varbinary (64), @text varchar(max)

insert into @freeproccache_target (b.plan_handle)
select distinct plan_handle 
from 
	(
	select distinct query_hash from [dbo].[dm_exec_query_stats_statement]
	where 
		 query_statement like '%CounterDataOrigin_%'

	  or query_statement like '%dm_exec_query_stats_summary%'
	  or query_statement like '%dm_exec_query_stats_statement%'
	  or query_statement like '%sp_readerrorlogSendHist%'
	  or query_statement like '%%rdsadmin%'
	  or query_statement like '%%lazylog%'
	  or query_statement like '%@tbl_sysprocesses%'
	  or query_statement like '%master.sys.databases%'
	  or query_statement like '%dm_exec_sessions%'
	  or query_statement like '%dm_os_sys_info%'
	  or query_statement like '%@TableList%'	
	  or query_statement like '%configurations%'	
	  or query_statement like '%freeproccache_target%'	
	  or query_statement like '%DBCC FREEPROCCACHE%'	
	  or query_statement like '%view_dm_exec_query_stats_summary%'	
	  or query_statement like '%dbo.sysaltfiles%'
	  or query_statement like '%sys.database%'
	  or query_statement like '%database_mirroring%'
	  or query_statement like '%spt_tablecollations_view%'
	  or query_statement like '%fn_helpcollations()%'
	  or query_statement like '%sys.indexes%'
	  or query_statement like '%syspolicy_configuration%'
	  or query_statement like '%sys.syscolumns%'
	  or query_statement like '%sys.tables%'
	  or query_statement like '%dm_exec_query_stats_%'
	  or query_statement like '%dm_os_workers_%'
	  or query_statement like '%sp_lock2_%'
	  or query_statement like '%CounterDataOriginSendHist%'
	  or query_statement like '%CounterDetailsAutoUpdated%'
	  or query_statement like '%CounterDetailsFilterInfo%'
	  or query_statement like '%DisplayToIDOrigin%'
	  or query_statement like '%dm_exec_query_stats_summary_%'
	  or query_statement like '%sp_readagentlogSendHist%'
	  or query_statement like '%sp_readerrorlogSendHist%'
	  or query_statement like '%BaseTableName%'
	  or query_statement like '%sql_handle = sql_handle%'
	  or query_statement like '%backupmediafamily%'
	  or query_statement like '%DisplayToID%'
	  or query_statement like '%dm_exec_sql_text%'
	) a 
	join [dbo].[view_dm_exec_query_stats_new] b 
	on a.query_hash = b.query_hash 
option (recompile)


delete b
from 
	(
	select distinct query_hash 
	from [dbo].[dm_exec_query_stats_statement]
	where 
		 query_statement like '%CounterDataOrigin_%'
	  or query_statement like '%dm_exec_query_stats_summary%'
	  or query_statement like '%dm_exec_query_stats_statement%'
	  or query_statement like '%sp_readerrorlogSendHist%'
	  or query_statement like '%%rdsadmin%'
	  or query_statement like '%%lazylog%'
	  or query_statement like '%@tbl_sysprocesses%'
	  or query_statement like '%master.sys.databases%'
	  or query_statement like '%dm_exec_sessions%'
	  or query_statement like '%dm_os_sys_info%'
	  or query_statement like '%@TableList%'	
	  or query_statement like '%configurations%'	
	  or query_statement like '%freeproccache_target%'	
	  or query_statement like '%DBCC FREEPROCCACHE%'	
	  or query_statement like '%view_dm_exec_query_stats_summary%'	
	  or query_statement like '%dbo.sysaltfiles%'
	  or query_statement like '%sys.database%'
	  or query_statement like '%database_mirroring%'
	  or query_statement like '%spt_tablecollations_view%'
	  or query_statement like '%fn_helpcollations()%'
	  or query_statement like '%sys.indexes%'
	  or query_statement like '%syspolicy_configuration%'
	  or query_statement like '%sys.syscolumns%'
	  or query_statement like '%sys.tables%'
	  or query_statement like '%dm_exec_query_stats_%'
	  or query_statement like '%dm_os_workers_%'
	  or query_statement like '%sp_lock2_%'
	  or query_statement like '%CounterDataOriginSendHist%'
	  or query_statement like '%CounterDetailsAutoUpdated%'
	  or query_statement like '%CounterDetailsFilterInfo%'
	  or query_statement like '%DisplayToIDOrigin%'
	  or query_statement like '%dm_exec_query_stats_summary_%'
	  or query_statement like '%sp_readagentlogSendHist%'
	  or query_statement like '%sp_readerrorlogSendHist%'
	  or query_statement like '%BaseTableName%'
	  or query_statement like '%sql_handle = sql_handle%'
	  or query_statement like '%backupmediafamily%'
	  or query_statement like '%DisplayToID%'
	  or query_statement like '%dm_exec_sql_text%'
	) a 
	join [dbo].[view_dm_exec_query_stats_new] b 
	on a.query_hash = b.query_hash 
option(recompile)

delete [dm_exec_query_stats_statement]
where 
		 query_statement like '%CounterDataOrigin_%'
	  or query_statement like '%dm_exec_query_stats_summary%'
	  or query_statement like '%dm_exec_query_stats_statement%'
	  or query_statement like '%sp_readerrorlogSendHist%'
	  or query_statement like '%%rdsadmin%'
	  or query_statement like '%%lazylog%'
	  or query_statement like '%@tbl_sysprocesses%'
	  or query_statement like '%master.sys.databases%'
	  or query_statement like '%dm_exec_sessions%'
	  or query_statement like '%dm_os_sys_info%'
	  or query_statement like '%@TableList%'	
	  or query_statement like '%configurations%'	
	  or query_statement like '%freeproccache_target%'	
	  or query_statement like '%DBCC FREEPROCCACHE%'	
	  or query_statement like '%view_dm_exec_query_stats_summary%'	
	  or query_statement like '%dbo.sysaltfiles%'
	  or query_statement like '%sys.database%'
	  or query_statement like '%database_mirroring%'
	  or query_statement like '%spt_tablecollations_view%'
	  or query_statement like '%fn_helpcollations()%'
	  or query_statement like '%sys.indexes%'
	  or query_statement like '%syspolicy_configuration%'
	  or query_statement like '%sys.syscolumns%'
	  or query_statement like '%sys.tables%'
	  or query_statement like '%dm_exec_query_stats_%'
	  or query_statement like '%dm_os_workers_%'
	  or query_statement like '%sp_lock2_%'
	  or query_statement like '%CounterDataOriginSendHist%'
	  or query_statement like '%CounterDetailsAutoUpdated%'
	  or query_statement like '%CounterDetailsFilterInfo%'
	  or query_statement like '%DisplayToIDOrigin%'
	  or query_statement like '%dm_exec_query_stats_summary_%'
	  or query_statement like '%sp_readagentlogSendHist%'
	  or query_statement like '%sp_readerrorlogSendHist%'
	  or query_statement like '%BaseTableName%'
	  or query_statement like '%sql_handle = sql_handle%'
	  or query_statement like '%backupmediafamily%'
	  or query_statement like '%DisplayToID%'
	  or query_statement like '%dm_exec_sql_text%'
option(recompile)

select @maxIdx = max(idx) 
from @freeproccache_target

if (@maxIdx is not null)
begin 
	while (1=1)
	begin
	  select @plan_handle = CONVERT(VARCHAR(100), plan_handle, 1)
	  from @freeproccache_target 
	  where idx = @maxIdx 

	  exec ('DBCC FREEPROCCACHE (' + @plan_handle +')')

	  set @maxIdx = @maxIdx - 1 
	  if @maxIdx % 30 = 0
		waitfor delay '00:00:01.000'
  
	  if @maxIdx = 0 
		break;
	end
end 
";

                dm_exec_query_stats_statement_TableGenQuery = @"
CREATE TABLE [dbo].[dm_exec_query_stats_statement](
	[last_access_time] [datetime] NULL,
	[query_hash] [binary](8) NULL,
	[query_statement] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

create clustered index cl_dm_exec_query_stats_statement on dm_exec_query_stats_statement (last_access_time)
go

create nonclustered index nc_dm_exec_query_stats_statement_01 on dm_exec_query_stats_statement (query_hash)
go
";

                GetQueryStatementQuery = @"
set nocount on 
set transaction isolation level read uncommitted 

update t
	set last_access_time = @last_porbe_time
from [dbo].[dm_exec_query_stats_statement] t 
	join [view_dm_exec_query_stats_summary] a
	on t.query_hash = a.query_hash 
where 
	probe_time = @last_porbe_time

insert into [dbo].[dm_exec_query_stats_statement] (last_access_time, query_hash, query_statement )
select  
	@last_porbe_time
	, qs.query_hash 
	, substring(
		qt.text
		, (qs.statement_start_offset/2)+1
		, ((case qs.statement_end_offset when -1 then datalength(qt.text) else qs.statement_end_offset end - qs.statement_start_offset)/2) + 1
		) as statement_text
from dbo.[view_dm_exec_query_stats_summary] as qs
	cross apply sys.dm_exec_sql_text(qs.sql_handle) as qt
where probe_time = @last_porbe_time
    and not exists (select * from [dm_exec_query_stats_statement] where query_hash = qs.query_hash) 

delete from [dm_exec_query_stats_statement] where last_access_time < (@last_porbe_time - @delete_from_day)
";


                if (!ExistTable("dm_exec_query_stats_statement"))
                {
                    Common.QueryExecuter(
                    config.GetConnectionString(InitialCatalog.Repository)
                    , dm_exec_query_stats_statement_TableGenQuery);
                }
            }
            catch (Exception ex)
            {
                log.Error(string.Format("{0}, {1}", ex.Message, ex.StackTrace));
            }
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
                        log.Warn("sqlmon (dm_exec_query_stats) can't start because ProbeIntervalSec is 0");
                        break;
                    }
                    else
                    {
                        ProbeTime = DateTime.Now;
                        Purge();
                        NewTableGen(BaseTableName, out CurrentTableName);

                        if (!GetCurrentTableName(BaseSummaryTableName, out CurrentSummaryTableName))
                        {
                            Common.QueryExecuter(
                                config.GetConnectionString(InitialCatalog.Repository)
                                , string.Format(SummaryTableGenQuery, DateTime.Now.ToString("yyyyMMdd_HHmmss")));
                        }
                        else
                        {
                            if (TableSlideCheck(BaseSummaryTableName, CurrentSummaryTableName, TableSlideMin))
                                TableSlide(BaseSummaryTableName, SummaryTableGenQuery, DropTableQuery, SummaryTableRemainCnt, out CurrentSummaryTableName);
                        }

                        GetData();
                        LocalSaveData(CurrentTableName);

                        if (PreviousTableName != string.Empty)
                        {
                            LocalSaveDeltaValue();
                            GetQueryStatement();

                            RemoteSaveData();
                        }

                        PreviousTableName = CurrentTableName;

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

        private void GetQueryStatement()
        {
            log.Warn("GetQueryStatement");

            try
            {
                DateTime DatetimeNow = DateTime.Now;
                using (SqlConnection conn = new SqlConnection(config.GetConnectionString(InitialCatalog.Repository)))
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = GetQueryStatementQuery;
                        cmd.Parameters.Add("@last_porbe_time", SqlDbType.DateTime).Value = Convert.ToDateTime(ProbeTime.ToString("yyyy-MM-dd HH:mm:ss.000"));
                        cmd.Parameters.Add("@delete_from_day", SqlDbType.Int).Value = 3;
                        cmd.ExecuteNonQuery();
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                log.Error(string.Format("{0}, {1}", ex.Message, ex.StackTrace));
            }
        }

        private void LocalSaveDeltaValue()
        {
            log.Warn("LocalSaveDeltaValue");

            try
            {
                DateTime DatetimeNow = DateTime.Now;
                using (SqlConnection conn = new SqlConnection(config.GetConnectionString(InitialCatalog.Repository)))
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = LocalSaveDeltaQuery;
                        cmd.Parameters.Add("@probe_time", SqlDbType.DateTime).Value = Convert.ToDateTime(ProbeTime.ToString("yyyy-MM-dd HH:mm:ss.000"));
                        cmd.ExecuteNonQuery();
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                log.Error(string.Format("{0}, {1}", ex.Message, ex.StackTrace));
            }
        }



        protected override void Purge()
        {
            try
            {
                buffer.Clear();
                log.Warn("Purge");
            }
            catch (Exception ex)
            {
                log.Error(string.Format("{0}, {1}", ex.Message, ex.StackTrace));
            }
        }

        // 이전과 이후만 있으니 딱 2개만 있으면 된다. 
        private void NewTableGen(string BaseTableName, out string CurrentTableName)
        {
            try
            {
                Common.QueryExecuter(
                    config.GetConnectionString(InitialCatalog.Repository)
                    , string.Format(TableGenQuery, DateTime.Now.ToString("yyyyMMdd_HHmmss")));

                Common.QueryExecuter(
                    config.GetConnectionString(InitialCatalog.Repository)
                    , string.Format(DropTableQuery, "2", BaseTableName)); // 2개만 남겨라 

                GetCurrentTableName(BaseTableName, out CurrentTableName);

                if (PreviousTableName != string.Empty) // 한바퀴 돌아서 테이블이 있으면 만들어라 (변수가 할당 되었으면)
                {
                    MakeView(PreviousTableName, "old");
                }
                MakeView(CurrentTableName, "new");
            }
            catch (Exception ex)
            {
                CurrentTableName = ""; 
                log.Error(string.Format("{0}, {1}", ex.Message, ex.StackTrace));
            }
            
        }

        private void MakeView(string tableName, string viewType)
        {
            try
            {
                Common.QueryExecuter(
                    config.GetConnectionString(InitialCatalog.Repository)
                    , string.Format(MakeViewQuery, tableName, viewType));
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
                DateTime DatetimeNow = DateTime.Now;
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
                                buffer.Add(new dm_exec_query_stats_data
                                {
                                    execution_count = config.DatabaseValue<long>(reader["execution_count"]),
                                    total_worker_time = config.DatabaseValue<long>(reader["total_worker_time"]),
                                    total_logical_reads = config.DatabaseValue<long>(reader["total_logical_reads"]),
                                    total_physical_reads = config.DatabaseValue<long>(reader["total_physical_reads"]),
                                    total_logical_writes = config.DatabaseValue<long>(reader["total_logical_writes"]),
                                    total_elapsed_time = config.DatabaseValue<long>(reader["total_elapsed_time"]),
                                    total_grant_kb = config.DatabaseValue<long>(reader["total_grant_kb"]),
                                    sql_handle = config.DatabaseValue<byte[]>(reader["sql_handle"]),
                                    statement_start_offset = config.DatabaseValue<int>(reader["statement_start_offset"]),
                                    statement_end_offset = config.DatabaseValue<int>(reader["statement_end_offset"]),
                                    plan_handle = config.DatabaseValue<byte[]>(reader["plan_handle"]),
                                    query_hash = config.DatabaseValue<byte[]>(reader["query_hash"]),
                                    query_plan_hash = config.DatabaseValue<byte[]>(reader["query_plan_hash"])
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
                Common.QueryExecuter(
                    config.GetConnectionString(InitialCatalog.Repository)
                    , RemovePlan
                    , 300
                );

                string SenderType = config.GetValue(Category.Sender, Key.Type);

                if (!SenderType.Equals("No", StringComparison.OrdinalIgnoreCase))
                {
                    BaseSender sender;
                    if (SenderType.Equals("A", StringComparison.OrdinalIgnoreCase))
                        sender = new dm_exec_query_statsTypeA(CurrentSummaryTableName, BaseTableName, ProbeTime);
                    else
                        sender = new dm_exec_query_statsTypeB(CurrentSummaryTableName, BaseTableName, ProbeTime);

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

                RunIntervalSec = Convert.ToInt32(config.GetValue(Category.Sqlmon, Key.dm_exec_query_stats_ProbeIntervalSec));
                TableSlideMin = Convert.ToInt32(config.GetValue(Category.Sqlmon, Key.dm_exec_query_stats_TableSlideMin));
                SummaryTableRemainCnt = Convert.ToInt32(config.GetValue(Category.Sqlmon, Key.dm_exec_query_stats_RemainTableCnt));
                WebApiIntervalModValue = Convert.ToInt32(config.GetValue(Category.Sqlmon, Key.dm_exec_query_stats_WebApiIntervalModValue));

                if (config.GetValue(Category.SqlServer, Key.Version) == "2016")
                {
                    GetDataQuery = @"
select top 100
	  execution_count
	, total_worker_time
	, total_logical_reads
	, total_physical_reads 
	, total_logical_writes 
	, total_elapsed_time
	, total_grant_kb
	, sql_handle
	, statement_start_offset
	, statement_end_offset
	, plan_handle
	, query_hash
	, query_plan_hash
from master.sys.dm_exec_query_stats
where last_execution_time > dateadd(second, abs(60*60) * -1 , getdate()) 
order by total_worker_time desc
option (recompile) ;
";
                }
                else
                {
                    GetDataQuery = @"
select top 100
	  execution_count
	, total_worker_time
	, total_logical_reads
	, total_physical_reads 
	, total_logical_writes 
	, total_elapsed_time
	, cast(0 as bigint) total_grant_kb
	, sql_handle
	, statement_start_offset
	, statement_end_offset
	, plan_handle
	, query_hash
	, query_plan_hash
from master.sys.dm_exec_query_stats
where last_execution_time > dateadd(second, abs(60*60) * -1 , getdate()) 
order by total_worker_time desc
option (recompile) ;
";
                }

                TableGenQuery = @"
CREATE TABLE [dbo].[dm_exec_query_stats_{0}](
	[execution_count]           [bigint] NOT NULL,
	[total_worker_time]         [bigint] NOT NULL,
	[total_logical_reads]       [bigint] NOT NULL,
	[total_physical_reads]      [bigint] NOT NULL,
	[total_logical_writes]      [bigint] NOT NULL,
	[total_elapsed_time]        [bigint] NOT NULL,
	[total_grant_kb]            [bigint] NULL,
	[sql_handle]                [varbinary](64) NOT NULL,
	[statement_start_offset]    [int] NOT NULL,
	[statement_end_offset]      [int] NOT NULL,
	[plan_handle]               [varbinary](64) NOT NULL,
	[query_hash]                [binary](8) NULL,
	[query_plan_hash]           [binary](8) NULL
) ON [PRIMARY]
GO

create clustered index cl_dm_exec_query_stats_{0} on dm_exec_query_stats_{0}
([query_hash])
GO

";
                MakeViewQuery = @"
declare @dropViewQuery varchar(8000)
declare @createViewQuery varchar(8000)

set @dropViewQuery
=
'
	drop view view_dm_exec_query_stats_{1}
';

set @createViewQuery
=
'
	create view view_dm_exec_query_stats_{1}
	as 
	select 
		  [execution_count]       
		, [total_worker_time]     
		, [total_logical_reads]   
		, [total_physical_reads]  
		, [total_logical_writes]  
		, [total_elapsed_time]    
		, [total_grant_kb]        
		, [sql_handle]            
		, [statement_start_offset]
		, [statement_end_offset]  
		, [plan_handle]           
		, [query_hash]            
		, [query_plan_hash]       
	from {0}
';

begin try
    if exists (select * from INFORMATION_SCHEMA.VIEWS where table_name = 'view_dm_exec_query_stats_{1}')
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
