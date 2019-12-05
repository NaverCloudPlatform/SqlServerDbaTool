using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Concurrent;
using NLog;

namespace LogClient
{
    public class LogQueue
    {
        private static Logger nlog = LogManager.GetCurrentClassLogger();

        public static LogQueue Instance { get { return lazy.Value; } }
        private static Thread thread;
        private static readonly Lazy<LogQueue> lazy =
            new Lazy<LogQueue>(() => new LogQueue(), LazyThreadSafetyMode.ExecutionAndPublication);

        private volatile bool isRunning = true;

        public bool IsRunning {
            get { return isRunning; }
            set { isRunning = value; }
        }

        ConcurrentQueue<LogDataArgs> logDatas;
        public Action<object, LogDataArgs> LogEvent;

        LogQueue()
        {
            logDatas = new ConcurrentQueue<LogDataArgs>();
            thread = new Thread(DeQueue);
            thread.IsBackground = true;
            thread.Start();
            //new Thread(() => DeQueue()).Start();
        }

        public void WriteLine(LogLevel level, string data)
        {
            try
            {
                if (LogLevel.INFO == level)
                {
                    nlog.Info(data);
                }
                else if (LogLevel.WARN == level)
                {
                    nlog.Warn(data);
                }
                else if (LogLevel.ERROR == level)
                {
                    nlog.Error(data);
                }

                long currentTimestamp = (long)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                logDatas.Enqueue(
                    new LogDataArgs
                    {
                        UtcTimestamp = currentTimestamp,
                        Level = level,
                        Data = data
                    });
            }
            catch (Exception ex)
            {
                nlog.Error(string.Format("{0}, {1}", ex.Message, ex.StackTrace));
            }
        }
        
        private void DeQueue ()
        {
            LogDataArgs data; 
            while (isRunning)
            {
                if (logDatas.TryDequeue(out data))
                {
                    if (data != null)
                        LogEvent?.Invoke(this, data);
                    else
                        Thread.Sleep(200);
                }
                else
                    Thread.Sleep(200);
            }
        }


    }


}
