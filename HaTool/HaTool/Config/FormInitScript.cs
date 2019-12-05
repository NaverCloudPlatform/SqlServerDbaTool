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
using System.IO;
using System.Net;

namespace HaTool.Config
{
    public partial class FormInitScript : Form
    {
        List<CancellationTokenSource> uploadCancellationTokenSources;

        private static readonly Lazy<FormInitScript> lazy =
            new Lazy<FormInitScript>(() => new FormInitScript(), LazyThreadSafetyMode.ExecutionAndPublication);

        public static FormInitScript Instance { get { return lazy.Value; } }

        LogClient.Config logClientConfig = LogClient.Config.Instance;
        DataManager dataManager = DataManager.Instance;

        string userDataTemplate { get; set; } = "";
        string userDataTemplateChanged { get; set; } = "";
        string psContentsTemplate { get; set; } = "";
        string psContentsTemplateChanged { get; set; } = "";
        string bucket { get; set; } = "";
        string objectEndPoint { get; set; } = "";


        public FormInitScript()
        {
            InitializeComponent();
        }

        private async void buttonVerify_Click(object sender, EventArgs e)
        {
            try
            {
                string userData = dataManager.GetValue(DataManager.Category.InitScript, DataManager.Key.userDataFinal);
                if (userData.Length < 1)
                    new Exception("userData does not saved!");
                string powerShellContents = await DownloadPowerShellScript();

                FormInitScriptVerify formInitScriptVerify = new FormInitScriptVerify();
                formInitScriptVerify.StartPosition = FormStartPosition.CenterScreen;

                formInitScriptVerify.UserData = userData;
                formInitScriptVerify.PowerShellContensts = powerShellContents;
                formInitScriptVerify.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async Task<string> DownloadPowerShellScript()
        {
            string text = string.Empty;
            try
            {
                WebClient client = new WebClient();
                Task<byte[]> task = client.DownloadDataTaskAsync(string.Format("{0}/{1}/{2}", objectEndPoint, bucket, textBoxPowerShellScriptName.Text));
                var contents = await task;
                text = Encoding.Default.GetString(contents);
                if (text.Length < 1)
                    new Exception("remote powershell script error");
            }
            catch (Exception)
            {
                throw;
            }
            return text; 
        }


        private async void buttonUpload_Click(object sender, EventArgs e)
        {
            try
            {
                await ObjectStorageBucketCheck();
                UserDataFinalSave();
                PsContentsTemplateChangedLocalSave();
                UpLoadFile2ObjectStorage(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Config/Upload"), true);
                buttonUpload.Text = "requested";
                buttonUpload.Enabled = false;
                var task = Task.Delay(1000);
                await task; 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                buttonUpload.Enabled = true;
                buttonUpload.Text = "Upload";
            }
        }

        private async Task ObjectStorageBucketCheck()
        {
            try
            {
                ObjectStorage o = new ObjectStorage(
                    logClientConfig.GetValue(Category.Api, Key.AccessKey),
                    logClientConfig.GetValue(Category.Api, Key.SecretKey),
                    dataManager.GetValue(DataManager.Category.ObjectStorage, DataManager.Key.Endpoint)
                    );
                if (!await o.IsExistsBucket(bucket))
                    throw new Exception("object storage bucket does not exists");
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void UserDataFinalSave()
        {
            try
            {
                dataManager.SetValue(DataManager.Category.InitScript, DataManager.Key.userDataFinal, userDataTemplateChanged);
                dataManager.SaveUserData();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void PsContentsTemplateChangedLocalSave()
        {
            try
            {
                string powerShellFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Config/Upload", textBoxPowerShellScriptName.Text);
                string path = System.IO.Path.GetDirectoryName(powerShellFileName);
                if (File.Exists(powerShellFileName))
                    File.Delete(powerShellFileName);
                File.WriteAllText(powerShellFileName, psContentsTemplateChanged);
            }
            catch (Exception)
            {
                throw;
            }
        }


        private async void UpLoadFile2ObjectStorage(string folder, bool publicReadTrueOrFalse = false)
        {
            uploadCancellationTokenSources = new List<CancellationTokenSource>();
            List<Task> tasks = new List<Task>();
            string[] files = Directory.GetFiles(folder);
            foreach (string f in files)
            {
                try
                {
                    CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
                    uploadCancellationTokenSources.Add(cancelTokenSource);
                    ObjectStorage o = new ObjectStorage(
                        logClientConfig.GetValue(Category.Api, Key.AccessKey),
                        logClientConfig.GetValue(Category.Api, Key.SecretKey),
                        dataManager.GetValue(DataManager.Category.ObjectStorage, DataManager.Key.Endpoint)
                        );
                    tasks.Add(o.UploadObjectAsync(bucket, Path.GetFullPath(f), Path.GetFileName(f), cancelTokenSource.Token, 0, publicReadTrueOrFalse));
                }
                catch (Exception)
                {
                    throw;
                }
            }
            try
            {
                await Task.WhenAll(tasks);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                uploadCancellationTokenSources.Clear();
            }
        }

        private void LoadData(object sender, EventArgs e)
        {
            buttonUpload.Text = "Upload";
            textBoxComment.ReadOnly = true;
            textBoxComment.BorderStyle = 0;
            textBoxComment.BackColor = this.BackColor;
            textBoxComment.TabStop = false;
            dataManager.LoadUserData();
            bucket = dataManager.GetValue(DataManager.Category.ObjectStorage, DataManager.Key.Bucket);
            textBoxAgentFolder.Text = dataManager.GetValue(DataManager.Category.InitScript, DataManager.Key.AgentFolder);
            textBoxPowerShellScriptName.Text = dataManager.GetValue(DataManager.Category.InitScript, DataManager.Key.PsFileName);
            userDataTemplate = dataManager.GetValue(DataManager.Category.InitScript, DataManager.Key.userData);
            psContentsTemplate = dataManager.GetValue(DataManager.Category.InitScript, DataManager.Key.PsContents);
            objectEndPoint = dataManager.GetValue(DataManager.Category.ObjectStorage, DataManager.Key.Endpoint);
            TemplateChange();
        }

        private void TemplateChange()
        {
            string vbTemplate = string.Empty;
            vbTemplate = userDataTemplate.Replace("DP_BUCKET_DP", bucket);
            vbTemplate = vbTemplate.Replace("DP_PSFILENAME_DP", textBoxPowerShellScriptName.Text);
            vbTemplate = vbTemplate.Replace("DP_OJBJECT_ENDPOINT_DP", objectEndPoint);
            userDataTemplateChanged = vbTemplate;
            
            string psTemplate = string.Empty;
            psTemplate = psContentsTemplate.Replace("DP_AGENT_FOLDER_DP", textBoxAgentFolder.Text);
            psTemplate = psTemplate.Replace("DP_BUCKET_DP", bucket);
            psTemplate = psTemplate.Replace("DP_OJBJECT_ENDPOINT_DP", objectEndPoint);
            psContentsTemplateChanged = psTemplate;
        }

        private void TemplateChanged(object sender, EventArgs e)
        {
            TemplateChange();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
