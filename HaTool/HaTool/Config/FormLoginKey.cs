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
using System.IO;
using HaTool.Model.NCloud;
using Newtonsoft.Json;

namespace HaTool.Config
{
    public partial class FormLoginKey : Form
    {
        private static readonly Lazy<FormLoginKey> lazy =
            new Lazy<FormLoginKey>(() => new FormLoginKey(), LazyThreadSafetyMode.ExecutionAndPublication);

        public static FormLoginKey Instance { get { return lazy.Value; } }


        LogClient.Config logClientConfig = LogClient.Config.Instance;
        DataManager dataManager = DataManager.Instance; 

        public FormLoginKey()
        {
            InitializeComponent();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private async void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (radioButtonOwnKey.Checked)
                {
                    dataManager.SetValue(DataManager.Category.LoginKey, DataManager.Key.Name, comboBoxSelectKey.Text.Trim());
                }
                else
                {
                    await CreateLoginKeyAndFileSave(textBoxKeySavePath.Text.Trim(), textBoxNewKeyName.Text.Trim());
                    dataManager.SetValue(DataManager.Category.LoginKey, DataManager.Key.Name, textBoxNewKeyName.Text.Trim());
                }
                dataManager.SaveUserData();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
                     
        private async void LoadData(object sender, EventArgs e)
        {
            dataManager.LoadUserData();
            radioButtonOwnKey.Checked = true; 
            textBoxComment.ReadOnly = true;
            textBoxComment.BorderStyle = 0;
            textBoxComment.BackColor = this.BackColor;
            textBoxComment.TabStop = false;
            textBoxNewKeyName.Enabled = false;
            textBoxKeySavePath.Enabled = false;
            await GetLoginKeyList();

            textBoxKeySavePath.Text = AppDomain.CurrentDomain.BaseDirectory;
        }

        private void buttonChangeKeySavePath_Click(object sender, EventArgs e)
        {

            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                folderBrowserDialog.Description = "Select a folder to save pem file";
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                    textBoxKeySavePath.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private async void LoginKeySettingTypeClicked(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            string radioButtonKeyName = rb.Name;

            if (radioButtonKeyName.Equals("radioButtonOwnKey"))
            {
                comboBoxSelectKey.Enabled = true;
                buttonReload.Enabled = true;
                textBoxNewKeyName.Enabled = false;
                textBoxKeySavePath.Enabled = false;
                buttonChangeKeySavePath.Enabled = false;
                await GetLoginKeyList(); 
            }

            if (radioButtonKeyName.Equals("radioButtonNewKey"))
            {
                comboBoxSelectKey.Enabled = false;
                buttonReload.Enabled = false;
                textBoxNewKeyName.Enabled = true;
                textBoxKeySavePath.Enabled = true;
                buttonChangeKeySavePath.Enabled = true;
            }
        }

        private async void buttonAuthenticationKeyReload_Click(object sender, EventArgs e)
        {
            await GetLoginKeyList(); 
        }

        private async Task CreateLoginKeyAndFileSave(string path, string loginKeyName)
        {
            try
            {
                string privateKey = string.Empty;
                if (loginKeyName.Length < 3)
                    throw new Exception("The minimum length of the login key is three characters.");

                string endpoint = dataManager.GetValue(DataManager.Category.ApiGateway, DataManager.Key.Endpoint);
                string action = @"/server/v2/createLoginKey";
                List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>();
                parameters.Add(new KeyValuePair<string, string>("responseFormatType", "json"));
                parameters.Add(new KeyValuePair<string, string>("keyName", loginKeyName));
                SoaCall soaCall = new SoaCall();
                var task = soaCall.WebApiCall(endpoint, RequestType.POST, action, parameters, LogClient.Config.Instance.GetValue(Category.Api, Key.AccessKey), LogClient.Config.Instance.GetValue(Category.Api, Key.SecretKey));
                string response = await task;

                JsonSerializerSettings options = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore
                };

                if (response.Contains("responseError"))
                {
                    hasError hasError = JsonConvert.DeserializeObject<hasError>(response, options);
                    throw new Exception(hasError.responseError.returnMessage);
                }
                else
                {
                    createLoginKey createLoginKey = JsonConvert.DeserializeObject<createLoginKey>(response, options);
                    if (createLoginKey.createLoginKeyResponse.returnCode.Equals("0"))
                    {
                        privateKey = createLoginKey.createLoginKeyResponse.privateKey;
                    }

                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);

                    File.WriteAllText(Path.Combine(path, loginKeyName + ".pem"), privateKey);
                }
            }
            catch (Exception )
            {
                throw;
            }
        }

        private async Task GetLoginKeyList()
        {
            try
            {
                string endpoint = dataManager.GetValue(DataManager.Category.ApiGateway, DataManager.Key.Endpoint);
                string action = @"/server/v2/getLoginKeyList";
                List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>();
                parameters.Add(new KeyValuePair<string, string>("responseFormatType", "json"));
                SoaCall soaCall = new SoaCall();
                var task = soaCall.WebApiCall(endpoint, RequestType.POST, action, parameters, LogClient.Config.Instance.GetValue(Category.Api, Key.AccessKey), LogClient.Config.Instance.GetValue(Category.Api, Key.SecretKey));
                string response = await task;

                JsonSerializerSettings options = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore
                };

                getLoginKeyList getLoginKeyList = JsonConvert.DeserializeObject<getLoginKeyList>(response, options);
                if (getLoginKeyList.getLoginKeyListResponse.returnCode.Equals("0"))
                {
                    comboBoxSelectKey.Items.Clear();
                    foreach (var a in getLoginKeyList.getLoginKeyListResponse.loginKeyList)
                    {
                        comboBoxSelectKey.Items.Add(a.keyName);
                    }
                }

                string loginKeyName = dataManager.GetValue(DataManager.Category.LoginKey, DataManager.Key.Name);
                if (loginKeyName.Length == 0)
                    comboBoxSelectKey.SelectedIndex = 0;
                else
                    comboBoxSelectKey.Text = loginKeyName; 

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
