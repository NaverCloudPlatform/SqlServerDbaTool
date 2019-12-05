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
using CsLib;

namespace lazylog
{
    partial class PerfmonInit
    {
        Log log = Log.Instance;
        Config config = Config.Instance;
        List<Tuple<string, string>> CounterDetailsFilterInfos = new List<Tuple<string, string>>();

        public void CheckRepository()
        {
            try
            {
                string CounterLevelQuery = string.Empty; 
                
                CounterDetailsFilterInfos.Clear();
                CounterDetailsFilterInfos = LoadCounterDetailsFilterInfoFromRepository();
                
                if (config.GetValue(Category.Perfmon, Key.SkipLoadCounter).ToUpper() == "Y" && config.CounterLoaded)
                {
                    log.Warn("PerfmonInit Step 4 : LoadCounterDetailsAutoUpdatedFromLocalServerToRepository Skipped by LazyLogConfig.txt, Perfmon:::SkipLoadCounter:::Y");
                }
                else
                {
                    if (LoadCounterDetailsAutoUpdatedFromLocalServerToRepository())
                        log.Warn("PerfmonInit Step 4 : LoadCounterDetailsAutoUpdatedFromLocalServerToRepository Success");
                    else
                        log.Warn("PerfmonInit Step 4 : LoadCounterDetailsAutoUpdatedFromLocalServerToRepository Failed");
                }

                int CounterLevel = int.Parse(config.GetValue(Category.Perfmon, Key.CounterLevel).Trim());

                if (CounterLevel == 1)
                    CounterLevelQuery = CounterDetailsAutoUpdatedIsEnabledYQueryLevel1;
                else
                    CounterLevelQuery = CounterDetailsAutoUpdatedIsEnabledYQueryLevel2;
                
                if (Common.QueryExecuter(config.GetConnectionString(InitialCatalog.Repository), CounterLevelQuery))
                    log.Warn("PerfmonInit Step 5 : CounterLevelEnableQuery Success");
                else
                    log.Warn("PerfmonInit Step 5 : CounterLevelEnableQuery Failed");
                
                if (Common.QueryExecuter(config.GetConnectionString(InitialCatalog.Repository), CounterDetailsInsertQuery))
                    log.Warn("PerfmonInit Step 6 : CounterDetailsInsertQuery Success");
                else
                    log.Warn("PerfmonInit Step 6 : CounterDetailsInsertQuery Failed");

                if (Common.QueryExecuter(config.GetConnectionString(InitialCatalog.Repository), DisplayToIDIfNotExistsCreateQuery))
                    log.Warn("PerfmonInit Step 7 : DisplayToIDIfNotExistsCreateQuery Success");
                else
                    log.Warn("PerfmonInit Step 7 : DisplayToIDIfNotExistsCreateQuery Failed");
            }
            catch (Exception ex)
            {
                log.Error(string.Format("{0}, {1}", ex.Message, ex.StackTrace));
            }
        }
        
        private bool LoadCounterDetailsAutoUpdatedFromLocalServerToRepository()
        {
            bool bReturn = false;
            List<string> FilterTypeIn = new List<string>();
            List<string> FilterTypeNotIn = new List<string>();

            int CollectedCnt = 0;
            string MachineName;
            string ObjectName;
            string CounterName;
            int CounterType;
            int DefaultScale;
            string InstanceName;
            int InstanceIndex;
            string ParentName;
            int ParentObjectId;
            int TimeBaseA;
            int TimeBaseB;

            try
            {
                FilterTypeIn = (
                    from a in CounterDetailsFilterInfos
                    where a.Item1 == "IN"
                    select Convert.ToString(a.Item2)).ToList();

                FilterTypeNotIn = (
                     from a in CounterDetailsFilterInfos
                     where a.Item1 == "NOTIN"
                     select Convert.ToString(a.Item2)).ToList();

                var performanceCounterCategories = PerformanceCounterCategory.GetCategories();

                foreach (var performanceCounterCategory in performanceCounterCategories)
                {
                    try
                    {
                        if (FilterCheck(performanceCounterCategory.CategoryName.ToString(), FilterTypeIn, FilterTypeNotIn)) // category filter check 
                        {
                            var instances = performanceCounterCategory.GetInstanceNames();
                            if (performanceCounterCategory.CategoryType.ToString() != "SingleInstance")
                            {
                                foreach (var instance in instances)
                                {
                                    foreach (var counter in performanceCounterCategory.GetCounters(instance))
                                    {
                                        MachineName = @"\\"+Environment.MachineName;
                                        ObjectName = performanceCounterCategory.CategoryName;
                                        CounterName = counter.CounterName;
                                        CounterType = counter.CounterType.GetHashCode();
                                        DefaultScale = 0;
                                        InstanceName = instance;
                                        InstanceIndex = 0;
                                        ParentName = "";
                                        ParentObjectId = 0;

                                        if (((int)counter.CounterType.GetHashCode() & (int)1048576) > 0)
                                            TimeBaseA = 10000000;
                                        else
                                        {
                                            TimeBaseA = (int)counter.NextSample().SystemFrequency;
                                        }
                                        TimeBaseB = 0;
                                        UpdateInsertCounterDetailsAutoUpdated(MachineName
                                            , ObjectName
                                            , CounterName
                                            , CounterType
                                            , DefaultScale
                                            , InstanceName
                                            , InstanceIndex
                                            , ParentName
                                            , ParentObjectId
                                            , TimeBaseA
                                            , TimeBaseB);
                                        //Thread.Sleep(10);  // bandwidth throttle
                                        CollectedCnt++;
                                    }
                                }
                            }
                            else
                            {
                                foreach (var counter in performanceCounterCategory.GetCounters())
                                {
                                    MachineName = @"\\" + Environment.MachineName;
                                    ObjectName = performanceCounterCategory.CategoryName;
                                    CounterName = counter.CounterName;
                                    CounterType = counter.CounterType.GetHashCode();
                                    DefaultScale = 0;
                                    InstanceName = "";
                                    InstanceIndex = 0;
                                    ParentName = "";
                                    ParentObjectId = 0;

                                    if (((int)counter.CounterType.GetHashCode() & (int)1048576) > 0)
                                        TimeBaseA = 10000000;
                                    else
                                    {
                                        TimeBaseA = (int)counter.NextSample().SystemFrequency;
                                    }
                                    TimeBaseB = 0;
                                    UpdateInsertCounterDetailsAutoUpdated(MachineName
                                        , ObjectName
                                        , CounterName
                                        , CounterType
                                        , DefaultScale
                                        , InstanceName
                                        , InstanceIndex
                                        , ParentName
                                        , ParentObjectId
                                        , TimeBaseA
                                        , TimeBaseB);
                                    //Thread.Sleep(10); // bandwidth throttle
                                    CollectedCnt++;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        log.Error(string.Format("{0}, {1}", ex.Message, ex.StackTrace));
                    }
                }
                
                bReturn = true;
            } 
            catch (Exception ex)
            {
                log.Error(string.Format("{0}, {1}", ex.Message, ex.StackTrace));
                bReturn = false;
            }
            return bReturn; 
        }
        
        private void UpdateInsertCounterDetailsAutoUpdated(
            string MachineName
            , string ObjectName
            , string CounterName
            , int CounterType
            , int DefaultScale
            , string InstanceName
            , int InstanceIndex
            , string ParentName
            , int ParentObjectId
            , int TimeBaseA
            , int TimeBaseB)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(config.GetConnectionString(InitialCatalog.Repository)))
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText =
                            @"
                            set nocount on 
                            set transaction isolation level read uncommitted 
                            if not exists (
	                            select * 
	                            from [dbo].[CounterDetailsAutoUpdated]  with (nolock)
	                            where MachineName = @MachineName
	                            and ObjectName = @ObjectName
	                            and CounterName = @CounterName
	                            and InstanceName = @InstanceName
                            ) 
                            begin
	                            insert into  [dbo].[CounterDetailsAutoUpdated] 
	                            (MachineName, ObjectName, CounterName, CounterType, DefaultScale
	                            , InstanceName, InstanceIndex, ParentName, ParentObjectId, TimeBaseA
	                            , TimeBaseB, IsEnabledYN)
	                            values 
	                            (@MachineName, @ObjectName, @CounterName, @CounterType, @DefaultScale
	                            , @InstanceName, @InstanceIndex, @ParentName, @ParentObjectId, @TimeBaseA
	                            , @TimeBaseB, @IsEnabledYN) option(recompile)
                            end 
                            ";
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add("@MachineName", SqlDbType.VarChar, 100).Value = MachineName;
                        cmd.Parameters.Add("@ObjectName", SqlDbType.VarChar, 100).Value = ObjectName;
                        cmd.Parameters.Add("@CounterName", SqlDbType.VarChar, 100).Value = CounterName;
                        cmd.Parameters.Add("@CounterType", SqlDbType.Int).Value = CounterType;
                        cmd.Parameters.Add("@DefaultScale", SqlDbType.Int).Value = DefaultScale;
                        cmd.Parameters.Add("@InstanceName", SqlDbType.VarChar, 100).Value = InstanceName;
                        cmd.Parameters.Add("@InstanceIndex", SqlDbType.Int).Value = InstanceIndex;
                        cmd.Parameters.Add("@ParentName", SqlDbType.VarChar, 100).Value = ParentName;
                        cmd.Parameters.Add("@ParentObjectId", SqlDbType.Int).Value = ParentObjectId;
                        cmd.Parameters.Add("@TimeBaseA", SqlDbType.Int).Value = TimeBaseA;
                        cmd.Parameters.Add("@TimeBaseB", SqlDbType.Int).Value = TimeBaseB;
                        cmd.Parameters.Add("@IsEnabledYN", SqlDbType.VarChar, 1).Value = "N";
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



        private bool FilterCheck(string counterCategory, List<string> FilterTypeIn, List<string> FilterTypeNotIn)
        {
            bool returnValue = false;
            foreach (var a in FilterTypeIn)
            {
                if (counterCategory.ToUpper().Contains(a.ToUpper()))
                {
                    returnValue = true;
                    break;
                }
                else
                    returnValue = false;
            }

            if (returnValue)
            {
                foreach (var a in FilterTypeNotIn)
                {
                    if (counterCategory.ToUpper().Contains(a.ToUpper()))
                    {
                        returnValue = false;
                        break;
                    }
                    else
                        returnValue = true;
                }
            }
            return returnValue;
        }

        private List<Tuple<string,string>> LoadCounterDetailsFilterInfoFromRepository()
        {
            List<Tuple<string, string>> CounterDetailsFilterInfos = new List<Tuple<string, string>>();
            try
            {
                
                using (SqlConnection conn = new SqlConnection(config.GetConnectionString(InitialCatalog.Repository)))
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = @"select FilterType, ObjectName from [dbo].[CounterDetailsFilterInfo]";
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            CounterDetailsFilterInfos.Add(
                                new Tuple<string, string>(
                                    config.DatabaseValue<string>(reader["FilterType"])
                                    , config.DatabaseValue<string>(reader["ObjectName"])
                                ));
                        }
                    }
                    conn.Close();
                }
                log.Warn("PerfmonInit Step 3 : Load CounterDetailsFilterInfo Success");
                
            }
            catch (Exception ex)
            {
                log.Error(string.Format("{0}, {1}", ex.Message, ex.StackTrace));
            }
            return CounterDetailsFilterInfos;
        }



        #region CounterDetailsAutoUpdatedIsEnabledYQueryLevel2
        private string CounterDetailsAutoUpdatedIsEnabledYQueryLevel2 =
@"
update  [dbo].CounterDetailsAutoUpdated set IsEnabledYN ='N' 
go

update  [dbo].CounterDetailsAutoUpdated set IsEnabledYN ='Y' 
-- select * from [dbo].CounterDetailsAutoUpdated
where (countername like '%Processor Time%' and instancename like '%total%') option (recompile)
go

update  [dbo].CounterDetailsAutoUpdated set IsEnabledYN ='Y' 
-- select * from [dbo].CounterDetailsAutoUpdated
where (countername like '%Privilege%' and instancename like '%total%') option (recompile)
go

update  [dbo].CounterDetailsAutoUpdated set IsEnabledYN ='Y' 
-- select * from [dbo].CounterDetailsAutoUpdated
where (countername like '%Processor Queue Length%') option (recompile)
go

update  [dbo].CounterDetailsAutoUpdated set IsEnabledYN ='Y' 
-- select * from [dbo].CounterDetailsAutoUpdated
where (countername like '%Context Switches/sec%') option (recompile)
go

update  [dbo].CounterDetailsAutoUpdated set IsEnabledYN ='Y' 
-- select * from [dbo].CounterDetailsAutoUpdated
where countername like '%Avg. Disk sec/Read%' and InstanceName <> '_Total' option (recompile)
go

update  [dbo].CounterDetailsAutoUpdated set IsEnabledYN ='Y' 
-- select * from [dbo].CounterDetailsAutoUpdated
where countername like '%Avg. Disk Sec/Write%' and InstanceName <> '_Total' option (recompile)
go

update  [dbo].CounterDetailsAutoUpdated set IsEnabledYN ='Y' 
-- select * from [dbo].CounterDetailsAutoUpdated
where countername like '%Avg. Disk Queue Length%' and InstanceName <> '_Total' option (recompile)
go

update  [dbo].CounterDetailsAutoUpdated set IsEnabledYN ='Y' 
-- select * from [dbo].CounterDetailsAutoUpdated
where countername like '%Disk Read Bytes/sec%' and InstanceName <> '_Total' option (recompile)
go

update  [dbo].CounterDetailsAutoUpdated set IsEnabledYN ='Y' 
-- select * from [dbo].CounterDetailsAutoUpdated
where countername like '%Disk Write Bytes/sec%' and InstanceName <> '_Total' option (recompile)
go

update  [dbo].CounterDetailsAutoUpdated set IsEnabledYN ='Y' 
-- select * from [dbo].CounterDetailsAutoUpdated
where countername like '%Free Megabytes%' and InstanceName <> '_Total' option (recompile)
go

update  [dbo].CounterDetailsAutoUpdated set IsEnabledYN ='Y' 
-- select * from [dbo].CounterDetailsAutoUpdated
where objectName ='Memory' and countername like '%Available Mbytes%' option (recompile)
go

update  [dbo].CounterDetailsAutoUpdated set IsEnabledYN ='Y' 
-- select * from [dbo].CounterDetailsAutoUpdated
where objectname like '%interface%' and countername = 'Bytes Sent/sec' option (recompile)
go

update  [dbo].CounterDetailsAutoUpdated set IsEnabledYN ='Y' 
-- select * from [dbo].CounterDetailsAutoUpdated
where objectname like '%interface%' and countername = 'Bytes Received/sec' option (recompile)
go

update  [dbo].CounterDetailsAutoUpdated set IsEnabledYN ='Y'
-- select * from [dbo].CounterDetailsAutoUpdated
where countername like '%Packets/sec%' option (recompile)
go

update  [dbo].CounterDetailsAutoUpdated set IsEnabledYN ='Y' 
-- select * from [dbo].CounterDetailsAutoUpdated
where countername like '%Lazy writes/sec%' option (recompile)
go

update  [dbo].CounterDetailsAutoUpdated set IsEnabledYN ='Y' 
-- select * from [dbo].CounterDetailsAutoUpdated
where countername like '%Free Memory%' option (recompile)
go

update  [dbo].CounterDetailsAutoUpdated set IsEnabledYN ='Y' 
-- select * from [dbo].CounterDetailsAutoUpdated
where countername like '%Page Life Expectancy%' option (recompile)
go

update  [dbo].CounterDetailsAutoUpdated set IsEnabledYN ='Y' 
-- select * from [dbo].CounterDetailsAutoUpdated
where countername like '%Memory Grants Pending%' option (recompile)
go

update  [dbo].CounterDetailsAutoUpdated set IsEnabledYN ='Y' 
-- select * from [dbo].CounterDetailsAutoUpdated
where countername like '%Batch Requests/sec%' option (recompile)
go

update  [dbo].CounterDetailsAutoUpdated set IsEnabledYN ='Y' 
-- select * from [dbo].CounterDetailsAutoUpdated
where countername like '%Page lookups/sec%' option (recompile)
go

update  [dbo].CounterDetailsAutoUpdated set IsEnabledYN ='Y' 
-- select * from [dbo].CounterDetailsAutoUpdated
where ObjectName like '%:Buffer Manager%' and countername like 'Page reads/sec' option (recompile)
go

update  [dbo].CounterDetailsAutoUpdated set IsEnabledYN ='Y' 
-- select * from [dbo].CounterDetailsAutoUpdated
where ObjectName like '%:Buffer Manager%' and countername like 'Page writes/sec' option (recompile)
go

update  [dbo].CounterDetailsAutoUpdated set IsEnabledYN ='Y' 
-- select * from [dbo].CounterDetailsAutoUpdated
where countername like '%Total Latch Wait Time (ms)%' option (recompile)
go

update  [dbo].CounterDetailsAutoUpdated set IsEnabledYN ='Y' 
-- select * from [dbo].CounterDetailsAutoUpdated
where countername like '%Lock Wait Time %' and instanceName ='_Total' option (recompile)
go

update  [dbo].CounterDetailsAutoUpdated set IsEnabledYN ='Y' 
-- select * from [dbo].CounterDetailsAutoUpdated
where countername like '%SQL Compilations/sec%' option (recompile)
go

update  [dbo].CounterDetailsAutoUpdated set IsEnabledYN ='Y' 
-- select * from [dbo].CounterDetailsAutoUpdated
where countername like '%SQL Re-Compilations/sec%' option (recompile)
go

update  [dbo].CounterDetailsAutoUpdated set IsEnabledYN ='Y' 
-- select * from [dbo].CounterDetailsAutoUpdated
where countername like '%User Connections%' option (recompile)
go
";
        #endregion
        
        #region CounterDetailsAutoUpdatedIsEnabledYQueryLevel1
        private string CounterDetailsAutoUpdatedIsEnabledYQueryLevel1 =
@"

update  [dbo].CounterDetailsAutoUpdated set IsEnabledYN ='N' 
go

update  [dbo].CounterDetailsAutoUpdated set IsEnabledYN ='Y' 
-- select * from [dbo].CounterDetailsAutoUpdated
where (countername like '%Processor Time%' and instancename like '%total%') option (recompile)
go

";
        #endregion

        #region CounterDetailsInsertQuery
        private string CounterDetailsInsertQuery =
@"
delete a
from [CounterDetails] a 
	left join [CounterDetailsAutoUpdated] b
	on a.MachineName = b.[MachineName]
	and a.[ObjectName] = b.[ObjectName]
	and a.[CounterName] = b.[CounterName]
	and a.InstanceName = b.InstanceName 
	and b.IsEnabledYN ='Y'
where 
	b.[MachineName] is null 
go


insert into [CounterDetails]
	([MachineName], [ObjectName], [CounterName], [CounterType], [DefaultScale], [InstanceName], [InstanceIndex], [ParentName], [ParentObjectId], [TimeBaseA], [TimeBaseB])
select 
	a.[MachineName], a.[ObjectName], a.[CounterName], a.[CounterType], a.[DefaultScale], a.[InstanceName], a.[InstanceIndex], a.[ParentName], a.[ParentObjectId], a.[TimeBaseA], a.[TimeBaseB]
from [dbo].[CounterDetailsAutoUpdated] a
	left outer join [CounterDetails] b 
	on a.MachineName = b.[MachineName]
	and a.[ObjectName] = b.[ObjectName]
	and a.[CounterName] = b.[CounterName]
	and a.InstanceName = b.InstanceName 
where 
	b.[MachineName] is null 
	and a.[IsEnabledYN] = 'Y'
";
        #endregion

        #region DisplayToIDIfNotExistsCreateQuery
        private string DisplayToIDIfNotExistsCreateQuery =
@"
if not exists(select * from [DisplayToID] with (nolock) where DisplayString = 'BaselineCollect')
begin 
	insert into [DisplayToIDOrigin] (guid, RunID, DisplayString, LogStartTime, LogStopTime, NumberOfRecords, MinutesToUTC, TimeZoneName )
	values (newid(), 0, 'BaselineCollect', convert(varchar(20), getdate(),120), convert(varchar(20), getdate(),120), 0, -540, 'Korea Standard Time') option(recompile)
end 
select cast(GUID as varchar(100)) as guid, RunID, DisplayString, LogStartTime, LogStopTime, NumberOfRecords, MinutesToUTC, TimeZoneName
from [DisplayToIDOrigin] with (nolock)
where DisplayString = 'BaselineCollect' option(recompile)
";
        #endregion
    }
}
