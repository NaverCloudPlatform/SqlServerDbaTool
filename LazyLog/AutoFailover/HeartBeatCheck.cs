using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using LogClient;
using System.Data.SqlClient;
using CsLib;
using System.IO;
using lazylog.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace lazylog.AutoFailover
{
    public class HeartBeatCheck : IManager
    {
        Log log = Log.Instance;
        Config config = Config.Instance;
        FileDb fileDb = FileDb.Instance;

        public bool IsRunning { get; set; }

        public async void Start()
        {
            IsRunning = true;
            //IsRunning = false;
            var task = Task.Delay(1000);
            await task;

            while (IsRunning)
            {
                try
                {
                    log.Info("Check HeartBeatCheck");

                    

                    if (!int.TryParse(config.GetValue(Category.Ha, Key.HeartBeatIntervalSec), out int RunIntervalSec))
                        RunIntervalSec = 0;
                    DateTime endTime = DateTime.Now.Add(new TimeSpan(0, 0, RunIntervalSec));

                    if (RunIntervalSec == 0)
                    {
                        IsRunning = false;
                        log.Warn("Ha (HeartBeatCheck) can't start because HeartBeatIntervalSec is 0");
                        break;
                    }

                    log.Warn("HeartBeatCheck Started");
                    log.Info($"thisServerName : {GetServerName()}");
                    var data = GetSqlServerLicenseData(GetServerName());
                    await pushLicenseData(data);

                    string partnerServerName = await GetPartnerInfo(GetServerName());
                    log.Info($"partnerServerName : {partnerServerName}");

                    if (!data.serverRole.Equals("MASTER"))
                    {
                        if (partnerServerName.Length > 0)
                        {
                            bool normal = await IsServerHealthHistoryNormal(partnerServerName);
                            if (!normal)
                            {
                                if (!await LastConnectionTest(partnerServerName))
                                    ServerFailover();
                            }
                        }
                    }
                    else
                    {
                        log.Warn("this Server is master");
                    }

                    while (DateTime.Now < endTime)
                    {
                        if (!IsRunning) break;
                        var sleepTask = Task.Delay(2000);
                        await sleepTask;
                    }
                }
                catch (Exception ex)
                {
                    log.Error($"{ex.Message}, {ex.StackTrace}");
                }
            }
        }

        public void Stop()
        {
            log.Warn("ha heartbeatCheck stop");
            IsRunning = false;
            log.Warn("ha heartbeatCheck stoped");
        }
        class TBL_HEALTH_INFO
        {
            public string serverName { get; set; }
            public string time { get; set; }
            public string healthInfo { get; set; }
        }

        class TBL_CLUSTER_SERVER
        {
            public string clusterName { get; set; }
            public string serverName { get; set; }
            public string serverRole { get; set; }
        }


        public async Task<bool> IsServerHealthHistoryNormal(string serverName)
        {
            bool result = false;
            try
            {
                string filePath = @"heartBeat/" + serverName;
                List<TBL_HEALTH_INFO> tbl_health_infos = new List<TBL_HEALTH_INFO>();

                string bucketName = config.GetValue(Category.Backup, Key.BucketName);
                CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
                ObjectStorage o = new ObjectStorage(
                    LogClient.Config.Instance.GetValue(LogClient.Category.Api, LogClient.Key.AccessKey),
                    LogClient.Config.Instance.GetValue(LogClient.Category.Api, LogClient.Key.SecretKey),
                    config.GetValue(Category.Backup, Key.ObjectStorageServiceUrl));

                if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + filePath))
                    Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + filePath);

                await o.DownloadObjectAsync(
                    bucketName
                    , Path.Combine(AppDomain.CurrentDomain.BaseDirectory + @"/" + filePath + @"/" + "TBL_HEALTH_INFO" + ".txt")
                    , filePath + @"/" + "TBL_HEALTH_INFO" + ".txt", cancelTokenSource.Token, 0
                    );

                string json = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory + filePath + @"/TBL_HEALTH_INFO.txt"));

                if (json.Length > 0)
                {
                    tbl_health_infos.Clear();

                    var TBL_HEALTH_INFOS = JsonConvert.DeserializeObject<List<KeyValuePair<TBL_HEALTH_INFO_KEY, TBL_HEALTH_INFO_VALUE>>>(json);

                    if (!int.TryParse(config.GetValue(Category.Ha, Key.HeartBeatTimeLimitSec), out int HeartBeatTimeLimitSec))
                        HeartBeatTimeLimitSec = 60;

                    if (TBL_HEALTH_INFOS.Count() > 0)
                    {
                        long cutTime = long.Parse(DateTime.Now.Add(new TimeSpan(0, 0, Math.Abs(HeartBeatTimeLimitSec) * -1)).ToString("yyyyMMddHHmmss"));
                        foreach (var a in TBL_HEALTH_INFOS)
                        {
                            if (!long.TryParse(a.Key.time, out long storedTime))
                                storedTime = 0;

                            if (cutTime < storedTime)
                            {
                                if (a.Value.healthInfo.Equals("normal", StringComparison.OrdinalIgnoreCase))
                                {
                                    tbl_health_infos.Add(new TBL_HEALTH_INFO
                                    {
                                        serverName = a.Key.serverName,
                                        time = a.Key.time,
                                        healthInfo = a.Value.healthInfo
                                    });
                                    result = true;
                                }
                            }
                        }
                        if (tbl_health_infos.Count() == 0)
                            log.Warn($"IsServerHealthHistoryNormal read health count : {TBL_HEALTH_INFOS.Count()}, normal count : {tbl_health_infos.Count()}");
                    }
                    else
                    {
                        log.Warn($"IsServerHealthHistoryNormal failed : {serverName}");
                    }
                }
                else
                {
                    log.Warn($"IsServerHealthHistoryNormal failed : {serverName}");
                }
            }
            catch (Exception ex)
            {
                log.Error($@"IsServerHealthHistoryNormal failed : {ex.Message}, {ex.StackTrace}");
            }

            return result;
        }

        private SqlServerLicenseData GetSqlServerLicenseData (string serverName)
        {
            string batchRequestsPerSec = "";
            string onlineDbCnt = "";
            string cpuCnt = "";
            string hyperthreadRatio = "";
            string physicalMemoryKb = "";

            SqlServerLicenseData sld = new SqlServerLicenseData();

            try
            {
                AsyncHelpers.RunSync(() => fileDb.ReadTable(FileDb.TableName.TBL_SERVER));
                TBL_SERVER_VALUE tbl_server_value = new TBL_SERVER_VALUE();
                if (fileDb.TBL_SERVER.Data.ContainsKey(new TBL_SERVER_KEY { serverName = serverName }))
                {
                    tbl_server_value = fileDb.TBL_SERVER.Data[new TBL_SERVER_KEY { serverName = serverName }];
                }

                string connString = string.Empty;

                if (tbl_server_value != null)
                {
                    try
                    {
                        string decryptedPassword = TranString.DecodeRijndael(
                            tbl_server_value.serverPassword,
                            LogClient.Config.Instance.GetCryptionKey());

                        connString = new SqlConnectionStringBuilder
                        {
                            DataSource = tbl_server_value.serverPublicIp + "," + tbl_server_value.serverPort,
                            UserID = tbl_server_value.serverUserId,
                            Password = decryptedPassword,
                            InitialCatalog = "master",
                            ConnectTimeout = 5,
                        }.ConnectionString;

                        using (SqlConnection conn = new SqlConnection(connString))
                        {
                            conn.Open();
                            using (SqlCommand cmd = conn.CreateCommand())
                            {
                                cmd.CommandType = System.Data.CommandType.Text;
                                cmd.CommandText =
@"
set nocount on 
set transaction isolation level read uncommitted 
declare @pre int, @post int, @BatchRequestsPerSec int, @OnlineDbCnt int, @CpuCnt int, @HyperthreadRatio int, @PhysicalMemoryKb int
begin try 
    select @CpuCnt = cpu_count, @HyperthreadRatio = hyperthread_ratio, @PhysicalMemoryKb = physical_memory_kb from [sys].[dm_os_sys_info]
    select @OnlineDbCnt = count(*) from sys.databases where name not in ('master', 'tempdb', 'model', 'msdb', 'LazyLog') and state_desc = 'ONLINE'
    select @pre = cntr_value from sys.dm_os_performance_counters WHERE counter_name  ='Batch Requests/sec'
    waitfor delay '00:00:01.000'
    select @post = cntr_value from sys.dm_os_performance_counters WHERE counter_name  ='Batch Requests/sec'
    select @BatchRequestsPerSec = (@post - @pre)
end try 
begin catch 
end catch 
select 
    cast(@BatchRequestsPerSec as varchar(100)) batchRequestsPerSec 
    , cast(@OnlineDbCnt as varchar(100)) onlineDbCnt
    , cast(@CpuCnt as varchar(100)) cpuCnt
    , cast(@HyperthreadRatio as varchar(100)) hyperthreadRatio
    , cast(@PhysicalMemoryKb as varchar(100)) physicalMemoryKb
";
                                cmd.CommandTimeout = 5;
                                SqlDataReader reader = cmd.ExecuteReader();
                                while (reader.Read())
                                {
                                    batchRequestsPerSec = config.DatabaseValue<string>(reader["batchRequestsPerSec"]);
                                    onlineDbCnt = config.DatabaseValue<string>(reader["onlineDbCnt"]);
                                    cpuCnt = config.DatabaseValue<string>(reader["cpuCnt"]);
                                    hyperthreadRatio = config.DatabaseValue<string>(reader["hyperthreadRatio"]);
                                    physicalMemoryKb = config.DatabaseValue<string>(reader["physicalMemoryKb"]);
                                }
                                reader.Close();
                            }
                            conn.Close();
                        }

                        GetClusterInfo(GetServerName(), out string loadBalancerInstanceNo, out string serverInstanceNo, out string clusterName, out string publicIp, out string privateIp, out string serverRole);

                        sld.loadBalancerInstanceNo = loadBalancerInstanceNo;
                        sld.loadBalancerInstanceName = clusterName; 
                        sld.serverName = serverName;
                        sld.serverInstanceNo = serverInstanceNo;
                        sld.privateIp = privateIp;
                        sld.serverRole = serverRole;
                        sld.batchRequestsPerSec = batchRequestsPerSec;
                        sld.onlineDbCnt = onlineDbCnt;
                        sld.cpuCnt = cpuCnt;
                        sld.hyperthreadRatio = hyperthreadRatio;
                        sld.physicalMemoryKb = physicalMemoryKb;

                        

                    }
                    catch (Exception ex)
                    {
                        log.Error(string.Format("get licensedata error {0}, {1}", ex.Message, ex.StackTrace));
                    }
                }
                
            }
            catch (Exception ex)
            {
                log.Error(string.Format("{0}, {1}", ex.Message, ex.StackTrace));
            }
            return sld;
        }


        private async Task pushLicenseData(SqlServerLicenseData sld)
        {
            try
            {
                
                string endpoint = config.GetValue(Category.ApiGateway, Key.Endpoint);
                string action = @"/mssql/v2/pushLicenseData";

                JToken jt = JToken.Parse(JsonConvert.SerializeObject(sld));
                string parameters = jt.ToString(Newtonsoft.Json.Formatting.Indented); 

                SoaCall soaCall = new SoaCall();
                var task = soaCall.WebApiCall(endpoint
                    , RequestType.POST
                    , action
                    , parameters
                    , LogClient.Config.Instance.GetValue(
                        LogClient.Category.Api, LogClient.Key.AccessKey)
                    , LogClient.Config.Instance.GetValue(
                        LogClient.Category.Api, LogClient.Key.SecretKey)
                    );

                string response = await task;
                log.Warn($"pushLicenseData() response : {response}");
                JsonSerializerSettings options = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore
                };

                if (response.Contains("responseError"))
                {
                    hasError hasError = JsonConvert.DeserializeObject<hasError>(response, options);
                    throw new Exception(hasError.responseError.returnMessage);
                }

            }
            catch (Exception ex)
            {
                log.Error(string.Format("{0}, {1}", ex.Message, ex.StackTrace));
            }

        }

        public async Task<bool> LastConnectionTest(string serverName)
        {
            bool returnValue = false;
            await fileDb.ReadTable(FileDb.TableName.TBL_SERVER);
            TBL_SERVER_VALUE tbl_server_value = new TBL_SERVER_VALUE();
            if (fileDb.TBL_SERVER.Data.ContainsKey(new TBL_SERVER_KEY { serverName = serverName }))
            {
                tbl_server_value = fileDb.TBL_SERVER.Data[new TBL_SERVER_KEY { serverName = serverName }];
            }

            string connString = string.Empty;

            if (tbl_server_value != null)
            {
                try
                {
                    string decryptedPassword = TranString.DecodeRijndael(
                        tbl_server_value.serverPassword,
                        LogClient.Config.Instance.GetCryptionKey());

                    connString = new SqlConnectionStringBuilder
                    {
                        DataSource = tbl_server_value.serverPublicIp + "," + tbl_server_value.serverPort,
                        UserID = tbl_server_value.serverUserId,
                        Password = decryptedPassword,
                        InitialCatalog = "master",
                        ConnectTimeout = 5,
                    }.ConnectionString;

                    using (SqlConnection conn = new SqlConnection(connString))
                    {
                        conn.Open();
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.CommandType = System.Data.CommandType.Text;
                            cmd.CommandText = @"select top 1 'normal' as healthStatus";
                            cmd.CommandTimeout = 5;
                            SqlDataReader reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {
                                config.DatabaseValue<string>(reader["healthStatus"]);
                            }
                            reader.Close();
                            returnValue = true;
                        }
                        conn.Close();
                        log.Warn("last connection test success!");
                    }
                }
                catch (Exception ex)
                {
                    log.Error(string.Format("{0}, {1}", ex.Message, ex.StackTrace));
                }
            }
            return returnValue;
        }

        public void ServerFailover()
        {
            log.Warn($@"=================================================================");
            log.Warn($@"partner server down. failover started!!! ");
            log.Warn($@"=================================================================");

            try
            {
                PartnerOffAndRestoreDatabase();
                LoadbalancerChagne();
                log.Warn("failover completed");
            }
            catch (Exception ex)
            {
                log.Error(string.Format("{0}, {1}", ex.Message, ex.StackTrace));
            }
        }

        private void LoadbalancerChagne()
        {
            log.Warn("loadbalancer setting change start");
            try
            {
                GetClusterInfo(GetServerName(), out string loadBalancerInstanceNo, out string masterServerInstacneNo, out string clusterName, out string publicIp, out string privateIp, out string serverRole);

                AsyncHelpers.RunSync(() => ChangeLoadBalancedServerInstances(loadBalancerInstanceNo, masterServerInstacneNo));
                AsyncHelpers.RunSync(() => SaveClusterServerInfo(clusterName, GetServerName()));
                log.Warn("loadbalancer changed");
            }
            catch (Exception ex)
            {
                log.Error(string.Format("{0}, {1}", ex.Message, ex.StackTrace));
            }
        }

        private void GetClusterInfo(string serverName, out string loadBalancerInstanceNo, out string masterServerInstanceNo, out string clusterName, out string publicIp, out string privateIp, out string serverRole)
        {
            clusterName = "";
            masterServerInstanceNo = "";
            loadBalancerInstanceNo = "";
            publicIp = "";
            privateIp = "";
            serverRole = "";

            try
            {
                AsyncHelpers.RunSync(() => fileDb.ReadTable(FileDb.TableName.TBL_CLUSTER));
                AsyncHelpers.RunSync(() => fileDb.ReadTable(FileDb.TableName.TBL_SERVER));
                AsyncHelpers.RunSync(() => fileDb.ReadTable(FileDb.TableName.TBL_CLUSTER_SERVER));

                foreach (var a in fileDb.TBL_SERVER.Data)
                {
                    if (a.Key.serverName.Equals(serverName, StringComparison.OrdinalIgnoreCase))
                    {
                        masterServerInstanceNo = a.Value.serverInstanceNo;
                        publicIp = a.Value.serverPublicIp;
                        privateIp = a.Value.serverPrivateIp;
                    }
                }

                if (masterServerInstanceNo.Length == 0)
                    log.Error($"GetClusterInfo() Error serverName {serverName}, masterServerInstanceNo : {masterServerInstanceNo}");

                foreach (var a in fileDb.TBL_CLUSTER_SERVER.Data)
                {
                    if (a.Key.serverName.Equals(serverName, StringComparison.OrdinalIgnoreCase))
                    {
                        clusterName = a.Key.clusterName;
                        serverRole = a.Value.serverRole;
                    }
                }

                if (clusterName.Length == 0)
                    log.Error($"GetClusterInfo() Error serverName {serverName}, clusterName : {clusterName}");

                foreach (var a in fileDb.TBL_CLUSTER.Data)
                {
                    if (a.Key.clusterName.Equals(clusterName, StringComparison.OrdinalIgnoreCase))
                        loadBalancerInstanceNo = a.Value.clusterNo;
                }

                if (loadBalancerInstanceNo.Length == 0)
                    log.Error($"GetClusterInfo() Error serverName {serverName}, loadBalancerInstanceNo : {loadBalancerInstanceNo}");

                log.Info($"GetClusterInfo completed input variable serverName : {serverName}, output loadBalancerInstanceNo : {loadBalancerInstanceNo}, ouput masterServerInstanceNo : {masterServerInstanceNo}, clusterName : {clusterName}");
            }
            catch (Exception ex)
            {
                log.Error(string.Format("{0}, {1}", ex.Message, ex.StackTrace));
            }
        }




        private async Task ChangeLoadBalancedServerInstances(string loadBalancerInstanceNo, string masterServerInstanceNo)
        {
            try
            {
                log.Warn($"ChangeLoadBalancedServerInstances() loadBalancerInstanceNo : {loadBalancerInstanceNo}, masterServerInstanceNo : {masterServerInstanceNo}");
                string endpoint = config.GetValue(Category.ApiGateway, Key.Endpoint);
                string action = @"/loadbalancer/v2/changeLoadBalancedServerInstances";
                List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>();
                parameters.Add(new KeyValuePair<string, string>("responseFormatType", "json"));
                parameters.Add(new KeyValuePair<string, string>("loadBalancerInstanceNo", loadBalancerInstanceNo));
                parameters.Add(new KeyValuePair<string, string>("serverInstanceNoList.1", masterServerInstanceNo));

                SoaCall soaCall = new SoaCall();
                var task = soaCall.WebApiCall(endpoint
                    , RequestType.POST
                    , action
                    , parameters
                    , LogClient.Config.Instance.GetValue(
                        LogClient.Category.Api, LogClient.Key.AccessKey)
                    , LogClient.Config.Instance.GetValue(
                        LogClient.Category.Api, LogClient.Key.SecretKey)
                    );

                string response = await task;
                log.Warn($"ChangeLoadBalancedServerInstances() response : {response}");
                JsonSerializerSettings options = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore
                };

                if (response.Contains("responseError"))
                {
                    hasError hasError = JsonConvert.DeserializeObject<hasError>(response, options);
                    throw new Exception(hasError.responseError.returnMessage);
                }

                changeLoadBalancedServerInstances changeLoadBalancedServerInstances = JsonConvert.DeserializeObject<changeLoadBalancedServerInstances>(response, options);
                if (changeLoadBalancedServerInstances.changeLoadBalancedServerInstancesResponse.returnCode.Equals("0"))
                {
                    log.Warn("change load balancer requested");
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        private async Task SaveClusterServerInfo(string loadBalancerName, string masterServerName)
        {
            try
            {
                await fileDb.ReadTable(FileDb.TableName.TBL_CLUSTER_SERVER);
                List<Tuple<string, string>> tempClusterServer = new List<Tuple<string, string>>();

                foreach (var a in fileDb.TBL_CLUSTER_SERVER.Data)
                    tempClusterServer.Add(new Tuple<string, string>(a.Key.clusterName, a.Key.serverName));

                foreach (var a in tempClusterServer)
                {
                    if (a.Item1.Equals(loadBalancerName, StringComparison.OrdinalIgnoreCase))
                    {
                        var p = new List<KeyValuePair<string, string>>();
                        p.Add(new KeyValuePair<string, string>("clusterName", a.Item1));
                        p.Add(new KeyValuePair<string, string>("serverName", a.Item2));
                        await fileDb.DeleteTable(FileDb.TableName.TBL_CLUSTER_SERVER, p);
                    }
                }

                var clusterServerInfo = new List<KeyValuePair<string, string>>();
                clusterServerInfo.Add(new KeyValuePair<string, string>("clusterName", loadBalancerName));
                clusterServerInfo.Add(new KeyValuePair<string, string>("serverName", masterServerName));
                clusterServerInfo.Add(new KeyValuePair<string, string>("serverRole", "MASTER"));
                await fileDb.UpSertTable(FileDb.TableName.TBL_CLUSTER_SERVER, clusterServerInfo);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void PartnerOffAndRestoreDatabase()
        {
            try
            {
                string connString = config.GetConnectionString(InitialCatalog.Master);
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = queryPartneroffAndRestoreDatabase;
                        cmd.ExecuteNonQuery();
                    }
                    conn.Close();
                    log.Warn("PartnerOffAndRestoreDatabase completed!");
                }
            }
            catch (Exception ex)
            {
                log.Error(string.Format("{0}, {1}", ex.Message, ex.StackTrace));
                throw;
            }
        }

        public async Task<string> GetPartnerInfo(string serverName)
        {
            string partnerServerName = string.Empty;

            try
            {
                List<TBL_CLUSTER_SERVER> tbl_cluster_servers = new List<TBL_CLUSTER_SERVER>();

                await fileDb.ReadTable(FileDb.TableName.TBL_CLUSTER_SERVER);
                foreach (var a in fileDb.TBL_CLUSTER_SERVER.Data)
                {
                    tbl_cluster_servers.Add(new TBL_CLUSTER_SERVER
                    {
                        clusterName = a.Key.clusterName,
                        serverName = a.Key.serverName,
                        serverRole = a.Value.serverRole
                    });
                    log.Info($"clusterName : {a.Key.clusterName}, serverName : {a.Key.serverName}, serverRole {a.Value.serverRole}");
                }

                TBL_CLUSTER_SERVER partnerInstance = new TBL_CLUSTER_SERVER();
                var instance = tbl_cluster_servers.Find(x => x.serverName.Equals(serverName, StringComparison.OrdinalIgnoreCase));
                if (instance != null)
                {
                    if (instance.serverRole.Equals("MASTER", StringComparison.OrdinalIgnoreCase))
                    {
                        partnerInstance = tbl_cluster_servers.Find
                            (x => x.clusterName.Equals(instance.clusterName, StringComparison.OrdinalIgnoreCase)
                            && x.serverRole.Equals("SLAVE", StringComparison.OrdinalIgnoreCase));
                    }
                    else
                    {
                        partnerInstance = tbl_cluster_servers.Find
                            (x => x.clusterName.Equals(instance.clusterName, StringComparison.OrdinalIgnoreCase)
                            && x.serverRole.Equals("MASTER", StringComparison.OrdinalIgnoreCase));
                    }
                }

                if (partnerInstance != null && partnerInstance.serverName != null)
                    partnerServerName = partnerInstance.serverName.Trim();
                else
                    partnerServerName = ""; 
            }
            catch (Exception ex)
            {
                log.Error(string.Format("{0}, {1}", ex.Message, ex.StackTrace));
            }
            finally
            {
                if (partnerServerName == null || partnerServerName.Length < 1)
                    partnerServerName = "";
            }

            return partnerServerName;
        }


        private string GetServerName()
        {
            string ServerName = config.GetValue(Category.Ha, Key.ServerName).ToLower();
            if (ServerName.Trim().Length < 1)
                ServerName = Environment.MachineName.ToLower();
            return ServerName;
        }

        private string GetHealthStatus()
        {
            string status = "failed";
            try
            {
                using (SqlConnection conn = new SqlConnection(config.GetConnectionString(InitialCatalog.Master)))
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = @"select top 1 'normal' as healthStatus";
                        cmd.CommandTimeout = 5;
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            status = config.DatabaseValue<string>(reader["healthStatus"]);
                        }
                        reader.Close();
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                log.Error($@"GetHealthStatus failed : {ex.Message}, healthStatus : {status}");
            }

            return status;
        }

        string queryPartneroffAndRestoreDatabase =
            @"
SET NOCOUNT ON
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED 

DECLARE @SERVICENAME NVARCHAR(100), @SQLSERVER_START_TIME DATETIME

DECLARE @TBL_DB_LISTS TABLE 
( IDX INT IDENTITY(1,1)
, DATABASE_NAME NVARCHAR(100)
, HAS_DBACCESS_STATE INT
, MIRRORING_STATE_DESC VARCHAR(100)
)

INSERT INTO @TBL_DB_LISTS (DATABASE_NAME, HAS_DBACCESS_STATE, MIRRORING_STATE_DESC) 
SELECT  
     DB_NAME(SD.[DATABASE_ID]) DATABASE_NAME
    ,HAS_DBACCESS(DB_NAME(SD.[DATABASE_ID])) HAS_DBACCESS_STATE
    ,SD.MIRRORING_STATE_DESC            
FROM SYS.DATABASE_MIRRORING AS SD
    JOIN SYS.DATABASES B
    ON DB_NAME(SD.DATABASE_ID) = B.NAME 
WHERE DB_NAME(SD.DATABASE_ID) NOT IN ('TEMPDB', 'MODEL') 
	AND MIRRORING_STATE_DESC IS NOT NULL 

DECLARE @MAX_IDX INT , @CURRENT_DATABASE_NAME NVARCHAR(100), @SQL NVARCHAR(MAX) , @RETRYCNT INT = 3 


WHILE (@RETRYCNT > 0)
BEGIN 
	SELECT @MAX_IDX = MAX(IDX) FROM @TBL_DB_LISTS
	WHILE (@MAX_IDX > 0)
	BEGIN
		SELECT @CURRENT_DATABASE_NAME = DATABASE_NAME 
		FROM @TBL_DB_LISTS 
		WHERE IDX = @MAX_IDX 

		BEGIN TRY
			SET @SQL = N'ALTER DATABASE [DBNAME] SET PARTNER OFF'
			SET @SQL = REPLACE (@SQL, 'DBNAME', @CURRENT_DATABASE_NAME)
			EXEC (@SQL)
			PRINT @SQL 
		END TRY
		BEGIN CATCH 
		END CATCH

		waitfor delay '00:00:02.000'

		BEGIN TRY
			SET @SQL = N'RESTORE DATABASE [DBNAME] WITH RECOVERY'
			SET @SQL = REPLACE (@SQL, 'DBNAME', @CURRENT_DATABASE_NAME)
			EXEC (@SQL)
			PRINT @SQL 
 		END TRY
		BEGIN CATCH 
		END CATCH

		SET @MAX_IDX = @MAX_IDX - 1
	END
	SET @RETRYCNT = @RETRYCNT - 1 
END 


";
    }

}
