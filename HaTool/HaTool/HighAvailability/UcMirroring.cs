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
using System.Diagnostics;
using HaTool.Server;

namespace HaTool.HighAvailability
{
    public partial class UcMirroring : UserControl
    {
        private static readonly Lazy<UcMirroring> lazy =
            new Lazy<UcMirroring>(() => new UcMirroring(), LazyThreadSafetyMode.ExecutionAndPublication);

        public static UcMirroring Instance { get { return lazy.Value; } }

        LogClient.Config logClientConfig = LogClient.Config.Instance;
        DataManager dataManager = DataManager.Instance;
        FileDb fileDb = FileDb.Instance;

        object Lock = new object();

        List<loadBalancerInstance> loadBalancerInstances = new List<loadBalancerInstance>();
        Graphics graphics;
        private int recordCount = 0;
        private int errorCnt = 0;
        private string connectionString = string.Empty;
        private StringBuilder sbResultAll = new StringBuilder();
        string sbResultAllBackup = string.Empty;
        public UcMirroring()
        {
            InitializeComponent();
            InitDgv();
            progressBarInit();
        }

        DataGridViewCheckBoxColumn ColumnCHECKBOX;
        DataGridViewTextBoxColumn ColumnMIRRORINGSTATUS;
        DataGridViewTextBoxColumn ColumnSERVERNAME;
        DataGridViewTextBoxColumn ColumnDATABASE_NAME;
        DataGridViewTextBoxColumn ColumnHAS_DBACCESS_STATE;
        DataGridViewTextBoxColumn ColumnMIRRORING_STATE_DESC;
        DataGridViewTextBoxColumn ColumnMIRRORING_PARTNER;
        DataGridViewTextBoxColumn ColumnMIRRORING_ROLE_DESC;
        DataGridViewTextBoxColumn ColumnMIRRORING_SAFETY_LEVEL_DESC;
        DataGridViewTextBoxColumn ColumnMIRRORING_WITNESS_NAME;
        DataGridViewTextBoxColumn ColumnMIRRORING_CONNECTION_TIMEOUT;
        DataGridViewTextBoxColumn ColumnSQLSERVER_START_TIME;
        DataGridViewTextBoxColumn ColumnRECOVERY_MODEL_DESC;
        DataGridViewTextBoxColumn ColumnDATABASE_CREATE_DATE;

        private void InitDgv()
        {
            ColumnCHECKBOX = new DataGridViewCheckBoxColumn();
            ColumnMIRRORINGSTATUS = new DataGridViewTextBoxColumn();
            ColumnSERVERNAME = new DataGridViewTextBoxColumn();
            ColumnDATABASE_NAME = new DataGridViewTextBoxColumn();
            ColumnHAS_DBACCESS_STATE = new DataGridViewTextBoxColumn();
            ColumnMIRRORING_STATE_DESC = new DataGridViewTextBoxColumn();
            ColumnMIRRORING_PARTNER = new DataGridViewTextBoxColumn();
            ColumnMIRRORING_ROLE_DESC = new DataGridViewTextBoxColumn();
            ColumnMIRRORING_SAFETY_LEVEL_DESC = new DataGridViewTextBoxColumn();
            ColumnMIRRORING_WITNESS_NAME = new DataGridViewTextBoxColumn();
            ColumnMIRRORING_CONNECTION_TIMEOUT = new DataGridViewTextBoxColumn();
            ColumnSQLSERVER_START_TIME = new DataGridViewTextBoxColumn();
            ColumnRECOVERY_MODEL_DESC = new DataGridViewTextBoxColumn();
            ColumnDATABASE_CREATE_DATE = new DataGridViewTextBoxColumn();

            ColumnCHECKBOX.HeaderText = "CheckBox";
            ColumnMIRRORINGSTATUS.HeaderText = "MirroringStatus";
            ColumnSERVERNAME.HeaderText = "ServerName";
            ColumnDATABASE_NAME.HeaderText = "DatabaseName";
            ColumnRECOVERY_MODEL_DESC.HeaderText = "RecoveryModel";
            ColumnHAS_DBACCESS_STATE.HeaderText = "HasDbAccess";
            ColumnMIRRORING_STATE_DESC.HeaderText = "MirroringState";
            ColumnMIRRORING_PARTNER.HeaderText = "MirroringPartner";
            ColumnMIRRORING_ROLE_DESC.HeaderText = "MirroringRole";
            ColumnMIRRORING_SAFETY_LEVEL_DESC.HeaderText = "MirroringSafetyLevel";
            ColumnMIRRORING_WITNESS_NAME.HeaderText = "MirroringWitness";
            ColumnMIRRORING_CONNECTION_TIMEOUT.HeaderText = "MirroringConnectionTimeout";
            ColumnSQLSERVER_START_TIME.HeaderText = "SqlServerStartTime";
            ColumnDATABASE_CREATE_DATE.HeaderText = "DatabaseCreateDate";

            ColumnCHECKBOX.Name = "CHECKBOX";
            ColumnMIRRORINGSTATUS.Name = "MIRRORINGSTATUS";
            ColumnSERVERNAME.Name = "SERVERNAME";
            ColumnDATABASE_NAME.Name = "DATABASE_NAME";
            ColumnRECOVERY_MODEL_DESC.Name = "RECOVERY_MODEL_DESC";
            ColumnHAS_DBACCESS_STATE.Name = "HAS_DBACCESS_STATE";
            ColumnMIRRORING_STATE_DESC.Name = "MIRRORING_STATE_DESC";
            ColumnMIRRORING_PARTNER.Name = "MIRRORING_PARTNER";
            ColumnMIRRORING_ROLE_DESC.Name = "MIRRORING_ROLE_DESC";
            ColumnMIRRORING_SAFETY_LEVEL_DESC.Name = "MIRRORING_SAFETY_LEVEL_DESC";
            ColumnMIRRORING_WITNESS_NAME.Name = "MIRRORING_WITNESS_NAME";
            ColumnMIRRORING_CONNECTION_TIMEOUT.Name = "MIRRORING_CONNECTION_TIMEOUT";
            ColumnSQLSERVER_START_TIME.Name = "SQLSERVER_START_TIME";
            ColumnDATABASE_CREATE_DATE.Name = "DATABASE_CREATE_DATE";

            dgvMirrorStatus.Columns.AddRange(new DataGridViewColumn[]
            {
                ColumnCHECKBOX,
                ColumnMIRRORINGSTATUS,
                ColumnSERVERNAME,
                ColumnDATABASE_NAME,
                ColumnRECOVERY_MODEL_DESC,
                ColumnHAS_DBACCESS_STATE,
                ColumnMIRRORING_STATE_DESC,
                ColumnMIRRORING_PARTNER,
                ColumnMIRRORING_ROLE_DESC,
                ColumnMIRRORING_SAFETY_LEVEL_DESC,
                ColumnMIRRORING_WITNESS_NAME,
                ColumnMIRRORING_CONNECTION_TIMEOUT,
                ColumnSQLSERVER_START_TIME,
                ColumnDATABASE_CREATE_DATE
            });

            dgvMirrorStatus.AllowUserToAddRows = false;
            dgvMirrorStatus.RowHeadersVisible = false;
            dgvMirrorStatus.BackgroundColor = Color.White;
            dgvMirrorStatus.AutoResizeColumns();
            dgvMirrorStatus.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvMirrorStatus.AllowUserToResizeRows = false;

            HaTool.Global.ControlHelpers.dgvDesign(dgvMirrorStatus);
            dgvMirrorStatus.CellContentClick += new DataGridViewCellEventHandler(ControlHelpers.dgvLineColorChange);
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
                textBoxBackupRestorePath.Text = dataManager.GetValue(DataManager.Category.HighAvailability, DataManager.Key.BackupRestorePath);
                ComboBoxDefaultValueSetting();
                progressBarInit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        private async void buttonLoadServerList_Click(object sender, EventArgs e)
        {
            try
            {
                await LoadLoadBalancerList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async Task LoadLoadBalancerList()
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

        private bool QueryExecuter(string ListStringQuery, int CommandTimeout = 30)
        {
            return true;
        }

        private void Cmd_StatementCompleted(object sender, StatementCompletedEventArgs e)
        {
            recordCount = e.RecordCount;
        }

        private void Conn_InfoMessage(object sender, SqlInfoMessageEventArgs e)
        {
            sbResultAll.Append(e.Message + Environment.NewLine);
        }

        void CheckButtonChange(string value)
        {
            buttonBackupRestorePathCheck.InvokeIfRequired(s =>
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

        private async void HaGroup_Changed(object sender, EventArgs e)
        {
            try
            {
                ComboBox c = (ComboBox)sender;
                string domainName = fileDb.TBL_CLUSTER.Data[new TBL_CLUSTER_KEY { clusterName = c.Text }].domainName;
                List<Tuple<string, string, string>> clusterServerInfo = new List<Tuple<string, string, string>>();
                foreach (var a in fileDb.TBL_CLUSTER_SERVER.Data)
                    clusterServerInfo.Add(new Tuple<string, string, string>(a.Key.clusterName, a.Key.serverName, a.Value.serverRole));

                textBoxDomain.Text = $"domain : {domainName}";
                try
                {
                    textBoxMasterServerName.Text = clusterServerInfo.Find(x => x.Item1 == c.Text && x.Item3 == "MASTER").Item2;
                }
                catch
                {
                    textBoxMasterServerName.Text = "";
                }
                try
                {
                    textBoxSlaveServerName.Text = clusterServerInfo.Find(x => x.Item1 == c.Text && x.Item3 == "SLAVE").Item2;
                }
                catch
                {
                    textBoxSlaveServerName.Text = "";
                }

                await MirroringStatusReload();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void buttonBackupRestorePathCheck_Click(object sender, EventArgs e)
        {
            try
            {
                CheckButtonChange("requested");
                labelMasterServerCheckStatusValue.Text = "unknown";
                labelSlaveServerCheckStatusValue.Text = "unknown";
                buttonBackupRestorePathCheck.Enabled = false;
                if (textBoxBackupRestorePath.Text.Trim().Length == 0)
                {
                    MessageBox.Show("check backup restore path");
                    return;
                }

                textBoxBackupRestorePath.Text = textBoxBackupRestorePath.Text.Trim();
                if (textBoxBackupRestorePath.Text.Substring(textBoxBackupRestorePath.Text.Length - 1) != @"\")
                    textBoxBackupRestorePath.Text = textBoxBackupRestorePath.Text + @"\";

                Task<string> masterResult = CheckBackupRestoreIfNotExsitsCreatePath("MASTER", textBoxMasterServerName.Text, textBoxBackupRestorePath.Text);
                Task<string> slaveResult = CheckBackupRestoreIfNotExsitsCreatePath("SLAVE", textBoxSlaveServerName.Text, textBoxBackupRestorePath.Text);

                List<Task<string>> tasks = new List<Task<string>>();
                tasks.Add(masterResult);
                tasks.Add(slaveResult);
                await Task.WhenAll(tasks);

                labelMasterServerCheckStatusValue.Text = masterResult.Result;
                labelSlaveServerCheckStatusValue.Text = slaveResult.Result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                CheckButtonChange("Check");
                buttonBackupRestorePathCheck.Enabled = true;
            }

        }


        private async Task<string> CheckBackupRestoreIfNotExsitsCreatePath(string serverType, string serverName, string checkPath)
        {
            string checkResult = "not ok";
            try
            {
                checkResult = "not ok";

                string ip = GetPublicIp(serverName);
                if (ip.Equals("") || serverName.Length == 0)
                {
                    MessageBox.Show("Check Cluster Configuration and PublicIp");
                    return checkResult;
                }

                var task = dataManager.Execute
                    ("ExecuterPs"
                    , "OUT-STRING"
                    , $"Test-Path {checkPath.Trim()} -PathType Any"
                    , CsLib.RequestType.POST
                    , $"https://{ip}:9090"
                    , @"/LazyServer/LazyCommand/PostCmd"
                    , LogClient.Config.Instance.GetValue(LogClient.Category.Api, LogClient.Key.AccessKey)
                    , LogClient.Config.Instance.GetValue(LogClient.Category.Api, LogClient.Key.SecretKey)
                    , 5);

                string response = await task;
                WcfResponse wcfResponse = new WcfResponse();
                wcfResponse = JsonConvert.DeserializeObject<WcfResponse>(response);

                if (wcfResponse.ResultMessage.ToString().Contains("False"))
                {
                    // mkdir 
                    task = dataManager.Execute
                    ("ExecuterPs"
                    , "OUT-STRING"
                    , $"mkdir {checkPath.Trim()}"
                    , CsLib.RequestType.POST
                    , $"https://{ip}:9090"
                    , @"/LazyServer/LazyCommand/PostCmd"
                    , LogClient.Config.Instance.GetValue(LogClient.Category.Api, LogClient.Key.AccessKey)
                    , LogClient.Config.Instance.GetValue(LogClient.Category.Api, LogClient.Key.SecretKey)
                    , 5);
                }

                response = await task;
                wcfResponse = new WcfResponse();
                wcfResponse = JsonConvert.DeserializeObject<WcfResponse>(response);

                TypeSql typeSql = new TypeSql
                {
                    SyncAsync = "Sync",
                    ConnectionTimeout = 5.ToString(),
                    CommandTimeout = (10).ToString(),
                    QueryEchoYN = "N",
                    HeaderYN = "N",
                    Database = "master",
                    Querys = @"
SET NOCOUNT ON
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED 
SELECT  
    @@SERVERNAME SERVERNAME
FOR JSON PATH
"
                };

                string cmdText = JsonConvert.SerializeObject(typeSql);

                task = dataManager.Execute
                    ("ExecuterSql"
                    , "TypeSql"
                    , cmdText
                    , CsLib.RequestType.POST
                    , $"https://{ip}:9090"
                    , @"/LazyServer/LazyCommand/PostCmd"
                    , LogClient.Config.Instance.GetValue(LogClient.Category.Api, LogClient.Key.AccessKey)
                    , LogClient.Config.Instance.GetValue(LogClient.Category.Api, LogClient.Key.SecretKey)
                    , 5);

                response = await task;
                wcfResponse = JsonConvert.DeserializeObject<WcfResponse>(response);
                if (!wcfResponse.IsSuccess)
                {
                    checkResult = "sql failed";
                    return checkResult;
                }

                if (wcfResponse.IsSuccess)
                    checkResult = "ok";
                else
                    throw new Exception(wcfResponse.ErrorMessage);

            }
            catch (Exception)
            {
                MessageBox.Show($"check {serverName} agent!");
            }

            return checkResult;
        }



        private string GetPublicIp(string serverName)
        {
            string serverPublicIp = "";
            foreach (var a in fileDb.TBL_SERVER.Data)
            {
                if (a.Key.serverName.Equals(serverName, StringComparison.OrdinalIgnoreCase))
                    serverPublicIp = a.Value.serverPublicIp;
            }
            return serverPublicIp;
        }



        private async Task MirroringStatusReload()
        {
            try
            {
                // query database mirroring info 
                TypeSql typeSql = new TypeSql
                {
                    SyncAsync = "Sync",
                    ConnectionTimeout = 5.ToString(),
                    CommandTimeout = (10).ToString(),
                    QueryEchoYN = "N",
                    HeaderYN = "N",
                    Database = "master",
                    Querys = @"
SET NOCOUNT ON
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED 

DECLARE @SERVICENAME NVARCHAR(100), @SQLSERVER_START_TIME DATETIME
SELECT  @SERVICENAME = @@SERVICENAME
    , @SQLSERVER_START_TIME = SQLSERVER_START_TIME
FROM MASTER.SYS.DM_OS_SYS_INFO

SELECT  
    @@SERVERNAME SERVERNAME
    ,DB_NAME(SD.[DATABASE_ID]) DATABASE_NAME
    ,HAS_DBACCESS(DB_NAME(SD.[DATABASE_ID])) HAS_DBACCESS_STATE
    ,SD.MIRRORING_STATE_DESC            
    ,SUBSTRING (SD.MIRRORING_PARTNER_NAME, 7,CHARINDEX(':', SD.MIRRORING_PARTNER_NAME, 10) -7) MIRRORING_PARTNER
    ,SD.MIRRORING_ROLE_DESC             
    ,SD.MIRRORING_SAFETY_LEVEL_DESC     
    ,SD.MIRRORING_WITNESS_NAME          
    ,SD.MIRRORING_CONNECTION_TIMEOUT
    ,CONVERT(VARCHAR(30), @SQLSERVER_START_TIME, 120) SQLSERVER_START_TIME 
    ,CONVERT(VARCHAR(30), B.CREATE_DATE, 120) AS DATABASE_CREATE_DATE
    ,B.RECOVERY_MODEL_DESC
FROM SYS.DATABASE_MIRRORING AS SD
    JOIN SYS.DATABASES B
    ON DB_NAME(SD.DATABASE_ID) = B.NAME 
WHERE DB_NAME(SD.DATABASE_ID) NOT IN ('TEMPDB', 'MODEL') 
FOR JSON PATH
"
                };

                string cmdText = JsonConvert.SerializeObject(typeSql);

                var task = dataManager.Execute
                    ("ExecuterSql"
                    , "TypeSql"
                    , cmdText
                    , CsLib.RequestType.POST
                    , $"https://{GetPublicIp(textBoxMasterServerName.Text)}:9090"
                    , @"/LazyServer/LazyCommand/PostCmd"
                    , LogClient.Config.Instance.GetValue(LogClient.Category.Api, LogClient.Key.AccessKey)
                    , LogClient.Config.Instance.GetValue(LogClient.Category.Api, LogClient.Key.SecretKey));

                string response = await task;
                WcfResponse wcfResponse = JsonConvert.DeserializeObject<WcfResponse>(response);
                if (!wcfResponse.IsSuccess)
                    throw new Exception(wcfResponse.ResultMessage + wcfResponse.ErrorMessage);

                List<DATABASE_MIRRORING_INFO> databaseMirroringInfos = JsonConvert.DeserializeObject<List<DATABASE_MIRRORING_INFO>>(wcfResponse.ResultMessage);

                dgvMirrorStatus.InvokeIfRequired(s =>
                {
                    s.Rows.Clear();
                    foreach (var a in databaseMirroringInfos)
                    {
                        int n = s.Rows.Add();
                        s.Rows[n].Cells["CHECKBOX"].Value = "false";
                        s.Rows[n].Cells["MIRRORINGSTATUS"].Value = "NULL";
                        s.Rows[n].Cells["SERVERNAME"].Value = a.SERVERNAME;
                        s.Rows[n].Cells["DATABASE_NAME"].Value = a.DATABASE_NAME;
                        s.Rows[n].Cells["RECOVERY_MODEL_DESC"].Value = a.RECOVERY_MODEL_DESC;
                        s.Rows[n].Cells["HAS_DBACCESS_STATE"].Value = a.HAS_DBACCESS_STATE;
                        s.Rows[n].Cells["MIRRORING_STATE_DESC"].Value = a.MIRRORING_STATE_DESC;
                        s.Rows[n].Cells["MIRRORING_PARTNER"].Value = a.MIRRORING_PARTNER;
                        s.Rows[n].Cells["MIRRORING_ROLE_DESC"].Value = a.MIRRORING_ROLE_DESC;
                        s.Rows[n].Cells["MIRRORING_SAFETY_LEVEL_DESC"].Value = a.MIRRORING_SAFETY_LEVEL_DESC;
                        s.Rows[n].Cells["MIRRORING_WITNESS_NAME"].Value = a.MIRRORING_WITNESS_NAME;
                        s.Rows[n].Cells["MIRRORING_CONNECTION_TIMEOUT"].Value = a.MIRRORING_CONNECTION_TIMEOUT;
                        s.Rows[n].Cells["SQLSERVER_START_TIME"].Value = a.SQLSERVER_START_TIME;
                        s.Rows[n].Cells["DATABASE_CREATE_DATE"].Value = a.DATABASE_CREATE_DATE;
                    }
                });

                if (!wcfResponse.IsSuccess)
                {
                    throw new Exception(wcfResponse.ErrorMessage);
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        private async void buttonMirrorStatusReload_Click(object sender, EventArgs e)
        {
            try
            {
                await MirroringStatusReload();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        async Task<WcfResponse> AsyncBackupRestore(string backupOrRestore, string fullOrLogBackup, string databaseName, string backupPath, string serverIp)
        {
            try
            {
                if (backupPath.Substring(backupPath.Length - 1, 1).Equals(@"\"))
                    backupPath = backupPath.Substring(0, backupPath.Length - 1);

                string query = string.Empty;
                if (backupOrRestore.Equals("Backup", StringComparison.OrdinalIgnoreCase))
                {
                    if (fullOrLogBackup.Equals("fullBackup", StringComparison.OrdinalIgnoreCase))
                        query = $@"backup database [{databaseName.Trim()}] to disk ='{backupPath.Trim() + @"\" + databaseName.Trim()}.full' with init, stats = 10";
                    else
                        query = $@"backup log [{databaseName.Trim()}] to disk ='{backupPath.Trim() + @"\" + databaseName.Trim()}.log' with init";
                }
                else
                {
                    if (fullOrLogBackup.Equals("fullBackup", StringComparison.OrdinalIgnoreCase))
                        query = $@"restore database [{databaseName.Trim()}] from disk ='{backupPath.Trim() + @"\" + databaseName.Trim()}.full' with norecovery, replace, stats = 10";
                    else
                        query = $@"restore log [{databaseName.Trim()}] from disk ='{backupPath.Trim() + @"\" + databaseName.Trim()}.log' with norecovery";

                }

                TypeSql typeSql = new TypeSql
                {
                    SyncAsync = "Async",
                    ConnectionTimeout = 5.ToString(),
                    CommandTimeout = (60 * 60 * 24).ToString(),
                    Database = "master",
                    Querys = query
                };

                string cmdText = JsonConvert.SerializeObject(typeSql);
                sbResultAll.Append(cmdText + Environment.NewLine);

                var task = dataManager.Execute
                    ("ExecuterSql"
                    , "TypeSql"
                    , cmdText
                    , CsLib.RequestType.POST
                    , $"https://{serverIp}:9090"
                    , @"/LazyServer/LazyCommand/PostCmd"
                    , LogClient.Config.Instance.GetValue(LogClient.Category.Api, LogClient.Key.AccessKey)
                    , LogClient.Config.Instance.GetValue(LogClient.Category.Api, LogClient.Key.SecretKey));

                string response = await task;
                sbResultAll.Append(response + Environment.NewLine);
                return JsonConvert.DeserializeObject<WcfResponse>(response);
            }
            catch (Exception)
            {
                throw;
            }
        }



        bool AsyncCmdCompleteCheck(string message)
        {
            if (message.IndexOf("async cmd completed") > 0)
                return true;
            else
                return false;
        }

        bool AsyncCmdSuccessCheck(string message)
        {
            if (message.IndexOf("exception info") > 0)
                return false;
            else
                return true;
        }

        async Task<WcfResponse> AsyncReadLog(string logFullname, string serverIp)
        {

            string resultMessageBackup = sbResultAll.ToString();
            WcfResponse wcfResponse;
            // result parse
            while (true)
            {
                var task = dataManager.Execute
                ("ExecuterPs"
                , "out-string"
                , $@"Get-Content '{logFullname}'"
                , CsLib.RequestType.POST
                , $"https://{serverIp}:9090"
                , @"/LazyServer/LazyCommand/PostCmd"
                , LogClient.Config.Instance.GetValue(LogClient.Category.Api, LogClient.Key.AccessKey)
                , LogClient.Config.Instance.GetValue(LogClient.Category.Api, LogClient.Key.SecretKey));

                string response = await task;
                wcfResponse = JsonConvert.DeserializeObject<WcfResponse>(response);
                int percentCompleted = 0;
                if (AsyncCmdCompleteCheck(wcfResponse.ResultMessage) || wcfResponse.IsSuccess == false)
                {
                    percentCompleted = GetPercentComplte(wcfResponse.ResultMessage);
                    progressBarPercentChange(percentCompleted);
                    if (!AsyncCmdSuccessCheck(wcfResponse.ResultMessage))
                    {
                        percentCompleted = GetPercentComplte(wcfResponse.ResultMessage);
                        progressBarPercentChange(percentCompleted);
                        wcfResponse.IsSuccess = false;
                    }
                    break;
                }
                sbResultAll.Clear();
                sbResultAll.Append(resultMessageBackup + wcfResponse.ResultMessage + wcfResponse.ErrorMessage);
                progressBarPercentChange(percentCompleted);

                var taskDelay = Task.Delay(2000);
                await taskDelay;
            }
            return wcfResponse;
        }

        private int GetPercentComplte(string message)
        {
            int percent = 0;
            if (message.Contains("TypeSql"))
            {
                string[] messageLine = message.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
                string lastPercent = string.Empty;
                foreach (var a in messageLine)
                    if (a.Contains("percent") && a.Contains(":::"))
                        lastPercent = a;

                if (lastPercent != "")
                {
                    string[] linePercent = lastPercent.Split(new[] { ":::" }, StringSplitOptions.None);
                    string messagePercentPart = linePercent[2].Trim();
                    linePercent = messagePercentPart.Split(new[] { " " }, StringSplitOptions.None);
                    string maybePercentString = linePercent[0];

                    if (int.TryParse(maybePercentString, out int maybePercentInt))
                        percent = maybePercentInt;
                    else
                        percent = 0;
                }
            }

            if (message.Contains("TypeObjectStorage"))
            {
                string[] messageLine = message.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
                string lastPercent = string.Empty;
                foreach (var a in messageLine)
                    if (a.Contains("%") && a.Contains(":::") && a.Contains(","))
                        lastPercent = a;

                if (lastPercent != "")
                {

                    lastPercent = lastPercent.Substring(lastPercent.Length - 5);
                    lastPercent = lastPercent.Replace("%", "");
                    lastPercent = lastPercent.Replace(",", "");
                    lastPercent = lastPercent.Trim();

                    if (int.TryParse(lastPercent, out int percentvalue))
                        percent = percentvalue;
                    else
                        percent = 0;
                }
            }

            if (message.Contains("async cmd completed"))
                percent = 100;

            return percent;
        }

        private void progressBarInit()
        {
            try
            {
                progressBarWidth = pictureBoxProgressBar.Width;
                progressBarHeihgt = pictureBoxProgressBar.Height;
                progressBarUnit = progressBarWidth / 100.0;
                bitmap = new Bitmap(progressBarWidth, progressBarHeihgt);
                progressBarPercentChange(CurrentPercent, CurrentStepName);
            }
            catch (Exception)
            { }
        }

        private void progressBarPercentChange(double progressBarCompletePercent, string progressBarStepText = "")
        {
            try
            {
                graphics = Graphics.FromImage(bitmap);
                graphics.Clear(Color.FromArgb(80, 103, 202, 236));
                graphics.FillRectangle(
                    new SolidBrush(Color.FromArgb(255, 103, 202, 236)),
                    new Rectangle(0, 0, (int)(progressBarCompletePercent * progressBarUnit), progressBarHeihgt
                    ));
                pictureBoxProgressBar.Image = bitmap;
                labelProgressBarPercent.Text = $"{(int)progressBarCompletePercent}% completed";
                if (progressBarStepText != "")
                    labelProgressBarText.Text = progressBarStepText;
            }
            catch (Exception)
            { }
        }

        int progressBarWidth, progressBarHeihgt;
        double progressBarUnit;
        Bitmap bitmap;
        int CurrentPercent = 0;
        string CurrentStepName = "";

        async Task<WcfResponse> AsyncObjectUploadDownload(string localFileFullname, string remoteFileFullname, string serverIp, string UploadOrDownload)
        {
            try
            {
                TypeObjectStorage typeObjectStorage = new TypeObjectStorage
                {
                    UploadDownload = UploadOrDownload,
                    ServiceUrl = dataManager.GetValue(DataManager.Category.ObjectStorage, DataManager.Key.Endpoint),
                    BucketName = dataManager.GetValue(DataManager.Category.ObjectStorage, DataManager.Key.Bucket),
                    LocalFileFullname = localFileFullname.Replace(@"/", @"\"),
                    RemoteFileFullname = remoteFileFullname.Replace(@"\", @"/")
                };

                string cmdText = JsonConvert.SerializeObject(typeObjectStorage);

                var task = dataManager.Execute
                    ("ExecuterRest"
                    , "TypeObjectStorage"
                    , cmdText
                    , CsLib.RequestType.POST
                    , $"https://{serverIp}:9090"
                    , @"/LazyServer/LazyCommand/PostCmd"
                    , LogClient.Config.Instance.GetValue(LogClient.Category.Api, LogClient.Key.AccessKey)
                    , LogClient.Config.Instance.GetValue(LogClient.Category.Api, LogClient.Key.SecretKey));

                string response = await task;
                return JsonConvert.DeserializeObject<WcfResponse>(response);
            }
            catch (Exception)
            {
                throw;
            }
        }
        private async void buttonStartAutomaticMirroring_Click(object sender, EventArgs e)
        {
            try
            {

                if (textBoxSlaveServerName.Text == "")
                {
                    MessageBox.Show("SlaveServerName is null!");
                    return;
                }
                if (textBoxMasterServerName.Text == "")
                {
                    MessageBox.Show("MasterServerName is null!");
                    return;
                }
                if (!labelMasterServerCheckStatusValue.Text.Equals("ok"))
                {
                    MessageBox.Show("check master server folder");
                    return;
                }

                if (!labelSlaveServerCheckStatusValue.Text.Equals("ok"))
                {
                    MessageBox.Show("check slave server folder");
                    return;
                }

                WcfResponse wcfResponse;

                sbResultAll.Clear();
                string cmdText = string.Empty;
                string response = string.Empty;

                var typeMonControllerStop = new TypeMonController
                {
                    MonName = "BackupManager",
                    StopStart = "Stop"
                };
                cmdText = JsonConvert.SerializeObject(typeMonControllerStop);
                Task<string> BackupMangerStopTask = dataManager.Execute
                    ("ExecuterRest"
                    , "TypeMonController"
                    , cmdText
                    , CsLib.RequestType.POST
                    , $"https://{GetPublicIp(textBoxMasterServerName.Text)}:9090"
                    , @"/LazyServer/LazyCommand/PostCmd"
                    , LogClient.Config.Instance.GetValue(LogClient.Category.Api, LogClient.Key.AccessKey)
                    , LogClient.Config.Instance.GetValue(LogClient.Category.Api, LogClient.Key.SecretKey));

                response = await BackupMangerStopTask;
                wcfResponse = JsonConvert.DeserializeObject<WcfResponse>(response);

                if (!wcfResponse.IsSuccess)
                    throw new Exception("Backup Manager Stop Failed");


                typeMonControllerStop = new TypeMonController
                {
                    MonName = "BackupManager",
                    StopStart = "Stop"
                };
                cmdText = JsonConvert.SerializeObject(typeMonControllerStop);
                Task<string> SlaveBackupMangerStopTask = dataManager.Execute
                    ("ExecuterRest"
                    , "TypeMonController"
                    , cmdText
                    , CsLib.RequestType.POST
                    , $"https://{GetPublicIp(textBoxSlaveServerName.Text)}:9090"
                    , @"/LazyServer/LazyCommand/PostCmd"
                    , LogClient.Config.Instance.GetValue(LogClient.Category.Api, LogClient.Key.AccessKey)
                    , LogClient.Config.Instance.GetValue(LogClient.Category.Api, LogClient.Key.SecretKey));

                response = await SlaveBackupMangerStopTask;
                wcfResponse = JsonConvert.DeserializeObject<WcfResponse>(response);

                if (!wcfResponse.IsSuccess)
                    throw new Exception("Slave Backup Manager Stop Failed");


                sbResultAllBackup = "";
                labelProgressBarText.Text = "Automatic Mirroring Started";
                string objectPath = @"mirrorInitailBackup/";
                foreach (DataGridViewRow item in dgvMirrorStatus.Rows)
                {
                    if (bool.Parse(item.Cells["CheckBox"].Value.ToString()))
                    {

                        item.Cells["MirroringStatus"].Value = "Working...";
                        ////////////////////////////////////////////////
                        labelProgressBarText.Text = "1. full backup";

                        if (true)
                        {
                            var task = AsyncBackupRestore(
                                "Backup"
                                , "fullBackup"
                                , item.Cells["DATABASE_NAME"].Value.ToString()
                                , textBoxBackupRestorePath.Text.Trim()
                                , GetPublicIp(textBoxMasterServerName.Text));
                            wcfResponse = await task;
                        }

                        sbResultAllBackup = sbResultAll.ToString();


                        #region start
                        // read log
                        if (wcfResponse.IsSuccess)
                        {
                            var task = AsyncReadLog(
                                wcfResponse.ResultMessage.Trim()
                                , GetPublicIp(textBoxMasterServerName.Text));
                            wcfResponse = await task;
                        }
                        else
                        {
                            MessageBox.Show("full backup start fail, please retry");
                            return;
                        }

                        sbResultAll.Clear();
                        sbResultAll.Append(sbResultAllBackup + wcfResponse.ResultMessage + wcfResponse.ErrorMessage);

                        if (!wcfResponse.IsSuccess)
                            return;

                        ////////////////////////////////////////////////
                        labelProgressBarText.Text = "2. log backup";

                        if (wcfResponse.IsSuccess)
                        {
                            var task = AsyncBackupRestore(
                                "Backup"
                                , "LogBackup"
                                , item.Cells["DATABASE_NAME"].Value.ToString()
                                , textBoxBackupRestorePath.Text.Trim()
                                , GetPublicIp(textBoxMasterServerName.Text));
                            wcfResponse = await task;
                        }

                        sbResultAllBackup = sbResultAll.ToString();

                        // read log file
                        if (wcfResponse.IsSuccess)
                        {
                            var task = AsyncReadLog(
                                wcfResponse.ResultMessage.Trim()
                                , GetPublicIp(textBoxMasterServerName.Text));
                            wcfResponse = await task;
                        }
                        else
                        {
                            MessageBox.Show("log backup start fail, please retry");
                            return;
                        }

                        sbResultAll.Clear();
                        sbResultAll.Append(sbResultAllBackup + wcfResponse.ResultMessage + wcfResponse.ErrorMessage);

                        if (!wcfResponse.IsSuccess)
                            return;

                        ////////////////////////////////////////////////
                        labelProgressBarText.Text = "3. full backup upload";

                        if (wcfResponse.IsSuccess)
                        {
                            var task = AsyncObjectUploadDownload(
                                textBoxBackupRestorePath.Text.Trim() + item.Cells["DATABASE_NAME"].Value.ToString().Trim() + ".full"
                                , objectPath + item.Cells["DATABASE_NAME"].Value.ToString().Trim() + ".full"
                                , GetPublicIp(textBoxMasterServerName.Text)
                                , "Upload"
                                );

                            wcfResponse = await task;
                        }

                        sbResultAllBackup = sbResultAll.ToString();

                        // read log file
                        if (wcfResponse.IsSuccess)
                        {
                            var task = AsyncReadLog(
                                wcfResponse.ResultMessage.Trim()
                                , GetPublicIp(textBoxMasterServerName.Text));
                            wcfResponse = await task;
                        }
                        else
                        {
                            MessageBox.Show("log backup start fail, please retry");
                            return;
                        }

                        sbResultAll.Clear();
                        sbResultAll.Append(sbResultAllBackup + wcfResponse.ResultMessage + wcfResponse.ErrorMessage);

                        if (!wcfResponse.IsSuccess)
                            return;

                        ////////////////////////////////////////////////
                        labelProgressBarText.Text = "4. log backup object upload";

                        if (wcfResponse.IsSuccess)
                        {
                            var task = AsyncObjectUploadDownload(
                                textBoxBackupRestorePath.Text.Trim() + item.Cells["DATABASE_NAME"].Value.ToString().Trim() + ".log"
                                , objectPath + item.Cells["DATABASE_NAME"].Value.ToString().Trim() + ".log"
                                , GetPublicIp(textBoxMasterServerName.Text)
                                , "Upload"
                                );
                            wcfResponse = await task;
                        }

                        sbResultAllBackup = sbResultAll.ToString();

                        // read log file
                        if (wcfResponse.IsSuccess)
                        {
                            var task = AsyncReadLog(
                                wcfResponse.ResultMessage.Trim()
                                , GetPublicIp(textBoxMasterServerName.Text));
                            wcfResponse = await task;
                        }
                        else
                        {
                            MessageBox.Show("log backup start fail, please retry");
                            return;
                        }

                        sbResultAll.Clear();
                        sbResultAll.Append(sbResultAllBackup + wcfResponse.ResultMessage + wcfResponse.ErrorMessage);

                        if (!wcfResponse.IsSuccess)
                            return;

                        ////////////////////////////////////////////////
                        labelProgressBarText.Text = "5. full backup object download";


                        if (wcfResponse.IsSuccess)
                        {
                            var task = AsyncObjectUploadDownload(
                                textBoxBackupRestorePath.Text.Trim() + item.Cells["DATABASE_NAME"].Value.ToString().Trim() + ".full"
                                , objectPath + item.Cells["DATABASE_NAME"].Value.ToString().Trim() + ".full"
                                , fileDb.TBL_SERVER.Data[new TBL_SERVER_KEY { serverName = textBoxSlaveServerName.Text }].serverPublicIp
                                , "Download"
                                );

                            wcfResponse = await task;
                        }

                        sbResultAllBackup = sbResultAll.ToString();

                        // read log file
                        if (wcfResponse.IsSuccess)
                        {
                            var task = AsyncReadLog(
                                wcfResponse.ResultMessage.Trim()
                                , fileDb.TBL_SERVER.Data[new TBL_SERVER_KEY { serverName = textBoxSlaveServerName.Text }].serverPublicIp);
                            wcfResponse = await task;
                        }
                        else
                        {
                            MessageBox.Show("full backup download fail, please retry");
                            return;
                        }

                        sbResultAll.Clear();
                        sbResultAll.Append(sbResultAllBackup + wcfResponse.ResultMessage + wcfResponse.ErrorMessage);

                        if (!wcfResponse.IsSuccess)
                            return;

                        ////////////////////////////////////////////////
                        labelProgressBarText.Text = "6. log backup object download";

                        if (wcfResponse.IsSuccess)
                        {
                            var task = AsyncObjectUploadDownload(
                                textBoxBackupRestorePath.Text.Trim() + item.Cells["DATABASE_NAME"].Value.ToString().Trim() + ".log"
                                , objectPath + item.Cells["DATABASE_NAME"].Value.ToString().Trim() + ".log"
                                , fileDb.TBL_SERVER.Data[new TBL_SERVER_KEY { serverName = textBoxSlaveServerName.Text }].serverPublicIp
                                , "Download"
                                );

                            wcfResponse = await task;
                        }

                        sbResultAllBackup = sbResultAll.ToString();

                        // read log file
                        if (wcfResponse.IsSuccess)
                        {
                            var task = AsyncReadLog(
                                wcfResponse.ResultMessage.Trim()
                                , fileDb.TBL_SERVER.Data[new TBL_SERVER_KEY { serverName = textBoxSlaveServerName.Text }].serverPublicIp);
                            wcfResponse = await task;
                        }
                        else
                        {
                            MessageBox.Show("log backup download fail, please retry");
                            return;
                        }

                        sbResultAll.Clear();
                        sbResultAll.Append(sbResultAllBackup + wcfResponse.ResultMessage + wcfResponse.ErrorMessage);

                        if (!wcfResponse.IsSuccess)
                            return;

                        ////////////////////////////////////////////////
                        labelProgressBarText.Text = "7. full restore";


                        if (true)
                        {
                            var task = AsyncBackupRestore(
                                "Restore"
                                , "fullBackup"
                                , item.Cells["DATABASE_NAME"].Value.ToString()
                                , textBoxBackupRestorePath.Text.Trim()
                                , fileDb.TBL_SERVER.Data[new TBL_SERVER_KEY { serverName = textBoxSlaveServerName.Text }].serverPublicIp);
                            wcfResponse = await task;
                        }


                        sbResultAllBackup = sbResultAll.ToString();

                        // read log
                        if (wcfResponse.IsSuccess)
                        {
                            var task = AsyncReadLog(
                                wcfResponse.ResultMessage.Trim()
                                , fileDb.TBL_SERVER.Data[new TBL_SERVER_KEY { serverName = textBoxSlaveServerName.Text }].serverPublicIp);
                            wcfResponse = await task;
                        }
                        else
                        {
                            MessageBox.Show("full restore start fail, please retry");
                            return;
                        }

                        sbResultAll.Clear();
                        sbResultAll.Append(sbResultAllBackup + wcfResponse.ResultMessage + wcfResponse.ErrorMessage);

                        if (!wcfResponse.IsSuccess)
                            return;

                        ////////////////////////////////////////////////
                        labelProgressBarText.Text = "8. log restore";

                        if (wcfResponse.IsSuccess)
                        {
                            var task = AsyncBackupRestore(
                                "Restore"
                                , "LogBackup"
                                , item.Cells["DATABASE_NAME"].Value.ToString()
                                , textBoxBackupRestorePath.Text.Trim()
                                , fileDb.TBL_SERVER.Data[new TBL_SERVER_KEY { serverName = textBoxSlaveServerName.Text }].serverPublicIp);
                            wcfResponse = await task;
                        }

                        sbResultAllBackup = sbResultAll.ToString();

                        // read log file
                        if (wcfResponse.IsSuccess)
                        {
                            var task = AsyncReadLog(
                                wcfResponse.ResultMessage.Trim()
                                , fileDb.TBL_SERVER.Data[new TBL_SERVER_KEY { serverName = textBoxSlaveServerName.Text }].serverPublicIp);
                            wcfResponse = await task;
                        }
                        else
                        {
                            MessageBox.Show("log restore start fail, please retry");
                            return;
                        }

                        sbResultAll.Clear();
                        sbResultAll.Append(sbResultAllBackup + wcfResponse.ResultMessage + wcfResponse.ErrorMessage);

                        if (!wcfResponse.IsSuccess)
                            return;

                        ////////////////////////////////////////////////
                        labelProgressBarText.Text = "9. Create EndPoint Master";

                        if (wcfResponse.IsSuccess)
                        {
                            TypeSql typeSql = new TypeSql
                            {
                                SyncAsync = "Sync",
                                ConnectionTimeout = 5.ToString(),
                                CommandTimeout = (60 * 60 * 24).ToString(),
                                QueryEchoYN = "Y",
                                Database = "master",
                                Querys = QueryCreateMirroringEndPoint
                            };

                            cmdText = JsonConvert.SerializeObject(typeSql);

                            var task = dataManager.Execute
                                ("ExecuterSql"
                                , "TypeSql"
                                , cmdText
                                , CsLib.RequestType.POST
                                , $"https://{GetPublicIp(textBoxMasterServerName.Text)}:9090"
                                , @"/LazyServer/LazyCommand/PostCmd"
                                , LogClient.Config.Instance.GetValue(LogClient.Category.Api, LogClient.Key.AccessKey)
                                , LogClient.Config.Instance.GetValue(LogClient.Category.Api, LogClient.Key.SecretKey));

                            response = await task;

                            wcfResponse = JsonConvert.DeserializeObject<WcfResponse>(response);
                            sbResultAll.Append(response);

                            if (!wcfResponse.IsSuccess)
                            {
                                return;
                            }
                        }

                        ////////////////////////////////////////////////
                        labelProgressBarText.Text = "10. Create EndPoint Slave";

                        if (wcfResponse.IsSuccess)
                        {
                            TypeSql typeSql = new TypeSql
                            {
                                SyncAsync = "Sync",
                                ConnectionTimeout = 5.ToString(),
                                CommandTimeout = (60 * 60 * 24).ToString(),
                                QueryEchoYN = "Y",
                                Database = "master",
                                Querys = QueryCreateMirroringEndPoint
                            };

                            cmdText = JsonConvert.SerializeObject(typeSql);

                            var task = dataManager.Execute
                                ("ExecuterSql"
                                , "TypeSql"
                                , cmdText
                                , CsLib.RequestType.POST
                                , $"https://{fileDb.TBL_SERVER.Data[new TBL_SERVER_KEY { serverName = textBoxSlaveServerName.Text }].serverPublicIp}:9090"
                                , @"/LazyServer/LazyCommand/PostCmd"
                                , LogClient.Config.Instance.GetValue(LogClient.Category.Api, LogClient.Key.AccessKey)
                                , LogClient.Config.Instance.GetValue(LogClient.Category.Api, LogClient.Key.SecretKey));

                            response = await task;

                            wcfResponse = JsonConvert.DeserializeObject<WcfResponse>(response);

                            sbResultAll.Append(response);

                            if (!wcfResponse.IsSuccess)
                            {
                                return;
                            }
                        }

                        ////////////////////////////////////////////////
                        labelProgressBarText.Text = "11. Mirroring Setting Slave";

                        if (wcfResponse.IsSuccess)
                        {
                            TypeSql typeSql = new TypeSql
                            {
                                SyncAsync = "Sync",
                                ConnectionTimeout = 5.ToString(),
                                CommandTimeout = (60 * 60 * 24).ToString(),
                                QueryEchoYN = "Y",
                                Database = "master",
                                Querys = $@"ALTER DATABASE [{item.Cells["DATABASE_NAME"].Value.ToString().Trim() }] SET PARTNER='TCP://{GetPublicIp(textBoxMasterServerName.Text)}:5022'"
                            };

                            cmdText = JsonConvert.SerializeObject(typeSql);

                            var task = dataManager.Execute
                                ("ExecuterSql"
                                , "TypeSql"
                                , cmdText
                                , CsLib.RequestType.POST
                                , $"https://{fileDb.TBL_SERVER.Data[new TBL_SERVER_KEY { serverName = textBoxSlaveServerName.Text }].serverPublicIp}:9090"
                                , @"/LazyServer/LazyCommand/PostCmd"
                                , LogClient.Config.Instance.GetValue(LogClient.Category.Api, LogClient.Key.AccessKey)
                                , LogClient.Config.Instance.GetValue(LogClient.Category.Api, LogClient.Key.SecretKey));

                            response = await task;
                            wcfResponse = JsonConvert.DeserializeObject<WcfResponse>(response);
                            sbResultAll.Append(response);

                            if (!wcfResponse.IsSuccess)
                            {
                                return;
                            }
                        }

                        ////////////////////////////////////////////////
                        labelProgressBarText.Text = "12. Mirroring Setting Master";

                        if (wcfResponse.IsSuccess)
                        {
                            TypeSql typeSql = new TypeSql
                            {
                                SyncAsync = "Sync",
                                ConnectionTimeout = 5.ToString(),
                                CommandTimeout = (60 * 60 * 24).ToString(),
                                QueryEchoYN = "Y",
                                Database = "master",
                                Querys =
$@"
                        ALTER DATABASE [{item.Cells["DATABASE_NAME"].Value.ToString().Trim() }] SET PARTNER='TCP://{fileDb.TBL_SERVER.Data[new TBL_SERVER_KEY { serverName = textBoxSlaveServerName.Text }].serverPublicIp}:5022'
                        go
                        ALTER DATABASE [{item.Cells["DATABASE_NAME"].Value.ToString().Trim()}] SET SAFETY FULL
                        "
                            };

                            cmdText = JsonConvert.SerializeObject(typeSql);

                            var task = dataManager.Execute
                                ("ExecuterSql"
                                , "TypeSql"
                                , cmdText
                                , CsLib.RequestType.POST
                                , $"https://{GetPublicIp(textBoxMasterServerName.Text)}:9090"
                                , @"/LazyServer/LazyCommand/PostCmd"
                                , LogClient.Config.Instance.GetValue(LogClient.Category.Api, LogClient.Key.AccessKey)
                                , LogClient.Config.Instance.GetValue(LogClient.Category.Api, LogClient.Key.SecretKey));

                            response = await task;
                            wcfResponse = JsonConvert.DeserializeObject<WcfResponse>(response);
                            sbResultAll.Append(response);

                            if (!wcfResponse.IsSuccess)
                            {
                                return;
                            }

                        }
                        item.Cells["MirroringStatus"].Value = "Completed";
                        #endregion
                    }
                }
                await MirroringStatusReload();

                var typeMonControllerStart = new TypeMonController
                {
                    MonName = "BackupManager",
                    StopStart = "Start"
                };
                cmdText = JsonConvert.SerializeObject(typeMonControllerStart);
                Task<string> BackupMangerStartTask = dataManager.Execute
                    ("ExecuterRest"
                    , "TypeMonController"
                    , cmdText
                    , CsLib.RequestType.POST
                    , $"https://{GetPublicIp(textBoxMasterServerName.Text)}:9090"
                    , @"/LazyServer/LazyCommand/PostCmd"
                    , LogClient.Config.Instance.GetValue(LogClient.Category.Api, LogClient.Key.AccessKey)
                    , LogClient.Config.Instance.GetValue(LogClient.Category.Api, LogClient.Key.SecretKey));

                response = await BackupMangerStartTask;
                wcfResponse = JsonConvert.DeserializeObject<WcfResponse>(response);

                if (!wcfResponse.IsSuccess)
                    MessageBox.Show("Backup Manager Start Failed");


                typeMonControllerStart = new TypeMonController
                {
                    MonName = "BackupManager",
                    StopStart = "Start"
                };
                cmdText = JsonConvert.SerializeObject(typeMonControllerStart);
                Task<string> SlaveBackupMangerStartTask = dataManager.Execute
                    ("ExecuterRest"
                    , "TypeMonController"
                    , cmdText
                    , CsLib.RequestType.POST
                    , $"https://{GetPublicIp(textBoxSlaveServerName.Text)}:9090"
                    , @"/LazyServer/LazyCommand/PostCmd"
                    , LogClient.Config.Instance.GetValue(LogClient.Category.Api, LogClient.Key.AccessKey)
                    , LogClient.Config.Instance.GetValue(LogClient.Category.Api, LogClient.Key.SecretKey));

                response = await SlaveBackupMangerStartTask;
                wcfResponse = JsonConvert.DeserializeObject<WcfResponse>(response);

                if (!wcfResponse.IsSuccess)
                    MessageBox.Show("Slave Backup Manager Start Failed");

                labelProgressBarText.Text = "completed !";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " " + ex.StackTrace);
            }
        }

        string QueryCreateMirroringEndPoint =
@"
if not exists (SELECT * FROM sys.database_mirroring_endpoints where name = 'Mirroring')
CREATE ENDPOINT[Mirroring] STATE=STARTED AS TCP(LISTENER_PORT = 5022, LISTENER_IP = ALL) FOR DATA_MIRRORING(ROLE = ALL, AUTHENTICATION = WINDOWS NEGOTIATE, ENCRYPTION = DISABLED)
";

        private void progressBarInit(object sender, EventArgs e)
        {
            progressBarInit();
        }

        private async void buttonRemoveMirroring_Click(object sender, EventArgs e)
        {

            try
            {

                DialogResult result = MessageBox.Show("Do you really want to run?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result != DialogResult.Yes)
                    return;

                int checkBoxCount = 0;
                string checkedDatabaseName = string.Empty;
                foreach (DataGridViewRow item in dgvMirrorStatus.Rows)
                {
                    if (bool.Parse(item.Cells["CheckBox"].Value.ToString()))
                        checkBoxCount++;
                }
                if (checkBoxCount == 0)
                    throw new Exception("select database");

                foreach (DataGridViewRow item in dgvMirrorStatus.Rows)
                {
                    if (bool.Parse(item.Cells["CheckBox"].Value.ToString()))
                    {
                        checkBoxCount++;
                        checkedDatabaseName = item.Cells["DATABASE_NAME"].Value.ToString().Trim();
                        string query = $@"alter database [{checkedDatabaseName}] set partner off";

                        TypeSql typeSql = new TypeSql
                        {
                            SyncAsync = "Sync",
                            HeaderYN = "N",
                            ConnectionTimeout = 5.ToString(),
                            CommandTimeout = (30).ToString(),
                            Database = "master",
                            Querys = query
                        };

                        string cmdText = JsonConvert.SerializeObject(typeSql);
                        sbResultAll.Append(cmdText + Environment.NewLine);

                        var task = dataManager.Execute
                            ("ExecuterSql"
                            , "TypeSql"
                            , cmdText
                            , CsLib.RequestType.POST
                            , $"https://{GetPublicIp(textBoxMasterServerName.Text)}:9090"
                            , @"/LazyServer/LazyCommand/PostCmd"
                            , LogClient.Config.Instance.GetValue(LogClient.Category.Api, LogClient.Key.AccessKey)
                            , LogClient.Config.Instance.GetValue(LogClient.Category.Api, LogClient.Key.SecretKey));

                        string response = await task;
                        sbResultAll.Append(response + Environment.NewLine);
                        WcfResponse wcfResponse = JsonConvert.DeserializeObject<WcfResponse>(response);
                        if (!wcfResponse.IsSuccess)
                            MessageBox.Show(wcfResponse.ResultMessage + wcfResponse.ErrorMessage);
                    }
                }
                MessageBox.Show("completed");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                await MirroringStatusReload();
            }
        }


        private async void buttonDropDatabase_Click(object sender, EventArgs e)
        {

            try
            {
                DialogResult result = MessageBox.Show("Do you really want to run?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result != DialogResult.Yes)
                    return;

                int checkBoxCount = 0;
                string checkedDatabaseName = string.Empty;
                foreach (DataGridViewRow item in dgvMirrorStatus.Rows)
                {
                    if (bool.Parse(item.Cells["CheckBox"].Value.ToString()))
                        checkBoxCount++;
                }
                if (checkBoxCount == 0)
                    throw new Exception("select database");

                foreach (DataGridViewRow item in dgvMirrorStatus.Rows)
                {
                    if (bool.Parse(item.Cells["CheckBox"].Value.ToString()))
                    {
                        checkedDatabaseName = item.Cells["DATABASE_NAME"].Value.ToString().Trim();
                        string query =
$@"
alter database [{checkedDatabaseName}] set single_user with rollback immediate
go
drop database [{checkedDatabaseName}]
go
";

                        TypeSql typeSql = new TypeSql
                        {
                            SyncAsync = "Sync",
                            HeaderYN = "N",
                            ConnectionTimeout = 5.ToString(),
                            CommandTimeout = (30).ToString(),
                            Database = "master",
                            Querys = query
                        };

                        string cmdText = JsonConvert.SerializeObject(typeSql);
                        sbResultAll.Append(cmdText + Environment.NewLine);

                        var task = dataManager.Execute
                            ("ExecuterSql"
                            , "TypeSql"
                            , cmdText
                            , CsLib.RequestType.POST
                            , $"https://{GetPublicIp(textBoxMasterServerName.Text)}:9090"
                            , @"/LazyServer/LazyCommand/PostCmd"
                            , LogClient.Config.Instance.GetValue(LogClient.Category.Api, LogClient.Key.AccessKey)
                            , LogClient.Config.Instance.GetValue(LogClient.Category.Api, LogClient.Key.SecretKey));

                        string response = await task;
                        sbResultAll.Append(response + Environment.NewLine);
                        WcfResponse wcfResponse = JsonConvert.DeserializeObject<WcfResponse>(response);
                        if (!wcfResponse.IsSuccess)
                            MessageBox.Show(wcfResponse.ResultMessage + wcfResponse.ErrorMessage);
                    }
                }
                MessageBox.Show("completed");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                await MirroringStatusReload();
            }
        }

        private void buttonClearLog_Click(object sender, EventArgs e)
        {
            sbResultAll.Clear();
            MessageBox.Show("log cleared!");
        }

        private void buttonShowDetailLog_Click(object sender, EventArgs e)
        {
            FormPreview formPreview = FormPreview.Instance;
            formPreview.GroupBoxText = "Mirroring Log";
            formPreview.Script = sbResultAll.ToString();
            formPreview.StartPosition = FormStartPosition.CenterScreen;
            formPreview.ShowDialog();
        }
    }
}
