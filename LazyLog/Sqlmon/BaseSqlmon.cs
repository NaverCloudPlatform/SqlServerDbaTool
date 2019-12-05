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
    abstract class BaseSqlmon<T>
    {
        protected List<T> buffer = new List<T>();
        public BaseSqlmon()
        {
            Initialization();
        }

        internal abstract void Start();
        
        internal virtual void Stop()
        {
            IsRunning = false;
        }

        protected virtual void Purge()
        {
            // 메모리 결과셋을 지운다. 
            buffer.Clear();

            // 데이터베이스 테이블이 슬라이드 타임이 되었는지 확인하고 슬라이드 시킨다. 
            // 현재 테이블이 있는지 본다. 
            if (!GetCurrentTableName(BaseTableName, out CurrentTableName))
                TableSlide(BaseTableName, TableGenQuery, DropTableQuery, RemainTableCnt, out CurrentTableName); // 생성, 삭제, 테이블이름 셋팅

            if (TableSlideCheck(BaseTableName, CurrentTableName, TableSlideMin))  // 시간 지나면 슬라이드 
                TableSlide(BaseTableName, TableGenQuery, DropTableQuery, RemainTableCnt, out CurrentTableName);
        }

        protected abstract void GetData();

        protected virtual void LocalSaveData(string CurrentTableName)
        {
            // 로컬에 저장한다. 
            log.Warn(string.Format("{0} LocalSaveData started", CurrentTableName));
            try
            {
                var objBulk = new BulkUploadToSql<T>()
                {
                    InternalStore = buffer,
                    TableName = CurrentTableName,
                    CommitBatchSize = 1000,
                    ConnectionString = config.GetConnectionString(InitialCatalog.Repository)
                };
                objBulk.Commit();
            }
            catch (Exception e)
            {
                log.Error(string.Format("{0}, {1}", e.Message, e.StackTrace));
            }
            finally
            {
                buffer.Clear();
                InternalRepositorySaveCnt++;
            }
        }

        protected abstract void RemoteSaveData();

        protected virtual bool GetCurrentTableName(string BaseTableName, out string CurrentTableName)
        {
            bool bReturn = false;
            string TableName = string.Empty;
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
where TABLE_NAME like @BaseTableName+'[_]________[_]______' 
order by TABLE_NAME desc 
option (recompile)
";
                        cmd.Parameters.Add("@BaseTableName", SqlDbType.NVarChar, 100).Value = BaseTableName;

                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                TableName = config.DatabaseValue<string>(reader["TABLE_NAME"]);
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
                TableName = "";
            }

            CurrentTableName = TableName;
            return bReturn;
        }

        protected virtual bool ExistTable(string TableName)
        {
            bool bReturn = false;
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
where TABLE_NAME = @TableName
order by TABLE_NAME desc ";
                        cmd.Parameters.Add("@TableName", SqlDbType.NVarChar, 100).Value = TableName;

                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            bReturn = true;
                        }
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                log.Error(string.Format("{0}, {1}", ex.Message, ex.StackTrace));
            }
            return bReturn;
        }

        protected virtual void TableSlide(string BaseTableName, string TableGenQuery, string DropTableQuery, int RemainTableCnt, out string CurrentTableName)
        {
            log.Warn(string.Format("{0} slide started", BaseTableName));

            try
            {
                // 테이블 생성 
                Common.QueryExecuter(
                    config.GetConnectionString(InitialCatalog.Repository)
                    , string.Format(TableGenQuery, DateTime.Now.ToString("yyyyMMdd_HHmmss")));

                // 테이블 삭제
                Common.QueryExecuter(
                    config.GetConnectionString(InitialCatalog.Repository)
                    , string.Format(DropTableQuery, RemainTableCnt, BaseTableName)); // 2개만 남겨라 
            }
            catch (Exception ex)
            {
                log.Error(string.Format("{0}, {1}", ex.Message, ex.StackTrace));
            }
            finally
            {
                // 현재 테이블 셋팅
                GetCurrentTableName(BaseTableName, out CurrentTableName);
            }
        }

        protected virtual bool TableSlideCheck(string BaseTableName, string CurrentTableName, int TableSlideMin)
        {
            // 시간이 넘었으면 슬라이드 한다. 몇개로 할건지 설정에서 읽어서 하면 좋지....
            log.Warn(string.Format("{0} slide check started", BaseTableName));
            bool bReturn = false;
            try
            {
                string CurrentTableNameTimeString = CurrentTableName.Substring(BaseTableName.Length + 1, 15);
                DateTime CurrentTableNameTime = DateTime.ParseExact(CurrentTableNameTimeString, "yyyyMMdd_HHmmss", System.Globalization.CultureInfo.InvariantCulture);
                DateTime CurrentTime = DateTime.Now;
                TimeSpan span = CurrentTime.Subtract(CurrentTableNameTime);
                if (span.TotalMinutes > TableSlideMin) // 일단 1분으로 셋팅 
                    bReturn = true;
            }
            catch (Exception ex)
            {
                log.Error(string.Format("{0}, {1}", ex.Message, ex.StackTrace));
            }
            return bReturn;
        }
        
        protected abstract void Initialization();
        
        protected Log log = Log.Instance;
        protected Config config = Config.Instance;
        protected int InternalRepositorySaveCnt = 0;
        protected int WebApiIntervalModValue = 0;
        protected bool IsRunning = false;
        protected string BaseTableName = string.Empty;
        protected string CurrentTableName = string.Empty;
        protected string CurrentViewName = string.Empty;
        protected DateTime ProbeTime = DateTime.Now; 
        protected string DropTableQuery = @"
declare @PartitionMaxCount int = {0}
declare @BaseTableName nvarchar(100) = N'{1}'

declare @TableList table
(idx int identity(1,1)
,table_name nvarchar(100)
,active_table_yn nvarchar(1)
)

insert into @TableList (table_name)
select table_name 
from INFORMATION_SCHEMA.TABLES
where TABLE_NAME like @BaseTableName+'[_]________[_]______' 
order by TABLE_NAME desc option (recompile)

update @TableList set active_table_yn ='Y'
where idx <= @PartitionMaxCount option (recompile)

update @TableList set active_table_yn ='N'
where active_table_yn is null option (recompile)


declare @maxIdx int = 0, @sql nvarchar(max) = cast('' as nvarchar(max)) , @currentTableName nvarchar(100) = ''

select @maxIdx = max(idx) from @TableList option (recompile)
while (@maxIdx > 0)
begin 
	select @currentTableName = table_name from @TableList where idx = @maxIdx and active_table_yn = 'N' option (recompile)
	if @@rowcount  = 0 break; 
	set @sql = 'drop table ' + @currentTableName 
	exec (@sql)
	--print @sql 
	set @maxIdx = @maxIdx - 1 
end 
";
        //Initialization 에서 정의해야 함 
        protected string GetDataQuery = string.Empty;
        
        protected string TableGenQuery = string.Empty;
        protected int TableSlideMin = 0;
        protected int RunIntervalSec = 0;
        protected int RemainTableCnt = 1; 
    }
}
