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
    public delegate void PoolWorkerResponseCallback(string ip, string port, string database, bool success, int errorCount, StringBuilder resultMessage);
    public delegate void PoolWorkerCompleteCallback();




    public partial class UcExecuterMultiSql : UserControl
    {
        private static readonly Lazy<UcExecuterMultiSql> lazy =
            new Lazy<UcExecuterMultiSql>(() => new UcExecuterMultiSql(), LazyThreadSafetyMode.ExecutionAndPublication);

        public static UcExecuterMultiSql Instance { get { return lazy.Value; } }

        PoolWorker poolWorker;

        LogClient.Config logClientConfig = LogClient.Config.Instance;
        DataManager dataManager = DataManager.Instance;
        TemplateManager templateManager;
        string MultiSqlConnectionFile = string.Empty;

        Dictionary<MultiServerKey, MultiServerValue> MultiServerGroupDic = new Dictionary<MultiServerKey, MultiServerValue>();
        List<string> MultiServerGroupList = new List<string>();
        DataGridViewCheckBoxColumn ColumnServerCheckBox;
        DataGridViewTextBoxColumn ColumnServerName;
        DataGridViewTextBoxColumn ColumnServerIp;
        DataGridViewTextBoxColumn ColumnServerPort;
        DataGridViewTextBoxColumn ColumnServerDatabase;
        DataGridViewButtonColumn ColumnServerViewLog;

        private void InitDgv()
        {
            ColumnServerCheckBox = new DataGridViewCheckBoxColumn();
            ColumnServerName = new DataGridViewTextBoxColumn();
            ColumnServerIp = new DataGridViewTextBoxColumn();
            ColumnServerPort = new DataGridViewTextBoxColumn();
            ColumnServerDatabase = new DataGridViewTextBoxColumn();
            ColumnServerViewLog = new DataGridViewButtonColumn();

            ColumnServerCheckBox.HeaderText = "Chk";
            ColumnServerName.HeaderText = "Name";
            ColumnServerIp.HeaderText = "Ip";
            ColumnServerPort.HeaderText = "Port";
            ColumnServerDatabase.HeaderText = "DbName";
            ColumnServerViewLog.HeaderText = "Log";

            ColumnServerCheckBox.Name = "Chk";
            ColumnServerName.Name = "Name";
            ColumnServerIp.Name = "Ip";
            ColumnServerPort.Name = "Port";
            ColumnServerDatabase.Name = "DbName";
            ColumnServerViewLog.Name = "Log";



            dgvServerList.Columns.AddRange(new DataGridViewColumn[]
            {
                ColumnServerCheckBox,
                ColumnServerName,
                ColumnServerIp,
                ColumnServerPort,
                ColumnServerDatabase,
                ColumnServerViewLog
            });

            dgvServerList.AllowUserToAddRows = false;
            dgvServerList.RowHeadersVisible = false;
            dgvServerList.BackgroundColor = Color.White;
            dgvServerList.AutoResizeColumns();
            dgvServerList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvServerList.Columns["Log"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvServerList.AllowUserToResizeRows = false;

            ControlHelpers.dgvDesign(dgvServerList);
            //dgvServerList.CellContentClick += new DataGridViewCellEventHandler(ControlHelpers.dgvLineColorChange);
        }
                     
        private void LoadFile2List(string filename, List<string> listName)
        {
            try
            {
                string line = string.Empty;
                using (StreamReader file = new StreamReader(filename))
                {
                    listName.Clear();
                    while ((line = file.ReadLine()) != null)
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
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void LoadMultiServerGroup(List<string> list, Dictionary<MultiServerKey, MultiServerValue> dic)
        {
            try
            {
                dic.Clear();

                foreach (string line in list)
                {
                    if (!line.StartsWith(@"#") && !(line.Trim() == ""))
                    {
                        try
                        {
                            string[] server = line.Split(new string[] { ":::" }, StringSplitOptions.None);
                            bool.TryParse(server[1].ToString(), out bool checkboxStatus);
                            dic.Add(
                                    new MultiServerKey
                                    {
                                        GroupName = server[0].ToString(),
                                        Ip = server[3].ToString(),
                                        Port = server[4].ToString(),
                                        Database = server[5].ToString()
                                    }
                                    ,
                                    new MultiServerValue
                                    {
                                        TrueFalse = checkboxStatus,
                                        ServerName = server[2].ToString(),
                                        Result = "not exec",
                                        Message = ""
                                    }
                                );
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ShowMultiServerComboBox()
        {
            comboBoxSelectServerGroup.Items.Clear();
            List<string> groups = new List<string>();
            foreach (var a in MultiServerGroupDic)
            {
                groups.Add(a.Key.GroupName);
            }
            IEnumerable<string> group = groups.Distinct();

            foreach (var a in group)
            {
                comboBoxSelectServerGroup.Items.Add(a);
            }
            if (comboBoxSelectServerGroup.Items.Count > 0)
                comboBoxSelectServerGroup.SelectedIndex = 0;
        }

        private void InitComboBoxScriptTemplates()
        {
            comboBoxScriptTemplates.Items.Clear();
            foreach (var a in templateManager.Templates)
            {
                comboBoxScriptTemplates.Items.Add(a.Key.ToString());
            }
        }

        private void DgvServerListUpdate()
        {
            dgvServerList.InvokeIfRequired(s =>
            {
                s.Rows.Clear();
                s.BackgroundColor = Color.White;
                foreach (var a in MultiServerGroupDic)
                {
                    if (comboBoxSelectServerGroup.SelectedItem.ToString().Equals(a.Key.GroupName, StringComparison.OrdinalIgnoreCase))
                    {
                        int n = s.Rows.Add();
                        s.Rows[n].Cells[0].Value = a.Value.TrueFalse.ToString();
                        s.Rows[n].Cells[1].Value = a.Value.ServerName;
                        s.Rows[n].Cells[2].Value = a.Key.Ip;
                        s.Rows[n].Cells[3].Value = a.Key.Port;
                        s.Rows[n].Cells[4].Value = a.Key.Database;
                        s.Rows[n].Cells[5].Value = a.Value.Result;
                    }
                }

            });

        }

        private void comboBoxSelectServerGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            DgvServerListUpdate();
        }

        FormServerGroupModify formModify = FormServerGroupModify.Instance;

        public UcExecuterMultiSql()
        {
            InitializeComponent();
            formModify.ScriptModifyEvent += ModifyClose;
            InitDgv();
        }

        private void LoadData(object sender, EventArgs e)
        {
            try
            {
                dataManager.LoadUserData();
                templateManager = new TemplateManager(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Tools", "TemplatesSql.txt"));
                templateManager.LoadTemplate();
                InitComboBoxScriptTemplates();
                comboBoxScriptTemplates.SelectedIndex = 0;
                MultiSqlConnectionFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Tools", "MultiServerGroupList.txt");
                LoadFile2List(MultiSqlConnectionFile, MultiServerGroupList);
                LoadMultiServerGroup(MultiServerGroupList, MultiServerGroupDic);
                ShowMultiServerComboBox();
                fastColoredTextBoxResult.ShowScrollBars = true;
                textBoxCommandTimeoutSec.Text = "30";
                textBoxThreadCount.Text = "20";
                textBoxUserId.Text = "";
                textBoxPassword.Text = "";
                textBoxConnectionTimeoutSec.Text = "3";
                textBoxThinkTimeSec.Text = "0";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ModifyClose(object sender, ScriptArgs e)
        {
            buttonReload.PerformClick();
        }



        private void buttonLoadServerList_Click(object sender, EventArgs e)
        {
            try
            {
                LoadFile2List(MultiSqlConnectionFile, MultiServerGroupList);
                LoadMultiServerGroup(MultiServerGroupList, MultiServerGroupDic);
                ShowMultiServerComboBox();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void ExecuteHotKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                try
                {
                    await Execute(buttonExecute);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    PoolWorkerCompleteCallback();
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

        private void buttonServerGroupModify_Click(object sender, EventArgs e)
        {

            StringBuilder sb = new StringBuilder();
            foreach (var a in MultiServerGroupList)
            {
                sb.Append(a);
                sb.Append(Environment.NewLine);
            }
            FormServerGroupModify formEdit = FormServerGroupModify.Instance;
            formEdit.GroupBoxText = "Modify Multi Server Group Info";
            formEdit.Script = sb.ToString();
            formEdit.FileName = MultiSqlConnectionFile;
            formEdit.StartPosition = FormStartPosition.CenterScreen;
            formEdit.ShowDialog();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            if (poolWorker != null)
                poolWorker.PoolWorkerCancel();
        }

        public void PoolWorkerResponseCallback(string ip, string port, string database, bool success, int errorCount, StringBuilder resultMessage)
        {
            string groupName = string.Empty;

            comboBoxSelectServerGroup.InvokeIfRequired(s =>
            {
                groupName = comboBoxSelectServerGroup.Text;
            });

            var a = MultiServerGroupDic[new MultiServerKey
            {
                GroupName = groupName,
                Ip = ip,
                Port = port,
                Database = database
            }];

            a.Result = errorCount == 0 ? "Completed" : "Error :" + errorCount.ToString();
            a.Message = resultMessage.ToString();

            dgvServerList.InvokeIfRequired(s =>
            {
                s.Rows.Clear();
                s.BackgroundColor = Color.White;
                foreach (var serverInfo in MultiServerGroupDic)
                {
                    if (comboBoxSelectServerGroup.SelectedItem.ToString().Equals(serverInfo.Key.GroupName, StringComparison.OrdinalIgnoreCase))
                    {
                        int n = s.Rows.Add();
                        s.Rows[n].Cells[0].Value = serverInfo.Value.TrueFalse.ToString();
                        s.Rows[n].Cells[1].Value = serverInfo.Value.ServerName;
                        s.Rows[n].Cells[2].Value = serverInfo.Key.Ip;
                        s.Rows[n].Cells[3].Value = serverInfo.Key.Port;
                        s.Rows[n].Cells[4].Value = serverInfo.Key.Database;
                        s.Rows[n].Cells[5].Value = serverInfo.Value.Result;
                    }
                }
            });
        }

        public void PoolWorkerCompleteCallback()
        {
            buttonExecute.InvokeIfRequired(s =>
            {
                buttonExecute.Text = "Execute";
                buttonConnectionTestSelectedServer.Text = "Connection Test";
                StringBuilder sbResult = new StringBuilder();
                sbResult.Append("QueryTime : " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                sbResult.Append(Environment.NewLine + "Query execution completed. Please check each server's log.");
                fastColoredTextBoxResult.Text = sbResult.ToString();
            });
        }

        private async Task Execute(Button button)
        {
            string Querys = string.Empty;
            if (button.Name == "buttonConnectionTestSelectedServer")
                Querys = @"select @@version";
            else
                Querys = fastColoredTextBoxTemplate.SelectedText.Length > 0 ? fastColoredTextBoxTemplate.SelectedText : fastColoredTextBoxTemplate.Text;


            if (dgvServerList.RowCount > 0)
            {

                int checkedServerCnt = 0;

                foreach (DataGridViewRow item in dgvServerList.Rows)
                {
                    var a = MultiServerGroupDic[new MultiServerKey
                    {
                        GroupName = comboBoxSelectServerGroup.Text,
                        Ip = item.Cells["Ip"].Value.ToString(),
                        Port = item.Cells["Port"].Value.ToString(),
                        Database = item.Cells["DbName"].Value.ToString()
                    }];

                    if (bool.Parse(item.Cells["Chk"].Value.ToString()))
                    {
                        a.TrueFalse = true;
                        checkedServerCnt++;
                    }
                    else
                    {
                        a.TrueFalse = false;
                    }
                }

                if (button.Text == "Running...")
                {
                    MessageBox.Show("It is already running...");
                    return;
                }

                if (checkedServerCnt == 0)
                {
                    MessageBox.Show("Check the server to run");
                    return;
                }

                // clear result
                foreach (DataGridViewRow item in dgvServerList.Rows)
                {
                    var a = MultiServerGroupDic[new MultiServerKey
                    {
                        GroupName = comboBoxSelectServerGroup.Text,
                        Ip = item.Cells["Ip"].Value.ToString(),
                        Port = item.Cells["Port"].Value.ToString(),
                        Database = item.Cells["DbName"].Value.ToString()
                    }];

                    a.Result = "not exec";
                    a.Message = "";
                }


                button.Text = "Running...";

                int minThread = Convert.ToInt32(textBoxThreadCount.Text);
                minThread = minThread < 2 ? 2 : minThread;

                if (checkedServerCnt > 0)
                {
                    poolWorker =
                        new PoolWorker(
                             MultiServerGroupDic
                            , comboBoxSelectServerGroup.Text
                            , Convert.ToInt32(textBoxConnectionTimeoutSec.Text)
                            , Convert.ToInt32(textBoxCommandTimeoutSec.Text)
                            , minThread
                            , textBoxUserId.Text
                            , textBoxPassword.Text
                            , Querys
                            , PoolWorkerResponseCallback
                            , PoolWorkerCompleteCallback
                            , Convert.ToInt32(textBoxThinkTimeSec.Text)
                            , textBoxColumnDelimiter.Text
                            );
                    await Task.Run(() => poolWorker.DoWork());
                }
                else
                {
                    MessageBox.Show("no checked server !");
                }
            }
            else
            {
                MessageBox.Show("server list is wrong !");
            }
        }
        
        private async void buttonExecute_Click(object sender, EventArgs e)
        {
            try
            {
                fastColoredTextBoxResult.Text = "";
                await Execute(buttonExecute);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                PoolWorkerCompleteCallback();
            }
            
        }

        private void dgvServerList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 5)
                {
                    DataGridViewRow item = dgvServerList.Rows[e.RowIndex];
                    var a = MultiServerGroupDic[new MultiServerKey
                    {
                        GroupName = comboBoxSelectServerGroup.Text,
                        Ip = item.Cells["Ip"].Value.ToString(),
                        Port = item.Cells["Port"].Value.ToString(),
                        Database = item.Cells["DbName"].Value.ToString()
                    }];
                    string resultServer = $"Ip : {item.Cells["Ip"].Value.ToString()}, Port : {item.Cells["Port"].Value.ToString()}, Database : {item.Cells["DbName"].Value.ToString()}";
                    fastColoredTextBoxResult.Text = resultServer + Environment.NewLine + a.Message;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void buttonConnectionTestSelectedServer_Click(object sender, EventArgs e)
        {
            

            try
            {
                await Execute(buttonConnectionTestSelectedServer);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                PoolWorkerCompleteCallback();
            }

        }

        private void buttonAllCheck_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow item in dgvServerList.Rows)
            {
                item.Cells["Chk"].Value = true;
            }
        }

        private void buttonAllUnCheck_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow item in dgvServerList.Rows)
            {
                item.Cells["Chk"].Value = false;
            }
        }

        private void buttonAllReverse_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow item in dgvServerList.Rows)
            {
                if (bool.Parse(item.Cells["Chk"].Value.ToString()))
                    item.Cells["Chk"].Value = false;
                else
                    item.Cells["Chk"].Value = true;
            }
        }
    }
}
