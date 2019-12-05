using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lazylog
{
    public class TypeObjectStorage
    {
        public string UploadDownload { get; set; }
        public string ServiceUrl { get; set; }
        public string BucketName { get; set; }
        public string LocalFileFullname { get; set; }
        public string RemoteFileFullname { get; set; }
    }

    public class TypeSqlIdPassSetting
    {
        public string SqlId { get; set; }
        public string SqlEncryptedPassword { get; set; }
        public string SqlDataSource { get; set; }
        public string SqlConnectTimeout { get; set; }
    }


    public class TypeMonController
    {
        public string MonName { get; set; }
        public string StopStart { get; set; }
    }



    public class TypeRestTest
    {
        public string para1 { get; set; }
        public string para2 { get; set; }
    }

    public class TypeSql
    {
        public string SyncAsync { get; set; }
        public string ConnectionTimeout { get; set; }
        public string CommandTimeout { get; set; }
        public string QueryEchoYN { get; set; }
        public string CountYN { get; set; }
        public string HeaderYN { get; set; }
        public string Database { get; set; }
        public string Querys { get; set; }
    }
}
