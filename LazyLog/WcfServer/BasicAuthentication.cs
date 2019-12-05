using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;
using LogClient;
using System.ServiceModel;
using System.ServiceModel.Channels;
using CsLib; 

namespace lazylog
{
    class BasicAuthentication
    {
        IncomingWebRequestContext request;
        
        public BasicAuthentication(IncomingWebRequestContext request)
        {
            this.request = request; 
        }

        public bool AuthSuccess()
        {
            string timestamp = string.Empty;
            string sig = string.Empty;
            string host = string.Empty; 
            string accessKey = string.Empty;
            
            OperationContext context = OperationContext.Current;
            MessageProperties properties = context.IncomingMessageProperties;
            RemoteEndpointMessageProperty endpoint = properties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
            string clientIp = string.Empty;
            //http://www.simosh.com/article/ddbggghj-get-client-ip-address-using-wcf-4-5-remoteendpointmessageproperty-in-load-balanc.html
            if (properties.Keys.Contains(HttpRequestMessageProperty.Name))
            {
                HttpRequestMessageProperty endpointLoadBalancer = properties[HttpRequestMessageProperty.Name] as HttpRequestMessageProperty;
                if (endpointLoadBalancer != null && endpointLoadBalancer.Headers["X-Forwarded-For"] != null)
                    clientIp = endpointLoadBalancer.Headers["X-Forwarded-For"];
            }
            if (string.IsNullOrEmpty(clientIp))
            {
                clientIp = endpoint.Address;
            }

            foreach (string headerName in request.Headers.AllKeys)
            {
                if (headerName.Equals("x-ncp-apigw-timestamp", StringComparison.OrdinalIgnoreCase))
                    timestamp = request.Headers[headerName];
                if (headerName.Equals("x-ncp-apigw-signature-v1", StringComparison.OrdinalIgnoreCase))
                    sig = request.Headers[headerName];
                if (headerName.Equals("x-ncp-iam-access-key",StringComparison.OrdinalIgnoreCase))
                    accessKey = request.Headers[headerName];
            }
            
            string getPostType = request.Method == "GET" ? "GET" : "POST";
            string action = request.UriTemplateMatch.RequestUri.AbsolutePath;
            return isMatchSignature(getPostType, action, timestamp, accessKey, sig);
        }

        private bool isMatchSignature(string calltype, string action, string timestamp, string accesskey, string sig)
        {
            bool bReturn = false;
            string storedAccessKey = LogClient.Config.Instance.GetValue(LogClient.Category.Api, LogClient.Key.AccessKey);
            string storedSecureKey = LogClient.Config.Instance.GetValue(LogClient.Category.Api, LogClient.Key.SecretKey);

            if (!accesskey.Equals(storedAccessKey))
                return false;

            CsLib.RequestType getPostType = calltype == "GET" ? CsLib.RequestType.GET : CsLib.RequestType.POST;
            string genSig = Auth.Instance.makeSignature(getPostType, action, timestamp, accesskey, storedSecureKey);

            if (genSig.Equals(sig))
            {
                if (IsMatchTime(timestamp, 600))
                {
                    return true;
                }
            }
            return bReturn;
        }

        private bool IsMatchTime(string timestamp, long limitSecond)
        {
            bool bReturn = false;

            long currentServerTimestamp = (long)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            long requestTimestamp = long.Parse(timestamp) / 1000;
            long absDiffSecond = Math.Abs(currentServerTimestamp - requestTimestamp);

            if (absDiffSecond <= limitSecond)
                bReturn = true;
            
            return bReturn;
        }
    }
}
