using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogClient;
using System.Data.SqlClient;
using System.Data;
using Newtonsoft.Json;
using CsLib; 
namespace lazylog
{
    abstract class PerfmonSender
    {
        internal Config config;
        internal Log log;
        public string CounterDataOriginCurrentTableName = string.Empty; 

        public PerfmonSender(string CounterDataOriginCurrentTableName)
        {
            config = Config.Instance;
            log = Log.Instance;
            LoadWebConfig();
            this.CounterDataOriginCurrentTableName = CounterDataOriginCurrentTableName;
            this.LocalIp = Common.GetLocalIpAddress(IpType.LocalFirstIp);
        }

        public abstract void LoadWebConfig();
        public abstract void SendData();

        public string LocalIp = string.Empty;
        public string EndPointUrl = string.Empty;
        public string PerfmonQuery = string.Empty;
    }
}
