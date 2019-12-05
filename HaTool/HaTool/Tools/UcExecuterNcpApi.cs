using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using HaTool.Config;
using HaTool.Global;
using CsLib;
using Newtonsoft.Json;
using HaTool.Model.NCloud;
using LogClient;
using Newtonsoft.Json.Linq;
using HaTool.Model;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.IO;

namespace HaTool.Tools
{
    public partial class UcExecuterNcpApi : UserControl
    {
        private static readonly Lazy<UcExecuterNcpApi> lazy =
            new Lazy<UcExecuterNcpApi>(() => new UcExecuterNcpApi(), LazyThreadSafetyMode.ExecutionAndPublication);

        public static UcExecuterNcpApi Instance { get { return lazy.Value; } }

        LogClient.Config logClientConfig = LogClient.Config.Instance;
        DataManager dataManager = DataManager.Instance;
        FileDb fileDb = FileDb.Instance;
        TemplateManager templateManager;
        
        public UcExecuterNcpApi()
        {
            InitializeComponent();
        }

        private void LoadData(object sender, EventArgs e)
        {
            try
            {
                templateManager = new TemplateManager(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Tools", "TemplatesNcpApi.txt"));
                templateManager.LoadTemplate();
                InitComboBoxScriptTemplates();
                comboBoxScriptTemplates.SelectedIndex = 0;
                dataManager.LoadUserData();
                textBoxEndPoint.Text = dataManager.GetValue(DataManager.Category.ApiGateway, DataManager.Key.Endpoint);

                textBoxAccessKey.Text = LogClient.Config.Instance.GetValue(Category.Api, Key.AccessKey);
                textBoxSecretKey.Text = LogClient.Config.Instance.GetValue(Category.Api, Key.SecretKey);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void InitComboBoxScriptTemplates()
        {
            comboBoxScriptTemplates.Items.Clear();
            foreach (var a in templateManager.Templates)
            {
                comboBoxScriptTemplates.Items.Add(a.Key.ToString());
            }
        }


        private string connectionString = string.Empty;
        private StringBuilder sbResultAll = new StringBuilder();

        private async void buttonExecute_Click(object sender, EventArgs e)
        {
            try
            {
                await Execute();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async Task Execute()
        {

            try
            {
                ControlHelpers.ButtonStatusChange(buttonExecute, "Requested");
                string cmdText = string.Empty;
                if (fastColoredTextBoxTemplate.SelectedText.Length > 0)
                    cmdText = fastColoredTextBoxTemplate.SelectedText;
                else
                    cmdText = fastColoredTextBoxTemplate.Text;


                List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>();
                var dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(cmdText);
                foreach (var a in dict)
                    parameters.Add(new KeyValuePair<string, string>(a.Key.ToString(), a.Value.ToString()));


                SoaCall soaCall = new SoaCall();
                Task<string> task = null;
                if (textBoxEndPoint.Text.Contains("kms") || comboBoxScriptTemplates.Text.Contains("mssql"))
                //    if (textBoxEndPoint.Text.Contains("kms") )
                    {
                    task = soaCall.WebApiCall(textBoxEndPoint.Text, RequestType.POST, comboBoxScriptTemplates.Text, cmdText, textBoxAccessKey.Text, textBoxSecretKey.Text);
                }
                else
                {
                    task = soaCall.WebApiCall(textBoxEndPoint.Text, RequestType.POST, comboBoxScriptTemplates.Text, parameters, textBoxAccessKey.Text, textBoxSecretKey.Text);
                }
                
                string response = await task;

                JToken jt = JToken.Parse(response);
                fastColoredTextBoxResult.Text = jt.ToString(Newtonsoft.Json.Formatting.Indented);
            } catch (Exception)
            {
                throw;
            }
            finally
            {
                ControlHelpers.ButtonStatusChange(buttonExecute, "Execute");
            }
            
        }

        private async void ExecuteHotKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                try
                {
                    await Execute();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void ComboBoxScriptTemplatesChanged(object sender, EventArgs e)
        {
            fastColoredTextBoxTemplate.Text = TranString.DecodeBase64Unicode(templateManager.Templates[comboBoxScriptTemplates.SelectedItem.ToString()]);
        }

        private void buttonModifyTemplate_Click(object sender, EventArgs e)
        {
            try
            {
                if (templateManager.UpdateTemplate(comboBoxScriptTemplates.Text, fastColoredTextBoxTemplate.Text))
                {
                    MessageBox.Show("updated");
                }
                else
                {
                    MessageBox.Show("error : template not exists");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("{0}, {1}", ex.Message, ex.StackTrace));
            }
        }

        private void buttonNewTemplate_Click(object sender, EventArgs e)
        {
            try
            {
                if (templateManager.InsertTemplate(comboBoxScriptTemplates.Text, fastColoredTextBoxTemplate.Text))
                {
                    comboBoxScriptTemplates.Items.Add(comboBoxScriptTemplates.Text);
                    MessageBox.Show("added");
                }
                else
                {
                    MessageBox.Show("error : template exists");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonDeleteTemplate_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (templateManager.DeleteTemplate(comboBoxScriptTemplates.Text))
                {
                    comboBoxScriptTemplates.Items.Remove(comboBoxScriptTemplates.Text);
                    comboBoxScriptTemplates.SelectedIndex = 0;
                    MessageBox.Show("deleted");
                }
                else
                {
                    MessageBox.Show("error : template not exists");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("{0}, {1}", ex.Message, ex.StackTrace));
            }
        }
    }
}
