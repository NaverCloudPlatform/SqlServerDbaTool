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
    class PerfmonManager : IManager
    {
        Log log = Log.Instance;
        Config config = Config.Instance;
        PerfmonInit perfmonInit; 
        PerfmonProbe perfmonProbe;
        bool IsInitSuccess = false; 
        public PerfmonManager()
        {
            while (!config.ConfigLoaded)
            {
                Thread.Sleep(1000);
                log.Warn("PerfmonManager reading lazylogConfog.txt... please wait");
            }
            perfmonInit = new PerfmonInit();
            perfmonInit.CheckRepository();
            perfmonProbe = new PerfmonProbe();
            IsInitSuccess = true; 
        }

        public void Start()
        {
            log.Warn("perfmon start");
            if (IsInitSuccess)
                perfmonProbe.StartTimer();
        }
        public void Stop()
        {
            log.Warn("perfmon stop");
            if (IsInitSuccess)
                perfmonProbe.StopTimer();
        }
    }
}
