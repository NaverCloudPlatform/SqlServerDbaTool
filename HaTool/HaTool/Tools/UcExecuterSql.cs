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

    public partial class UcExecuterSql : UserControl
    {
        private static readonly Lazy<UcExecuterSql> lazy =
            new Lazy<UcExecuterSql>(() => new UcExecuterSql(), LazyThreadSafetyMode.ExecutionAndPublication);

        public static UcExecuterSql Instance { get { return lazy.Value; } }

        LogClient.Config logClientConfig = LogClient.Config.Instance;
        DataManager dataManager = DataManager.Instance;
        FileDb fileDb = FileDb.Instance;
        TemplateManager templateManager;
        List<loadBalancerInstance> loadBalancerInstances = new List<loadBalancerInstance>();

        public UcExecuterSql()
        {
            InitializeComponent();
        }

        private async void LoadData(object sender, EventArgs e)
        {
            try
            {
                dataManager.LoadUserData();
                templateManager = new TemplateManager(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Tools", "TemplatesSql.txt"));
                templateManager.LoadTemplate();
                InitComboBoxScriptTemplates();
                comboBoxScriptTemplates.SelectedIndex = 0;

                await fileDb.ReadTable(FileDb.TableName.TBL_CLUSTER);
                await fileDb.ReadTable(FileDb.TableName.TBL_SERVER);
                await fileDb.ReadTable(FileDb.TableName.TBL_CLUSTER_SERVER);

                comboBoxloadBalancerName.Items.Clear();
                foreach (var a in fileDb.TBL_CLUSTER.Data)
                {
                    comboBoxloadBalancerName.Items.Add(a.Key.clusterName);
                }
                ComboBoxDefaultValueSetting();
                radioButtonDomain.Checked = true;
                checkBoxUsePrivateIp.Checked = false;
                FillConnectionInfo(comboBoxloadBalancerName.Text, "DOMAIN", checkBoxUsePrivateIp.Checked);
                DbListUpdate();
                
            }
            catch (Exception)
            {
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
                string db = "master";
                int connectTimeoutSec = 3;
                int commandTimeoutSec = 30;
                string masterServerName = string.Empty;
                string slaveServerName = string.Empty;
                string serverName = string.Empty;

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

                    if (radioConnectionType.Equals("DOMAIN", StringComparison.OrdinalIgnoreCase))
                        ip = fileDb.TBL_CLUSTER.Data[new TBL_CLUSTER_KEY { clusterName = clusterName }].domainName;

                    port = fileDb.TBL_SERVER.Data[new TBL_SERVER_KEY { serverName = serverName }].serverPort;
                    userid = fileDb.TBL_SERVER.Data[new TBL_SERVER_KEY { serverName = serverName }].serverUserId;
                    password = TranString.DecodeRijndael(
                                    fileDb.TBL_SERVER.Data[new TBL_SERVER_KEY { serverName = serverName }].serverPassword,
                                    LogClient.Config.Instance.GetCryptionKey());

                    textBoxServerName.Text = serverName;
                    textBoxIP.Text = ip;
                    textBoxPort.Text = port;
                    textBoxUserId.Text = userid;
                    textBoxPassword.Text = password;
                    textBoxConnectionTimeoutSec.Text = connectTimeoutSec.ToString();
                    textBoxCommandTimeoutSec.Text = commandTimeoutSec.ToString();
                    comboBoxDatabase.Text = db;
                }
                else
                {
                    textBoxServerName.Text = "";
                    textBoxIP.Text = "";
                    textBoxPort.Text = "";
                    textBoxUserId.Text = "";
                    textBoxPassword.Text = "";
                    textBoxConnectionTimeoutSec.Text = "";
                    textBoxCommandTimeoutSec.Text = "";
                    comboBoxDatabase.Text = "";
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                throw;
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

                if (radioButtonDomain.Checked)
                    checkBoxUsePrivateIp.Enabled = false;
                else
                    checkBoxUsePrivateIp.Enabled = true;

                if (radioButtonMaster.Checked)
                    buttonText = "MASTER";
                if (radioButtonSlave.Checked)
                    buttonText = "SLAVE";
                if (radioButtonDomain.Checked)
                    buttonText = "DOMAIN";

                FillConnectionInfo(comboBoxloadBalancerName.Text, buttonText, checkBoxUsePrivateIp.Checked);

                DbListUpdate();
            }
            catch (Exception ex)
            {
                
                if (ex.Message.Contains("Padding is invalid"))
                    MessageBox.Show("Check Encryption Key");
                else
                    MessageBox.Show(ex.Message);

            }
        }

        private bool QueryExecuter(string listStringQuery, int commandTimeout = 30)
        {
            bool bReturn = false;
            sbResultAll.Clear();
            List<string> querys = TranString.ReadQuery(listStringQuery);

            sbResultAll.Append("-->>---------------------------------" + Environment.NewLine);
            sbResultAll.Append(DateTime.Now + Environment.NewLine);
            sbResultAll.Append("---------------------------------<<--" + Environment.NewLine + Environment.NewLine);

            foreach (var query in querys)
            {
                try
                {
                    sbResultAll.Append("-->>---------------------------------");
                    sbResultAll.Append(query + Environment.NewLine);
                    sbResultAll.Append("---------------------------------<<--" + Environment.NewLine + Environment.NewLine);
                    if (query.Trim().ToUpper().StartsWith("USE"))
                    {
                        string[] database = query.Trim().Split(new[] { Environment.NewLine, " " }, StringSplitOptions.None);
                        connectionString = SetConnectionString(database[1]);
                        comboBoxDatabase.InvokeIfRequired(s =>
                        {
                            s.Text = "";
                            s.SelectedText = database[1].ToString();
                        });
                    }
                    string result = string.Empty;
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        conn.InfoMessage += Conn_InfoMessage; // message hook (like backup message) 

                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.StatementCompleted += Cmd_StatementCompleted; // retrive row count
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = query;
                            cmd.CommandTimeout = commandTimeout;
                            SqlDataReader reader = cmd.ExecuteReader();
                            int recordsAffected = reader.RecordsAffected;

                            do
                            {
                                StringBuilder sb = new StringBuilder();
                                string Header = string.Empty;
                                string Line = string.Empty;
                                DataTable schemaTable = reader.GetSchemaTable();
                                if (schemaTable != null)
                                {
                                    try
                                    {
                                        foreach (DataRow row in schemaTable.Rows)
                                        {
                                            foreach (DataColumn column in schemaTable.Columns)
                                            {
                                                if (column.ColumnName == "ColumnName")
                                                {
                                                    Header = Header + row[column] + "\t";
                                                    Line = Line + "------- ";
                                                }
                                            }
                                        }
                                        Header = Header + Environment.NewLine;
                                        Line = Line + Environment.NewLine;
                                        sb.Append(Header);
                                        sb.Append(Line);

                                        while (reader.Read())
                                        {
                                            for (int i = 0; i < reader.FieldCount; i++)
                                            {
                                                if (reader.GetValue(i).ToString() == "System.Byte[]")
                                                    sb.Append("0x" + BitConverter.ToString((byte[])reader.GetValue(i)).Replace("-", ""));
                                                else
                                                    sb.Append(reader.GetValue(i).ToString());
                                                sb.Append("\t");
                                            }
                                            sb.Append(Environment.NewLine);
                                        }

                                    }
                                    catch (SqlException ex)
                                    {
                                        errorCnt++;
                                        sbResultAll.Append("-->>---------------------------------" + Environment.NewLine);
                                        sbResultAll.Append("--SQL Exception" + Environment.NewLine);
                                        sbResultAll.Append("-->>---------------------------------" + Environment.NewLine);
                                        for (int i = 0; i < ex.Errors.Count; i++)
                                        {
                                            sbResultAll.Append("Inner SqlException No #" + i + Environment.NewLine +
                                            "Message: " + ex.Errors[i].Message + Environment.NewLine +
                                            "Source: " + ex.Errors[i].Source + Environment.NewLine +
                                            "Procedure: " + ex.Errors[i].Procedure + Environment.NewLine);
                                        }
                                    }
                                    finally
                                    {
                                        sb.Append(Environment.NewLine);
                                        sbResultAll.Append(sb);
                                        sbResultAll.Append(string.Format("({0} {1} affected)" + Environment.NewLine + Environment.NewLine, recordCount, (recordCount == 1) ? "row" : "rows"));
                                    }
                                }
                                else
                                {
                                    string[] Query = query.Trim().Split(new[] { Environment.NewLine, " " }, StringSplitOptions.None);
                                    if (
                                        Query[0].Equals("update", StringComparison.OrdinalIgnoreCase)
                                        || Query[0].Equals("insert", StringComparison.OrdinalIgnoreCase)
                                        || Query[0].Equals("delete", StringComparison.OrdinalIgnoreCase)
                                        || Query[1].Equals("update", StringComparison.OrdinalIgnoreCase)
                                        || Query[1].Equals("insert", StringComparison.OrdinalIgnoreCase)
                                        || Query[1].Equals("delete", StringComparison.OrdinalIgnoreCase)
                                        )
                                        sbResultAll.Append(string.Format("({0} {1} affected)" + Environment.NewLine + Environment.NewLine, recordCount, (recordCount == 1) ? "row" : "rows"));
                                    else
                                        sbResultAll.Append(string.Format("Commands completed successfully." + Environment.NewLine + Environment.NewLine));
                                }
                                reader.NextResult();
                            } while (reader.HasRows);
                        }
                        conn.Close();
                        bReturn = true;
                    }

                    if (checkBoxResultUpdateByGo.Checked)
                        fastColoredTextBoxResult.InvokeIfRequired(s =>
                        {
                            s.Text = sbResultAll.ToString();
                        });
                }

                catch (SqlException ex)
                {
                    errorCnt++;
                    sbResultAll.Append("-->>---------------------------------" + Environment.NewLine);
                    sbResultAll.Append("--SQL Exception" + Environment.NewLine);
                    sbResultAll.Append("-->>---------------------------------" + Environment.NewLine);

                    for (int i = 0; i < ex.Errors.Count; i++)
                    {
                        sbResultAll.Append("SqlException No #" + i + Environment.NewLine +
                        "Message: " + ex.Errors[i].Message + Environment.NewLine +
                        "Source: " + ex.Errors[i].Source + Environment.NewLine +
                        "Procedure: " + ex.Errors[i].Procedure + Environment.NewLine);
                    }

                    sbResultAll.Append("---------------------------------<<--" + Environment.NewLine + Environment.NewLine);

                    if (checkBoxResultUpdateByGo.Checked)
                        fastColoredTextBoxResult.InvokeIfRequired(s =>
                        {
                            s.Text = sbResultAll.ToString();
                        });

                    bReturn = false;
                }
                catch (Exception ex)
                {
                    errorCnt++;
                    sbResultAll.Append("-->>---------------------------------" + Environment.NewLine);
                    sbResultAll.Append("--Exception" + Environment.NewLine);
                    sbResultAll.Append("-->>---------------------------------" + Environment.NewLine);
                    sbResultAll.Append(ex.Message);
                    sbResultAll.Append("---------------------------------<<--" + Environment.NewLine + Environment.NewLine);

                    if (checkBoxResultUpdateByGo.Checked)
                        fastColoredTextBoxResult.InvokeIfRequired(s =>
                        {
                            s.Text = sbResultAll.ToString();
                        });

                    bReturn = false;
                }
            }

            fastColoredTextBoxResult.InvokeIfRequired(s =>
            {
                s.Text = sbResultAll.ToString();
            });

            return bReturn;
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
                UserID = textBoxUserId.Text,
                Password = textBoxPassword.Text,
                InitialCatalog = initialCatalog,
                ConnectTimeout = connectionTimeout
            }.ConnectionString;
        }


        private void DbListUpdate()
        {
            try
            {
                if (!textBoxIP.Text.Equals("") &&
                    !textBoxPort.Text.Equals("") &&
                    !textBoxUserId.Text.Equals("") &&
                    !textBoxPassword.Text.Equals(""))
                    {

                    connectionString = SetConnectionString(comboBoxDatabase.Text);

                    comboBoxDatabase.Items.Clear();
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = "select name from master.dbo.sysdatabases";
                            cmd.CommandTimeout = 5;
                            SqlDataReader reader = cmd.ExecuteReader();
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    comboBoxDatabase.Items.Add(reader.GetValue(0).ToString());
                                }
                                reader.Close();
                            }
                        }
                        conn.Close();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        private async Task Execute()
        {
            string EndMessage = string.Empty;
            try
            {
                connectionString = SetConnectionString(comboBoxDatabase.Text);

                StatusChange("Execute Started...");
                int commandTimeoutSec = Convert.ToInt32(textBoxCommandTimeoutSec.Text);
                errorCnt = 0;

                commandTimeoutSec = Convert.ToInt32(textBoxCommandTimeoutSec.Text);
                string query = string.Empty;
                if (fastColoredTextBoxTemplate.SelectedText.Length > 0)
                    query = fastColoredTextBoxTemplate.SelectedText;
                else
                    query = fastColoredTextBoxTemplate.Text;

                await Task.Run(() => QueryExecuter(query, commandTimeoutSec));


                if (errorCnt == 0)
                    EndMessage = "(Finished.)";
                else if (errorCnt == 1)
                    EndMessage = "(1 error)";
                else
                    EndMessage = string.Format("({0} errors)", errorCnt);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                StatusChange(string.Format("Execute {0}", EndMessage));
            }
        }

        void StatusChange(string value)
        {
            buttonExecute.InvokeIfRequired(s =>
            {
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
        private int errorCnt = 0;
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
