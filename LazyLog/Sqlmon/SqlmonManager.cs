
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

namespace lazylog
{
    class SqlmonManager : IManager
    {
        Log log = Log.Instance;
        Config config = Config.Instance;

        sp_lock2 sp_lock2_instance;
        dm_os_workers dm_os_workers_instance;
        sp_readerrorlog sp_readerrorlog_instance;
        sp_readagentlog sp_readagentlog_instance;
        dm_exec_query_stats dm_exec_query_stats_instance;
        bool IsInitSuccess = false;
        public SqlmonManager()
        {
            while (!config.ConfigLoaded)
            {
                Thread.Sleep(1000);
                log.Warn("SqlmonManager reading lazylogConfog.txt... please wait");
            }
            sp_lock2_instance = new sp_lock2();
            dm_os_workers_instance = new dm_os_workers();
            sp_readerrorlog_instance = new sp_readerrorlog();
            sp_readagentlog_instance = new sp_readagentlog();
            dm_exec_query_stats_instance = new dm_exec_query_stats();
            IsInitSuccess = true;
        }

        public void Start()
        {
            log.Warn("sqlmon start");
            if (IsInitSuccess)
            {
                new Thread(() => sp_lock2_instance.Start()).Start();
                new Thread(() => dm_os_workers_instance.Start()).Start();
                new Thread(() => sp_readerrorlog_instance.Start()).Start();
                new Thread(() => sp_readagentlog_instance.Start()).Start();
                new Thread(() => dm_exec_query_stats_instance.Start()).Start();
                log.Warn("sqlmon started");
            }
        }

        public void Stop()
        {
            try
            {
                log.Warn("sqlmon stop");
                if (IsInitSuccess)
                {
                    sp_lock2_instance.Stop();
                    dm_os_workers_instance.Stop();
                    sp_readerrorlog_instance.Stop();
                    sp_readagentlog_instance.Stop();
                    dm_exec_query_stats_instance.Stop();
                    log.Warn("sqlmon stoped");
                }
            }
            catch (Exception ex)
            {
                log.Error(string.Format("{0}, {1}", ex.Message, ex.StackTrace));
            }
        }

        
    }
}
