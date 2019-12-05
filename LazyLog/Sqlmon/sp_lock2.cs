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
    class sp_lock2 : BaseSqlmon<sp_lock2_data>
    {
        public sp_lock2() : base()
        {
            BaseTableName = GetType().Name;
        }
        
        internal override void Start()
        {
            Initialization();
            buffer = new List<sp_lock2_data>();
            IsRunning = true;

            while (IsRunning)
            {
                try
                {
                    DateTime endTime = DateTime.Now.Add(new TimeSpan(0, 0, RunIntervalSec));  // 3초마다 동작 

                    if (RunIntervalSec == 0)
                    {
                        IsRunning = false;
                        log.Warn("sqlmon (sp_lock2) can't start because ProbeIntervalSec is 0");
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
                                buffer.Add(new sp_lock2_data
                                {
                                    //MachineName = config.GetValue(Category.GLOBAL, Key.hostname),
                                    //FullInstanceName = config.DatabaseValue<string>(reader["servicename"]),
                                    //MachinePrivateIp = config.GetValue(Category.GLOBAL, Key.private_ip),
                                    probe_time = Convert.ToDateTime(probe_time),
                                    hh_mm_ss = config.DatabaseValue<string>(reader["hh_mm_ss"]),
                                    wait_sec = config.DatabaseValue<long>(reader["wait_sec"]),
                                    locktree = config.DatabaseValue<string>(reader["locktree"]),
                                    spid = config.DatabaseValue<short>(reader["spid"]),
                                    kpid = config.DatabaseValue<short>(reader["kpid"]),
                                    blocked = config.DatabaseValue<short>(reader["blocked"]),
                                    waittype = config.DatabaseValue<byte[]>(reader["waittype"]),
                                    waittime = config.DatabaseValue<long>(reader["waittime"]),
                                    lastwaittype = config.DatabaseValue<string>(reader["lastwaittype"]),
                                    waitresource = config.DatabaseValue<string>(reader["waitresource"]),
                                    dbid = config.DatabaseValue<short>(reader["dbid"]),
                                    uid = config.DatabaseValue<short>(reader["uid"]),
                                    cpu = config.DatabaseValue<int>(reader["cpu"]),
                                    physical_io = config.DatabaseValue<long>(reader["physical_io"]),
                                    memusage = config.DatabaseValue<int>(reader["memusage"]),
                                    login_time = config.DatabaseValue<DateTime>(reader["login_time"]),
                                    last_batch = config.DatabaseValue<DateTime>(reader["last_batch"]),
                                    ecid = config.DatabaseValue<short>(reader["ecid"]),
                                    open_tran = config.DatabaseValue<short>(reader["open_tran"]),
                                    status = config.DatabaseValue<string>(reader["status"]),
                                    sid = config.DatabaseValue<byte[]>(reader["sid"]),
                                    hostname = config.DatabaseValue<string>(reader["hostname"]),
                                    program_name = config.DatabaseValue<string>(reader["program_name"]),
                                    hostprocess = config.DatabaseValue<string>(reader["hostprocess"]),
                                    cmd = config.DatabaseValue<string>(reader["cmd"]),
                                    nt_domain = config.DatabaseValue<string>(reader["nt_domain"]),
                                    nt_username = config.DatabaseValue<string>(reader["nt_username"]),
                                    net_address = config.DatabaseValue<string>(reader["net_address"]),
                                    net_library = config.DatabaseValue<string>(reader["net_library"]),
                                    loginame = config.DatabaseValue<string>(reader["loginame"]),
                                    context_info = config.DatabaseValue<byte[]>(reader["context_info"]),
                                    sql_handle = config.DatabaseValue<byte[]>(reader["sql_handle"]),
                                    stmt_start = config.DatabaseValue<int>(reader["stmt_start"]),
                                    stmt_end = config.DatabaseValue<int>(reader["stmt_end"]),
                                    request_id = config.DatabaseValue<int>(reader["request_id"]),
                                    objectid = config.DatabaseValue<int>(reader["objectid"]),
                                    number = config.DatabaseValue<short>(reader["number"]),
                                    encrypted = config.DatabaseValue<bool>(reader["encrypted"]),
                                    text = config.DatabaseValue<string>(reader["text"])
                                    //죽 다 넣어준다. 
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
                        sender = new sp_lock2TypeA(CurrentTableName, BaseTableName, ProbeTime);
                    else
                        sender = new sp_lock2TypeB(CurrentTableName, BaseTableName, ProbeTime);

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
                TableSlideMin = Convert.ToInt32(config.GetValue(Category.Sqlmon, Key.sp_lock2_TableSlideMin));
                WebApiIntervalModValue = Convert.ToInt32(config.GetValue(Category.Sqlmon, Key.sp_lock2_WebApiIntervalModValue));
                RunIntervalSec = Convert.ToInt32(config.GetValue(Category.Sqlmon, Key.sp_lock2_ProbeIntervalSec));
                RemainTableCnt = Convert.ToInt32(config.GetValue(Category.Sqlmon, Key.sp_lock2_RemainTableCnt));

                GetDataQuery = @"
set nocount on 
set transaction isolation level read uncommitted 
declare @host_name nvarchar(100), @servicename nvarchar(100)
select @host_name = host_name(), @servicename = @@servicename 

declare @tbl_sysprocesses table
(
depth int
, tree varchar(7000)
, spid int
, blocked int
)
 
insert into @tbl_sysprocesses(depth, tree, spid, blocked)
select 0, cast(spid as varchar(100)) spid , spid, blocked
from master.dbo.sysprocesses with(nolock)
where blocked = 0 
    and spid in (select blocked from master.dbo.sysprocesses with (nolock) where blocked <> 0)
    and nt_username not like '%rdsadmin%' and loginame not like '%rdsadmin%' 
option(recompile)

declare @max_depth int
    set @max_depth = 5
 
while (1=1)
begin
    insert into @tbl_sysprocesses (depth, tree, spid, blocked) 
    select a.depth + 1 depth, a.tree + ' > ' +cast(b.spid as varchar(8000)) tree, b.spid, b.blocked
    from @tbl_sysprocesses a
        inner join master.dbo.sysprocesses b with (nolock)
        on a.spid = b.blocked
    where depth in (select max(depth) from @tbl_sysprocesses )
        and b.spid<> b.blocked 
    option(recompile)
    if @@rowcount = 0 break
    set @max_depth = @max_depth - 1
    if @max_depth <= 1 break 
end

select
        convert(char(10), cast((b.waittime / 1000) * 1.1574074074074073E-5 as datetime) , 108) as [hh_mm_ss]
    , b.waittime / 1000 wait_sec
    , left(a.tree, 40)+case when len(a.tree) > 40 then '...' else '' end locktree
    , b.spid
    , b.kpid
    , b.blocked
    , b.waittype
    , b.waittime
    , b.lastwaittype
    , b.waitresource
    , b.dbid
    , b.uid
    , b.cpu
    , b.physical_io
    , b.memusage
    , b.login_time
    , b.last_batch
    , b.ecid
    , b.open_tran
    , b.status
    , b.sid
    , b.hostname
    , b.program_name
    , b.hostprocess
    , b.cmd
    , b.nt_domain
    , b.nt_username
    , b.net_address
    , b.net_library
    , b.loginame
    , b.context_info
    , b.sql_handle
    , b.stmt_start
    , b.stmt_end
    , b.request_id
    , qt.objectid
    , qt.number
    , qt.encrypted
    , left(qt.text , 1000) text
--    ,@host_name hostname
    ,@servicename servicename 
from @tbl_sysprocesses a
    inner join master.dbo.sysprocesses b with(nolock)
    on a.spid = b.spid
    cross apply sys.dm_exec_sql_text(b.sql_handle) AS qt
where b.nt_username not like '%rdsadmin%' and b.loginame not in ('sa')
order by tree 
option(recompile)
";

                TableGenQuery = @"
CREATE TABLE [dbo].[sp_lock2_{0}](
	[idx]                       [bigint] IDENTITY(1,1) NOT NULL,
	[probe_time]                [datetime] NULL,
	[hh_mm_ss]                  [varchar](10) NULL,
	[wait_sec]                  [bigint] NULL,
	[locktree]                  [varchar](1000) NULL,
	[spid]                      [int] NULL,
	[kpid]                      [int] NULL,
	[blocked]                   [int] NULL,
	[waittype]                   [varbinary](10) NULL,
	[waittime]                   [bigint] NULL,
	[lastwaittype]              [varchar](100) NULL,
	[waitresource]              [varchar](256) NULL,
	[dbid]                       [int] NULL,
	[uid]                       [int] NULL,
	[cpu]                       [bigint] NULL,
	[physical_io]                [bigint] NULL,
	[memusage]                     [bigint] NULL,
	[login_time]                [datetime] NULL,
	[last_batch]                [datetime] NULL,
	[ecid]                      [int] NULL,
	[open_tran]                  [int] NULL,
	[status]                     [varchar](100) NULL,
	[sid]                        [varbinary](200) NULL,
	[hostname]                  [varchar](256) NULL,
	[program_name]               [varchar](256) NULL,
	[hostprocess]               [varchar](256) NULL,
	[cmd]                         [varchar](256) NULL,
	[nt_domain]                 [varchar](256) NULL,
	[nt_username]                [varchar](256) NULL,
	[net_address]                [varchar](256) NULL,
	[net_library]                [varchar](256) NULL,
	[loginame]                    [varchar](256) NULL,
	[context_info]              [varbinary](128) NULL,
	[sql_handle]                  [binary](20) NULL,
	[stmt_start]                  [int] NULL,
	[stmt_end]                  [int] NULL,
	[request_id]                 [int] NULL,
	[objectid]                  [int] NULL,
	[number]                     [int] NULL,
	[encrypted]                 [varchar](256) NULL,
	[text]                      [nvarchar](1000) NULL
) ON [PRIMARY]
GO

CREATE CLUSTERED INDEX [cl_sp_lock2_{0}] ON [dbo].[sp_lock2_{0}]
(
	[probe_time] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO



declare @dropViewQuery varchar(8000)
declare @createViewQuery varchar(8000)

set @dropViewQuery
=
'
	drop view view_sp_lock2
';

set @createViewQuery 
=
'
	create view view_sp_lock2
	as 
	select 
          [idx]           
        , [probe_time]    
        , [hh_mm_ss]      
        , [wait_sec]      
        , [locktree]      
        , [spid]          
        , [kpid]          
        , [blocked]       
        , [waittype]      
        , [waittime]      
        , [lastwaittype]  
        , [waitresource]  
        , [dbid]          
        , [uid]           
        , [cpu]           
        , [physical_io]   
        , [memusage]      
        , [login_time]    
        , [last_batch]    
        , [ecid]          
        , [open_tran]     
        , [status]        
        , [sid]           
        , [hostname]      
        , [program_name]  
        , [hostprocess]   
        , [cmd]           
        , [nt_domain]     
        , [nt_username]   
        , [net_address]   
        , [net_library]   
        , [loginame]      
        , [context_info]  
        , [sql_handle]    
        , [stmt_start]    
        , [stmt_end]      
        , [request_id]    
        , [objectid]      
        , [number]        
        , [encrypted]     
        , [text]          
	from sp_lock2_{0}
';

begin try
    if exists (select * from INFORMATION_SCHEMA.VIEWS where table_name = 'view_sp_lock2')
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
