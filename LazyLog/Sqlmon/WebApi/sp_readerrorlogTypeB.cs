using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogClient;
using Newtonsoft.Json;
using CsLib; 

namespace lazylog
{
    

    class sp_readerrorlogTypeB : BaseSender
    {
        public sp_readerrorlogTypeB(object data) 
            : base (data)
        { }

        private string ClaUserKey = string.Empty;
        private string ClaLogTypes = string.Empty; 
        protected override void Initialization()
        {
            try
            {
                EndPointUrl = config.GetValue(Category.CLA, Key.URL);
                MachineName = (config.GetValue(Category.GLOBAL, Key.hostname) == "") ? Environment.MachineName : config.GetValue(Category.GLOBAL, Key.hostname);
                LocalIp = (config.GetValue(Category.GLOBAL, Key.private_ip) == "") ? LocalIp : config.GetValue(Category.GLOBAL, Key.private_ip);
                FullInstanceName = config.GetValue(Category.GLOBAL, Key.servicename);
                ClaUserKey = "MSSQL" + config.GetValue(Category.CLA, Key.UserKey);
                ClaLogTypes = config.GetValue(Category.CLA, Key.LogTypes);
            }
            catch (Exception ex)
            {
                log.Error(string.Format("{0}, {1}", ex.Message, ex.StackTrace));
            }
        }

        protected override string GetData()
        {
            List<sp_readerrorlog_TypeB> listdata = new List<sp_readerrorlog_TypeB>();
            try
            {
                Dictionary<Tuple<DateTime, string, string>, string>
                    originalData = (Dictionary<Tuple<DateTime, string, string>, string>)data;


                long probe_timestamp = (long)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;

                foreach (var a in originalData)
                {
                    listdata.Add(new sp_readerrorlog_TypeB
                    {
                        key = this.ClaUserKey,
                        type = this.ClaLogTypes,
                        timestamp = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"),
                        subject = "sp_readerrorlog",
                        hostname = this.MachineName.ToLower(),
                        ip = this.LocalIp,
                        probe_time = probe_timestamp,
                        LogDate = (long)(a.Key.Item1.ToUniversalTime().Subtract(new DateTime(1970, 1, 1))).TotalSeconds,
                        ProcessInfo = a.Key.Item2,
                        LogFileType = "ErrorLog",
                        message = a.Key.Item3
                    });
                }

            }
            catch (Exception ex)
            {
                log.Error(string.Format("{0}, {1}", ex.Message, ex.StackTrace));
            }

            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            var json = JsonConvert.SerializeObject(listdata, settings);
            return json.Replace("timestamp", "@timestamp");
        }

        internal override void SendData()
        {
            try
            {
                SendToCla(GetData());
            }
            catch(Exception ex)
            {
                log.Error(string.Format("{0}, {1}", ex.Message, ex.StackTrace));
            }
        }

        private void SendToCla(string json)
        {
            try
            {
                if (json.Trim() != "")
                {
                    log.Warn("SendToCla");
                    string response = new SoaCall().WebApiCallCla(
                    EndPointUrl,
                    json,
                    "sp_readerrorlog"
                    );
                    log.Warn(response);
                }
            }
            catch (Exception ex)
            {
                log.Error(string.Format("{0}, {1}", ex.Message, ex.StackTrace));
            }
        }

    }
}
