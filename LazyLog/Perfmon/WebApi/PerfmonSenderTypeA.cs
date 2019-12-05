using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Newtonsoft.Json;
using CsLib; 

namespace lazylog
{
    class PerfmonSenderTypeA : PerfmonSender
    {
       
        public PerfmonSenderTypeA(string CounterDataOriginCurrentTableName) : base(CounterDataOriginCurrentTableName) { }

        public override void LoadWebConfig()
        {
            EndPointUrl = config.GetValue(Category.Perfmon, Key.PerfmonWebApiCallEndPointUrl);

            PerfmonQuery =
@"
select 
	replace(MachineName, '\\','') MachineName
	, ObjectName
	, CounterName
	, InstanceName
	, CounterValue
	, cast(CounterDateTime as datetime) CounterDateTime
from 
	( 
	select * 
	from [dbo].[view_CounterDataOrigin]
	where RecordIndex = (select top 1 RecordIndex from [dbo].[view_CounterDataOrigin] order by RecordIndex desc) 
	) a 
	join [dbo].[CounterDetails] b 
	on a.CounterID = b.CounterID 
option (loop join, recompile)
";
        }

        public async override void SendData()
        {
            List<PerfmonTypeA> ListPerfmonTypeA = GetPerfmonData();
            string json = JsonConvert.SerializeObject(ListPerfmonTypeA);
            log.Warn(EndPointUrl);
            log.Warn(json);

            // auth 인증의 경우 
            //Task<string> response = asyncCall.WebApiCall(
            //    EndPointUrl,
            //    LogClient.GetPostType.POST,
            //    @"/api/lazylog/perfmon",
            //    json, 
            //    config.GetValue(Category.WebApi, Key.AccessKey),
            //    config.GetValue(Category.WebApi, Key.SecretKey));
            //string responseString = await response;
            //log.Warn(responseString);

            // noauth 의 경우 
            Task<string> response = new SoaCall().WebApiCall(
                EndPointUrl,
                RequestType.POST,
                @"/api/lazylog/perfmon2",
                json);
            string responseString = await response;
            log.Warn(responseString);
        }

        private List<PerfmonTypeA> GetPerfmonData()
        {
            List<PerfmonTypeA> ListPerfmonTypeA = new List<PerfmonTypeA>();
            try
            {
                using (SqlConnection conn = new SqlConnection(config.GetConnectionString(InitialCatalog.Repository)))
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = string.Format(PerfmonQuery, CounterDataOriginCurrentTableName);
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            ListPerfmonTypeA.Add(new PerfmonTypeA
                            {
                                Ip = LocalIp,
                                MachineName = config.DatabaseValue<string>(reader["MachineName"]),
                                ObjectName = config.DatabaseValue<string>(reader["ObjectName"]),
                                CounterName = config.DatabaseValue<string>(reader["CounterName"]),
                                InstanceName = config.DatabaseValue<string>(reader["InstanceName"]),
                                CounterValue = config.DatabaseValue<double>(reader["CounterValue"]).ToString("R"),
                                CounterDateTime = string.Format("{0:yyyy-MM-dd HH:mm:ss}", config.DatabaseValue<DateTime>(reader["CounterDateTime"]))
                            });
                        }
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                log.Error(string.Format("{0}, {1}", ex.Message, ex.StackTrace));
            }
            return ListPerfmonTypeA;
        }
    }
}




//// POST: api/Perfmon
//[Route("api/lazylog/perfmon")]
//[BasicAuthentication]
//public string PostLazyLogPerfmon([FromBody]List<PerfmonTypeA> value)
//{
//    logger.Warn("post start !");
//    logger.Warn(value);
//    return "ok";
//}

//// POST: api/Perfmon
//[Route("api/lazylog/perfmon2")]
//public string PostLazyLogPerfmon2([FromBody]List<PerfmonTypeA> value)
//{
//    logger.Warn("post start !");
//    logger.Warn(value);
//    return "no auth ok";
//}

//public class PerfmonTypeA
//{
//    public string Ip { get; set; }
//    public string MachineName { get; set; }
//    public string ObjectName { get; set; }
//    public string CounterName { get; set; }
//    public string InstanceName { get; set; }
//    public string CounterValue { get; set; }
//    public string CounterDateTime { get; set; }
//}