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

namespace HaTool.Config
{
    public partial class FormObjectStorage : Form
    {
        private static readonly Lazy<FormObjectStorage> lazy =
            new Lazy<FormObjectStorage>(() => new FormObjectStorage(), LazyThreadSafetyMode.ExecutionAndPublication);

        public static FormObjectStorage Instance { get { return lazy.Value; } }

        LogClient.Config logClientConfig = LogClient.Config.Instance;
        DataManager dataManager = DataManager.Instance; 

        public FormObjectStorage()
        {
            InitializeComponent();
            textBoxAccessKey.Enabled = false;
            textBoxSecretKey.Enabled = false; 
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private async void buttonSave_Click(object sender, EventArgs e)
        {
            
            dataManager.SetValue(DataManager.Category.ObjectStorage, DataManager.Key.Endpoint, textBoxObjectStorageEndPoint.Text.Trim());
            dataManager.SetValue(DataManager.Category.ObjectStorage, DataManager.Key.Bucket, textBoxBucketName.Text.Trim());
            if (dataManager.GetValue(DataManager.Category.Login, DataManager.Key.IsSaveKeyYn).Equals("Y", StringComparison.OrdinalIgnoreCase))
            {
                logClientConfig.SetValue(LogClient.Category.Api, LogClient.Key.AccessKey, textBoxAccessKey.Text.Trim());
                logClientConfig.SetValue(LogClient.Category.Api, LogClient.Key.SecretKey, textBoxSecretKey.Text.Trim());
            }
            else
            {
                logClientConfig.SetValue(LogClient.Category.Api, LogClient.Key.AccessKey, "");
                logClientConfig.SetValue(LogClient.Category.Api, LogClient.Key.SecretKey, "");
            }
            ObjectStorage o = new ObjectStorage(textBoxAccessKey.Text, textBoxSecretKey.Text, textBoxObjectStorageEndPoint.Text);
            if (!await o.IsExistsBucket(textBoxBucketName.Text.Trim()))
            {
                buttonCreateBucket.PerformClick();
            }

            dataManager.SaveUserData();
            logClientConfig.SaveLogClientData();
            logClientConfig.SetValue(LogClient.Category.Api, LogClient.Key.AccessKey, textBoxAccessKey.Text.Trim());
            logClientConfig.SetValue(LogClient.Category.Api, LogClient.Key.SecretKey, textBoxSecretKey.Text.Trim());
            Close();
        }

        private void LoadData(object sender, EventArgs e)
        {
            textBoxComment.ReadOnly = true;
            textBoxComment.BorderStyle = 0;
            textBoxComment.BackColor = this.BackColor;
            textBoxComment.TabStop = false; 
            dataManager.LoadUserData();
            textBoxObjectStorageEndPoint.Text = dataManager.GetValue(DataManager.Category.ObjectStorage, DataManager.Key.Endpoint);
            textBoxAccessKey.Text = logClientConfig.GetValue(LogClient.Category.Api, LogClient.Key.AccessKey);
            textBoxSecretKey.Text = logClientConfig.GetValue(LogClient.Category.Api, LogClient.Key.SecretKey);
            textBoxBucketName.Text = dataManager.GetValue(DataManager.Category.ObjectStorage, DataManager.Key.Bucket);
        }

        private async void buttonCreateBucket_Click(object sender, EventArgs e)
        {
            try
            {
                buttonCreateBucket.Enabled = false;
                buttonCreateBucket.Text = "requested...";

                ObjectStorage o = new ObjectStorage(textBoxAccessKey.Text, textBoxSecretKey.Text, textBoxObjectStorageEndPoint.Text);
                if (!await o.IsExistsBucket(textBoxBucketName.Text))
                {
                    o.CreateBucket(textBoxBucketName.Text);
                    while (!await o.IsExistsBucket(textBoxBucketName.Text))
                    {
                        Task task = Task.Delay(1000);
                        await task;
                    }
                    MessageBox.Show("bucket created");
                }
                else
                {
                    MessageBox.Show("bucket exists");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                buttonCreateBucket.Text = "Create Bucket";
                buttonCreateBucket.Enabled = true;
            }
        }

        private async void buttonBucketTest_Click(object sender, EventArgs e)
        {
            if (textBoxAccessKey.Text.Trim().Length == 0 || textBoxSecretKey.Text.Trim().Length == 0)
                MessageBox.Show("please check accessKey and secretKey");
            else
            {
                LogClient.Config.Instance.SetValue(Category.Api, Key.AccessKey, textBoxAccessKey.Text);
                LogClient.Config.Instance.SetValue(Category.Api, Key.SecretKey, textBoxSecretKey.Text);
                LogClient.Config.Instance.SaveLogClientData();
            }

            try
            {
                ObjectStorage o = new ObjectStorage(textBoxAccessKey.Text, textBoxSecretKey.Text, textBoxObjectStorageEndPoint.Text);
                if (!await o.IsExistsBucket(textBoxBucketName.Text))
                {
                    MessageBox.Show("bucket does not exists");
                }
                else
                {
                    MessageBox.Show("OK");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
