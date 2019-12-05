using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net;
using System.IO;
using CsLib;

namespace lazylog
{
    class PerfmonSenderTypeB : PerfmonSender
    {
        string private_ip = string.Empty;
        string hostname = string.Empty;
        string ncp_mbr_no = string.Empty;
        string ncp_group_no = string.Empty;
        string npot_auth = string.Empty; 

        public PerfmonSenderTypeB(string CounterDataOriginCurrentTableName) : base(CounterDataOriginCurrentTableName) { }
        public override void LoadWebConfig()
        {
            log.Warn("PerfmonWebApiCallTypeB LoadWebConfig B");
            
            private_ip = (config.GetValue(Category.GLOBAL, Key.private_ip) == "") ? config.LocalIp : config.GetValue(Category.GLOBAL, Key.private_ip);
            hostname = (config.GetValue(Category.GLOBAL, Key.hostname) == "") ? Environment.MachineName : config.GetValue(Category.GLOBAL, Key.hostname);
            ncp_mbr_no = config.GetValue(Category.NPOT, Key.ncp_mbr_no);
            ncp_group_no = config.GetValue(Category.NPOT, Key.ncp_group_no);
            npot_auth = config.GetValue(Category.NPOT, Key.AuthInfo);
            EndPointUrl = config.GetValue(Category.NPOT, Key.URL);

            PerfmonQuery = @"
SELECT 
	countername                                                AS metric, 
    datediff(second,'1970-01-01', DATEADD(mi, DATEDIFF(mi, GETDATE(), GETUTCDATE()), counterdatetime))  AS timestamp,
    cast(a.counterValue as numeric(20,6) )                     AS value,
	@hostname                                                  AS 'tags.hostname',
	@private_ip                                                AS 'tags.ip',
	@ncp_mbr_no                                                AS 'tags.ncp_mbr_no',
	@ncp_group_no                                              AS 'tags.ncp_group_no',
	nullif(objectname,'')                                      AS 'tags.objectname',
	nullif(replace(replace (InstanceName, '{',''), '}',''),'') AS 'tags.instancename'
FROM dbo.view_CounterDataOrigin a
    JOIN [dbo].[counterdetails] b
    ON a.counterid = b.counterid
where RecordIndex = (select top 1 RecordIndex from [dbo].[view_CounterDataOrigin] order by RecordIndex desc) 
FOR JSON PATH option(recompile)";
        }

        public async override void SendData()
        {
            log.Warn("PerfmonWebApiCallTypeB SendData B");
            log.Warn(EndPointUrl);
            string json = GetPerfmonData();

            Task<string> response = new SoaCall().WebApiCallNpot(
            EndPointUrl,
            json,
            npot_auth
            );
            string responseString = await response;
            log.Warn(responseString);
        }
        
        private string GetPerfmonData()
        {
            StringBuilder jsonResult = new StringBuilder();
            try
            {
                using (SqlConnection conn = new SqlConnection(config.GetConnectionString(InitialCatalog.Repository)))
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        //cmd.CommandText = PerfmonQuery.Replace("{0}", CounterDataOriginCurrentTableName);
                        cmd.CommandText = PerfmonQuery;
                        cmd.CommandTimeout = 10;
                        cmd.Parameters.Add("@private_ip", SqlDbType.NVarChar, 100).Value = private_ip;
                        cmd.Parameters.Add("@hostname", SqlDbType.NVarChar, 100).Value = hostname;
                        cmd.Parameters.Add("@ncp_mbr_no", SqlDbType.NVarChar, 100).Value = ncp_mbr_no;
                        cmd.Parameters.Add("@ncp_group_no", SqlDbType.NVarChar, 100).Value = ncp_group_no;

                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                jsonResult.Append(config.DatabaseValue<string>(reader[0]));
                            }
                        }
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                log.Error(string.Format("{0}, {1}", ex.Message, ex.StackTrace));
            }
            return jsonResult.ToString();
        }
    }
}
