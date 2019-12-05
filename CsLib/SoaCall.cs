using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace CsLib
{
    public class SoaCall
    {
        public SoaCall()
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback +=
            delegate (
                object sender,
                X509Certificate certificate,
                X509Chain chain,
                SslPolicyErrors sslPolicyErrors)
            {
                return true;
            };
        }

        public async Task<string> WebApiCall(string Url, RequestType calltype, string action, string accessKey, string secureKey, int timeout = 0)
        {
            return await WebApiCall(Url, calltype, action, "", accessKey, secureKey, timeout);
        }

        public List<KeyValuePair<string, string>> GetKeyValuePairValue(string template)
        {
            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>();
            var dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(template);
            foreach (var a in dict)
                parameters.Add(new KeyValuePair<string, string>(a.Key.ToString(), a.Value.ToString()));
            return parameters;
        }

        // for ncp...
        public async Task<string> WebApiCall(string Url, RequestType calltype, string action, List<KeyValuePair<string, string>> parameters, string accessKey, string secureKey, int timeoutMiliSec = 30000)
        {
            string responseString = string.Empty;
            try
            {
                if (action.Substring(0, 1) == "/")
                    action = action.Substring(1, action.Length - 1);

                using (HttpClient client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromMilliseconds(timeoutMiliSec);
                    string timestamp = string.Empty;
                    string sig = Auth.Instance.makeSignature(calltype, action, ref timestamp, accessKey, secureKey);
                    string url = Url + @"/" + action;
                    client.DefaultRequestHeaders.Add("x-ncp-apigw-timestamp", timestamp);
                    client.DefaultRequestHeaders.Add("x-ncp-iam-access-key", accessKey);
                    client.DefaultRequestHeaders.Add("x-ncp-apigw-signature-v1", sig);

                    if (calltype == RequestType.POST)
                    {
                        var content = new FormUrlEncodedContent(parameters);
                        var response = await client.PostAsync(url, content);
                        responseString = await response.Content.ReadAsStringAsync();
                    }
                    else
                    {
                        var response = await client.GetAsync(url);
                        responseString = await response.Content.ReadAsStringAsync();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return responseString;
        }

        // for logclient, lazylog ....
        public async Task<string> WebApiCall(string Url, RequestType calltype, string action, string parameters, string accessKey, string secureKey, int timeoutSec = 0)
        {
            string responseString = string.Empty;
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    if (action.Substring(0, 1) == "/")
                        action = action.Substring(1, action.Length - 1);

                    string url = string.Format("{0}/{1}", Url, action);
                    if (timeoutSec == 0)
                        client.Timeout = new TimeSpan(0, 5, 0, 0, 0);
                    else
                        client.Timeout = new TimeSpan(0, 0, 0, timeoutSec, 0);
                    string timestamp = string.Empty;
                    string sig = Auth.Instance.makeSignature(calltype, action, ref timestamp, accessKey, secureKey);
                    client.DefaultRequestHeaders.Add("x-ncp-apigw-timestamp", timestamp);
                    client.DefaultRequestHeaders.Add("x-ncp-iam-access-key", accessKey);
                    client.DefaultRequestHeaders.Add("x-ncp-apigw-signature-v1", sig);
                    if (calltype == RequestType.POST)
                    {

                        var content = new StringContent(parameters, Encoding.UTF8, "application/json");
                        var task = client.PostAsync(url, content);
                        var response = await task;
                        var task1 = response.Content.ReadAsStringAsync();
                        responseString = await task1;
                    }
                    else
                    {
                        var task = client.GetAsync(url);
                        var response = await task;
                        var task1 = response.Content.ReadAsStringAsync();
                        responseString = await task1;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return responseString;
        }
        
        public async Task<string> WebApiCall(string url, RequestType calltype, string action)
        {
            return await WebApiCall(url, calltype, action, "");
        }

        public async Task<string> WebApiCall(string ncpUrl, RequestType calltype, string action, List<KeyValuePair<string, string>> parameters)
        {
            string responseString = string.Empty;
            try
            {
                if (action.Substring(0, 1) == "/")
                    action = action.Substring(1, action.Length - 1);

                using (HttpClient client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromMilliseconds(5000);

                    string url = ncpUrl + @"/" + action;
                    if (calltype == RequestType.POST)
                    {
                        var content = new FormUrlEncodedContent(parameters);
                        var response = await client.PostAsync(url, content);
                        responseString = await response.Content.ReadAsStringAsync();
                    }
                    else
                    {
                        var response = await client.GetAsync(url);
                        responseString = await response.Content.ReadAsStringAsync();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return responseString;
        }

        public async Task<string> WebApiCall(string Url, RequestType calltype, string action, string parameters)
        {
            string responseString = string.Empty;

            try
            {
                if (action.Substring(0, 1) == "/")
                    action = action.Substring(1, action.Length - 1);
                using (HttpClient client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromMilliseconds(5000);
                    string url = Url + @"/" + action;

                    if (calltype == RequestType.POST)
                    {
                        var content = new StringContent(parameters, Encoding.UTF8, "application/json");
                        var response = await client.PostAsync(url, content);
                        responseString = await response.Content.ReadAsStringAsync();
                    }
                    else
                    {
                        var response = await client.GetAsync(url);
                        responseString = await response.Content.ReadAsStringAsync();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return responseString;
        }


        // cla
        public string WebApiCallCla(string Url, string jsonData, string tableName)
        {
            string responseString = string.Empty;

            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(Url);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";
                httpWebRequest.Headers.Add("tablename", tableName);
                httpWebRequest.Timeout = 50000;
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    streamWriter.Write(jsonData);
                    streamWriter.Flush();
                    streamWriter.Close();
                }
                using (HttpWebResponse httpResponse = (HttpWebResponse)httpWebRequest.GetResponse())
                {
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        var responseText = streamReader.ReadToEnd();
                        responseString = httpResponse.StatusCode.ToString();
                    }
                    httpResponse.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }

            return responseString;
        }

        // npot
        public async Task<string> WebApiCallNpot(string Url, string jsonData, string Auth)
        {
            string responseString = string.Empty;
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromMilliseconds(5000);
                    client.DefaultRequestHeaders.Add("Auth", Auth);
                    var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(Url, content);
                    responseString = await response.Content.ReadAsStringAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return responseString;
        }
    }
}
