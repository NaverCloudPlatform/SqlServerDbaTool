using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using LogClient;
using System.Data.SqlClient;

namespace lazylog.AutoFailover
{
    public class HeartBeat : IManager
    {
        Log log = Log.Instance;
        Config config = Config.Instance;
        FileDb fileDb = FileDb.Instance;

        public bool IsRunning { get; set; }

        public async void Start()
        {
            IsRunning = true;
            var task = Task.Delay(1000);
            await task;

            while (IsRunning)
            {
                try
                {
                    if (!int.TryParse(config.GetValue(Category.Ha, Key.HeartBeatIntervalSec), out int RunIntervalSec))
                        RunIntervalSec = 0;
                    DateTime endTime = DateTime.Now.Add(new TimeSpan(0, 0, RunIntervalSec));

                    if (RunIntervalSec == 0)
                    {
                        IsRunning = false;
                        log.Warn("Ha (HeartBeat) can't start because HeartBeatIntervalSec is 0");
                        break;
                    }

                    Debug.WriteLine("HeartBeatStarted");
                    await HeartBeatWrite();

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
            log.Warn("ha heartbeat stop");
            IsRunning = false;
            log.Warn("ha heartbeat stoped");
        }
        class TBL_HEALTH_INFO
        {
            public string serverName {get;set;}
            public string time { get; set; }
            public string healthInfo { get; set; }
        }


        public async Task HeartBeatWrite()
        {
            string filePath =  @"heartBeat/" + config.GetServerName();
            List<TBL_HEALTH_INFO> purgeTarget = new List<TBL_HEALTH_INFO>();
            long cutTime = long.Parse(DateTime.Now.Add(new TimeSpan(0, 0, -60)).ToString("yyyyMMddHHmmss"));
            await fileDb.ReadTable(FileDb.TableName.TBL_HEALTH_INFO, filePath);
            foreach (var a in fileDb.TBL_HEALTH_INFO.Data)
            {
                if (!long.TryParse(a.Key.time, out long storedTime))
                    storedTime = long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss"));
                if (storedTime < cutTime)
                    purgeTarget.Add(new TBL_HEALTH_INFO
                    {
                        serverName = a.Key.serverName,
                        time = a.Key.time,
                        healthInfo = a.Value.healthInfo
                    });
            }

            foreach (var a in purgeTarget)
            {
                var dp = new List<KeyValuePair<string, string>>();
                dp.Add(new KeyValuePair<string, string>("serverName", a.serverName));
                dp.Add(new KeyValuePair<string, string>("time", a.time));
                await fileDb.DeleteTable(FileDb.TableName.TBL_HEALTH_INFO, dp, filePath);
            }

            string healthStatus = GetHealthStatus();
            string serverName = config.GetServerName(); 
            var p = new List<KeyValuePair<string, string>>();
            p.Add(new KeyValuePair<string, string>("serverName", serverName));
            p.Add(new KeyValuePair<string, string>("time", DateTime.Now.ToString("yyyyMMddHHmmss")));
            p.Add(new KeyValuePair<string, string>("healthInfo", healthStatus));

            await fileDb.UpSertTable(FileDb.TableName.TBL_HEALTH_INFO, p, filePath);
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

    }

}
