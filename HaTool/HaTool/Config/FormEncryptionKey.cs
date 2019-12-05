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
using System.Threading;
using CsLib;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using HaTool.Model.NCloud;

namespace HaTool.Config
{
    public partial class FormEncryptionKey : Form
    {
        private static readonly Lazy<FormEncryptionKey> lazy =
            new Lazy<FormEncryptionKey>(() => new FormEncryptionKey(), LazyThreadSafetyMode.ExecutionAndPublication);

        public static FormEncryptionKey Instance { get { return lazy.Value; } }



        LogClient.Config logClientConfig = LogClient.Config.Instance;

        public FormEncryptionKey()
        {
            InitializeComponent();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void KeyServerTypeClicked(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            string radioButtonText = rb.Name;

            string serverUrl = logClientConfig.GetValue(LogClient.Category.Encryption, LogClient.Key.GetCryptionKeyUrl);
            textBoxAccessKey.Text = LogClient.Config.Instance.GetValue(LogClient.Category.Api, LogClient.Key.AccessKey);
            textBoxSecretKey.Text = LogClient.Config.Instance.GetValue(LogClient.Category.Api, LogClient.Key.SecretKey);
            textBoxKeyTag.Text = LogClient.Config.Instance.GetValue(LogClient.Category.Encryption, LogClient.Key.KeyTag);
            textBoxCiphertext.Text = LogClient.Config.Instance.GetValue(LogClient.Category.Encryption, LogClient.Key.Ciphertext);
            if (radioButtonText.Equals("radioButtonLocalKey"))
            {
                textBoxKey.Enabled = true;
                textBoxKeyTag.Enabled = false;
                textBoxCiphertext.Enabled = false; 
                textBoxAccessKey.Enabled = false;
                textBoxSecretKey.Enabled = false;
                buttonGetCiphertext.Enabled = false;
            }

            if (radioButtonText.Equals("radioButtonNcpKms"))
            {
                textBoxKey.Enabled = true;
                textBoxKeyTag.Enabled = true;
                textBoxCiphertext.Enabled = true;
                textBoxAccessKey.Enabled = true;
                textBoxSecretKey.Enabled = true;
                buttonGetCiphertext.Enabled = true;
            }

            if (radioButtonText.Equals("radioButtonRemoteKeyServer"))
            {
                textBoxKey.Enabled = false;
                textBoxKeyTag.Enabled = false;
                textBoxCiphertext.Enabled = false;
                textBoxAccessKey.Enabled = true;
                textBoxSecretKey.Enabled = true;
                buttonGetCiphertext.Enabled = false;
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (radioButtonLocalKey.Checked)
            {
                GetCryptionKey = "Local";
                logClientConfig.SetValue(LogClient.Category.Encryption, LogClient.Key.LocalCryptionKey, textBoxKey.Text.Trim());
            }
            else // (radioButtonNcpKms.Checked)
            {
                GetCryptionKey = "NcpKms";
                logClientConfig.SetValue(LogClient.Category.Encryption, LogClient.Key.LocalCryptionKey, "");
            }


            logClientConfig.SetValue(LogClient.Category.Encryption, LogClient.Key.GetCryptionKey, GetCryptionKey);
            logClientConfig.SetValue(LogClient.Category.Encryption, LogClient.Key.KeyTag, textBoxKeyTag.Text.Trim());
            logClientConfig.SetValue(LogClient.Category.Encryption, LogClient.Key.Ciphertext, textBoxCiphertext.Text.Trim());
            
            logClientConfig.SaveLogClientData();
            textBoxKey.Text = ""; 
            Close();
        }

        private void LoadData(object sender, EventArgs e)
        {
            textBoxComment.ReadOnly = true;
            textBoxComment.BorderStyle = 0;
            textBoxComment.BackColor = this.BackColor;
            textBoxComment.TabStop = false;
            GetCryptionKey = logClientConfig.GetValue(LogClient.Category.Encryption, LogClient.Key.GetCryptionKey);
            textBoxKeyTag.Text = LogClient.Config.Instance.GetValue(LogClient.Category.Encryption, LogClient.Key.KeyTag);
            textBoxCiphertext.Text = LogClient.Config.Instance.GetValue(LogClient.Category.Encryption, LogClient.Key.Ciphertext);


            if (GetCryptionKey.Equals("Local", StringComparison.OrdinalIgnoreCase))
            {
                radioButtonLocalKey.PerformClick();
            }
            else //(GetCryptionKey.Equals("NcpKms", StringComparison.OrdinalIgnoreCase))
            {
                radioButtonNcpKms.PerformClick();
            }
        }

        string GetCryptionKey = string.Empty;

        private void buttonKeyTest_Click(object sender, EventArgs e)
        {
            if (radioButtonLocalKey.Checked)
                MessageBox.Show(logClientConfig.GetValue(Category.Encryption, Key.LocalCryptionKey));
            else //(radioButtonNcpKms.Checked)
            {
                try
                {

                    if (textBoxKeyTag.Text.Trim().Length == 0)
                        throw new Exception("keytag is empty. Please enter keytag");
                    if (textBoxCiphertext.Text.Trim().Length == 0)
                        throw new Exception("ciphertext is empty. Please enter ciphertext");

                    var kmsDecrypteParameters = new
                    {
                        ciphertext = textBoxCiphertext.Text
                    };
                    var jt = JToken.Parse(JsonConvert.SerializeObject(kmsDecrypteParameters));
                    string parameters = jt.ToString(Newtonsoft.Json.Formatting.Indented);

                    SoaCall asyncCall = new SoaCall();
                    var response = AsyncHelpers.RunSync<string>(() => asyncCall.WebApiCall(
                        @"https://kms.apigw.ntruss.com",
                        RequestType.POST,
                        @"/keys/v2/" + textBoxKeyTag.Text + @"/decrypt",
                        parameters,
                        textBoxAccessKey.Text.Trim(),
                        textBoxSecretKey.Text.Trim(), 5));

                    JsonSerializerSettings options = new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        MissingMemberHandling = MissingMemberHandling.Ignore
                    };

                    if (!response.Contains("SUCCESS"))
                        throw new Exception(response);

                    KmsDecryptResponse KmsDecryptResponse = JsonConvert.DeserializeObject<KmsDecryptResponse>(response, options);
                    MessageBox.Show(TranString.DecodeBase64(KmsDecryptResponse.data.plaintext)); 
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            //else
            //{
            //    try
            //    {
            //        SoaCall asyncCall = new SoaCall();

            //        var key = AsyncHelpers.RunSync<string>(() => asyncCall.WebApiCall(
            //            textBoxRemoteKeyServerUrl.Text.Trim(),
            //            RequestType.GET,
            //            textBoxAction.Text.Trim(),
            //            textBoxAccessKey.Text.Trim(),
            //            textBoxSecretKey.Text.Trim(), 5));

            //        if (key.Contains("Endpoint not found."))
            //        {
            //            throw new Exception("Endpoint not found.");
            //        }
            //        key = TranString.DecodeBase64((key.Replace("\"", "")));
            //        if (key.Equals(""))
            //            MessageBox.Show("authentication error, check accessKey and secretKey");
            //        else
            //            MessageBox.Show(key);
            //    }
            //    catch (Exception ex)
            //    {
            //        if (ex.Message.Contains("A task was canceled."))
            //        {
            //            MessageBox.Show("Unable to connect to the remote server");
            //            return;
            //        }
            //        if (ex.InnerException != null)
            //        {
            //            MessageBox.Show(ex.InnerException.Message);
            //        }
            //        else
            //        {
            //            MessageBox.Show(ex.Message);
            //        }
            //    }
            //}
        }

        private void buttonGetCiphertext_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBoxKey.Text.Trim().Length == 0)
                    throw new Exception("key is null. Please enter key");
                if (textBoxKeyTag.Text.Trim().Length == 0)
                    throw new Exception("keytag is empty. Please enter keytag");

                var kmsEncrypteParameters = new
                {
                    plaintext = TranString.EncodeBase64(textBoxKey.Text)
                };
                var jt = JToken.Parse(JsonConvert.SerializeObject(kmsEncrypteParameters));
                string parameters = jt.ToString(Newtonsoft.Json.Formatting.Indented);

                SoaCall asyncCall = new SoaCall();
                var response = AsyncHelpers.RunSync<string>(() => asyncCall.WebApiCall(
                    @"https://kms.apigw.ntruss.com",
                    RequestType.POST,
                    @"/keys/v2/" + textBoxKeyTag.Text + @"/encrypt",
                    parameters,
                    textBoxAccessKey.Text.Trim(),
                    textBoxSecretKey.Text.Trim(), 5));

                JsonSerializerSettings options = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore
                };

                if (!response.Contains("SUCCESS"))
                    throw new Exception(response);

                KmsEncryptResponse KmsEncryptResponse = JsonConvert.DeserializeObject<KmsEncryptResponse>(response, options);
                textBoxCiphertext.Text = KmsEncryptResponse.data.ciphertext;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
