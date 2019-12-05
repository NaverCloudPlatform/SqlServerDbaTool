using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogClient;
using System.Threading;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using CsLib; 

namespace lazylog
{

    //SqlServer:::Version:::2014

    public enum Category {
              Config
            //, Api
            , Repository
            , Perfmon
            // for ncp 
            , CLA
            , DMV
            , GLOBAL
            , NPOT
            , PERFMON
            , Sender
            , Sqlmon
            , SqlServer
            //, WcfServer
            , Encryption
        // for ncp end 
            , Backup
            , Ha
            , ApiGateway
    };
    public enum Key {
        Type
        , DataSource
        , UserId
        , EncryptedPassword
        , InitialCatalog
        , ConnectTimeout
        , DatabaseFilePath
        , LogFilePath
        , SkipLoadCounter
        , ProbeIntervalSec
        , CounterLevel
        , TableSlideMin
        , CounterDataRemainTableCnt
        , PerfmonWebApiCall
        , PerfmonWebApiCallEndPointUrl
        , SqlmonWebApiCallEndPointUrl
        //, AccessKey
        //, SecretKey
        , WebApiIntervalModValue
        , BucketName
        // Encryption
        //, GetCryptionKey
        //, GetCryptionKeyUrl

        , HeartBeatIntervalSec
        , HeartBeatTimeLimitSec
        , ServerName
        // for ncp 
        , AuthInfo
        , CollectIntervalSec
        , CollectPerServerItemSleepMiliSec
        , CounterDetailsThreads
        , hostname
        , ItemName
        , LogTypes
        , ncp_group_no
        , ncp_mbr_no
        , PerfmonReset
        , private_ip
        , URL
//        , Port
        , UseCLAYN
        , UseNPOTYN
        , UserKey
        , UseYN
        // for ncp end 
        , Endpoint

        , FullBackupIntervalSec
        , LogBackupIntervalSec
        , FullBackupHaCompleteCheckYN
        , Path
        , maxtransfersize
        , buffercount
        , compressionYN
        , ObjectStorageServiceUrl
        , ObjectStorageBandWidthThrottleSleepMiliSec
        , PurgeLocalLimitSec
        , PurgeObjectLimitSec
        , servicename // sql server instancename 

        , Version // sql server version 

        // Sqlmon 설정 
        , dm_exec_query_stats_ProbeIntervalSec
        , dm_exec_query_stats_TableSlideMin
        , dm_exec_query_stats_RemainTableCnt
        , dm_exec_query_stats_WebApiIntervalModValue
        , dm_os_workers_ProbeIntervalSec
        , dm_os_workers_TableSlideMin
        , dm_os_workers_RemainTableCnt
        , dm_os_workers_WebApiIntervalModValue
        , sp_lock2_ProbeIntervalSec
        , sp_lock2_TableSlideMin
        , sp_lock2_RemainTableCnt
        , sp_lock2_WebApiIntervalModValue
        , sp_readerrorlog_ProbeIntervalSec
        , sp_readagentlog_ProbeIntervalSec


    };    

    public enum InitialCatalog { Master, Repository }
    public enum RepositoryType { Pipe, Tcp };



    class Config
    {

        public LogClient.Config logClientConfig;
        
        private List<string> listData;
        private Dictionary<Tuple<Category, Key>, string> dicData;
        object LockObj = new object();
        static Log log = Log.Instance;
        static string cryptionKey = string.Empty;
        
        static string dataSettingFileName = "LazylogConfig.txt";
        static string dataSettingFullName = string.Empty;
        
        public string DecryptedPassword { get; set; }
        bool DbConfigLoaded = false;

        public string LocalIp { get; set; } = string.Empty;
        public string LocalPort { get; set; } = string.Empty;
        public bool CounterLoaded { get; set; }
        public bool ConfigLoaded { get; set; } = false; 

        public static Config Instance { get { return lazy.Value; } }
        private static readonly Lazy<Config> lazy =
            new Lazy<Config>(
                () => new Config()
                , LazyThreadSafetyMode.ExecutionAndPublication
                );

        public Config()
        {
            logClientConfig = LogClient.Config.Instance;
            logClientConfig.SqlPasswordChangedEvent += UpdateEncryptedSqlPassword;
            while (!logClientConfig.KeySettingCompleted())
            {
                Thread.Sleep(2000);
                Console.WriteLine("Wait For AccessKey and SecretKey");
            }
            listData = new List<string>();
            dicData = new Dictionary<Tuple<Category, Key>, string>();

            dataSettingFullName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, dataSettingFileName);
            LoadFile2List(dataSettingFullName, listData);
            LoadDataList2Dictionary(listData, dicData);
            ConfigLoaded = true;


            while (true)
            {
                if (!logClientConfig.SqlIdPassordSettingCompleted())
                {
                    Thread.Sleep(2000);
                    Console.WriteLine("Wait For SqlId SqlPass");
                }
                else
                {
                    try
                    {
                        if (!LogClient.Config.Instance.GetValue(LogClient.Category.Repository, LogClient.Key.SqlEncryptedPassword).Trim().Equals(""))
                            DecryptedPassword = TranString.DecodeRijndael(
                                        LogClient.Config.Instance.GetValue(LogClient.Category.Repository, LogClient.Key.SqlEncryptedPassword),
                                        LogClient.Config.Instance.GetCryptionKey());
                        break;
                    }
                    catch (Exception ex)
                    {
                        log.Error(string.Format("Cryption Error : {0}, {1}", ex.Message, ex.StackTrace));
                        Thread.Sleep(2000);
                    }
                }
            }

            // Sender:::Type:::B     ncp 
            if (GetValue(Category.Sender, Key.Type).Equals("b", StringComparison.OrdinalIgnoreCase))
            {
                log.Error("Database Create Skipped Sender:::Type:::B");
                DbConfigLoad();
                log.Error("database configuration load completed!");
            }
            else
            {
                //// schema gen 
                CounterLoaded = false;
                if (IsExistsRepository(GetValue(Category.Repository, Key.InitialCatalog)))
                {
                    log.Warn("Database Create Skipped");
                }
                else
                {
                    try
                    {
                        if (!Directory.Exists(GetValue(Category.Repository, Key.DatabaseFilePath)))
                            Directory.CreateDirectory(GetValue(Category.Repository, Key.DatabaseFilePath));
                        if (!Directory.Exists(GetValue(Category.Repository, Key.LogFilePath)))
                            Directory.CreateDirectory(GetValue(Category.Repository, Key.LogFilePath));

                        if (Common.QueryExecuter(GetConnectionString(InitialCatalog.Master)
                            , string.Format(CreateRepositoryQuery,
                               GetValue(Category.Repository, Key.InitialCatalog),
                               GetValue(Category.Repository, Key.DatabaseFilePath),
                               GetValue(Category.Repository, Key.LogFilePath))))
                            log.Warn("Database Create Success");
                    }
                    catch (Exception ex)
                    {
                        log.Error(string.Format("{0}, {1}", ex.Message, ex.StackTrace));
                    }
                }
            }

            try
            {
                CounterLoaded = IsExistsCounterDetails();

                if (GetValue(Category.Perfmon, Key.SkipLoadCounter).ToUpper() == "Y" && CounterLoaded)
                {
                    log.Warn("Schema Create Skipped");
                }
                else
                {
                    if (Common.QueryExecuter(GetConnectionString(InitialCatalog.Repository), CreateSchemaQuery))
                        log.Warn("Schema Create Success");
                    else
                        log.Warn("Schema Create Failed");
                }
                //// schema gen completed 

                LocalIp = Common.GetLocalIpAddress(IpType.LocalFirstIp);
                LocalPort = GetPort();
                SetSqlVersion();
            }
            catch (Exception ex)
            {
                log.Error(string.Format("{0}, {1}", ex.Message, ex.StackTrace));
            }
        }

        public void SaveData()
        {
            logClientConfig.SaveLogClientData(); 
            WriteUpdateValue2File(listData, dicData, dataSettingFullName);
        }

        private void LoadFile2List(string filename, List<string> listName)
        {
            try
            {
                string line = string.Empty;
                using (StreamReader file = new StreamReader(filename))
                {
                    listName.Clear();
                    while ((line = file.ReadLine()) != null)
                    {
                        try
                        {
                            listName.Add(line);
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        private void UpdateEncryptedSqlPassword (object sender, SqlPasswordArgs e)
        {
            DecryptedPassword = TranString.DecodeRijndael(e.SqlEncryptedPassword, LogClient.Config.Instance.GetCryptionKey());

        }

        private void LoadDataList2Dictionary(List<string> listName, Dictionary<Tuple<Category, Key>, string> dicName)
        {
            lock (LockObj)
            {
                dicData.Clear();
                string data = string.Empty;
                foreach (string line in listName)
                {
                    try
                    {
                        if (!line.StartsWith(@"#") && !(line == null) && !(line.Trim().Equals("")))
                        {
                            string[] lineValues = line.Split(new string[] { ":::" }, StringSplitOptions.None);

                            if (lineValues[0].ToString().Equals("Base64Unicode", StringComparison.OrdinalIgnoreCase))
                                data = TranString.DecodeBase64Unicode(lineValues[3]);
                            else if (lineValues[0].ToString().Equals("Base64Ascii", StringComparison.OrdinalIgnoreCase))
                                data = TranString.DecodeBase64(lineValues[3]);
                            else
                                data = lineValues[3];

                            dicName.Add
                                (
                                    new Tuple<Category, Key>
                                    (
                                        (Category)Enum.Parse(typeof(Category), lineValues[1]),
                                        (Key)Enum.Parse(typeof(Key), lineValues[2])
                                    ), data
                                );
                        }
                    }
                    catch (Exception ex)
                    {
                        log.Error(string.Format("{0}, {1}", ex.Message, ex.StackTrace));
                        throw;
                    }
                }
            }
        }

        private bool IsExistsCounterDetails()
        {
            bool bReturn = false;
            int CounterDetailCnt = 0;
            DateTime endTime = DateTime.Now.Add(new TimeSpan(0, 0, 10));
            while (true) // wait for sql repository ready
            {
                try
                {
                    bool preStepHasError = false;
                    endTime = DateTime.Now.Add(new TimeSpan(0, 0, 10));  // 10초마다 동작 

                    using (SqlConnection conn = new SqlConnection(GetConnectionString(InitialCatalog.Repository)))
                    {
                        conn.Open();
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = @"SELECT count(*) cnt FROM INFORMATION_SCHEMA.TABLES where TABLE_NAME = 'CounterDetails'";
                            SqlDataReader reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {
                                CounterDetailCnt = DatabaseValue<int>(reader["cnt"]);
                            }
                            if (CounterDetailCnt == 0)
                                preStepHasError = true;
                            reader.Close();
                        }
                        if (!preStepHasError)
                        {
                            using (SqlCommand cmd = conn.CreateCommand())
                            {
                                cmd.CommandType = CommandType.Text;
                                cmd.CommandText = @"SELECT count(*) cnt FROM dbo.CounterDetails";
                                SqlDataReader reader = cmd.ExecuteReader();
                                while (reader.Read())
                                {
                                    CounterDetailCnt = DatabaseValue<int>(reader["cnt"]);
                                }
                                if (CounterDetailCnt >= 1)
                                    bReturn = true;
                                else
                                    bReturn = false;
                                reader.Close();
                            }
                        }
                        conn.Close();
                        break; 
                    }
                }
                catch (Exception ex)
                {
                    log.Error($"IsExistsCounterDetails  {ex.Message}");
                }

                while (DateTime.Now < endTime)
                {
                    if (DbConfigLoaded) break;
                    Thread.Sleep(200);
                }
            }
            return bReturn;
        }

        private bool IsExistsRepository(string initialCatalog)
        {

            bool bReturn = false;
            DateTime endTime = DateTime.Now.Add(new TimeSpan(0, 0, 10));
            while (true) // wait for sql instance ready
            {
                try
                {
                    endTime = DateTime.Now.Add(new TimeSpan(0, 0, 10));  // 10초마다 동작 
                    using (SqlConnection conn = new SqlConnection(GetConnectionString(InitialCatalog.Master)))
                    {
                        conn.Open();
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = @"select name from master.dbo.sysdatabases where name = @initialCatalog";
                            cmd.Parameters.Add("@initialCatalog", SqlDbType.NVarChar, 100).Value = initialCatalog;
                            SqlDataReader reader = cmd.ExecuteReader();
                            bReturn = reader.HasRows;
                        }
                        conn.Close();
                        break; 
                    }
                }
                catch (Exception ex)
                {
                    log.Error($"IsExistsRepository Error {ex.Message}");
                }

                while (DateTime.Now < endTime)
                {
                    if (DbConfigLoaded) break;
                    Thread.Sleep(200);
                }
            }
            return bReturn;
        }

        private string GetPort()
        {
            string port = string.Empty;
            try
            {
                if (LogClient.Config.Instance.GetValue(LogClient.Category.Repository, LogClient.Key.SqlDataSource).IndexOf(",")>0)
                {
                    port = LogClient.Config.Instance.GetValue(LogClient.Category.Repository, LogClient.Key.SqlDataSource).Split(',')[1].Trim();
                }
                else
                {
                    using (SqlConnection conn = new SqlConnection(GetConnectionString(InitialCatalog.Repository)))
                    {
                        conn.Open();
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = @"SELECT top 1 port FROM sys.dm_tcp_listener_states WHERE is_ipv4 = 1 AND [type] = 0 AND ip_address <> '127.0.0.1'";
                            SqlDataReader reader = cmd.ExecuteReader();
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    port = DatabaseValue<int>(reader["port"]).ToString();
                                }
                            }
                            reader.Close();
                        }
                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(string.Format("{0}, {1}", ex.Message, ex.StackTrace));
            }
            return port;
        }

        private void SetSqlVersion()
        {
            DateTime endTime = DateTime.Now.Add(new TimeSpan(0, 0, 10));
            while (true)
            {
                endTime = DateTime.Now.Add(new TimeSpan(0, 0, 10));  // 10초마다 동작 
                try
                {
                    using (SqlConnection conn = new SqlConnection(GetConnectionString(InitialCatalog.Master)))
                    {
                        conn.Open();
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = @"
SELECT
  CASE 
     WHEN CONVERT(VARCHAR(128), SERVERPROPERTY ('productversion')) like '8%' THEN		'2000'
     WHEN CONVERT(VARCHAR(128), SERVERPROPERTY ('productversion')) like '9%' THEN		'2005'
     WHEN CONVERT(VARCHAR(128), SERVERPROPERTY ('productversion')) like '10.0%' THEN	'2008'
     WHEN CONVERT(VARCHAR(128), SERVERPROPERTY ('productversion')) like '10.5%' THEN	'2008R2'
     WHEN CONVERT(VARCHAR(128), SERVERPROPERTY ('productversion')) like '11%' THEN		'2012'
     WHEN CONVERT(VARCHAR(128), SERVERPROPERTY ('productversion')) like '12%' THEN		'2014'
     WHEN CONVERT(VARCHAR(128), SERVERPROPERTY ('productversion')) like '13%' THEN		'2016'     
     WHEN CONVERT(VARCHAR(128), SERVERPROPERTY ('productversion')) like '14%' THEN		'2017' 
     ELSE 'unknown'
  END AS Version
";
                            SqlDataReader reader = cmd.ExecuteReader();
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    dicData.Add(
                                        new Tuple<Category, Key>(
                                            (Category)Enum.Parse(typeof(Category), "SqlServer")
                                            , (Key)Enum.Parse(typeof(Key), "Version")
                                            )
                                        , DatabaseValue<string>(reader["Version"]));
                                }
                            }
                            reader.Close();
                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    log.Error(string.Format("{0}, {1}", ex.Message, ex.StackTrace));
                }

                while (DateTime.Now < endTime)
                {
                    Thread.Sleep(200);
                }
            }

        }
        
        public void SetValue(Category category, Key key, string value)
        {
            try
            {
                lock (LockObj)
                    dicData[new Tuple<Category, Key>(category, key)] = value;
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Categoy : {category}, Key : {key}, Value : {value}", ex);
            }
        }

        public string GetValue(Category category, Key key)
        {
            string sReturn = string.Empty;
            try
            {
                 sReturn = dicData[new Tuple<Category, Key>(category, key)];
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Categoy : {category}, Key : {key}", ex);
            }
            return sReturn;
        }

        public string GetServerName()
        {
            string ServerName = GetValue(Category.Ha, Key.ServerName).ToLower();
            if (ServerName.Trim().Length < 1)
                ServerName = Environment.MachineName.ToLower();
            return ServerName;
        }

        private void WriteUpdateValue2File(List<string> listName, Dictionary<Tuple<Category, Key>, string> dicName, string filename)
        {
            lock (LockObj)
            {
                if (File.Exists(filename))
                    File.Delete(filename);

                using (StreamWriter file = new StreamWriter(filename))
                {
                    foreach (string line in listName)
                    {
                        try
                        {
                            if (line.StartsWith(@"#") || (line == null) || (line.Trim().Equals("")))
                            {
                                file.WriteLine(line);
                            }
                            else
                            {
                                string[] lineValues = line.Split(new string[] { ":::" }, StringSplitOptions.None);
                                string data = string.Empty;
                                data = dicData[new Tuple<Category, Key>((Category)Enum.Parse(typeof(Category), lineValues[1]), (Key)Enum.Parse(typeof(Key), lineValues[2]))];

                                if (lineValues[0].ToString().ToUpper().Equals("Base64Unicode".ToUpper()))
                                    data = TranString.EncodeBase64Unicode(data);
                                else if (lineValues[0].ToString().ToUpper().Equals("Base64Ascii".ToUpper()))
                                    data = TranString.EncodeBase64(data);

                                file.WriteLine(lineValues[0] + ":::" + lineValues[1] + ":::" + lineValues[2] + ":::" + data);
                            }
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                    }
                }
            }
        }


        public string GetConnectionString(InitialCatalog initialCatalog)
        {
            string connString = string.Empty;
            try
            {
                connString = new SqlConnectionStringBuilder
                {
                    DataSource = LogClient.Config.Instance.GetValue(LogClient.Category.Repository, LogClient.Key.SqlDataSource),
                    UserID = LogClient.Config.Instance.GetValue(LogClient.Category.Repository, LogClient.Key.SqlId),
                    Password = DecryptedPassword,
                    InitialCatalog = initialCatalog == InitialCatalog.Master ? "master" : GetValue(Category.Repository, Key.InitialCatalog),
                    ConnectTimeout = int.Parse(LogClient.Config.Instance.GetValue(LogClient.Category.Repository, LogClient.Key.SqlConnectTimeout))
                }.ConnectionString;
            }
            catch (Exception ex)
            {
                log.Error(string.Format("{0}, {1}", ex.Message, ex.StackTrace));
            }
            return connString;
        }

        public T DatabaseValue<T>(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return default(T);
            else
                return (T)obj;
        }

        //private string GetConnectionString(RepositoryType repositoryType, InitialCatalog initialCatalog)
        //{
        //    string connectionString = string.Empty;

        //    try
        //    {
        //        switch (repositoryType)
        //        {
        //            //case RepositoryType.Pipe:
        //            //    connectionString = new SqlConnectionStringBuilder
        //            //    {
        //            //        DataSource = GetValue(Category.Repository, Key.PipeDataSource),
        //            //        InitialCatalog = initialCatalog == InitialCatalog.Master ? "master" : GetValue(Category.Repository, Key.InitialCatalog),
        //            //        ConnectTimeout = int.Parse(GetValue(Category.Repository, Key.ConnectTimeout))
        //            //    }.ConnectionString;
        //            //    break;
        //            case RepositoryType.Pipe:
        //                connectionString = new SqlConnectionStringBuilder
        //                {
        //                    DataSource = GetValue(Category.Repository, Key.PipeDataSource),
        //                    UserID = GetValue(Category.Repository, Key.TcpUserId),
        //                    Password = TcpDecryptedPassword,
        //                    InitialCatalog = initialCatalog == InitialCatalog.Master ? "master" : GetValue(Category.Repository, Key.InitialCatalog),
        //                    ConnectTimeout = int.Parse(GetValue(Category.Repository, Key.ConnectTimeout))
        //                }.ConnectionString;
        //                break;
        //            case RepositoryType.Tcp:
        //                connectionString = new SqlConnectionStringBuilder
        //                {
        //                    DataSource = GetValue(Category.Repository, Key.TcpDataSource),
        //                    UserID = GetValue(Category.Repository, Key.TcpUserId),
        //                    Password = TcpDecryptedPassword,
        //                    InitialCatalog = initialCatalog == InitialCatalog.Master ? "master" : GetValue(Category.Repository, Key.InitialCatalog),
        //                    ConnectTimeout = int.Parse(GetValue(Category.Repository, Key.ConnectTimeout))
        //                }.ConnectionString;
        //                break;
        //            default:
        //                throw new ArgumentOutOfRangeException();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        log.Error(string.Format("{0}, {1}", ex.Message, ex.StackTrace));
        //    }
        //    return connectionString;
        //}


        public void DbConfigLoad()
        {
            if (!DbConfigLoaded)
            {
                DateTime endTime = DateTime.Now.Add(new TimeSpan(0, 0, 10));

                while (true)
                {
                    try
                    {
                        endTime = DateTime.Now.Add(new TimeSpan(0, 0, 10));  // 10초마다 동작 
                        using (SqlConnection conn = new SqlConnection(GetConnectionString(InitialCatalog.Repository)))
                        {
                            conn.Open();
                            using (SqlCommand cmd = conn.CreateCommand())
                            {
                                cmd.CommandType = CommandType.Text;
                                cmd.CommandText = @"select @@servicename servicename";
                                SqlDataReader reader = cmd.ExecuteReader();
                                if (reader.HasRows)
                                {
                                    while (reader.Read())
                                    {
                                        dicData.Add(
                                        new Tuple<Category, Key>(
                                            (Category)Enum.Parse(typeof(Category), "GLOBAL")
                                            , (Key)Enum.Parse(typeof(Key), "servicename")
                                            )
                                        , DatabaseValue<string>(reader["servicename"])
                                        );
                                    }
                                }
                                reader.Close();
                            }

                            using (SqlCommand cmd = conn.CreateCommand())
                            {
                                cmd.CommandType = CommandType.Text;
                                cmd.CommandText = @"select type, name, value from dbo.config";
                                SqlDataReader reader = cmd.ExecuteReader();
                                if (reader.HasRows)
                                {
                                    while (reader.Read())
                                    {
                                        dicData.Add(
                                        new Tuple<Category, Key>(
                                            (Category)Enum.Parse(typeof(Category), DatabaseValue<string>(reader["type"]))
                                            , (Key)Enum.Parse(typeof(Key), DatabaseValue<string>(reader["name"]))
                                            )
                                        , DatabaseValue<string>(reader["value"])
                                        );

                                        log.Warn(string.Format("database configuration loading... {0}, {1}, {2}"
                                            , DatabaseValue<string>(reader["type"])
                                            , DatabaseValue<string>(reader["name"])
                                            , DatabaseValue<string>(reader["value"])));
                                    }
                                }
                                reader.Close();
                            }

                            conn.Close();
                            DbConfigLoaded = true;
                            break; 
                        }
                    }
                    catch (Exception ex)
                    {
                        log.Error(string.Format("{0}, {1}", ex.Message, ex.StackTrace));
                    }

                    
                    while (DateTime.Now < endTime)
                    {
                        if (DbConfigLoaded) break;
                        Thread.Sleep(200);
                    }
                    log.Warn("!DbConfigLoaded while....");
                }
            }
        }
        

        private string CreateRepositoryQuery =
    @"CREATE DATABASE [{0}]
ON  PRIMARY 
( NAME = N'{0}', FILENAME = N'{1}\{0}.mdf' , SIZE = 10MB , MAXSIZE = UNLIMITED, FILEGROWTH = 50MB )
LOG ON 
( NAME = N'{0}_log', FILENAME = N'{2}\{0}_log.ldf' , SIZE = 10MB , MAXSIZE = 2048GB , FILEGROWTH = 50MB)
go

alter database [{0}] set recovery simple
go
";


        #region CreateSchemaQuery
        private string CreateSchemaQuery =
    @"
if object_id('CounterDetailsFilterInfo') is not null
drop table CounterDetailsFilterInfo
go

CREATE TABLE [dbo].[CounterDetailsFilterInfo](
	[Idx] [int] IDENTITY(1,1) NOT NULL,
	[FilterType] [varchar](10) NULL,
	[ObjectName] [varchar](1024) NULL
) ON [PRIMARY]
GO

INSERT INTO CounterDetailsFilterInfo ( FilterType , ObjectName)VALUES('IN','LogicalDisk')
INSERT INTO CounterDetailsFilterInfo ( FilterType , ObjectName)VALUES('IN',':Buffer Manager')
INSERT INTO CounterDetailsFilterInfo ( FilterType , ObjectName)VALUES('IN',':CLR')
INSERT INTO CounterDetailsFilterInfo ( FilterType , ObjectName)VALUES('IN',':Databases')
INSERT INTO CounterDetailsFilterInfo ( FilterType , ObjectName)VALUES('IN',':Latches')
INSERT INTO CounterDetailsFilterInfo ( FilterType , ObjectName)VALUES('IN','Processor')
INSERT INTO CounterDetailsFilterInfo ( FilterType , ObjectName)VALUES('IN',':SQL Statistics')
INSERT INTO CounterDetailsFilterInfo ( FilterType , ObjectName)VALUES('IN',':Database Replica')
INSERT INTO CounterDetailsFilterInfo ( FilterType , ObjectName)VALUES('IN',':Availability Replica')
INSERT INTO CounterDetailsFilterInfo ( FilterType , ObjectName)VALUES('IN','SQLServer:General Statistics')
INSERT INTO CounterDetailsFilterInfo ( FilterType , ObjectName)VALUES('NOTIN','Network Adapter')
INSERT INTO CounterDetailsFilterInfo ( FilterType , ObjectName)VALUES('NOTIN','Hyper-V')
INSERT INTO CounterDetailsFilterInfo ( FilterType , ObjectName)VALUES('NOTIN','Per Processor Network')
INSERT INTO CounterDetailsFilterInfo ( FilterType , ObjectName)VALUES('NOTIN','Processor Information')
INSERT INTO CounterDetailsFilterInfo ( FilterType , ObjectName)VALUES('NOTIN','Physical Network Interface Card Activity')
INSERT INTO CounterDetailsFilterInfo ( FilterType , ObjectName)VALUES('IN','System')
INSERT INTO CounterDetailsFilterInfo ( FilterType , ObjectName)VALUES('IN','Memory')
INSERT INTO CounterDetailsFilterInfo ( FilterType , ObjectName)VALUES('IN','Network Interface')
INSERT INTO CounterDetailsFilterInfo ( FilterType , ObjectName)VALUES('IN',':Memory Manager')
INSERT INTO CounterDetailsFilterInfo ( FilterType , ObjectName)VALUES('IN',':Locks')
INSERT INTO CounterDetailsFilterInfo ( FilterType , ObjectName)VALUES('IN',':General Statistics')
INSERT INTO CounterDetailsFilterInfo ( FilterType , ObjectName)VALUES('NOTIN','.NET CLR Memory')
INSERT INTO CounterDetailsFilterInfo ( FilterType , ObjectName)VALUES('NOTIN','NUMA Node Memory')
INSERT INTO CounterDetailsFilterInfo ( FilterType , ObjectName)VALUES('NOTIN','Security System-Wide Statistics')
INSERT INTO CounterDetailsFilterInfo ( FilterType , ObjectName)VALUES('NOTIN','SQL Server 2016 XTP Phantom Processor')
INSERT INTO CounterDetailsFilterInfo ( FilterType , ObjectName)VALUES('NOTIN','SQLAgent:SystemJobs')
INSERT INTO CounterDetailsFilterInfo ( FilterType , ObjectName)VALUES('NOTIN','SQLServer:Availability Replica')
INSERT INTO CounterDetailsFilterInfo ( FilterType , ObjectName)VALUES('NOTIN','SQLServer:CLR')
INSERT INTO CounterDetailsFilterInfo ( FilterType , ObjectName)VALUES('NOTIN','SQLServer:Database Replica')
INSERT INTO CounterDetailsFilterInfo ( FilterType , ObjectName)VALUES('NOTIN','SQLServer:Databases')
INSERT INTO CounterDetailsFilterInfo ( FilterType , ObjectName)VALUES('NOTIN','SQLServer:Memory Broker Clerks')
INSERT INTO CounterDetailsFilterInfo ( FilterType , ObjectName)VALUES('NOTIN','SQLServer:Memory Node')
GO

if object_id('CounterDetailsAutoUpdated') is not null
drop table [CounterDetailsAutoUpdated]
go

CREATE TABLE [dbo].[CounterDetailsAutoUpdated](
	[MachineName] [varchar](100) NULL,
	[ObjectName] [varchar](100) NULL,
	[CounterName] [varchar](100) NULL,
	[CounterType] [int] NULL,
	[DefaultScale] [int] NULL,
	[InstanceName] [varchar](100) NULL,
	[InstanceIndex] [int] NULL,
	[ParentName] [varchar](1024) NULL,
	[ParentObjectId] [int] NULL,
	[TimeBaseA] [int] NULL,
	[TimeBaseB] [int] NULL,
	[IsEnabledYN] [varchar](1) NULL
) ON [PRIMARY]
GO

/****** Object:  Index [ucl_CounterDetailsAutoUpdated]    Script Date: 12/6/2018 2:25:46 PM ******/
CREATE UNIQUE CLUSTERED INDEX [ucl_CounterDetailsAutoUpdated] ON [dbo].[CounterDetailsAutoUpdated]
(
	[MachineName] ASC,
	[ObjectName] ASC,
	[CounterName] ASC,
	[InstanceName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO


if object_id('CounterDetails') is not null
drop table [CounterDetails]
go

CREATE TABLE [dbo].[CounterDetails](
	[CounterID] [int] IDENTITY(1,1) NOT NULL,
	[MachineName] [varchar](100) NOT NULL,
	[ObjectName] [varchar](100) NOT NULL,
	[CounterName] [varchar](100) NOT NULL,
	[CounterType] [int] NOT NULL,
	[DefaultScale] [int] NOT NULL,
	[InstanceName] [varchar](100) NULL,
	[InstanceIndex] [int] NULL,
	[ParentName] [varchar](100) NULL,
	[ParentObjectID] [int] NULL,
	[TimeBaseA] [int] NULL,
	[TimeBaseB] [int] NULL
) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [nc_CounterDetails_01] ON [dbo].[CounterDetails]
(
	[MachineName] ASC,
	[CounterID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

if object_id('DisplayToIDOrigin') is not null
drop table [DisplayToIDOrigin]
go

CREATE TABLE [dbo].[DisplayToIDOrigin](
	[GUID] [uniqueidentifier] NOT NULL,
	[RunID] [int] NULL,
	[DisplayString] [varchar](100) NOT NULL,
	[LogStartTime] [char](24) NULL,
	[LogStopTime] [char](24) NULL,
	[NumberOfRecords] [int] NULL,
	[MinutesToUTC] [int] NULL,
	[TimeZoneName] [char](32) NULL
) ON [PRIMARY]
GO

/****** Object:  Index [nc_DisplayToID_01]    Script Date: 12/6/2018 5:38:47 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [nc_DisplayToIDOrigin_01] ON [dbo].[DisplayToIDOrigin]
(
	[DisplayString] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO


if object_id('CounterData') is not null
drop view [CounterData]
go

create view [dbo].[CounterData]
as 

select 
1 GUID 
, 1 CounterID 
, 1 RecordIndex
, 1 CounterDateTime
, 1 CounterValue
, 1 FirstValueA
, 1 FirstValueB
, 1 SecondValueA
, 1 SecondValueB
, 1 MultiCount
go

if object_id('DisplayToID') is not null
drop view [DisplayToID]
go

create view DisplayToID 
as
with CounterDataOriginCte
as
(
select 
	min(isnull(CounterDateTime, convert(char(19), getdate(), 121))) as LogStartTime
	, convert(char(19), getdate(), 121) as LogStopTime 
	, count(distinct RecordIndex) NumberOfRecords
from dbo.CounterData with (nolock)
)
select 
	[GUID], [RunID], [DisplayString], b.[LogStartTime], b.[LogStopTime], b.NumberOfRecords, [MinutesToUTC], [TimeZoneName]
from DisplayToIDOrigin a
cross join CounterDataOriginCte b
go



if object_id('CounterDataOriginSendHist') is not null
drop table CounterDataOriginSendHist
go

create table CounterDataOriginSendHist
(
RecordIndex int 
, CounterID int 
, LastSendDate datetime
)
go

-- CREATE TABLE [dbo].[Config](
-- 	[type] [nvarchar](200) NULL,
-- 	[Name] [nvarchar](200) NULL,
-- 	[Value] [nvarchar](1024) NULL
-- ) ON [PRIMARY]
-- GO
-- 
-- 
-- CREATE UNIQUE CLUSTERED INDEX [cl_config] ON [dbo].[Config]
-- (
-- 	[type] ASC,
-- 	[Name] ASC
-- )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
-- GO
-- ncp 용 sample data
-- INSERT INTO Config ( type , Name , Value)VALUES('CLA','LogTypes','CDB_MSSQL')
-- INSERT INTO Config ( type , Name , Value)VALUES('CLA','URL','http://collect.beta-cla.ncloud.com:9010')
-- INSERT INTO Config ( type , Name , Value)VALUES('CLA','UserKey','3bf891d5d7e240f99d42b62da93f3a88')
-- INSERT INTO Config ( type , Name , Value)VALUES('DMV','UseCLAYN','Y')
-- INSERT INTO Config ( type , Name , Value)VALUES('DMV','UseNPOTYN','Y')
-- INSERT INTO Config ( type , Name , Value)VALUES('DMV','UseYN','Y')
-- INSERT INTO Config ( type , Name , Value)VALUES('GLOBAL','hostname','')
-- INSERT INTO Config ( type , Name , Value)VALUES('GLOBAL','private_ip','')
-- INSERT INTO Config ( type , Name , Value)VALUES('NPOT','AuthInfo','db7d2dc:952fee5')
-- INSERT INTO Config ( type , Name , Value)VALUES('NPOT','ncp_group_no','880101')
-- INSERT INTO Config ( type , Name , Value)VALUES('NPOT','ncp_mbr_no','127')
-- INSERT INTO Config ( type , Name , Value)VALUES('NPOT','URL','http://npot-ncp-beta-tsw.navercorp.com:10041/api/put')
-- INSERT INTO Config ( type , Name , Value)VALUES('PERFMON','CollectIntervalSec','15')
-- INSERT INTO Config ( type , Name , Value)VALUES('PERFMON','CollectPerServerItemSleepMiliSec','150')
-- INSERT INTO Config ( type , Name , Value)VALUES('PERFMON','CounterDetailsThreads','1')
-- INSERT INTO Config ( type , Name , Value)VALUES('PERFMON','ItemName','BaselineCollect')
-- INSERT INTO Config ( type , Name , Value)VALUES('PERFMON','PerfmonReset','N (Processed by lazylog App)')
-- INSERT INTO Config ( type , Name , Value)VALUES('PERFMON','UseNPOTYN','Y')
-- INSERT INTO Config ( type , Name , Value)VALUES('PERFMON','UseYN','Y')
go

";


        
        #endregion

    }



}
