using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaTool.Model
{
    public class TypeKeySetting
    {
        public string ResultMessage { get; set; }
        public string ErrorMessage { get; set; }
        public bool IsSuccess { get; set; }
    }

    public class TypeRestTest
    {
        public string para1 { get; set; }
        public string para2 { get; set; }
    }

    public class TypeSqlIdPassSetting
    {
        public string SqlId { get; set; }
        public string SqlEncryptedPassword { get; set; }
        public string SqlDataSource { get; set; }
        public string SqlConnectTimeout { get; set; }
    }



    public class TypeObjectStorage
    {
        public string UploadDownload { get; set; }
        public string ServiceUrl { get; set; }
        public string BucketName { get; set; }
        public string LocalFileFullname { get; set; }
        public string RemoteFileFullname { get; set; }
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

    public class TypeConfigRead
    {
        public string ConfigFile { get; set; }
        public string Category { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }

    public class TypeConfigSetting
    {
        public string ConfigFile { get; set; }
        public string Category { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }

    public class TypeMonController
    {
        public string MonName { get; set; }
        public string StopStart { get; set; }
    }

    public class DATABASE_MIRRORING_INFO
    {
        public string SERVERNAME { get; set; }
        public string DATABASE_NAME { get; set; }
        public string HAS_DBACCESS_STATE { get; set; }
        public string MIRRORING_STATE_DESC { get; set; }
        public string MIRRORING_PARTNER { get; set; }
        public string MIRRORING_ROLE_DESC { get; set; }
        public string MIRRORING_SAFETY_LEVEL_DESC { get; set; }
        public string MIRRORING_WITNESS_NAME { get; set; }
        public string MIRRORING_CONNECTION_TIMEOUT { get; set; }
        public string SQLSERVER_START_TIME { get; set; }
        public string DATABASE_CREATE_DATE { get; set; }
        public string RECOVERY_MODEL_DESC { get; set; }
    }
}
