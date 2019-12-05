using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using NLog;
using CsLib; 
namespace LogClient
{

    public class Log
    {
        private static Logger nlog = LogManager.GetCurrentClassLogger();
        private static Config config = Config.Instance;
        public static Log Instance { get { return lazy.Value; } }
        private static Thread thread;

        private static readonly Lazy<Log> lazy =
            new Lazy<Log>(() => new Log(), LazyThreadSafetyMode.ExecutionAndPublication);

        LogQueue logger;

        public Log()
        {
            LogSender logSender = new LogSender();
            thread = new Thread(logSender.SendLog);
            thread.IsBackground = true;
            thread.Start(); 

            logger = LogQueue.Instance;
        }
        
        public void Info(string data)
        {
            logger.WriteLine(LogLevel.INFO, data);
#if (DEBUG)
            Console.WriteLine(data);
#endif
        }

        public void Warn(string data)
        {
            logger.WriteLine(LogLevel.WARN, data);
#if (DEBUG)
            Console.WriteLine(data);
#endif
        }

        public void Error(string data)
        {
            logger.WriteLine(LogLevel.ERROR, data);
#if (DEBUG)
            Console.WriteLine(data);
#endif
        }


    }
}
