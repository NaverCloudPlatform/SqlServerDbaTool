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
    public partial class UcExecuterAgent : UserControl
    {
        private static readonly Lazy<UcExecuterAgent> lazy =
            new Lazy<UcExecuterAgent>(() => new UcExecuterAgent(), LazyThreadSafetyMode.ExecutionAndPublication);

        public static UcExecuterAgent Instance { get { return lazy.Value; } }

        LogClient.Config logClientConfig = LogClient.Config.Instance;
        DataManager dataManager = DataManager.Instance;
        FileDb fileDb = FileDb.Instance;
        TemplateManager templateManager;
        List<loadBalancerInstance> loadBalancerInstances = new List<loadBalancerInstance>();
        
        public UcExecuterAgent()
        {
            InitializeComponent();
        }

        private async void LoadData(object sender, EventArgs e)
        {
            try
            {

                dataManager.LoadUserData();
                await fileDb.ReadTable(FileDb.TableName.TBL_CLUSTER);
                await fileDb.ReadTable(FileDb.TableName.TBL_SERVER);
                await fileDb.ReadTable(FileDb.TableName.TBL_CLUSTER_SERVER);

                comboBoxloadBalancerName.Items.Clear();
                foreach (var a in fileDb.TBL_CLUSTER.Data)
                {
                    comboBoxloadBalancerName.Items.Add(a.Key.clusterName);
                }
                ComboBoxDefaultValueSetting();
                
                checkBoxUsePrivateIp.Checked = false;
                FillConnectionInfo(comboBoxloadBalancerName.Text, "MASTER", checkBoxUsePrivateIp.Checked);
                templateManager = new TemplateManager(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Tools", "TemplatesSql.txt"));
                templateManager.LoadTemplate();
                InitComboBoxScriptTemplates();
                comboBoxScriptTemplates.SelectedIndex = 0;

                comboBoxCmd.Items.Add("ExecuterRest");
                comboBoxCmd.Items.Add("ExecuterPs");
                comboBoxCmd.Items.Add("ExecuterSql");
                comboBoxCmd.SelectedItem = "ExecuterPs";

                comboBoxCmdType.Items.Add("OUT-STRING");
                comboBoxCmdType.Items.Add("OBJECT");
                comboBoxCmdType.SelectedItem = "OUT-STRING";
                //
                textBoxAccessKey.Text = LogClient.Config.Instance.GetValue(Category.Api, Key.AccessKey);
                textBoxSecretKey.Text = LogClient.Config.Instance.GetValue(Category.Api, Key.SecretKey);

                radioButtonMaster.Checked = true;

            }
            catch(Exception ex)
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

        private void FillConnectionInfo(string clusterName, string radioConnectionType, bool usePrivateIp)
        {                                                          // master slave domain     
            try
            {
                string ip = string.Empty;
                string port = string.Empty;
                string userid = string.Empty;
                string password = string.Empty;
                string masterServerName = string.Empty;
                string slaveServerName = string.Empty;
                string serverName = string.Empty;

                
                textBoxAction.Text = @"/LazyServer/LazyCommand/PostCmd";
                foreach (var a in fileDb.TBL_CLUSTER_SERVER.Data)
                {
                    if (a.Key.clusterName.Equals(clusterName) && a.Value.serverRole.Equals("MASTER", StringComparison.OrdinalIgnoreCase))
                        masterServerName = a.Key.serverName;
                    else if ((a.Key.clusterName.Equals(clusterName) && a.Value.serverRole.Equals("SLAVE", StringComparison.OrdinalIgnoreCase)))
                        slaveServerName = a.Key.serverName;
                }

                serverName = radioConnectionType.Equals("SLAVE", StringComparison.OrdinalIgnoreCase) == false ? masterServerName : slaveServerName;

                if (!serverName.Equals(""))
                {
                    if (usePrivateIp)
                        ip = fileDb.TBL_SERVER.Data[new TBL_SERVER_KEY { serverName = serverName }].serverPrivateIp;
                    else
                        ip = fileDb.TBL_SERVER.Data[new TBL_SERVER_KEY { serverName = serverName }].serverPublicIp;


                    textBoxServerName.Text = serverName;
                    textBoxIP.Text = ip;
                    textBoxPort.Text = "9090";
                }
                else
                {
                    textBoxServerName.Text = "";
                    textBoxIP.Text = "";
                    textBoxPort.Text = "9090";
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }



        void cmd_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxCmdType.Items.Clear();
            if (comboBoxCmd.Text.Equals("ExecuterRest", StringComparison.OrdinalIgnoreCase))
            {
                comboBoxCmdType.Items.Add("TypeMonController");
                comboBoxCmdType.Items.Add("TypeObjectStorage");
                comboBoxCmdType.Items.Add("TypeRestTest");
                comboBoxCmdType.Items.Add("TypeKeySetting");
                comboBoxCmdType.Items.Add("TypeConfigRead");
                comboBoxCmdType.Items.Add("TypeConfigSetting");
                
                comboBoxCmdType.Text = "TypeRestTest";

                comboBoxScriptTemplates.Items.Clear();
                templateManager = new TemplateManager(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Tools", "TemplatesRest.txt"));
                templateManager.LoadTemplate();
                InitComboBoxScriptTemplates();
                comboBoxScriptTemplates.SelectedIndex = 0;
            }
            else if (comboBoxCmd.Text.Equals("ExecuterPs", StringComparison.OrdinalIgnoreCase))
            {
                comboBoxCmdType.Items.Add("OUT-STRING");
                comboBoxCmdType.Items.Add("OBJECT");
                comboBoxCmdType.Text = "OUT-STRING";

                comboBoxScriptTemplates.Items.Clear();
                templateManager = new TemplateManager(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Tools", "TemplatesPs.txt"));
                templateManager.LoadTemplate();
                InitComboBoxScriptTemplates();
                comboBoxScriptTemplates.SelectedIndex = 0;
            }
            else if (comboBoxCmd.Text.Equals("ExecuterSql", StringComparison.OrdinalIgnoreCase))
            {
                comboBoxCmdType.Items.Add("TypeSql");
                comboBoxCmdType.Text = "TypeSql";

                comboBoxScriptTemplates.Items.Clear();
                templateManager = new TemplateManager(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Tools", "TemplatesSql2.txt"));
                templateManager.LoadTemplate();
                InitComboBoxScriptTemplates();
                comboBoxScriptTemplates.SelectedIndex = 0;
            }
        }


        private async void buttonLoadServerList_Click(object sender, EventArgs e)
        {
            try
            {
                await LoadServerList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async Task LoadServerList()
        {
            try
            {
                ControlHelpers.ButtonStatusChange(buttonReload, "Requested");
                await fileDb.ReadTable(FileDb.TableName.TBL_CLUSTER);
                await fileDb.ReadTable(FileDb.TableName.TBL_SERVER);
                await fileDb.ReadTable(FileDb.TableName.TBL_CLUSTER_SERVER);

                comboBoxloadBalancerName.Items.Clear();
                foreach (var a in fileDb.TBL_CLUSTER.Data)
                {
                    comboBoxloadBalancerName.Items.Add(a.Key.clusterName);
                }
                ComboBoxDefaultValueSetting();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                ControlHelpers.ButtonStatusChange(buttonReload, "Reload");
            }
        }


        private void ComboBoxDefaultValueSetting()
        {
            if (comboBoxloadBalancerName.Items.Count > 0)
                comboBoxloadBalancerName.SelectedIndex = 0;
        }


        private void ConnectionTypeProperty_ClickOrChanged(object sender, EventArgs e)
        {
            try
            {
                string buttonText = string.Empty;

                if (radioButtonMaster.Checked)
                    buttonText = "MASTER";
                if (radioButtonSlave.Checked)
                    buttonText = "SLAVE";

                FillConnectionInfo(comboBoxloadBalancerName.Text, buttonText, checkBoxUsePrivateIp.Checked);

                //DbListUpdate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        

        private void Cmd_StatementCompleted(object sender, StatementCompletedEventArgs e)
        {
            recordCount = e.RecordCount;
        }

        private void Conn_InfoMessage(object sender, SqlInfoMessageEventArgs e)
        {
            sbResultAll.Append(e.Message + Environment.NewLine);
        }

        private string SetConnectionString(string initialCatalog, int connectionTimeout = 5)
        {
            return new SqlConnectionStringBuilder
            {
                DataSource = textBoxIP.Text + "," + textBoxPort.Text,
                InitialCatalog = initialCatalog,
                ConnectTimeout = connectionTimeout
            }.ConnectionString;
        }

        void StatusChange(string value)
        {
            buttonExecute.InvokeIfRequired(s => {
                s.Invalidate();
                s.Text = value.ToString();
                s.Update();
                Thread.Sleep(100);
                s.Hide();
                s.Refresh();
                s.Show();
                Application.DoEvents();
            });
        }

        
        private int recordCount = 0;
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


                var task = Execute
                    (comboBoxCmd.Text
                    , comboBoxCmdType.Text
                    , cmdText
                    , CsLib.RequestType.POST
                    , $"https://{textBoxIP.Text}:{textBoxPort.Text}"
                    , textBoxAction.Text
                    , textBoxAccessKey.Text
                    , textBoxSecretKey.Text
                    );

                string response = await task;

                WcfResponse wcfResponse = JsonConvert.DeserializeObject<WcfResponse>(response);
                fastColoredTextBoxResult.Text = wcfResponse.ResultMessage + wcfResponse.ErrorMessage;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                ControlHelpers.ButtonStatusChange(buttonExecute, "Execute");
            }
        }


        async Task<string> Execute(string cmd, string cmdType, string cmdText, CsLib.RequestType type, string endPoint, string action, string accessKey, string secretKey)
        {
            var json = new
            {
                cmd = cmd,
                cmdType = cmdType,
                cmdText = TranString.EncodeBase64Unicode(cmdText)
            };

            string responseString = string.Empty;
            try
            {
                string jsonCmd = JsonConvert.SerializeObject(json);
                Task<string> response = new SoaCall().WebApiCall(
                    endPoint,
                    type,
                    action,
                    jsonCmd,
                    accessKey,
                    secretKey
                    );
                string temp = await response;
                if (temp.Length > 0)
                {
                    JToken jt = JToken.Parse(temp);
                    responseString = jt.ToString(Newtonsoft.Json.Formatting.Indented);
                }
                else
                    responseString = "response is empty...";
            }
            catch (Exception)
            {
                throw;
            }
            return responseString;
        }

        private async void ExecuteHotKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F8)
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
