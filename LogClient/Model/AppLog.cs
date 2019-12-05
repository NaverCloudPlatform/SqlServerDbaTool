using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogClient
{
    public class AppLog
    {
        public string AppName { get; set; }
        public string GUID { get; set; }
        public string ClientIpAddress { get; set; }
        public List<LogDataArgs> LogData { get; set; }
    }

    public class LogDataArgs : EventArgs
    {
        public long UtcTimestamp { get; set; }
        public LogLevel Level { get; set; }
        public string Data { get; set; }
    }

    public class HealthReport
    {
        public string AppName { get; set; }
        public string Version { get; set; }
        public string ClientIp { get; set; }
        public string Guid { get; set; }
    }

    public class WcfResponse
    {
        public bool IsSuccess { get; set; }
        public string ResultMessage { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class SqlPasswordArgs : EventArgs
    {
        public string SqlEncryptedPassword { get; set; }
    }

    public class KmsDecryptResponse
    {
        public string code { get; set; }
        public string msg { get; set; }
        public DecryptedData data { get; set; }
    }

    public class DecryptedData
    {
        public string plaintext { get; set; }
    }

    public class KmsEncryptResponse
    {
        public string code { get; set; }
        public string msg { get; set; }
        public EncryptedData data { get; set; }
    }

    public class EncryptedData
    {
        public string ciphertext { get; set; }
    }
}
