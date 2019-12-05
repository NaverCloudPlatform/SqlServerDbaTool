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
    abstract class BaseSender
    {
        internal Config config;
        internal Log log;
        internal object data;

        protected BaseSender(object data)
        {
            try
            {
                config = Config.Instance;
                log = Log.Instance;
                this.data = data;
                this.LocalIp = config.LocalIp;
                this.LocalPort = config.LocalPort;
                Initialization();
            }
            catch (Exception ex)
            {
                log.Error(string.Format("{0}, {1}", ex.Message, ex.StackTrace));
            }
        }

        protected BaseSender(string CurrentTableName, string BaseTableName, DateTime ProbeTime)
        {
            try
            {
                config = Config.Instance;
                log = Log.Instance;
                Initialization();
                this.CurrentTableName = CurrentTableName;
                this.BaseTableName = BaseTableName;
                this.LocalIp = config.LocalIp;
                this.LocalPort = config.LocalPort;
                this.ProbeTime = ProbeTime;
            }
            catch (Exception ex)
            {
                log.Error(string.Format("{0}, {1}", ex.Message, ex.StackTrace));
            }
        }

        protected async void SendToGeneralRepository(string json, string action)
        {
            try
            {
                if (json.Trim() != "")
                {

                    log.Warn("SendToGeneralRepository");
                    Task<string> response = new SoaCall().WebApiCall(
                    EndPointUrl,
                    RequestType.POST,
                    action,
                    json
                    );
                    string responseString = await response;
                    log.Warn(responseString);
                }
            }
            catch (Exception ex)
            {
                log.Error(string.Format("{0}, {1}", ex.Message, ex.StackTrace));
            }
        }

        protected abstract void Initialization();
        protected abstract string GetData();
        internal abstract void SendData();


        protected string EndPointUrl = string.Empty;
        protected string LocalIp = string.Empty;
        protected string LocalPort = string.Empty; 
        protected string CurrentTableName = string.Empty;
        protected string BaseTableName = string.Empty;
        protected DateTime ProbeTime;
        protected string MachineName = string.Empty;
        protected string FullInstanceName = string.Empty;
        protected string GetLatestResultsInJsonQuery = string.Empty;
        protected string GetDataQuery = string.Empty;

    }
}
