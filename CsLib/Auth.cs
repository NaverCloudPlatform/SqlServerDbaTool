using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Security.Cryptography;

namespace CsLib
{
    public enum RequestType { GET, POST, PUT, DELETE };

    public class Auth
    {
        private static readonly Lazy<Auth> lazy =
            new Lazy<Auth>(() => new Auth(), LazyThreadSafetyMode.ExecutionAndPublication);

        public static Auth Instance { get { return lazy.Value; } }

        Auth() { }

        public string makeSignature(RequestType calltype, string action, ref string stringtimestamp, string accessKey, string secureKey)
        {
            if (string.IsNullOrEmpty(action))
                throw new ArgumentException($"action is null or empty : {action}");

            if (action.Substring(0, 1) == "/")
                action = action.Substring(1, action.Length - 1);

            long timestamp = (long)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalMilliseconds;
            string space = " ";
            string newLine = "\n";
            string method = calltype == RequestType.POST ? "POST" : "GET";
            string url = @"/" + action;
            stringtimestamp = timestamp.ToString();

            string message = new StringBuilder()
                .Append(method)
                .Append(space)
                .Append(url)
                .Append(newLine)
                .Append(stringtimestamp)
                .Append(newLine)
                .Append(accessKey)
                .ToString();

            byte[] secretKey = Encoding.UTF8.GetBytes(secureKey);
            HMACSHA256 hmac = new HMACSHA256(secretKey);
            hmac.Initialize();
            byte[] bytes = Encoding.UTF8.GetBytes(message);
            byte[] rawHmac = hmac.ComputeHash(bytes);

            return Convert.ToBase64String(rawHmac);
        }

        public string makeSignature(RequestType calltype, string action, string stringtimestamp, string accessKey, string secureKey)
        {
            if (string.IsNullOrEmpty(action))
                throw new ArgumentException($"action is null or empty : {action}");

            if (action.Substring(0, 1) == "/")
                action = action.Substring(1, action.Length - 1);

            string space = " ";
            string newLine = "\n";
            string method = calltype == RequestType.POST ? "POST" : "GET";
            string url = @"/" + action;

            string message = new StringBuilder()
                .Append(method)
                .Append(space)
                .Append(url)
                .Append(newLine)
                .Append(stringtimestamp)
                .Append(newLine)
                .Append(accessKey)
                .ToString();

            byte[] secretKey = Encoding.UTF8.GetBytes(secureKey);
            HMACSHA256 hmac = new HMACSHA256(secretKey);
            hmac.Initialize();
            byte[] bytes = Encoding.UTF8.GetBytes(message);
            byte[] rawHmac = hmac.ComputeHash(bytes);

            return Convert.ToBase64String(rawHmac);
        }
    }
}