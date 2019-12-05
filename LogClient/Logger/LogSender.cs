using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using NLog;
using Newtonsoft.Json;
using System.IO;
using CsLib; 

namespace LogClient
{

    public class LogSender
    {
        private static Logger nlog = LogManager.GetCurrentClassLogger();
        Config config; 
        public static LogSender Instance { get { return lazy.Value; } }
        
        private static readonly Lazy<LogSender> lazy =
            new Lazy<LogSender>(() => new LogSender(), LazyThreadSafetyMode.ExecutionAndPublication);

        private volatile bool isRunning = true;
        private long currentDataCnt = 0;
        private long sendLogCnt = 0; 
        private DateTime firstReceiveTime = DateTime.Now;
        string ConfigLogType = string.Empty; 
        readonly Object Lock = new object();

        public bool IsRunning
        {
            get { return isRunning; }
            set { isRunning = value; }
        }

        List<LogDataArgs> buffer;
        public LogSender()
        {
            buffer = new List<LogDataArgs>();
            LogQueue.Instance.LogEvent += DataBuffering;
            config = Config.Instance; 
            firstReceiveTime = DateTime.Now;
            ConfigLogType = config.GetValue(Category.Config, Key.LogType);
        }

        private void DataBuffering(object o, LogDataArgs args)
        {
            lock (Lock)
            {
                buffer.Add(args);
                currentDataCnt = currentDataCnt + 1;
            }
        }

        public void SendLog()
        {
            while (isRunning)
            {
                try
                {
                    if (ConfigLogType.Equals("Local", StringComparison.OrdinalIgnoreCase))
                        Send();
                    else
                    {
                        if ((DateTime.Now - firstReceiveTime).TotalSeconds > Int32.Parse(Config.Instance.GetValue(Category.Config, Key.LogQueueDelaySecond))
                            || (currentDataCnt > Int32.Parse(Config.Instance.GetValue(Category.Config, Key.LogQueueDataCount))))
                        {
                            if (currentDataCnt > 0)
                                Send();
                        }
                    }
                    Thread.Sleep(1000);

                    sendLogCnt++;
                    if (sendLogCnt > 1000)
                    {
                        Common.FileDeleteFromDate(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log"), "*.log", new TimeSpan(24 * 7, 0, 0));
                        sendLogCnt = 0;
                    }
                }
                catch (Exception ex)
                {
                    nlog.Error(string.Format("{0}, {1}", ex.Message, ex.StackTrace));
                }
            }
        }
        
        private void Send()
        {
            lock (Lock)
            {
                List<LogDataArgs> permitLogLists = new List<LogDataArgs>();
                try
                {
                    int logLevel = LogLevelQuery().Result;
                    //int logLevel = LogLevelQuery();
                    foreach (var a in buffer)
                    {
                        if ((int)a.Level >= logLevel)
                        {
                            permitLogLists.Add(a);
                        }
                    }

                    if (permitLogLists.Count > 0)
                    {
                        AppLog joyLog = new AppLog
                        {
                            AppName = Config.Instance.GetValue(Category.Config, Key.AppName),
                            GUID = Config.Instance.GUID,
                            ClientIpAddress = Config.Instance.LocalIp,
                            LogData = permitLogLists
                        };
                        var jsonLog = JsonConvert.SerializeObject(joyLog);
                        nlog.Info(jsonLog);

                        if (!ConfigLogType.Equals("Local", StringComparison.OrdinalIgnoreCase))
                        {
                            SoaCall asyncCall = new SoaCall();
                            Task<string> t = asyncCall.WebApiCall(config.Url(RequestUrlType.LOG), RequestType.POST, @"Api/Log", jsonLog.ToString(), config.GetValue(Category.Config, Key.AccessKey), config.GetValue(Category.Config, Key.SecretKey));
                            t.Wait();
                        }
                    }
                }
                catch (Exception ex)
                {
                    nlog.Error(string.Format("{0}, {1}", ex.Message, ex.StackTrace));
                }
                finally
                {
                    buffer.Clear();
                    permitLogLists.Clear();
                    firstReceiveTime = DateTime.Now;
                    currentDataCnt = 0;
                }
            }
        }

        private async Task<int> LogLevelQuery()
        {
            int returnValue = 0;
            try
            {
                var postParams = new List<KeyValuePair<string, string>>();
                postParams.Add(new KeyValuePair<string, string>("AppName", Config.Instance.GetValue(Category.Config, Key.AppName)));
                postParams.Add(new KeyValuePair<string, string>("ClientIpAddress", Config.Instance.LocalIp));
                postParams.Add(new KeyValuePair<string, string>("Guid", Config.Instance.GUID));

                if (!ConfigLogType.Equals("Local", StringComparison.OrdinalIgnoreCase))
                {
                    SoaCall asyncCall = new SoaCall();
                    var task = asyncCall.WebApiCall(config.Url(RequestUrlType.LOG), RequestType.POST, @"Api/Log/LogLevelQuery", postParams, config.GetValue(Category.Config, Key.AccessKey), config.GetValue(Category.Config, Key.SecretKey));
                    string t = await task; 
                    returnValue = int.Parse(t.Replace("\"", ""));
                    nlog.Info(string.Format("return LogLevelQuery Result : {0}", returnValue));
                }
                else
                {
                    returnValue = 3; 
                }
            }
            catch (Exception ex)
            {
                nlog.Error("{0},{1}", ex.Message, ex.StackTrace);
                returnValue = 0;
            }
            return returnValue;
        }

    }
}
