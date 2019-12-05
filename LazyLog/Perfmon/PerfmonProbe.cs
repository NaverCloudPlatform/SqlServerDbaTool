using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogClient;
using System.Threading;
using System.Diagnostics;
using System.IO;
using System.Timers;
using System.Data.SqlClient;
using System.Data;
using System.Text.RegularExpressions;
using CsLib;
// 64bit odbc 를 추가하고 perfmon 에서 보면 보인다. 

namespace lazylog
{
    class PerfmonProbe
    {
        Log log = Log.Instance;
        Config config = Config.Instance;
        List<PerfmonData> buffer;
        int CurrentPorobeCnt = 0;
        readonly Object Lock = new object();

        string CounterDataOriginCurrentTableName = string.Empty; 

        Dictionary<Tuple<string, string, string, int>, PerformanceCounter> counters =
            new Dictionary<Tuple<string, string, string, int>, PerformanceCounter>();

        System.Timers.Timer timer;
        System.Timers.Timer PartitionSlidingTimer;

        int WebApiIntervalModValue = 0;
        int InternalRepositorySaveCnt = 0; 

        public PerfmonProbe()
        {
            buffer = new List<PerfmonData>();
            WebApiIntervalModValue = int.Parse(config.GetValue(Category.Perfmon, Key.WebApiIntervalModValue));
            if (!GetCounterData_CurrentTableName(out CounterDataOriginCurrentTableName))
                CounterDataOriginTableSwitchAndCreateView(); // CounterDataOrigin_XXXXXXXX_XXXX 테이블 생성
        }
        
        public void StartTimer()
        {
            if (int.Parse(config.GetValue(Category.Perfmon, Key.ProbeIntervalSec).Trim()) > 0)
            {
                log.Warn("perfmon started");
                GenerateCounter();
                timer = new System.Timers.Timer();
                timer.Interval = int.Parse(config.GetValue(Category.Perfmon, Key.ProbeIntervalSec).Trim()) * 1000;
                timer.Elapsed += timer_Elapsed;
                timer.Start();
                CurrentPorobeCnt = 0;

                PartitionSlidingTimer = new System.Timers.Timer();
                PartitionSlidingTimer.Interval = 30 * 1000; // 60초마다 검사해서 파티션 할지 말지 결정 
                PartitionSlidingTimer.Elapsed += PartitionSlidingTimer_Elapsed;
                PartitionSlidingTimer.Start();
            }
            else
            {
                log.Warn("perfmon can't start because ProbeIntervalSec is 0");
            }
        }
        
        public void StopTimer()
        {
            try
            {
                if (timer != null)
                {
                    timer.Elapsed -= timer_Elapsed;
                    timer.Stop();
                    CurrentPorobeCnt = 0;
                }
                log.Warn("perfmon stopped");
            }
            catch (Exception ex)
            {
                log.Error(string.Format("{0}, {1}", ex.Message, ex.StackTrace));
            }
        }

        private void CounterDataOriginTableSwitchAndCreateView()
        {
            lock (Lock)
            {
                log.Warn("slide");
                try
                {
                    // 현재시간으로 테이블을 만들어 현재 테이블을 셋팅함 
                    Common.QueryExecuter(config.GetConnectionString(InitialCatalog.Repository)
                        , string.Format(CounterDataOrigin_CurrentTimeCreateQuery, DateTime.Now.ToString("yyyyMMdd_HHmmss")));

                    // 현재 테이블 셋팅
                    GetCounterData_CurrentTableName(out CounterDataOriginCurrentTableName);

                    // 과거 테이블이 있는지 보고 있으면, 
                    // 마지막 값을 복사해 넣어줌
                    // 과거 테이블에서 마지막 값은 지워줌 
                    Common.QueryExecuter(config.GetConnectionString(InitialCatalog.Repository)
                        , CounterDataOrigin_LastDataCopy);

                    // CounterDataOrigin 모든 테이블 리스트를 구함 
                    // CounterDataRemainTableCnt 값을 보고 시간이 지나면 지나면 지워줌 
                    // 2-10 이내로 설정하자 
                    Common.QueryExecuter(config.GetConnectionString(InitialCatalog.Repository)
                        , string.Format(
                            CounterDataOrigin_RemovePartitionAndCreateView
                            , config.GetValue(Category.Perfmon, Key.CounterDataRemainTableCnt)));
                }
                catch (Exception ex)
                {
                    log.Error(string.Format("{0}, {1}", ex.Message, ex.StackTrace));
                }
            }
        }


        private bool GetCounterData_CurrentTableName(out string CurrentTableName)
        {
            bool bReturn = false;
            string CounterDataOrigin_CurrentTableName = string.Empty; 
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
where TABLE_NAME like 'CounterDataOrigin[_]%' 
order by TABLE_NAME desc ";

                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                CounterDataOrigin_CurrentTableName = config.DatabaseValue<string>(reader["TABLE_NAME"]);
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
                CounterDataOrigin_CurrentTableName = "";
            }
            CurrentTableName = CounterDataOrigin_CurrentTableName; 
            return bReturn;
        }

        private void GetCounterDataInfo(out string GUID, out int RecordIndex, out string CounterDateTime)
        {
            GUID = string.Empty;
            RecordIndex = 0;
            CounterDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            try
            {
                
                using (SqlConnection conn = new SqlConnection(config.GetConnectionString(InitialCatalog.Repository)))
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = string.Format(
@"
set nocount on 
declare @GUID nvarchar(100) 
declare @RecordIndex int 
select @GUID = GUID from [dbo].[DisplayToID]
select @RecordIndex = isnull(max(RecordIndex), 0) + 1 from {0}
select @GUID GUID, @RecordIndex RecordIndex
"
, CounterDataOriginCurrentTableName);
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            GUID = config.DatabaseValue<string>(reader["GUID"]);
                            RecordIndex = config.DatabaseValue<int>(reader["RecordIndex"]);
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


        private void PartitionSlidingTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (CounterDataOriginCurrentTableName.Trim().Length > 0)
            {
                if(IsPartitionSlidingTime(CounterDataOriginCurrentTableName.Trim()))
                    CounterDataOriginTableSwitchAndCreateView();
            }
        }

        private bool IsPartitionSlidingTime(string CounterDataOriginCurrentTableName)
        {
            bool bReturn = false;
            try
            {
                string CounterDataOriginTimeString = CounterDataOriginCurrentTableName.Substring(18, 15);
                DateTime CounterDataOriginTime = DateTime.ParseExact(CounterDataOriginTimeString, "yyyyMMdd_HHmmss", System.Globalization.CultureInfo.InvariantCulture);
                DateTime CurrentTime = DateTime.Now;
                TimeSpan span = CurrentTime.Subtract(CounterDataOriginTime);
                if (span.TotalMinutes > int.Parse(config.GetValue(Category.Perfmon, Key.TableSlideMin)))
                    bReturn = true;
            }
            catch (Exception ex)
            {
                log.Error(string.Format("{0}, {1}", ex.Message, ex.StackTrace));
            }
            return bReturn; 
        }

        private void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            string GUID;
            int RecordIndex;
            string CounterDateTime;


            lock(Lock)
            {
                if (CounterDataOriginCurrentTableName.Trim().Length > 0)
                {

                    GetCounterDataInfo(out GUID, out RecordIndex, out CounterDateTime);
#if (DEBUG)
                    Console.WriteLine(RecordIndex);
#endif

                    try
                    {
                        // 모든 카운터 측정 시작 
                        foreach (var a in counters)
                        {
                            try
                            {
                                #region PerfCalc 
                                float nextValue = a.Value.NextValue();
                                long RawValue = a.Value.NextSample().RawValue;
                                long TimeStamp = a.Value.NextSample().TimeStamp;
                                long TimeStamp100nSec = a.Value.NextSample().TimeStamp100nSec;
                                long BaseValue = a.Value.NextSample().BaseValue;
                                long CounterFrequency = a.Value.NextSample().CounterFrequency;
                                long CounterTimeStamp = a.Value.NextSample().CounterTimeStamp;

                                ulong FirstValue;
                                ulong SecondValue;

                                if (((int)a.Value.CounterType.GetHashCode() == (int)65536)
                                    || ((int)a.Value.CounterType.GetHashCode() == (int)65792))
                                {
                                    FirstValue = (ulong)RawValue;
                                    SecondValue = (ulong)0;
                                }
                                else if (((int)a.Value.CounterType.GetHashCode() & (int)272696320) == (int)272696320)
                                {
                                    FirstValue = (ulong)RawValue;
                                    SecondValue = (ulong)TimeStamp;
                                }
                                else if (((int)a.Value.CounterType.GetHashCode() & (int)132096) == (int)132096)
                                {
                                    FirstValue = (ulong)RawValue;
                                    SecondValue = (ulong)BaseValue;
                                }
                                else if (((int)a.Value.CounterType.GetHashCode() & (int)537003264) == (int)537003264)
                                {
                                    FirstValue = (ulong)BaseValue * ((ulong)nextValue / (ulong)100);
                                    SecondValue = (ulong)BaseValue;
                                }
                                else
                                {
                                    FirstValue = (ulong)RawValue;
                                    SecondValue = (ulong)TimeStamp100nSec;
                                }
                                #endregion

                                if (CurrentPorobeCnt > 1)
                                {
                                    DataBuffering(new PerfmonData
                                    {
                                        GUID = GUID,
                                        CounterID = a.Key.Item4,  // CounterID,
                                        RecordIndex = RecordIndex,
                                        CounterDateTime = CounterDateTime,
                                        CounterValue = nextValue,
                                        FirstValueA = (int)((ulong)FirstValue & 4294967295),
                                        FirstValueB = (int)((ulong)FirstValue >> 32),
                                        SecondValueA = (int)((ulong)SecondValue & 4294967295),
                                        SecondValueB = (int)((ulong)SecondValue >> 32),
                                        MultiCount = 1
                                    });
                                }
                            }
                            catch(Exception)
                            { }
                        }

                        CurrentPorobeCnt++;

                        if (CurrentPorobeCnt > 1)
                            CurrentPorobeCnt = 2;

                        // internal repository 에 넣기
                        Save();
                        
                        // webapi 에 쏘기 
                        if (InternalRepositorySaveCnt % WebApiIntervalModValue == 0)
                        {
                            RemoteSaveData();
                            InternalRepositorySaveCnt = 0;
                        }
                        
                    }
                    catch (Exception ex)
                    {
                        log.Error(string.Format("{0}, {1}", ex.Message, ex.StackTrace));
                    }
                }
            }
        }

        private void RemoteSaveData()
        {

            string SenderType = config.GetValue(Category.Sender, Key.Type);


            if (!SenderType.Equals("No", StringComparison.OrdinalIgnoreCase))
            {
                PerfmonSender sender;
                if (SenderType.Equals("A", StringComparison.OrdinalIgnoreCase))
                    sender = new PerfmonSenderTypeA(CounterDataOriginCurrentTableName);
                else
                    sender = new PerfmonSenderTypeB(CounterDataOriginCurrentTableName);
                sender.SendData();
                log.Warn("PerfmonWebApiSend");
            }
        }


        private void GenerateCounterTestSet()
        {
            counters.Clear();
            Console.WriteLine("GenerateCounter Start");
            counters.Add(new Tuple<string, string, string, int>("Processor", "% Processor Time", "_Total", 1)
                    , new PerformanceCounter("Processor", "% Processor Time", "_Total"));
            Console.WriteLine("GenerateCounter End");
            // PerformanceCounter(string categoryName, string counterName, string instanceName);
        }

        private void GenerateCounter()
        {
            log.Warn("PerfmonProbe Step 1 : GenerateCounter Start");
            
            counters.Clear();
            try
            {
                using (SqlConnection conn = new SqlConnection(config.GetConnectionString(InitialCatalog.Repository)))
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = @"SELECT [ObjectName] ,[CounterName] ,[InstanceName], [CounterID] FROM [dbo].[CounterDetails]";
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            string ObjectName = string.Empty;
                            string CounterName = string.Empty;
                            string InstanceName = string.Empty;
                            try
                            {
                                ObjectName = config.DatabaseValue<string>(reader["ObjectName"]);
                                CounterName = config.DatabaseValue<string>(reader["CounterName"]);
                                InstanceName = config.DatabaseValue<string>(reader["InstanceName"]);

                                counters.Add(
                                    new Tuple<string, string, string, int>(
                                        config.DatabaseValue<string>(reader["ObjectName"])
                                        , config.DatabaseValue<string>(reader["CounterName"])
                                        , config.DatabaseValue<string>(reader["InstanceName"])
                                        , config.DatabaseValue<int>(reader["CounterID"]))
                                    , new PerformanceCounter(ObjectName
                                        , CounterName
                                        , InstanceName
                                        )
                                    );
                            }
                            catch (Exception ex)
                            {
                                log.Error(string.Format("PerfmonProbe Step 1 : {0} : {1},{2},{3}", ex.Message, ObjectName, CounterName, InstanceName));
                            }

                        }
                    }
                    conn.Close();
                }
                log.Warn("PerfmonProbe Step 1 : GenerateCounter End");
                
            }
            catch (Exception ex)
            {
                log.Error(string.Format("{0}, {1}", ex.Message, ex.StackTrace));
            }
        }
        
        private void DataBuffering(PerfmonData perfmonData)
        {
            lock (Lock)
            {
                buffer.Add(perfmonData);
            }
        }

        private void Save()
        {
            lock (Lock)
            {
                try
                {
                    var objBulk = new BulkUploadToSql<PerfmonData>()
                    {
                        InternalStore = buffer,
                        TableName = CounterDataOriginCurrentTableName,
                        CommitBatchSize = 1000,
                        ConnectionString = config.GetConnectionString(InitialCatalog.Repository)
                    };
                    objBulk.Commit();
                }
                catch (Exception ex)
                {
                    log.Error(string.Format("{0}, {1}", ex.Message, ex.StackTrace));
                }
                finally
                {
                    buffer.Clear();
                    InternalRepositorySaveCnt++;
                }
            }
        }
        

        private string CounterDataOrigin_CurrentTimeCreateQuery =
@"
CREATE TABLE [dbo].[CounterDataOrigin_{0}](
	[GUID]              [varchar](50) NOT NULL,
	[CounterID]         [int] NOT NULL,
	[RecordIndex]       [int] NOT NULL,
	[CounterDateTime]   [char](24) NOT NULL,
	[CounterValue]      [float] NOT NULL,
	[FirstValueA]       [int] NULL,
	[FirstValueB]       [int] NULL,
	[SecondValueA]      [int] NULL,
	[SecondValueB]      [int] NULL,
	[MultiCount]        [int] NULL
) ON [PRIMARY]
GO

CREATE CLUSTERED INDEX [cl_CounterDataOrigin] ON [dbo].[CounterDataOrigin_{0}]
(
	[CounterDateTime] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [nc_CounterDataOrigin_01] ON [dbo].[CounterDataOrigin_{0}]
(
	[RecordIndex] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [nc_CounterDataOrigin_02] ON [dbo].[CounterDataOrigin_{0}]
(
	[CounterDateTime] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [nc_counterDataOrigin_03] ON [dbo].[CounterDataOrigin_{0}]
(
	[RecordIndex] ASC,
	[CounterID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO



declare @dropViewQuery varchar(8000)
declare @createViewQuery varchar(8000)

set @dropViewQuery
=
'
	drop view view_CounterDataOrigin
';

set @createViewQuery 
=
'
	create view view_CounterDataOrigin
	as 
	select 
		  [GUID]            
		, [CounterID]       
		, [RecordIndex]     
		, [CounterDateTime] 
		, [CounterValue]    
		, [FirstValueA]     
		, [FirstValueB]     
		, [SecondValueA]    
		, [SecondValueB]    
		, [MultiCount]      
	from CounterDataOrigin_{0}
';

begin try
    if exists (select * from INFORMATION_SCHEMA.VIEWS where table_name = 'view_CounterDataOrigin')
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
"
;
        
        string CounterDataOrigin_LastDataCopy = @"
declare @CURRENT_TABLE_NAME varchar(100), @OLD_TABLE_NAME varchar(100)

select top 1 @CURRENT_TABLE_NAME = TABLE_NAME
from INFORMATION_SCHEMA.TABLES
where TABLE_NAME like 'CounterDataOrigin[_]%' 
order by TABLE_NAME desc option (recompile)

select top 1 @OLD_TABLE_NAME = TABLE_NAME
from INFORMATION_SCHEMA.TABLES
where TABLE_NAME like 'CounterDataOrigin[_]%' 
and TABLE_NAME <> @CURRENT_TABLE_NAME
order by TABLE_NAME desc option (recompile)

if (@OLD_TABLE_NAME is not null)
begin 
	select 'old table exists!'
	declare @query varchar(8000) = '
        declare @maxRecordindex int 
        select top 1 @maxRecordindex = RecordIndex from [dbo].'+@OLD_TABLE_NAME+' order by RecordIndex desc option (recompile)

        insert into [dbo].'+@CURRENT_TABLE_NAME+'
        select * from [dbo].'+@OLD_TABLE_NAME+'
        where RecordIndex = @maxRecordindex option (recompile)

        delete '+@OLD_TABLE_NAME+' where RecordIndex = @maxRecordindex option (recompile)
        '
	exec (@query)
end 
else 
begin 
	select 'skip' option (recompile)
end ";


        string CounterDataOrigin_RemovePartitionAndCreateView =
            @"
declare @CounterDataRemainTableCnt int = {0}


if object_id('tempdb..#CounterDataOrigin') is not null
drop table #CounterDataOrigin

create table #CounterDataOrigin
(idx int identity(1,1)
,table_name nvarchar(100)
,active_table_yn nvarchar(1)
)

insert into #CounterDataOrigin (table_name)
select table_name 
from INFORMATION_SCHEMA.TABLES
where TABLE_NAME like 'CounterDataOrigin[_]%'
order by TABLE_NAME desc 

update #CounterDataOrigin set active_table_yn ='Y'
where idx <= @CounterDataRemainTableCnt

update #CounterDataOrigin set active_table_yn ='N'
where active_table_yn is null 

if object_id('tempdb..#DropTargetCounterDataOrigin') is not null
drop table #DropTargetCounterDataOrigin

create table #DropTargetCounterDataOrigin
(idx int identity(1,1)
,table_name nvarchar(100)
)

insert into #DropTargetCounterDataOrigin 
select table_name from #CounterDataOrigin where active_table_yn ='N'


declare @maxIdx int = 0, @sql nvarchar(max) = cast('' as nvarchar(max)) , @currentTableName nvarchar(100) = ''
select @maxIdx = max(idx) from #DropTargetCounterDataOrigin
while (@maxIdx > 0)
begin 
	select @currentTableName = table_name from #DropTargetCounterDataOrigin where idx = @maxIdx
	set @sql = 'drop table ' + @currentTableName 
	exec (@sql)
	set @maxIdx = @maxIdx - 1 
end 


-- select * from #CounterDataOrigin 

delete #CounterDataOrigin where active_table_yn = 'N'

begin try
	exec ('drop view [CounterData]')
end try
begin catch 
end catch 

declare @minCounterDataOrign nvarchar (4000) = ''
select @minCounterDataOrign = min (table_name) from #CounterDataOrigin

-- select @minCounterDataOrign

declare @unionallScript nvarchar(max) = cast('' as nvarchar(max))

select @unionallScript = @unionallScript + ' select * from ' + table_name + ' with (nolock) union all '
from #CounterDataOrigin

select @unionallScript = substring (@unionallScript, 1, len(@unionallScript)- 10)

set @sql =
'
create view [dbo].[CounterData]
as 
with CounterDataOriginCte as
(
select min(RecordIndex) - 1 minRecordIndex from dbo.[' + @minCounterDataOrign+ '] with (nolock)
)
, 
unionTable as (
' +
@unionallScript
+ ' 
)
select GUID
, CounterID
, RecordIndex - b.minRecordIndex RecordIndex
, CounterDateTime
, CounterValue
, FirstValueA
, FirstValueB
, SecondValueA
, SecondValueB
, MultiCount
from unionTable a  with (nolock)
cross join CounterDataOriginCte b  with (nolock)
'
exec (@sql)

";

    }


}
