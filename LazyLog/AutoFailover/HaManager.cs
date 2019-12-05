using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogClient;
using System.Threading;

namespace lazylog.AutoFailover
{
    class HaManager : IManager
    {
        Log log = Log.Instance;
        Config config = Config.Instance;

        HeartBeat heartBeat;
        HeartBeatCheck heartBeatCheck;
        bool IsInitSuccess = false; 

        public HaManager()
        {
            while(!config.ConfigLoaded)
            {
                Thread.Sleep(1000);
                log.Warn("HaManager reading lazylogConfig.txt... please wait");
            }
            heartBeat = new HeartBeat();
            heartBeatCheck = new HeartBeatCheck();
            IsInitSuccess = true; 
        }

        public void Start()
        {
            log.Warn("haManager start");
            if(IsInitSuccess)
            {
                //    new Thread(() => heartBeat.Start());
                //    new Thread(() => heartBeatCheck.Start());

                Task.Factory.StartNew(() => heartBeat.Start());
                Task.Factory.StartNew(() => heartBeatCheck.Start());
                log.Warn("haManager started");
            }
        }
        public void Stop()
        {
            try
            {
                log.Warn("haManager stop");
                if (IsInitSuccess)
                {
                    heartBeat.Stop();
                    heartBeatCheck.Stop();
                    log.Warn("haManager stoped");
                }
            }
            catch (Exception ex)
            {
                log.Error(string.Format("{0}, {1}", ex.Message, ex.StackTrace));
            }
        }
    }
}
