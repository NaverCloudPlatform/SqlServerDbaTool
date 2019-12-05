using System;
using System.Threading;
using System.Configuration;
using System.Net;
using System.Text;
using System.Collections.Generic;
using System.IO;
using CsLib;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace LogClient
{
    
    public enum RequestUrlType { LOG };
    public enum LogLevel { INFO, WARN, ERROR };    
    public enum Category {Api, Config, Encryption, HealthReport, WcfServer, Repository}
    public enum Key
    {
        // Config
        AppName
            , AccessKey
            , SecretKey
            , LogUrl
            , LogQueueDelaySecond
            , LogQueueDataCount
            , LogIpType
            , HealthRepoterIpType
            //, ApiGatewayAccessKey
            //, ApiGatewaySecretKey
            , AppVersionCheckIntervalSec
        // Others 
            , LogType
        
        // Encryption
            , LocalCryptionKey
            , GetCryptionKey
            , GetCryptionKeyUrl
            , KeyTag
            , Ciphertext

        // Health
            , HealthReportUrl
            , HealthReportIntervalSec

        // WcfServer
            , Port

        // SqlConnectionIdPassSetting
            , SqlId
            , SqlEncryptedPassword
            , SqlDataSource
            , SqlConnectTimeout

    }
    public class Config
    {
        private List<string> listData;
        private Dictionary<Tuple<Category, Key>, string> dicData;
        object LockObj = new object();

        public event EventHandler<SqlPasswordArgs> SqlPasswordChangedEvent;
        private static string path = AppDomain.CurrentDomain.BaseDirectory;
        private static string logClientConfigInitFilename = "LogClientConfigInit.txt";
        private static string encryptedLogClientConfigFilename = "LogClientConfigUser.txt";
        static string encryptionKeyFilename = "CryptionKeyLogClient.txt";

        string key = string.Empty;

        string encryptionKeyFullFilename = Path.Combine(path, encryptionKeyFilename);
        string logClientConfigInitFullFilename = Path.Combine(path, logClientConfigInitFilename);
        string encryptedlogClientConfigContentsFullFilename = Path.Combine(path, encryptedLogClientConfigFilename);
        
        private static readonly Lazy<Config> lazy =
            new Lazy<Config>(() => new Config(), LazyThreadSafetyMode.ExecutionAndPublication);

        public static Config Instance { get { return lazy.Value; } }

        public Guid guid { get; }
        public bool ConfigLoaded { get; set; } = false;


        private string GetRandomKey()
        {
            string key = string.Empty;
            if (!File.Exists(encryptionKeyFullFilename))
            {
                // create key file
                int randomKey = new Random().Next(1, 100000);
                key = TranString.EncodeBase64Unicode(randomKey.ToString());
                File.WriteAllText(encryptionKeyFullFilename, key, Encoding.Default);
                key = ("0123456789012345" + key);
                key = key.Substring(key.Length - 16);
            }
            else
            {
                // read key file
                key = File.ReadAllText(encryptionKeyFullFilename, Encoding.Default);
                //string InitFile = File.ReadAllText(dataManagerContentsInitFullFilename, Encoding.Default);
                key = ("0123456789012345" + key);
                key = key.Substring(key.Length - 16);
            }
            return key;
        }

        private void LoadLogClientData()
        {
            lock (LockObj)
            {
                if (!File.Exists(encryptionKeyFullFilename))
                {
                    key = GetRandomKey();

                    // write user initial config file
                    string strLogClientConfigInit = File.ReadAllText(logClientConfigInitFullFilename, Encoding.Default);
                    string strEncryptedLogClientConfigInit = TranString.EncodeAES(strLogClientConfigInit, key);
                    if (File.Exists(encryptedlogClientConfigContentsFullFilename))
                        File.Delete(encryptedlogClientConfigContentsFullFilename);
                    File.WriteAllText(encryptedlogClientConfigContentsFullFilename, strEncryptedLogClientConfigInit);
                }

                // read user config file 
                key = GetRandomKey();
                string CryptedConfigFile = File.ReadAllText(encryptedlogClientConfigContentsFullFilename, Encoding.Default);
                string PlainConfigFile = TranString.DecodeAES(CryptedConfigFile, key);
                string2List(PlainConfigFile, listData);
                LoadDataList2Dictionary(listData, dicData);
            }
        }

        public void SaveLogClientData()
        {
            //WriteUpdateValue2File(listData, dicData, logClientConfigFullFilename);
            string fullFilename = encryptedlogClientConfigContentsFullFilename;
            List<string> listName = listData;

            lock (LockObj)
            {
                if (File.Exists(fullFilename))
                    File.Delete(fullFilename);

                var contents = new StringBuilder();

                foreach (string line in listName)
                {
                    try
                    {
                        if (line.StartsWith(@"#") || (line == null) || (line.Trim().Equals("")))
                        {
                            contents.Append(line + Environment.NewLine);
                        }
                        else
                        {
                            string[] lineValues = line.Split(new string[] { ":::" }, StringSplitOptions.None);
                            string data = string.Empty;
                            data = dicData[new Tuple<Category, Key>((Category)Enum.Parse(typeof(Category), lineValues[1]), (Key)Enum.Parse(typeof(Key), lineValues[2]))];
                            if (lineValues[0].ToString().ToUpper().Equals("Base64Unicode".ToUpper()))
                                data = TranString.EncodeBase64Unicode(data);
                            else if (lineValues[0].ToString().ToUpper().Equals("Base64Ascii".ToUpper()))
                                data = TranString.EncodeBase64(data);
                            contents.Append(lineValues[0] + ":::" + lineValues[1] + ":::" + lineValues[2] + ":::" + data + Environment.NewLine);
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
                File.WriteAllText(fullFilename, TranString.EncodeAES(contents.ToString(), key));
            }
        }


        private void string2List(string contents, List<string> listName)
        {
            try
            {
                string[] lines = Regex.Split(contents, Environment.NewLine);
                listName.Clear();
                foreach (string line in lines)
                {
                    try
                    {
                        listName.Add(line);
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Config()
        {
            listData = new List<string>();
            dicData = new Dictionary<Tuple<Category, Key>, string>();
            LoadLogClientData();

            //logClientConfigFullFilename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, logClientConfigInitFilename);
            //LoadFile2List(logClientConfigFullFilename, listData);
            //LoadDataList2Dictionary(listData, dicData);
            ConfigLoaded = true;
            guid = Guid.NewGuid();

            //cryptionKey = GetCryptionKey();
            //cryption = new Cryption(cryptionKey);

            // LogIpType 은 설정에서 읽어서 LocalFirstIp 로 설정한다. 
            LocalIp = Common.GetLocalIpAddress((IpType)Enum.Parse(typeof(IpType), GetValue(Category.Config, Key.LogIpType)));
        }

        public string GetCryptionKey()
        {
            // for sql server password
            return GetCryptionKey(
               GetValue(Category.Encryption, Key.GetCryptionKey),
               GetValue(Category.Encryption, Key.GetCryptionKeyUrl),
               GetValue(Category.Api, Key.AccessKey),
               GetValue(Category.Api, Key.SecretKey)
               );
        }

        public void PasswordChanged()
        {
            SqlPasswordChangedEvent?.Invoke(
            this,
            new SqlPasswordArgs
            {
                SqlEncryptedPassword = GetValue(Category.Repository, Key.SqlEncryptedPassword)
            });
        }

        public string GetCryptionKey(string getCryptionKey, string getCryptionKeyUrl, string accessKey, string SecretKey)
        {
            if (getCryptionKey.Equals("Local", StringComparison.OrdinalIgnoreCase))
            {
                return GetValue(Category.Encryption, Key.LocalCryptionKey);
            }
            else if (getCryptionKey.Equals("NcpKms", StringComparison.OrdinalIgnoreCase))
            {
                for (int i = 0; i <= 3; i++)
                {
                    try
                    {

                        string _keytag = GetValue(Category.Encryption, Key.KeyTag).Trim();
                        string _ciphertext = GetValue(Category.Encryption, Key.Ciphertext).Trim();

                        if (_keytag.Length == 0)
                            throw new Exception("keytag is empty. Please enter keytag");
                        if (_ciphertext.Length == 0)
                            throw new Exception("ciphertext is empty. Please enter ciphertext");

                        var kmsDecrypteParameters = new
                        {
                            ciphertext = _ciphertext
                        };
                        var jt = JToken.Parse(JsonConvert.SerializeObject(kmsDecrypteParameters));
                        string parameters = jt.ToString(Newtonsoft.Json.Formatting.Indented);

                        SoaCall asyncCall = new SoaCall();
                        var response = AsyncHelpers.RunSync<string>(() => asyncCall.WebApiCall(
                            @"https://kms.apigw.ntruss.com",
                            RequestType.POST,
                            @"/keys/v2/" + _keytag + @"/decrypt",
                            parameters,
                            GetValue(Category.Api, Key.AccessKey),
                            GetValue(Category.Api, Key.SecretKey), 5));

                        JsonSerializerSettings options = new JsonSerializerSettings
                        {
                            NullValueHandling = NullValueHandling.Ignore,
                            MissingMemberHandling = MissingMemberHandling.Ignore
                        };

                        if (!response.Contains("SUCCESS"))
                            throw new Exception(response);

                        KmsDecryptResponse KmsDecryptResponse = JsonConvert.DeserializeObject<KmsDecryptResponse>(response, options);
                        return TranString.DecodeBase64(KmsDecryptResponse.data.plaintext);
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"ncpkms not ready!, Message : {ex.Message}");
                    }
                    Thread.Sleep(2000);
                }
            }
            else // (getCryptionKey.Equals("RemoteKeyServer", StringComparison.OrdinalIgnoreCase))
            {
                for (int i = 0; i <= 3; i++)
                {
                    try
                    {
                        SoaCall asyncCall = new SoaCall();

                        var key = AsyncHelpers.RunSync<string>(() => asyncCall.WebApiCall(
                            GetValue(Category.Encryption, Key.GetCryptionKeyUrl),
                            RequestType.GET,
                            @"/Api/GetCryptionKey",
                            GetValue(Category.Api, Key.AccessKey),
                            GetValue(Category.Api, Key.SecretKey), 5));

                        key = TranString.DecodeBase64((key.Replace("\"", "")));
                        
                        return key;
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"KeyServer not ready!, Message : {ex.Message}");
                    }
                    Thread.Sleep(2000);
                }
            }
            throw new Exception("GET KEY ERROR"); 
        }

        public bool KeySettingCompleted()
        {
            if (
                GetValue(Category.Api, Key.AccessKey).Length > 0
                && GetValue(Category.Api, Key.SecretKey).Length > 0
                )
                return true;
            else
                return false;
        }

        public bool SqlIdPassordSettingCompleted()
        {
            if (
                GetValue(Category.Repository, Key.SqlId).Length > 0
                && GetValue(Category.Repository, Key.SqlEncryptedPassword).Length > 0
                && GetValue(Category.Repository, Key.SqlDataSource).Length > 0
                && GetValue(Category.Repository, Key.SqlConnectTimeout).Length > 0
                )
                return true;
            else
                return false;
        }

        public string GetValue(Category category, Key key)
        {
            string sReturn = string.Empty;
            try
            {
                sReturn = dicData[new Tuple<Category, Key>(category, key)];
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Categoy : {category}, Key : {key}", ex);
            }
            return sReturn;
        }

        public void SetValue(Category category, Key key, string value)
        {
            try
            {
                lock (LockObj)
                    dicData[new Tuple<Category, Key>(category, key)] = value;
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Categoy : {category}, Key : {key}, Value : {value}", ex);
            }
        }
        
        public string LocalIp { get; set; }

        public string Url (RequestUrlType requestUrlType)
        {
            string returnValue = string.Empty;

            switch (requestUrlType)
            {
                case RequestUrlType.LOG:
                    returnValue = GetValue(Category.Config, Key.LogUrl);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("undefinded request url type"); 
            }
            return returnValue;
        }

        public string GUID {
            get
            {
                if (guid.ToString().Length > 0)
                    return guid.ToString();
                else
                    return Guid.NewGuid().ToString();
            }
        }



        //private void LoadFile2List(string filename, List<string> listName)
        //{
        //    try
        //    {
        //        string line = string.Empty;
        //        using (StreamReader file = new StreamReader(filename))
        //        {
        //            listName.Clear();
        //            while ((line = file.ReadLine()) != null)
        //            {
        //                try
        //                {
        //                    listName.Add(line);
        //                }
        //                catch (Exception ex)
        //                {
        //                    throw ex;
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}


        private void LoadDataList2Dictionary(List<string> listName, Dictionary<Tuple<Category, Key>, string> dicName)
        {
            lock (LockObj)
            {
                dicData.Clear();
                string data = string.Empty;
                foreach (string line in listName)
                {
                    try
                    {
                        if (!line.StartsWith(@"#") && !(line == null) && !(line.Trim().Equals("")))
                        {
                            string[] lineValues = line.Split(new string[] { ":::" }, StringSplitOptions.None);

                            if (lineValues[0].ToString().Equals("Base64Unicode", StringComparison.OrdinalIgnoreCase))
                                data = TranString.DecodeBase64Unicode(lineValues[3]);
                            else if (lineValues[0].ToString().Equals("Base64Ascii", StringComparison.OrdinalIgnoreCase))
                                data = TranString.DecodeBase64(lineValues[3]);
                            else
                                data = lineValues[3];

                            dicName.Add
                                (
                                    new Tuple<Category, Key>
                                    (
                                        (Category)Enum.Parse(typeof(Category), lineValues[1]),
                                        (Key)Enum.Parse(typeof(Key), lineValues[2])
                                    ), data
                                );
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }
        }


        //private void WriteUpdateValue2File(List<string> listName, Dictionary<Tuple<Category, Key>, string> dicName, string filename)
        //{
        //    lock (LockObj)
        //    {
        //        if (File.Exists(filename))
        //            File.Delete(filename);

        //        using (StreamWriter file = new StreamWriter(filename))
        //        {
        //            foreach (string line in listName)
        //            {
        //                try
        //                {
        //                    if (line.StartsWith(@"#") || (line == null) || (line.Trim().Equals("")))
        //                    {
        //                        file.WriteLine(line);
        //                    }
        //                    else
        //                    {
        //                        string[] lineValues = line.Split(new string[] { ":::" }, StringSplitOptions.None);
        //                        string data = string.Empty;
        //                        data = dicData[new Tuple<Category, Key>((Category)Enum.Parse(typeof(Category), lineValues[1]), (Key)Enum.Parse(typeof(Key), lineValues[2]))];

        //                        if (lineValues[0].ToString().ToUpper().Equals("Base64Unicode".ToUpper()))
        //                            data = TranString.EncodeBase64Unicode(data);
        //                        else if (lineValues[0].ToString().ToUpper().Equals("Base64Ascii".ToUpper()))
        //                            data = TranString.EncodeBase64(data);

        //                        file.WriteLine(lineValues[0] + ":::" + lineValues[1] + ":::" + lineValues[2] + ":::" + data);
        //                    }
        //                }
        //                catch (Exception)
        //                {
        //                    throw;
        //                }
        //            }
        //        }
        //    }
        //}

    }

}
