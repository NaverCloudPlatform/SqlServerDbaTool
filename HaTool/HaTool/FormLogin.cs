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
using Newtonsoft.Json;
using HaTool.Model.NCloud; 

namespace HaTool
{
    public partial class FormLogin : Form
    {
        private static readonly Lazy<FormLogin> lazy =
            new Lazy<FormLogin>(() => new FormLogin(), LazyThreadSafetyMode.ExecutionAndPublication);

        public static FormLogin Instance { get { return lazy.Value; } }

        LogClient.Config logClientConfig = LogClient.Config.Instance;
        DataManager dataManager = DataManager.Instance; 

        public FormLogin()
        {
            InitializeComponent();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }


        private void LoadData(object sender, EventArgs e)
        {
            
            dataManager.LoadUserData();
            textBoxApiGatewayEndpoint.Text = dataManager.GetValue(DataManager.Category.ApiGateway, DataManager.Key.Endpoint);

            string isSaveKeyYn = dataManager.GetValue(DataManager.Category.Login, DataManager.Key.IsSaveKeyYn);
            if (isSaveKeyYn.Equals("Y", StringComparison.OrdinalIgnoreCase))
            {
                checkBoxSave.Checked = true;
                textBoxAccessKey.Text = logClientConfig.GetValue(LogClient.Category.Api, LogClient.Key.AccessKey);
                textBoxSecretKey.Text = logClientConfig.GetValue(LogClient.Category.Api, LogClient.Key.SecretKey);
            }
            else
            {
                checkBoxSave.Checked = false;
            }
        }


        private async void buttonLogin_Click(object sender, EventArgs e)
        {
            try
            {
                buttonLogin.Enabled = false;
                List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>();

                parameters.Add(new KeyValuePair<string, string>("responseFormatType", "json"));
                SoaCall soaCall = new SoaCall();
                var task = soaCall.WebApiCall(textBoxApiGatewayEndpoint.Text, RequestType.POST, @"/server/v2/getRegionList", parameters, textBoxAccessKey.Text, textBoxSecretKey.Text);
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

                if (response.Contains("error"))
                {
                    authError authError = JsonConvert.DeserializeObject<authError>(response, options);
                    throw new Exception(authError.error.message);
                }

                getRegionList getRegionList = JsonConvert.DeserializeObject<getRegionList>(response, options);
                if (!getRegionList.getRegionListResponse.returnCode.Equals("0"))
                {
                    MessageBox.Show(response);
                }

                if (checkBoxSave.Checked)
                {
                    dataManager.SetValue(DataManager.Category.Login, DataManager.Key.IsSaveKeyYn, "Y");
                    logClientConfig.SetValue(LogClient.Category.Api, LogClient.Key.AccessKey, textBoxAccessKey.Text.Trim());
                    logClientConfig.SetValue(LogClient.Category.Api, LogClient.Key.SecretKey, textBoxSecretKey.Text.Trim());
                    dataManager.SaveUserData();
                    logClientConfig.SaveLogClientData();
                }
                else
                {
                    dataManager.SetValue(DataManager.Category.Login, DataManager.Key.IsSaveKeyYn, "N");
                    logClientConfig.SetValue(LogClient.Category.Api, LogClient.Key.AccessKey, "");
                    logClientConfig.SetValue(LogClient.Category.Api, LogClient.Key.SecretKey, "");
                    logClientConfig.SaveLogClientData();
                    logClientConfig.SetValue(LogClient.Category.Api, LogClient.Key.AccessKey, textBoxAccessKey.Text.Trim());
                    logClientConfig.SetValue(LogClient.Category.Api, LogClient.Key.SecretKey, textBoxSecretKey.Text.Trim());
                    dataManager.SaveUserData();
                }

                Hide();
                FormMain formMain = new FormMain();
                formMain.FormClosed += (s, args) => this.Close(); 
                formMain.Show();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
