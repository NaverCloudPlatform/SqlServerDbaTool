using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using HaTool.Global;
using HaTool.Model;
using HaTool.Config;
using CsLib;
using Newtonsoft.Json;
using LogClient;
using Newtonsoft.Json.Linq;

namespace HaTool.Server
{
    public partial class FormNcpRestPreview : Form
    {
        private static readonly Lazy<FormNcpRestPreview> lazy =
            new Lazy<FormNcpRestPreview>(() => new FormNcpRestPreview(), LazyThreadSafetyMode.ExecutionAndPublication);
        public static FormNcpRestPreview Instance { get { return lazy.Value; } }
        
        LogClient.Config logClientConfig = LogClient.Config.Instance;
        DataManager dataManager = DataManager.Instance;
        public event EventHandler<ScriptArgs> ScriptCompleteEvent;
        private bool callback = false;
        public string TitleText
        {
            get { return this.Text; }
            set { this.Text = value; }
        }

        private string commandBackup = string.Empty; 

        public string Command
        {
            get { return fastColoredTextBoxCommand.Text; }
            set
            {
                fastColoredTextBoxCommand.Text = value;
                commandBackup = value;
            }
        }

        public string Result
        {
            set { fastColoredTextBoxResult.Text = value; }
            get { return fastColoredTextBoxResult.Text; }
        }

        public bool Callback
        {
            get
            {
                return callback;
            }
            set
            {
                callback = value;
            }
        }

        public string Action
        {
            get { return textBoxAction.Text; }
            set
            {
                textBoxAction.Text = value;
            }
        }

        public FormNcpRestPreview()
        {
            InitializeComponent();
            buttonLoadPemFile.Visible = false;
        }

        private void StatusChange(string value)
        {
            buttonExecute.InvokeIfRequired(async s =>
            {
                s.Invalidate();
                s.Text = value.ToString();
                s.Update();
                var task = Task.Delay(200);
                await task;
                s.Hide();
                s.Refresh();
                s.Show();
                Application.DoEvents();
            });
        }

        private async void buttonExecute_Click(object sender, EventArgs e)
        {
            try
            {
                await RestCall();
                if (Callback)
                    ScriptCompleteEvent?.Invoke(this, new ScriptArgs { Script = fastColoredTextBoxResult.Text });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public async Task RestCall()
        {
            try
            {
                StatusChange("requested..");
                string endpoint = dataManager.GetValue(DataManager.Category.ApiGateway, DataManager.Key.Endpoint);
                string action = Action;

                List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>();
                var dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(Command);
                foreach (var a in dict)
                    parameters.Add(new KeyValuePair<string, string>(a.Key.ToString(), a.Value.ToString()));

                SoaCall soaCall = new SoaCall();
                var task = soaCall.WebApiCall(endpoint, RequestType.POST, action, parameters, LogClient.Config.Instance.GetValue(Category.Api, Key.AccessKey), LogClient.Config.Instance.GetValue(Category.Api, Key.SecretKey));
                string response = await task;

                if (response.Length > 0)
                {
                    JToken jt = JToken.Parse(response);
                    response = jt.ToString(Newtonsoft.Json.Formatting.Indented);
                }
                else
                {
                    response = "resonse is empty...";
                }
                fastColoredTextBoxResult.Text = response;
                StatusChange("Execute");
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void buttonLoadPemFile_Click(object sender, EventArgs e)
        {
            try
            {
                string fullFileName = OpenDialog();
                if (fullFileName != "")
                {
                    string pem = System.IO.File.ReadAllText(fullFileName);
                    fastColoredTextBoxCommand.Text = commandBackup.Replace("INPUT YOUR RSA PRIVATE KEY", pem);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private string OpenDialog()
        {
            OpenFileDialog o = new OpenFileDialog();
            o.Title = "Select your Pem File";
            o.Filter = "RSA KEY FILE (*.pem) | *.pem; | 모든 파일 (*.*) | *.*";
            DialogResult dr = o.ShowDialog();
            if (dr == DialogResult.OK)
            {
                string fileFullName = o.FileName;
                return fileFullName;
            }
            else
                return "";
        }

        private void LoadPreview(object sender, EventArgs e)
        {
            if (this.Text.Equals("Get Password", StringComparison.OrdinalIgnoreCase))
            {
                buttonLoadPemFile.Visible = true;
            }
        }
    }
}
