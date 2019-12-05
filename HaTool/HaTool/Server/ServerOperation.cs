using CsLib;
using HaTool.Config;
using HaTool.Model.NCloud;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogClient; 

namespace HaTool.Server
{
    public static class ServerOperation
    {
        public static async Task RebootServerInstances(List<string> instanceNoList)
        {
            try
            {
                DataManager dataManager = DataManager.Instance;
                string endpoint = dataManager.GetValue(DataManager.Category.ApiGateway, DataManager.Key.Endpoint);
                string action = @"/server/v2/rebootServerInstances";
                List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>();
                parameters.Add(new KeyValuePair<string, string>("responseFormatType", "json"));

                int i = 0;
                foreach (var instanceNo in instanceNoList)
                {
                    i++;
                    string serverInstanceNoListKey = "serverInstanceNoList." + i;
                    string serverInstanceNoListValue = instanceNo;
                    parameters.Add(new KeyValuePair<string, string>(serverInstanceNoListKey, serverInstanceNoListValue));
                }

                SoaCall soaCall = new SoaCall();
                var task = soaCall.WebApiCall(endpoint, RequestType.POST, action, parameters, LogClient.Config.Instance.GetValue(Category.Api, Key.AccessKey), LogClient.Config.Instance.GetValue(Category.Api, Key.SecretKey));
                string response = await task;

                JsonSerializerSettings options = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore
                };

                if (response.Contains("responseError"))
                {
                    hasError hasError = JsonConvert.DeserializeObject<hasError>(response, options);
                    throw new Exception(hasError.responseError.returnMessage);
                }

                rebootServerInstances rebootServerInstances = JsonConvert.DeserializeObject<rebootServerInstances>(response, options);
                if (rebootServerInstances.rebootServerInstancesResponse.returnCode.Equals("0"))
                {

                    foreach (var a in rebootServerInstances.rebootServerInstancesResponse.serverInstanceList)
                    {
                        var item = new serverInstance
                        {
                            serverInstanceNo = a.serverInstanceNo,
                            serverName = a.serverName,
                            publicIp = a.publicIp,
                            privateIp = a.privateIp,
                            serverInstanceStatus = a.serverInstanceStatus,
                            serverInstanceOperation = a.serverInstanceOperation
                        };

                    }
                    if (rebootServerInstances.rebootServerInstancesResponse.totalRows == 0)
                    {
                        throw new Exception("server not founds");
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<List<serverInstance>> GetServerInstanceList(List<string> instanceNoList)
        {
            try
            {
                DataManager dataManager = DataManager.Instance;
                List<serverInstance> serverInstances = new List<serverInstance>();

                string endpoint = dataManager.GetValue(DataManager.Category.ApiGateway, DataManager.Key.Endpoint);
                string action = @"/server/v2/getServerInstanceList";
                List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>();
                parameters.Add(new KeyValuePair<string, string>("responseFormatType", "json"));

                int i = 0;
                foreach (var instanceNo in instanceNoList)
                {
                    i++;
                    string serverInstanceNoListKey = "serverInstanceNoList." + i;
                    string serverInstanceNoListValue = instanceNo;
                    parameters.Add(new KeyValuePair<string, string>(serverInstanceNoListKey, serverInstanceNoListValue));
                }

                SoaCall soaCall = new SoaCall();
                var task = soaCall.WebApiCall(endpoint, RequestType.POST, action, parameters, LogClient.Config.Instance.GetValue(Category.Api, Key.AccessKey), LogClient.Config.Instance.GetValue(Category.Api, Key.SecretKey));
                string response = await task;

                JsonSerializerSettings options = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore
                };

                if (response.Contains("responseError"))
                {
                    hasError hasError = JsonConvert.DeserializeObject<hasError>(response, options);
                    throw new Exception(hasError.responseError.returnMessage);
                }
                else
                {
                    getServerInstanceList getServerInstanceList = JsonConvert.DeserializeObject<getServerInstanceList>(response, options);
                    if (getServerInstanceList.getServerInstanceListResponse.returnCode.Equals("0"))
                    {
                        serverInstances.Clear();
                        foreach (var a in getServerInstanceList.getServerInstanceListResponse.serverInstanceList)
                        {
                            //var item = new serverInstance
                            //{
                            //    serverInstanceNo = a.serverInstanceNo,
                            //    serverName = a.serverName,
                            //    publicIp = a.publicIp,
                            //    privateIp = a.privateIp,
                            //    serverInstanceStatus = a.serverInstanceStatus,
                            //    serverInstanceOperation = a.serverInstanceOperation
                            //};
                            //serverInstances.Add(item);

                            serverInstances.Add(a);
                        }
                        if (getServerInstanceList.getServerInstanceListResponse.totalRows == 0)
                        {
                            throw new Exception("server not founds");
                        }
                    }
                    return serverInstances;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
