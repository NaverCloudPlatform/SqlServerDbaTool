using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Reflection;
using CsLib;
using Newtonsoft.Json;

namespace LogClient
{
    public class HealthReporter
    {
        static Log log = Log.Instance;

        private volatile bool IsRunning = true;
        Config config = Config.Instance;
        SoaCall soaCall = new SoaCall();
        private int RunIntervalSec = 60;


        public static HealthReporter Instance { get { return lazy.Value; } }

        private static readonly Lazy<HealthReporter> lazy =
            new Lazy<HealthReporter>(
                () => new HealthReporter()
                , LazyThreadSafetyMode.ExecutionAndPublication
                );

        public void Start()
        {
            new Thread(() => StartThread()).Start();
        }

        private void StartThread()
        {
            IsRunning = true;
            RunIntervalSec = Int32.Parse(config.GetValue(Category.HealthReport, Key.HealthReportIntervalSec));

            while (IsRunning)
            {
                try
                {
                    DateTime endTime = DateTime.Now.Add(new TimeSpan(0, 0, RunIntervalSec));  // 3초마다 동작 

                    if (RunIntervalSec == 0)
                    {
                        IsRunning = false;
                        log.Warn("HealthReport can't start because HealthReportIntervalSec is 0");
                        break;
                    }
                    else
                    {
                        HealthReport();

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


        public void Stop()
        {
            IsRunning = false;
        }

        public void HealthReport()
        {
            try
            {
                string postParms = JsonConvert.SerializeObject(
                    new HealthReport
                        {
                            AppName = config.GetValue(Category.Config, Key.AppName),
                            Version = Assembly.GetEntryAssembly().GetName().Version.ToString(),
                            ClientIp = Common.GetLocalIpAddress((IpType)Enum.Parse(typeof(IpType), config.GetValue(Category.Config, Key.HealthRepoterIpType))),
                            Guid = config.guid.ToString()
                        });

                string json = soaCall.WebApiCall(
                    config.GetValue(Category.HealthReport, Key.HealthReportUrl),
                    RequestType.POST,
                    "Api/HealthReport",
                    postParms).Result;  

                log.Warn(json);
            }
            catch (Exception ex)
            {
                log.Error(string.Format("{0}, {1}", ex.Message, ex.StackTrace));
            }
        }
    }

}
