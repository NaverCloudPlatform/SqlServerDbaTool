using System;
using System.Linq;
using System.ServiceProcess;
using System.Threading;
using LogClient; 
using System.Collections.Generic;
using lazylog.Backup;
using lazylog.AutoFailover;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;

namespace lazylog
{
    class Service1 : ServiceBase
    {
        Log log = Log.Instance;
        private bool isRunning = false;
        IManager perfmonManager;
        IManager sqlmonManager;
        IManager backupManager;
        IManager haManager; 
        WcfRestServer wcfRestServer;

        public static Dictionary<string, object> appInstances = new Dictionary<string, object>();

        public bool IsRunning
        {
            get { return isRunning; }
            set { isRunning = value; }
        }
        private System.ComponentModel.IContainer components = null;
        
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
             
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            this.ServiceName = "Service1";
        }

        public Service1()
        {
            InitializeComponent();
            wcfRestServer = new WcfRestServer(appInstances);
            wcfRestServer.Start();
            perfmonManager = new PerfmonManager();
            sqlmonManager = new SqlmonManager();
            backupManager = new BackupManager();
            haManager = new HaManager();
            appInstances.Add("PerfmonManager", perfmonManager);
            appInstances.Add("SqlmonManager", sqlmonManager);
            appInstances.Add("BackupManager", backupManager);
            appInstances.Add("HaManager", haManager);
            log.Warn("app prepare");
        }

        public void ApplyFolderSecurityHardening(string folder, string account)
        {
            var directoryInfo = new DirectoryInfo(folder);
            var security = directoryInfo.GetAccessControl();

            // remove inheritance
            security.SetAccessRuleProtection(true, true);
            directoryInfo.SetAccessControl(security);

            // remove user group 
            security = directoryInfo.GetAccessControl();
            var rules = security.GetAccessRules(true, true, typeof(NTAccount));
            foreach (FileSystemAccessRule rule in rules)
            {
                if (rule.IdentityReference.Value == account)
                    security.RemoveAccessRuleSpecific(rule);
            }
            directoryInfo.SetAccessControl(security);
        }


        protected override void OnStart(string[] args)
        {
            isRunning = true;
            log.Warn("app Start!");

            log.Warn("ACL Set Started!");
            ApplyFolderSecurityHardening(AppDomain.CurrentDomain.BaseDirectory, @"BUILTIN\Users");
            log.Warn("ACL Set Completed!");

            Config config = Config.Instance;

            foreach (var a in appInstances)
            {
                ((IManager)a.Value).Start();
            }
            log.Warn("app Started!");
        }

        internal void TestStartupAndStop(string[] args)
        {
            this.OnStart(args);
        }
        protected override void OnStop()
        {
            isRunning = false;
            
            perfmonManager.Stop();
            sqlmonManager.Stop();
            backupManager.Stop();
            haManager.Stop();
            wcfRestServer.Stop();
        }

    }
}
