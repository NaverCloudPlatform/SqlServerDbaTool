using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HaTool.Config;
using LogClient;
using CsLib;
using System.Threading;
using System.Globalization;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net;
using HaTool.Model.NCloud; 

namespace HaTool.Config
{
    public partial class FormConfigurationCheck : Form
    {
        private static readonly Lazy<FormConfigurationCheck> lazy =
            new Lazy<FormConfigurationCheck>(() => new FormConfigurationCheck(), LazyThreadSafetyMode.ExecutionAndPublication);

        public static FormConfigurationCheck Instance { get { return lazy.Value; } }

        LogClient.Config logClientConfig = LogClient.Config.Instance;
        DataManager dataManager = DataManager.Instance;
        StringBuilder sbResults = new StringBuilder();

        public FormConfigurationCheck()
        {
            InitializeComponent();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            sbResults.Clear();
            textBoxLog.Text = "";
            Close();
        }

        private void LoadData(object sender, EventArgs e)
        {
            textBoxComment.ReadOnly = true;
            textBoxComment.BorderStyle = 0;
            textBoxComment.BackColor = this.BackColor;
            textBoxComment.TabStop = false;
            dataManager.LoadUserData();

            sbResults.Clear();
            textBoxLog.Text = "";
        }

        private void AppendVerifyLog(string log)
        {
            textBoxLog.Clear();
            DateTime logTime = DateTime.Now;
            CultureInfo ci = CultureInfo.InvariantCulture;
            sbResults.Append($"[{logTime.ToString("hh:mm:ss")}] : {log}"+Environment.NewLine);
            textBoxLog.Text = sbResults.ToString();
        }

        private void CheckConfigurationKey()
        {
            AppendVerifyLog("*. Encrytpion Key");

            string getCryptionKeyType = logClientConfig.GetValue(LogClient.Category.Encryption, LogClient.Key.GetCryptionKey);
            string keyTag = LogClient.Config.Instance.GetValue(LogClient.Category.Encryption, LogClient.Key.KeyTag);
            string ciphertext = LogClient.Config.Instance.GetValue(LogClient.Category.Encryption, LogClient.Key.Ciphertext);
            try
            {
                AppendVerifyLog($"   Current Encrytpion Type : {getCryptionKeyType}");
                if (getCryptionKeyType.Equals("Local", StringComparison.OrdinalIgnoreCase))
                {
                    AppendVerifyLog("   Cryption Key : " + logClientConfig.GetValue(Category.Encryption, Key.LocalCryptionKey));
                    if (logClientConfig.GetValue(Category.Encryption, Key.LocalCryptionKey).Length == 0)
                        AppendVerifyLog($"   [Warning] Cryption Key is too short! (key length is : {logClientConfig.GetValue(Category.Encryption, Key.LocalCryptionKey).Length})");
                }
                else
                {
                    if (keyTag.Length <= 1)
                        throw new Exception("   [ERROR] The KMS keytag is not corrent!");

                    if (ciphertext.Length <= 1)
                        throw new Exception("   [ERROR] The KMS ciphertext is not corrent!");

                    var kmsDecrypteParameters = new
                    {
                        ciphertext = ciphertext
                    };
                    var jt = JToken.Parse(JsonConvert.SerializeObject(kmsDecrypteParameters));
                    string parameters = jt.ToString(Newtonsoft.Json.Formatting.Indented);

                    SoaCall asyncCall = new SoaCall();
                    var response = AsyncHelpers.RunSync<string>(() => asyncCall.WebApiCall(
                        @"https://kms.apigw.ntruss.com",
                        RequestType.POST,
                        @"/keys/v2/" + keyTag + @"/decrypt",
                        parameters,
                        LogClient.Config.Instance.GetValue(LogClient.Category.Api, LogClient.Key.AccessKey),
                        LogClient.Config.Instance.GetValue(LogClient.Category.Api, LogClient.Key.SecretKey), 5));

                    JsonSerializerSettings options = new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        MissingMemberHandling = MissingMemberHandling.Ignore
                    };

                    if (!response.Contains("SUCCESS"))
                        throw new Exception(response);

                    KmsDecryptResponse KmsDecryptResponse = JsonConvert.DeserializeObject<KmsDecryptResponse>(response, options);
                    AppendVerifyLog("   Cryption KMS key : " + TranString.DecodeBase64(KmsDecryptResponse.data.plaintext));
                    
                }

                AppendVerifyLog($"   Encryption Key Check Result : Success");
            }
            catch (Exception ex)
            {
                AppendVerifyLog(ex.Message);
                AppendVerifyLog("   Encryption Key(KMS) Help Message...");
                AppendVerifyLog("   -----------------------------------------------");
                AppendVerifyLog("   1. Enable subaccount in MC Console.");
                AppendVerifyLog("   2. In the Management Console, create a key for encryption / decryption.");
                AppendVerifyLog("   3. Paste the generated keytag into the SQL Server DBA Tool.");
                AppendVerifyLog("   4. In the SQL Server DBA Tool, type key");
                AppendVerifyLog("   5. Create ciphertext in the SQL Server DBA Tool.");
                AppendVerifyLog("   6. Save.");
                AppendVerifyLog("   -----------------------------------------------");
                throw new Exception("Encryption Key Error!");
            }
        }

        private void CheckObjectStorage()
        {
            AppendVerifyLog("*. Object Stoage");
            try
            {
                string bucketName = dataManager.GetValue(DataManager.Category.ObjectStorage, DataManager.Key.Bucket);
                string endPoint = dataManager.GetValue(DataManager.Category.ObjectStorage, DataManager.Key.Endpoint);
                string accessKey = logClientConfig.GetValue(LogClient.Category.Api, LogClient.Key.AccessKey);
                string secretKey = logClientConfig.GetValue(LogClient.Category.Api, LogClient.Key.SecretKey);
                
                AppendVerifyLog($"   EndPoint : {endPoint}");
                AppendVerifyLog($"   BucketName : {bucketName}");

                if (bucketName.Length < 1)
                    throw new Exception("   [ERROR] The object storage bucket name is too short.");

                ObjectStorage o = new ObjectStorage(accessKey, secretKey, endPoint);
                if (!AsyncHelpers.RunSync<bool>(() => o.IsExistsBucket(bucketName)))
                {
                    throw new Exception("   Bucket Connection Result : Failed");   
                }
                AppendVerifyLog($"   Object Storage Check Result : Success");
            }
            catch (Exception ex)
            {
                AppendVerifyLog(ex.Message);
                AppendVerifyLog("   Object Storage Help Message...");
                AppendVerifyLog("   -----------------------------------------------");
                AppendVerifyLog("   1. Create a bucket with non-duplicate names in SQL Server DBA > Config > Object Storage");
                AppendVerifyLog("   2. Save.");
                AppendVerifyLog("   -----------------------------------------------");
                throw new Exception("Object Storage Error!");
            }
        }

        private void CheckLoginKey()
        {
            AppendVerifyLog("*. LoginKey");
            try
            {
                string endpoint = dataManager.GetValue(DataManager.Category.ApiGateway, DataManager.Key.Endpoint);
                string accessKey = logClientConfig.GetValue(LogClient.Category.Api, LogClient.Key.AccessKey);
                string secretKey = logClientConfig.GetValue(LogClient.Category.Api, LogClient.Key.SecretKey);
                string loginKey = dataManager.GetValue(DataManager.Category.LoginKey, DataManager.Key.Name);
                bool isExistsLoginKey = false; 
                
                string action = @"/server/v2/getLoginKeyList";
                List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>();
                parameters.Add(new KeyValuePair<string, string>("responseFormatType", "json"));
                SoaCall soaCall = new SoaCall();
                var response = AsyncHelpers.RunSync<string>(() => soaCall.WebApiCall(endpoint, RequestType.POST, action, parameters, accessKey, secretKey));

                JsonSerializerSettings options = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore
                };

                getLoginKeyList getLoginKeyList = JsonConvert.DeserializeObject<getLoginKeyList>(response, options);

                if (getLoginKeyList.getLoginKeyListResponse.returnCode.Equals("0"))
                {
                    foreach (var a in getLoginKeyList.getLoginKeyListResponse.loginKeyList)
                    {
                        if (loginKey.Equals(a.keyName, StringComparison.OrdinalIgnoreCase))
                        {
                            isExistsLoginKey = true;
                            break;
                        }
                    }

                    if (!isExistsLoginKey)
                        throw new Exception("   LoginKey does not exists in Managemnet Console!");
                }

                AppendVerifyLog($"   LoginKey Check Result : Success");
            }
            catch (Exception ex)
            {
                AppendVerifyLog(ex.Message);
                AppendVerifyLog("   LoginKey Help Message...");
                AppendVerifyLog("   -----------------------------------------------");
                AppendVerifyLog("   1. Select and save the login key saved in SQL Server DBA Tool or create a new login key.");
                AppendVerifyLog("   -----------------------------------------------");
                throw new Exception("Encryption Key Error!");
            }
        }

        private void CheckInitScript()
        {
            AppendVerifyLog("*. InitScript");
            try
            {
                string bucketName = dataManager.GetValue(DataManager.Category.ObjectStorage, DataManager.Key.Bucket);
                string endPoint = dataManager.GetValue(DataManager.Category.ObjectStorage, DataManager.Key.Endpoint);
                string accessKey = logClientConfig.GetValue(LogClient.Category.Api, LogClient.Key.AccessKey);
                string secretKey = logClientConfig.GetValue(LogClient.Category.Api, LogClient.Key.SecretKey);
                string userData = dataManager.GetValue(DataManager.Category.InitScript, DataManager.Key.userDataFinal);
                string psFileName = dataManager.GetValue(DataManager.Category.InitScript, DataManager.Key.PsFileName);
                bool isExistsAgent = false; 
                if (userData.Length < 1)
                {
                    throw new Exception("   Init script not saved.");
                }

                WebClient client = new WebClient();
                var contents = AsyncHelpers.RunSync<byte[]>(() => client.DownloadDataTaskAsync(string.Format("{0}/{1}/{2}", endPoint, bucketName, psFileName)));
                string psScript = Encoding.Default.GetString(contents);
                if (psScript.Length < 1)
                    new Exception("   Remote powershell script error");

                ObjectStorage o = new ObjectStorage(accessKey, secretKey, endPoint);
                var lists = AsyncHelpers.RunSync<List<ObjectStorageFile>>(() => o.List(bucketName, "Lazylog64.zip"));
                
                foreach (var a in lists)
                {
                    if (a.Name.Equals("Lazylog64.zip", StringComparison.OrdinalIgnoreCase))
                    {
                        AppendVerifyLog($"   Agent file name : {a.Name}, size : {a.Length}, date : {a.LastWriteTime}");
                        isExistsAgent = true; 
                    }
                }
                if (!isExistsAgent)
                    new Exception("   [ERROR] Agent file does not exist in object storage.");

                AppendVerifyLog($"   Init Script Check Result : Success");
            }
            catch (Exception ex)
            {
                AppendVerifyLog(ex.Message);
                AppendVerifyLog("   InitScript Help Message...");
                AppendVerifyLog("   -----------------------------------------------");
                AppendVerifyLog("   1. Upload the initialization script in SQL Server DBA Tool > Config > Init Script");
                AppendVerifyLog("   -----------------------------------------------");
                throw new Exception("Init Script Error!");
            }
        }

        private void buttonCreateBucket_Click(object sender, EventArgs e)
        {
            try
            {
                buttonCreateBucket.Enabled = false;
                buttonCreateBucket.Text = "requested...";
                AppendVerifyLog("");
                AppendVerifyLog("Configuration Check Started!");
                AppendVerifyLog("");

                CheckConfigurationKey();
                CheckObjectStorage();
                CheckLoginKey();
                CheckInitScript();
                AppendVerifyLog("");
                AppendVerifyLog("Configuration Check Completed Successfully.");
                AppendVerifyLog("");
            }
            catch (Exception ex)
            {
                AppendVerifyLog(ex.Message);
                AppendVerifyLog("Configuration check failed.");
            }
            finally
            {
                buttonCreateBucket.Text = "Check";
                buttonCreateBucket.Enabled = true;
            }
        }

    }
}