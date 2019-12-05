using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HaTool.Tools
{
    public delegate void ThreadCallBackMethod(string ip, string port, string database, bool success, int errorCount, StringBuilder results);

    class PoolWorker
    {
        bool CancelRequested = false; 
        public PoolWorkerResponseCallback PoolWorkerResponseCallbackMethodName { get; set; }
        public PoolWorkerCompleteCallback PoolWorkerCompleteCallbackMethodName { get; set; }
        public int SetMinThreads { get; set; }
        public int SetMaxThreads { get; set; }
        public Dictionary<MultiServerKey, MultiServerValue> DicAllServerGroup { get; set; }
        public string SelectedServerGroup { get; set; }
        public int CommamdTimeoutSec { get; set; }
        public int ConnectionTimeoutSec { get; set; }
        public int ThreadCount { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
        public string Querys { get; set; }
        public string ColumnDelimiter { get; set; }
        public void PoolWorkerCancel()
        {
            if (mainWorkerList.Count > 0)
            {
                CancelRequested = true; 
                foreach (var worker in mainWorkerList)
                {
                    worker.CancelWork();
                }
            }
        }
        public int ThinkTimeSec { get; set; }

        public List<MainWorker> mainWorkerList;

        public PoolWorker
            (
             Dictionary<MultiServerKey, MultiServerValue> MultiServerGroupDic
            , string selectedServerGroup
            , int connectionTimeoutSec
            , int commandTimeoutSec
            , int threadCount
            , string userId
            , string password
            , string querys
            , PoolWorkerResponseCallback poolWorkerResponseCallback
            , PoolWorkerCompleteCallback poolWorkerCompleteCallback
            , int thinkTimeSec
            , string columnDelimiter
            )
        {
            DicAllServerGroup = MultiServerGroupDic;
            SelectedServerGroup = selectedServerGroup;
            ConnectionTimeoutSec = connectionTimeoutSec;
            CommamdTimeoutSec = commandTimeoutSec;
            ThreadCount = threadCount;
            UserId = userId;
            Password = password;
            Querys = querys;
            PoolWorkerCompleteCallbackMethodName = poolWorkerCompleteCallback;
            PoolWorkerResponseCallbackMethodName = poolWorkerResponseCallback;
            mainWorkerList = new List<MainWorker>();
            ThinkTimeSec = thinkTimeSec;
            ColumnDelimiter = columnDelimiter;
        }

        ManualResetEvent manualResetEvent = new ManualResetEvent(false);

        public void DoWork()
        {
            ThreadPool.SetMinThreads(ThreadCount, ThreadCount);
            List<ManualResetEvent> manualResetEvents = new List<ManualResetEvent>();
                        
            foreach (var a in DicAllServerGroup)
            {
                if (Convert.ToBoolean(a.Value.TrueFalse) && a.Key.GroupName.Equals(SelectedServerGroup, StringComparison.OrdinalIgnoreCase))
                {
                    ThreadPoolStatus threadPoolStatus = new ThreadPoolStatus();
                    threadPoolStatus.signal = new ManualResetEvent(false);
                    threadPoolStatus.stop = manualResetEvent;

                    manualResetEvents.Add(threadPoolStatus.signal);
                    MainWorker mainWorker =
                        new MainWorker(ThreadCompletedAndExecuteMethod
                        , a.Key.Ip
                        , a.Key.Port
                        , UserId
                        , Password
                        , a.Key.Database
                        , ConnectionTimeoutSec
                        , CommamdTimeoutSec
                        , Querys
                        , ColumnDelimiter
                        );

                    mainWorkerList.Add(mainWorker);
                    ThreadPool.QueueUserWorkItem(mainWorker.DoWork, threadPoolStatus);
                }
            }


            DateTime endTime = DateTime.Now.Add(new TimeSpan(0, 0, ThinkTimeSec)); 

            while (DateTime.Now < endTime)
            {
                if (CancelRequested)
                {
                    break; 
                }

                Thread.Sleep(100);
             
            }

            if (!CancelRequested)
            {

                // 동시에 시작~ 고~
                manualResetEvent.Set();
                WaitForAll(manualResetEvents);
            }
            PoolWorkerCompleteCallbackMethodName();
        }

        public void ThreadCompletedAndExecuteMethod(string ip, string port, string database, bool success, int errorCount, StringBuilder resultMessage)
        {
            Debug.WriteLine(string.Format("{0} / {1} / {2}", success, errorCount, resultMessage.ToString()));
            PoolWorkerResponseCallbackMethodName(ip, port, database, success, errorCount, resultMessage);
        }

        static void WaitForAll(List<ManualResetEvent> manualResetEvents)
        {
            if (manualResetEvents == null) return;
            foreach (ManualResetEvent t in manualResetEvents)
            {
                t.WaitOne();
            }
        }
    }

    public class ThreadPoolStatus
    {
        public ManualResetEvent signal;
        public ManualResetEvent stop;
    }
}
