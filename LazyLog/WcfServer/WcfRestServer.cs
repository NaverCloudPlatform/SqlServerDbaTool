using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Web;
using LogClient;
using System.Threading;
using Newtonsoft.Json;
using System.Net;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;  // data contract 를 쓰기 위해
using CsLib;
using System.Security.Cryptography.X509Certificates;

namespace lazylog
{
    class WcfRestServer
    {
        Log log = Log.Instance;
        LogClient.Config config;
        WebServiceHost host;
        public WcfRestServer(Dictionary<string, object> appInstances)
        {
            config = LogClient.Config.Instance;
        }

        public void Start()
        {
            try
            {
                if (!config.GetValue(LogClient.Category.WcfServer, LogClient.Key.Port).Equals("NO", StringComparison.OrdinalIgnoreCase))
                {
                    string port = config.GetValue(LogClient.Category.WcfServer, LogClient.Key.Port);
                    if (!new Certification().Bind("SignData", "HOSTNAME.pfx", "1234", "HOSTNAME.cer", port))
                        throw new Exception("Install Certification Error");

                    Uri uri = new Uri("https://127.0.0.1:" + port + "/LazyServer");
                    WebHttpBinding b = new WebHttpBinding(WebHttpSecurityMode.Transport);
                    host = new WebServiceHost(typeof(LazyWebService), new Uri[] { uri });
                    host.AddServiceEndpoint(
                        typeof(IRestContract),
                        b,
                        uri);
                    host.Credentials.ServiceCertificate.SetCertificate(
                        StoreLocation.LocalMachine,
                        StoreName.My,
                        X509FindType.FindBySubjectName,
                        "HOSTNAME"
                        );

                    host.Open();

                    //host = new WebServiceHost(typeof(LazyWebService));
                    //string uri = @"http://127.0.0.1:" + config.GetValue(LogClient.Category.WcfServer, LogClient.Key.Port) + @"/LazyServer";
                    //log.Warn(uri);
                    //host.AddServiceEndpoint(
                    //    typeof(IRestContract),
                    //    new WebHttpBinding(WebHttpSecurityMode.None),
                    //    new Uri(uri));
                    //host.Open();
                    log.Warn("wcfServer Started !");
                }
            }
            catch (Exception ex)
            {
                log.Error(string.Format("{0}, {1}", ex.Message, ex.StackTrace));
            }
        }

        public void Stop()
        {
            log.Warn("wcfServer stop");
            try
            {
                if (!config.GetValue(LogClient.Category.WcfServer, LogClient.Key.Port).Equals("NO",StringComparison.OrdinalIgnoreCase))
                    host.Close();
                log.Warn("wcfServer stopped");
            }
            catch (Exception ex)
            {
                log.Error(string.Format("{0}, {1}", ex.Message, ex.StackTrace));
            }
        }
        
        [DataContract]
        public class LazyCommand
        {
            [DataMember]
            public string cmd { get; set; }
            [DataMember]
            public string cmdType { get; set; }
            [DataMember]
            public string cmdText { get; set; }
        }

        [DataContract]
        public class WcfResponse
        {
            [DataMember]
            public bool IsSuccess { get; set; }
            [DataMember]
            public string ResultMessage { get; set; }
            [DataMember]
            public string ErrorMessage { get; set; }
        }

        [ServiceContract]
        public interface IRestContract
        {
            [OperationContract]
            [WebInvoke(Method = "GET", UriTemplate = "/ProductName/{productID}", ResponseFormat = WebMessageFormat.Json)]
            string GetSample(string productID);
            
            [OperationContract]
            [WebInvoke(Method = "POST", UriTemplate = "LazyCommand/PostCmd", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
            WcfResponse PostLazyCommand(LazyCommand lazyCommand);
        }



        public class LazyWebService : IRestContract
        {
            Log log = Log.Instance;
            public string GetSample(string productID)
            {
                if (new BasicAuthentication(WebOperationContext.Current.IncomingRequest).AuthSuccess())
                {
                    log.Warn("PASS");
                }
                else
                {
                    log.Warn("PASS FAIL");
                }

                List<string> listString = new List<string>();
                listString.Add(string.Format("test success :{0}", productID));

                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore
                };

                var json = JsonConvert.SerializeObject(listString, settings);
                return json;
            }

            private bool HasCriticalString(string message)
            {
                bool returnValue = false;
                if (message.Contains("cryption"))
                    returnValue = true;
                if (message.Contains("Cryption"))
                    returnValue = true;
                if (message.Contains("SAPWD"))
                    returnValue = true;
                if (message.Contains("net user"))
                    returnValue = true;
                if (message.Contains("password"))
                    returnValue = true;
                if (message.Contains("TypeKeySetting"))
                    returnValue = true;
                if (message.Contains("TypeSqlIdPassSetting"))
                    returnValue = true;

                return returnValue;
            }

            public WcfResponse PostLazyCommand(LazyCommand lazyCommand)
            {
                WcfResponse wcfResponse;
                string cmdText = string.Empty;

                try
                {
                    cmdText = TranString.DecodeBase64Unicode(lazyCommand.cmdText);
                }
                catch (Exception)
                {
                    cmdText = lazyCommand.cmdText;
                }

                if (new BasicAuthentication(WebOperationContext.Current.IncomingRequest).AuthSuccess())
                {
                    string result = string.Empty;

                    if (!HasCriticalString(lazyCommand.cmdType) && !HasCriticalString(lazyCommand.cmdText) && !HasCriticalString(cmdText))
                        log.Warn(string.Format("pre logging, {0}, {1}, {2}", lazyCommand.cmdType, lazyCommand.cmdText, cmdText));
                    else 
                        log.Warn(string.Format("pre logging, string has critical word, skipped log."));

                    if (lazyCommand.cmd.Equals("ExecuterPs", StringComparison.OrdinalIgnoreCase)) // sync 
                        return new ExecuterPs(lazyCommand.cmdType).Execute(cmdText);
                    else if (lazyCommand.cmd.Equals("ExecuterRest", StringComparison.OrdinalIgnoreCase) && lazyCommand.cmdType.Equals("TypeKeySetting", StringComparison.OrdinalIgnoreCase)
                        || lazyCommand.cmd.Equals("ExecuterRest", StringComparison.OrdinalIgnoreCase) && lazyCommand.cmdType.Equals("TypeSqlIdPassSetting", StringComparison.OrdinalIgnoreCase)
                        || lazyCommand.cmd.Equals("ExecuterRest", StringComparison.OrdinalIgnoreCase) && lazyCommand.cmdType.Equals("TypeConfigSetting", StringComparison.OrdinalIgnoreCase)
                        || lazyCommand.cmd.Equals("ExecuterRest", StringComparison.OrdinalIgnoreCase) && lazyCommand.cmdType.Equals("TypeConfigRead", StringComparison.OrdinalIgnoreCase)
                        )
                        return new KeyManager(lazyCommand.cmdType).Execute(cmdText, true); // TypeKeySetting, ChangeKeySetting, Auth Success, TypeConfigSetting
                    else if (lazyCommand.cmd.Equals("ExecuterRest", StringComparison.OrdinalIgnoreCase))
                        return new ExecuterRest(lazyCommand.cmdType).Execute(cmdText);
                    else if (lazyCommand.cmd.Equals("ExecuterSql", StringComparison.OrdinalIgnoreCase))
                        return new ExecuterSql(lazyCommand.cmdType).Execute(cmdText);
                    else
                    {
                        return new WcfResponse
                        {
                            IsSuccess = false,
                            ResultMessage = "",
                            ErrorMessage = "unknown cmd"
                        };
                    }
                }
                else // auth fail 
                { 
                     if (lazyCommand.cmd.Equals("ExecuterRest", StringComparison.OrdinalIgnoreCase) && lazyCommand.cmdType.Equals("TypeKeySetting", StringComparison.OrdinalIgnoreCase))
                        return new KeyManager(lazyCommand.cmdType).Execute(cmdText, false); //TypeKeySetting, FirstKeySetting, Auth Fail 

                    log.Warn("PASS FAIL");
                    wcfResponse = new WcfResponse
                    {
                        IsSuccess = false,
                        ResultMessage = "",
                        ErrorMessage = "Authentication Failed"
                    };
                }

                return wcfResponse; 
            }
            
        }
    }

}