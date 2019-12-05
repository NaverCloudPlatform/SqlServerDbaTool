using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogClient;
using System.Threading;
using System.Diagnostics;
using System.ServiceProcess;
using System.IO;
using System.Timers;


// ncp beta deplyed....2019.05.22

namespace lazylog
{

    class StartUpCondition
    {
        public static bool isExistsProcessWithSameName()
        {
            int processCnt = 0;
            Process curProcess = Process.GetCurrentProcess();
            Process[] processes = Process.GetProcessesByName(curProcess.ProcessName);
            foreach (var process in processes)
            {
                if (process.Id != curProcess.Id)
                {
                    processCnt++;
                }
            }
            if (processCnt > 0)
                return true;
            else
                return false;
        }
    }

    class Program
    {
        static Log log = Log.Instance;

        static void Main(string[] args)
        {
            if (StartUpCondition.isExistsProcessWithSameName())
            {
                log.Warn("isExistsProcessWithSameName : true and exit!");
                return;
            }

            log.Warn($"Environment.UserName : {Environment.UserName}");
            log.Warn($"Environment.UserInteractive : {Environment.UserInteractive}");
            HealthReporter.Instance.Start();

            if (Environment.UserInteractive)
            {
                //Execute by Console
                log.Warn("UserInteractive true");
                Service1 service1 = new Service1();
                service1.TestStartupAndStop(args);
                while (service1.IsRunning)
                {
                    Console.WriteLine("----------------");
                    Console.WriteLine("Press Y to STOP!");
                    Console.WriteLine("----------------");
                    Thread.Sleep(500);
                    ConsoleKeyInfo result = Console.ReadKey();
                    if (result.KeyChar.ToString().Equals("Y", StringComparison.OrdinalIgnoreCase))
                    {
                        service1.Stop();
                        Environment.Exit(0);
                    }
                }
            }

            if (!Environment.UserInteractive && Environment.UserName.Equals("SYSTEM", StringComparison.OrdinalIgnoreCase))
            {
                //Execute by SqlServer or Task Scheduler
                log.Warn("UserInteractive UserName SYSTEM");
                log.Warn("UserInteractive false");

                Service1 service1 = new Service1();
                service1.TestStartupAndStop(args);
                while (service1.IsRunning)
                    Thread.Sleep(500);
            }

            if (!Environment.UserInteractive)
            {
                //Execute by Service
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[] { new Service1() };
                ServiceBase.Run(ServicesToRun);
            }
        }
    }
}
