using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogClient;
using System.Threading;

namespace lazylog.Backup
{
    class BackupManager : IManager
    {
        Log log = Log.Instance;
        Config config = Config.Instance;

        FullBackup fullBackupInstance;
        LogBackup logBackupInstance;
        bool IsInitSuccess = false;
        public BackupManager()
        {
            while (!config.ConfigLoaded)
            {
                Thread.Sleep(1000);
                log.Warn("BackupManager reading lazylogConfog.txt... please wait");
            }
            fullBackupInstance = new FullBackup();
            logBackupInstance = new LogBackup();
            IsInitSuccess = true;
        }
        public void Start()
        {
            log.Warn("backupmon start");
            if (IsInitSuccess)
            {
                new Thread(() => fullBackupInstance.Start()).Start();
                new Thread(() => logBackupInstance.Start()).Start();
                log.Warn("backupmon started");
            }

        }
        public void Stop()
        {
            try
            {
                log.Warn("backupmon stop");
                if (IsInitSuccess)
                {
                    fullBackupInstance.Stop();
                    logBackupInstance.Stop();
                    log.Warn("backupmon stoped");
                }
            }
            catch (Exception ex)
            {
                log.Error(string.Format("{0}, {1}", ex.Message, ex.StackTrace));
            }
        }


    }
}
