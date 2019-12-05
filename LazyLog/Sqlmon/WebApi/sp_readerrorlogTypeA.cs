using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogClient;
using Newtonsoft.Json;

namespace lazylog
{

    class sp_readerrorlogTypeA : BaseSender
    {
        public sp_readerrorlogTypeA(object data)
            : base(data)
        {
            originalData = (Dictionary<Tuple<DateTime, string, string>, string>)data;
            list_sp_readerrorlog_TypeA = new List<sp_readerrorlog_TypeA>();
        }

        Dictionary<Tuple<DateTime, string, string>, string> originalData;
        List<sp_readerrorlog_TypeA> list_sp_readerrorlog_TypeA;

        protected override string GetData()
        {
            try
            {
                list_sp_readerrorlog_TypeA.Clear();
                foreach (var a in originalData)
                {

                    list_sp_readerrorlog_TypeA.Add(
                        new sp_readerrorlog_TypeA
                        {
                            ip = LocalIp,
                            port = LocalPort,
                            probe_time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.000"),
                            LogDate = a.Key.Item1,
                            ProcessInfo = a.Key.Item2,
                            Text = a.Key.Item3
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

            var json = JsonConvert.SerializeObject(list_sp_readerrorlog_TypeA, settings);
            return json;
        }

        protected override void Initialization()
        {
            try
            {
                EndPointUrl = config.GetValue(Category.Sqlmon, Key.SqlmonWebApiCallEndPointUrl);
            }
            catch (Exception ex)
            {
                log.Error(string.Format("{0}, {1}", ex.Message, ex.StackTrace));
            }
        }

        internal override void SendData()
        {
            try
            {
                SendToGeneralRepository(GetData(), "/api/sqlmon/sp_readerrorlog");
            }
            catch (Exception ex)
            {
                log.Error(string.Format("{0}, {1}", ex.Message, ex.StackTrace));
            }
            finally
            {
                list_sp_readerrorlog_TypeA = null;
            }
        }
    }
}
