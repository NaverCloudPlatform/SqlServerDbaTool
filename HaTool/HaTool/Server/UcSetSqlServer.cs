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

namespace HaTool.Server
{
    public partial class UcSetSqlServer : UserControl
    {
        private static readonly Lazy<UcSetSqlServer> lazy =
            new Lazy<UcSetSqlServer>(() => new UcSetSqlServer(), LazyThreadSafetyMode.ExecutionAndPublication);

        public static UcSetSqlServer Instance { get { return lazy.Value; } }

        LogClient.Config logClientConfig = LogClient.Config.Instance;
        DataManager dataManager = DataManager.Instance;
        FileDb fileDb = FileDb.Instance;
        private StringBuilder sbResultAll = new StringBuilder();


        DataGridViewCheckBoxColumn ColumnServerCheckBox;
        DataGridViewTextBoxColumn ColumnServerName;
        DataGridViewTextBoxColumn ColumnServerSetupStatus;
        DataGridViewTextBoxColumn ColumnServerZoneNo;
        DataGridViewTextBoxColumn ColumnServerInstanceNo;
        DataGridViewTextBoxColumn ColumnServerPublicIp;
        DataGridViewTextBoxColumn ColumnServerPrivateIp;
        DataGridViewTextBoxColumn ColumnServerStatus;
        DataGridViewTextBoxColumn ColumnServerOperation;

        FormPreview formPreview = FormPreview.Instance;

        public string Sp_configure { get; set; }

        public string PsTemplate { get; set; }
        public string psTemplateChanged { get; set; }

        private void InitDgv()
        {
            ColumnServerCheckBox = new DataGridViewCheckBoxColumn();
            ColumnServerSetupStatus = new DataGridViewTextBoxColumn();
            ColumnServerName = new DataGridViewTextBoxColumn();
            ColumnServerZoneNo = new DataGridViewTextBoxColumn();
            ColumnServerInstanceNo = new DataGridViewTextBoxColumn();
            ColumnServerPublicIp = new DataGridViewTextBoxColumn();
            ColumnServerPrivateIp = new DataGridViewTextBoxColumn();
            ColumnServerStatus = new DataGridViewTextBoxColumn();
            ColumnServerOperation = new DataGridViewTextBoxColumn();

            ColumnServerCheckBox.HeaderText = "CheckBox";
            ColumnServerSetupStatus.HeaderText = "SetupStatus";
            ColumnServerName.HeaderText = "Name";
            ColumnServerZoneNo.HeaderText = "ZoneNo";
            ColumnServerInstanceNo.HeaderText = "InstanceNo";
            ColumnServerPublicIp.HeaderText = "PublicIp";
            ColumnServerPrivateIp.HeaderText = "PrivateIp";
            ColumnServerStatus.HeaderText = "Status";
            ColumnServerOperation.HeaderText = "Operation";

            ColumnServerCheckBox.Name = "CheckBox";
            ColumnServerSetupStatus.Name = "SetupStatus";
            ColumnServerName.Name = "Name";
            ColumnServerZoneNo.Name = "ZoneNo";
            ColumnServerInstanceNo.Name = "InstanceNo";
            ColumnServerPublicIp.Name = "PublicIp";
            ColumnServerPrivateIp.Name = "PrivateIp";
            ColumnServerStatus.Name = "Status";
            ColumnServerOperation.Name = "Operation";


            dgvServerList.Columns.AddRange(new DataGridViewColumn[]
            {
                ColumnServerCheckBox   ,
                ColumnServerSetupStatus,
                ColumnServerName       ,
                ColumnServerZoneNo     ,
                ColumnServerInstanceNo ,
                ColumnServerPublicIp   ,
                ColumnServerPrivateIp  ,
                ColumnServerStatus     ,
                ColumnServerOperation
            });



            dgvServerList.AllowUserToAddRows = false;
            dgvServerList.RowHeadersVisible = false;
            dgvServerList.BackgroundColor = Color.White;
            dgvServerList.AutoResizeColumns();
            dgvServerList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvServerList.Columns["Operation"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvServerList.AllowUserToResizeRows = false;


            HaTool.Global.ControlHelpers.dgvDesign(dgvServerList);
            dgvServerList.CellContentClick += new DataGridViewCellEventHandler(ControlHelpers.dgvLineColorChange);
        }

        public UcSetSqlServer()
        {
            InitializeComponent();
            formPreview.ScriptModifyEvent += PreviewClose;
            InitDgv();
            CollationDefaultSetting();
            progressBarInit();
        }

        private void CollationDefaultSetting()
        {
            comboBoxCollation.SelectedText = "Korean_Wansung_CI_AS";
        }

        private async Task ServerListLoad()
        {
            try
            {
                ControlHelpers.ButtonStatusChange(buttonServerListReload, "Requested");
                await fileDb.ReadTable(FileDb.TableName.TBL_SERVER);

                List<string> instanceNoList = new List<string>();

                foreach (var a in fileDb.TBL_SERVER.Data)
                {
                    if (a.Value.serverInstanceNo != "NULL")
                        instanceNoList.Add(a.Value.serverInstanceNo);
                }

                List<serverInstance> serverInstances = await ServerOperation.GetServerInstanceList(instanceNoList);

                dgvServerList.InvokeIfRequired(async s =>
                {
                    try
                    {

                        List<string> deleteServerNameList = new List<string>();
                        s.Rows.Clear();
                        foreach (var a in fileDb.TBL_SERVER.Data)
                        {
                            var serverInstance = serverInstances.Find(x => x.serverName == a.Key.serverName);

                            if (serverInstance != null)
                            {
                                int n = s.Rows.Add();
                                s.Rows[n].Cells["CheckBox"].Value = false;
                                s.Rows[n].Cells["SetupStatus"].Value = "NULL";
                                s.Rows[n].Cells["Name"].Value = a.Key.serverName;
                                s.Rows[n].Cells["ZoneNo"].Value = a.Value.zoneNo + "(" + serverInstance.zone.zoneCode + ")";
                                s.Rows[n].Cells["InstanceNo"].Value = a.Value.serverInstanceNo;
                                s.Rows[n].Cells["PublicIp"].Value = a.Value.serverPublicIp;
                                s.Rows[n].Cells["PrivateIp"].Value = a.Value.serverPrivateIp;
                                s.Rows[n].Cells["Status"].Value = serverInstance.serverInstanceStatus.code;
                                s.Rows[n].Cells["Operation"].Value = serverInstance.serverInstanceOperation.code;
                            }
                            else
                            {
                                deleteServerNameList.Add(a.Key.serverName);
                            }
                        }

                        foreach (var a in deleteServerNameList)
                        {
                            var p = new List<KeyValuePair<string, string>>();
                            p.Add(new KeyValuePair<string, string>("serverName", a));
                            await fileDb.DeleteTable(FileDb.TableName.TBL_SERVER, p);
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                });
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                ControlHelpers.ButtonStatusChange(buttonServerListReload, "Reload");
            }
        }



        private async void LoadData(object sender, EventArgs e)
        {
            try
            {
                dataManager.LoadUserData();
                LoadTextData();
                List<Task> tasks = new List<Task>();
                tasks.Add(ServerListLoad());
                await Task.WhenAll(tasks);
                progressBarInit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void LoadTextData()
        {
            try
            {
                PsTemplate = dataManager.GetValue(DataManager.Category.SetSql, DataManager.Key.PsTemplate).Trim();
                textBoxId.Text = dataManager.GetValue(DataManager.Category.SetSql, DataManager.Key.Id).Trim();
                textBoxPassword.Text = dataManager.GetValue(DataManager.Category.SetSql, DataManager.Key.Password).Trim();
                comboBoxCollation.Text = dataManager.GetValue(DataManager.Category.SetSql, DataManager.Key.Collation).Trim();
                textBoxPort.Text = dataManager.GetValue(DataManager.Category.SetSql, DataManager.Key.Port).Trim();
                textBoxTraceFlags.Text = dataManager.GetValue(DataManager.Category.SetSql, DataManager.Key.TraceFlags).Trim();
                Sp_configure = dataManager.GetValue(DataManager.Category.SetSql, DataManager.Key.Sp_configure).Trim();
                PsTemplateChange();

            }
            catch (Exception)
            {
                throw;
            }
        }

        public static Dictionary<string, string> ReadPowerShell(string queryAll)
        {
            Dictionary<string, string> querys = new Dictionary<string, string>();
            try
            {

                string query = string.Empty;
                string cmd = string.Empty;
                string[] lines = Regex.Split(queryAll, Environment.NewLine);
                foreach (string line in lines)
                {

                    if (line.Trim().StartsWith("###"))
                    {
                        if (query.Trim() != "")
                        {
                            querys.Add(cmd, query);
                        }
                        cmd = line;
                        query = string.Empty;
                    }
                    else
                    {
                        if (line != "")
                        {
                            query = query + Environment.NewLine + line;
                        }
                    }
                }
                if (query.Trim() != "")
                    querys.Add(cmd, query);
            }
            catch (Exception)
            {
                throw;
            }
            return querys;
        }

        private async Task<WcfResponse> SqlConnectionStringSetting(string publicIp)
        {

            await Execute(@"$FileName = 'c:\temp\lazylog.mdf'
if (Test-Path $FileName) {
  Remove-Item $FileName -Force
}", publicIp);
            await Execute(@"$FileName = 'c:\temp\lazylog_log.ldf'
if (Test-Path $FileName) {
  Remove-Item $FileName -Force
}", publicIp);

            WcfResponse wcfResponse = new WcfResponse();
            Task<string> taskString;
            string response = string.Empty;

            var TypeConfigSetting = new
            {
                ConfigFile = "LogClientConfig.txt",
                Category = "Encryption",
                Key = "GetCryptionKey",
                Value = LogClient.Config.Instance.GetValue(LogClient.Category.Encryption, LogClient.Key.GetCryptionKey)
            };

            var jt = JToken.Parse(JsonConvert.SerializeObject(TypeConfigSetting));
            string command = jt.ToString(Newtonsoft.Json.Formatting.Indented);

            taskString = dataManager.Execute
                ("ExecuterRest"
                , "TypeConfigSetting"
                , command
                , CsLib.RequestType.POST
                , $"https://{publicIp}:9090"
                , @"/LazyServer/LazyCommand/PostCmd"
                , LogClient.Config.Instance.GetValue(LogClient.Category.Api, LogClient.Key.AccessKey)
                , LogClient.Config.Instance.GetValue(LogClient.Category.Api, LogClient.Key.SecretKey)
                , 60);

            response = await taskString;
            wcfResponse = JsonConvert.DeserializeObject<WcfResponse>(response);

            if (!wcfResponse.IsSuccess)
                MessageBox.Show(wcfResponse.ResultMessage + wcfResponse.ErrorMessage);

            // start 
            TypeConfigSetting = new
            {
                ConfigFile = "LogClientConfig.txt",
                Category = "Encryption",
                Key = "GetCryptionKeyUrl",
                Value = LogClient.Config.Instance.GetValue(LogClient.Category.Encryption, LogClient.Key.GetCryptionKeyUrl)
            };

            jt = JToken.Parse(JsonConvert.SerializeObject(TypeConfigSetting));
            command = jt.ToString(Newtonsoft.Json.Formatting.Indented);

            taskString = dataManager.Execute
                ("ExecuterRest"
                , "TypeConfigSetting"
                , command
                , CsLib.RequestType.POST
                , $"https://{publicIp}:9090"
                , @"/LazyServer/LazyCommand/PostCmd"
                , LogClient.Config.Instance.GetValue(LogClient.Category.Api, LogClient.Key.AccessKey)
                , LogClient.Config.Instance.GetValue(LogClient.Category.Api, LogClient.Key.SecretKey)
                , 60);

            response = await taskString;
            wcfResponse = JsonConvert.DeserializeObject<WcfResponse>(response);

            if (!wcfResponse.IsSuccess)
                MessageBox.Show(wcfResponse.ResultMessage + wcfResponse.ErrorMessage);
            // end 






            TypeConfigSetting = new
            {
                ConfigFile = "LogClientConfig.txt",
                Category = "Encryption",
                Key = "KeyTag",
                Value = LogClient.Config.Instance.GetValue(LogClient.Category.Encryption, LogClient.Key.KeyTag)
            };

            jt = JToken.Parse(JsonConvert.SerializeObject(TypeConfigSetting));
            command = jt.ToString(Newtonsoft.Json.Formatting.Indented);

            taskString = dataManager.Execute
                ("ExecuterRest"
                , "TypeConfigSetting"
                , command
                , CsLib.RequestType.POST
                , $"https://{publicIp}:9090"
                , @"/LazyServer/LazyCommand/PostCmd"
                , LogClient.Config.Instance.GetValue(LogClient.Category.Api, LogClient.Key.AccessKey)
                , LogClient.Config.Instance.GetValue(LogClient.Category.Api, LogClient.Key.SecretKey)
                , 60);

            response = await taskString;
            wcfResponse = JsonConvert.DeserializeObject<WcfResponse>(response);

            if (!wcfResponse.IsSuccess)
                MessageBox.Show(wcfResponse.ResultMessage + wcfResponse.ErrorMessage);






            TypeConfigSetting = new
            {
                ConfigFile = "LogClientConfig.txt",
                Category = "Encryption",
                Key = "Ciphertext",
                Value = LogClient.Config.Instance.GetValue(LogClient.Category.Encryption, LogClient.Key.Ciphertext)
            };

            jt = JToken.Parse(JsonConvert.SerializeObject(TypeConfigSetting));
            command = jt.ToString(Newtonsoft.Json.Formatting.Indented);

            taskString = dataManager.Execute
                ("ExecuterRest"
                , "TypeConfigSetting"
                , command
                , CsLib.RequestType.POST
                , $"https://{publicIp}:9090"
                , @"/LazyServer/LazyCommand/PostCmd"
                , LogClient.Config.Instance.GetValue(LogClient.Category.Api, LogClient.Key.AccessKey)
                , LogClient.Config.Instance.GetValue(LogClient.Category.Api, LogClient.Key.SecretKey)
                , 60);

            response = await taskString;
            wcfResponse = JsonConvert.DeserializeObject<WcfResponse>(response);

            if (!wcfResponse.IsSuccess)
                MessageBox.Show(wcfResponse.ResultMessage + wcfResponse.ErrorMessage);






            TypeConfigSetting = new
            {
                ConfigFile = "LogClientConfig.txt",
                Category = "Encryption",
                Key = "LocalCryptionKey",
                Value = LogClient.Config.Instance.GetValue(LogClient.Category.Encryption, LogClient.Key.LocalCryptionKey)
            };

            jt = JToken.Parse(JsonConvert.SerializeObject(TypeConfigSetting));
            command = jt.ToString(Newtonsoft.Json.Formatting.Indented);

            taskString = dataManager.Execute
                ("ExecuterRest"
                , "TypeConfigSetting"
                , command
                , CsLib.RequestType.POST
                , $"https://{publicIp}:9090"
                , @"/LazyServer/LazyCommand/PostCmd"
                , LogClient.Config.Instance.GetValue(LogClient.Category.Api, LogClient.Key.AccessKey)
                , LogClient.Config.Instance.GetValue(LogClient.Category.Api, LogClient.Key.SecretKey)
                , 60);

            response = await taskString;
            wcfResponse = JsonConvert.DeserializeObject<WcfResponse>(response);

            if (!wcfResponse.IsSuccess)
                MessageBox.Show(wcfResponse.ResultMessage + wcfResponse.ErrorMessage);






            TypeSqlIdPassSetting typeSqlIdPassSetting = new TypeSqlIdPassSetting
            {
                SqlId = textBoxId.Text,
                SqlEncryptedPassword = TranString.EncodeRijndael(textBoxPassword.Text, LogClient.Config.Instance.GetCryptionKey()),
                SqlDataSource = ".",
                SqlConnectTimeout = "5"
            };

            string cmdText = JsonConvert.SerializeObject(typeSqlIdPassSetting);
            string responseString = string.Empty;

            try
            {
                var task = dataManager.Execute
                    ("ExecuterRest"
                    , "TypeSqlIdPassSetting"
                    , cmdText
                    , CsLib.RequestType.POST
                    , $"https://{publicIp}:9090"
                    , @"/LazyServer/LazyCommand/PostCmd"
                    , LogClient.Config.Instance.GetValue(LogClient.Category.Api, LogClient.Key.AccessKey)
                    , LogClient.Config.Instance.GetValue(LogClient.Category.Api, LogClient.Key.SecretKey));

                response = await task;
                wcfResponse = JsonConvert.DeserializeObject<WcfResponse>(response);
            }
            catch (Exception)
            {
                throw;
            }
            return wcfResponse;
        }

        private async Task<WcfResponse> Execute(string psCmd, string serverIp)
        {
            StringBuilder resultMessageBackup = new StringBuilder();
            WcfResponse wcfResponse = new WcfResponse();
            try
            {
                sbResultAll.Append(psCmd);
                var task = dataManager.Execute
                ("ExecuterPs"
                , "out-string"
                , psCmd
                , CsLib.RequestType.POST
                , $"https://{serverIp}:9090"
                , @"/LazyServer/LazyCommand/PostCmd"
                , LogClient.Config.Instance.GetValue(LogClient.Category.Api, LogClient.Key.AccessKey)
                , LogClient.Config.Instance.GetValue(LogClient.Category.Api, LogClient.Key.SecretKey)
                , 182
                );
                string response = await task;
                sbResultAll.Append(response);
                wcfResponse = JsonConvert.DeserializeObject<WcfResponse>(response);

                if (!wcfResponse.IsSuccess)
                {
                    if (wcfResponse.ResultMessage.Contains("1378"))
                    {
                        // skip;
                    }
                    else if (wcfResponse.ResultMessage.Trim() == "")
                    {
                        // skip 
                    }
                    else if (wcfResponse.ResultMessage.Contains("service is starting"))
                    {
                        // skip 
                    }
                    else
                    {
                        throw new Exception(wcfResponse.ResultMessage);
                    }
                }
                if (wcfResponse.IsSuccess && wcfResponse.ResultMessage.Contains("error"))
                {
                    //if (wcfResponse.ResultMessage.Contains("1378"))
                    //{
                    //    // skip;
                    //}
                    //else
                    //{
                    throw new Exception(wcfResponse.ResultMessage);
                    //}
                }


            }
            catch (Exception)
            {
                throw;
            }
            return wcfResponse;
        }


        private void buttonSaveTemplate_Click(object sender, EventArgs e)
        {
            try
            {
                dataManager.SetValue(DataManager.Category.SetSql, DataManager.Key.Id, textBoxId.Text);
                dataManager.SetValue(DataManager.Category.SetSql, DataManager.Key.Password, textBoxPassword.Text.Trim());
                dataManager.SetValue(DataManager.Category.SetSql, DataManager.Key.Collation, comboBoxCollation.Text);
                dataManager.SetValue(DataManager.Category.SetSql, DataManager.Key.Port, textBoxPort.Text.Trim());
                dataManager.SetValue(DataManager.Category.SetSql, DataManager.Key.TraceFlags, textBoxTraceFlags.Text.Trim());
                dataManager.SetValue(DataManager.Category.SetSql, DataManager.Key.Sp_configure, Sp_configure.Trim());
                dataManager.SaveUserData();
                MessageBox.Show("template changed");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void buttonSetSqlServer_Click(object sender, EventArgs e)
        {
            try
            {
                if (ControlHelpers.CheckBoxCheckedCnt(dgvServerList) ==0)
                    throw new Exception("select a server");

                DialogResult result = MessageBox.Show("Do you really want to run?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result != DialogResult.Yes)
                    return;

                ControlHelpers.ButtonStatusChange(buttonSetSqlServer, "Requested");
                buttonServerListReload.Enabled = false;
                await DbSave();

                foreach (DataGridViewRow item in dgvServerList.Rows)
                {

                    if (bool.Parse(item.Cells["CheckBox"].Value.ToString()))
                    {
                        string publicIp = item.Cells["PublicIp"].Value.ToString();
                        if (publicIp == null || publicIp.Length == 0)
                        {
                            throw new Exception("publicip is null");
                        }
                        else
                        {


                            item.Cells["SetupStatus"].Value = "Working...";

                            string ip = item.Cells["PublicIp"].Value.ToString();

                            var psCmds = ReadPowerShell(psTemplateChanged);

                            int totalCmdCount = psCmds.Count();
                            int currentCmd = 0;
                            foreach (var psCmd in psCmds)
                            {
                                currentCmd++;
                                CurrentStepName = psCmd.Key.ToString().Substring(4);
                                CurrentPercent = (double)currentCmd / (double)totalCmdCount * (double)100;
                                if (CurrentPercent == 100)
                                    CurrentPercent = 99;

                                progressBarPercentChange(
                                    CurrentPercent
                                    , CurrentStepName
                                    );


                                //try
                                //{
                                    var task = Execute(psCmd.Value, ip);
                                    await task;
                                //}
                                //catch (Exception ex)
                                //{
                                //    if (ex.Message.ToString().Contains("object reference not set"))
                                //    {
                                //        // sql not started...
                                //    }
                                //}
                            }

                            CurrentStepName = "98. Sql Id and Pass Setting";
                            progressBarPercentChange(
                                99
                                , CurrentStepName
                                );
                            var wcfResponse = await SqlConnectionStringSetting(ip);
                            if (!wcfResponse.IsSuccess)
                                throw new Exception($"Error SqlConnectionStringSetting {wcfResponse.ResultMessage} + {wcfResponse.ErrorMessage}");

                            var taskDelay = Task.Delay(10000);
                            await taskDelay;

                            CurrentStepName = "99. Default ObjectStorage Setting";
                            progressBarPercentChange(
                                99
                                , CurrentStepName
                                );

                            await DefualtObjectStorageSettingWithRetry(ip);

                            string serverInstanceNo = item.Cells["InstanceNo"].Value.ToString();
                            var serverList = new List<string>();
                            serverList.Add(serverInstanceNo);

                            await ServerOperation.RebootServerInstances(serverList);

                            item.Cells["SetupStatus"].Value = "Completed";
                        }
                    }
                }
                MessageBox.Show("server restart requested!");

                CurrentStepName = "Completed";
                progressBarPercentChange(
                    100
                    , CurrentStepName
                    );

            }
            catch (Exception ex)
            {
                CurrentStepName = "Sql Setting Error... Try Again...";
                MessageBox.Show(ex.Message);
            }
            finally
            {
                ControlHelpers.ButtonStatusChange(buttonSetSqlServer, "Set Sql Server");
                buttonServerListReload.Enabled = true;
            }

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
                labelProgressBarText.Text = progressBarStepText;
            }
            catch (Exception)
            { }
        }

        int progressBarWidth, progressBarHeihgt;
        double progressBarUnit;
        Bitmap bitmap;

        private void progressBarInit(object sender, EventArgs e)
        {
            progressBarInit();
        }

        public double CurrentPercent = 0;
        public string CurrentStepName = string.Empty;

        private void buttonModifyScript_Click(object sender, EventArgs e)
        {
            FormPreview formPreview = FormPreview.Instance;
            formPreview.GroupBoxText = "sp_configure Preview";
            formPreview.Script = Sp_configure;
            formPreview.StartPosition = FormStartPosition.CenterScreen;
            formPreview.ShowDialog();
        }

        private void buttonPowerShellScriptPreview_Click(object sender, EventArgs e)
        {
            FormPreview formPreview = FormPreview.Instance;
            formPreview.GroupBoxText = "PowerShell Preview";
            formPreview.Script = psTemplateChanged;
            formPreview.StartPosition = FormStartPosition.CenterScreen;
            formPreview.ShowDialog();
        }

        private void PreviewClose(object sender, ScriptArgs e)
        {
            FormPreview formPreview = (FormPreview)sender;
            if (formPreview.GroupBoxText.Equals("sp_configure Preview", StringComparison.OrdinalIgnoreCase))
                Sp_configure = e.Script;
            if (formPreview.GroupBoxText.Equals("PowerShell Preview", StringComparison.OrdinalIgnoreCase))
                psTemplateChanged = e.Script;

        }

        private void PsTemplateChange(object sender, EventArgs e)
        {
            PsTemplateChange();
        }

        private void PsTemplateChange()
        {
            string template = string.Empty;
            template = PsTemplate.Replace("DP_userid_DP", textBoxId.Text);
            template = template.Replace("DP_password_DP", textBoxPassword.Text);
            template = template.Replace("DP_collation_DP", comboBoxCollation.Text);
            template = template.Replace("DP_sqlPort_DP", textBoxPort.Text);
            template = template.Replace("DP_traceflags_DP", textBoxTraceFlags.Text);
            template = template.Replace("DP_sp_configure_DP", Sp_configure);
            psTemplateChanged = template;
        }


        private async Task DefualtObjectStorageSettingWithRetry(string ip)
        {
            for (int i = 0; i<3; i++)
            {
                try
                {
                    var task = DefualtObjectStorageSetting(ip);
                    await task;
                    break; 
                }
                catch (Exception ex)
                {
                    if (i == 2)
                        throw new Exception(ex.Message);
                }
            }
        }


        private async Task DefualtObjectStorageSetting(string ip)
        {
            try
            {
                // config setting required start
                string response = string.Empty;
                WcfResponse wcfResponse;
                Task<string> taskString;

                // object storage url 

                var TypeConfigSetting = new
                {
                    ConfigFile = "LazylogConfig.txt",
                    Category = "Backup",
                    Key = "ObjectStorageServiceUrl",
                    Value = dataManager.GetValue(DataManager.Category.ObjectStorage, DataManager.Key.Endpoint)
                };

                var jt = JToken.Parse(JsonConvert.SerializeObject(TypeConfigSetting));
                string command = jt.ToString(Newtonsoft.Json.Formatting.Indented);

                taskString = dataManager.Execute
                    ("ExecuterRest"
                    , "TypeConfigSetting"
                    , command
                    , CsLib.RequestType.POST
                    , $"https://{ip}:9090"
                    , @"/LazyServer/LazyCommand/PostCmd"
                    , LogClient.Config.Instance.GetValue(LogClient.Category.Api, LogClient.Key.AccessKey)
                    , LogClient.Config.Instance.GetValue(LogClient.Category.Api, LogClient.Key.SecretKey)
                    , 60);

                response = await taskString;
                wcfResponse = JsonConvert.DeserializeObject<WcfResponse>(response);

                if (!wcfResponse.IsSuccess)
                    throw new Exception($"error : TypeConfigSetting ObjectStorageServiceUrl {wcfResponse.ResultMessage + wcfResponse.ErrorMessage}");


                //Backup:::BucketName
                TypeConfigSetting = new
                {
                    ConfigFile = "LazylogConfig.txt",
                    Category = "Backup",
                    Key = "BucketName",
                    Value = dataManager.GetValue(DataManager.Category.ObjectStorage, DataManager.Key.Bucket)
                };

                jt = JToken.Parse(JsonConvert.SerializeObject(TypeConfigSetting));
                command = jt.ToString(Newtonsoft.Json.Formatting.Indented);

                taskString = dataManager.Execute
                    ("ExecuterRest"
                    , "TypeConfigSetting"
                    , command
                    , CsLib.RequestType.POST
                    , $"https://{ip}:9090"
                    , @"/LazyServer/LazyCommand/PostCmd"
                    , LogClient.Config.Instance.GetValue(LogClient.Category.Api, LogClient.Key.AccessKey)
                    , LogClient.Config.Instance.GetValue(LogClient.Category.Api, LogClient.Key.SecretKey)
                    , 60);

                response = await taskString;
                wcfResponse = JsonConvert.DeserializeObject<WcfResponse>(response);

                if (!wcfResponse.IsSuccess)
                    throw new Exception($"error : TypeConfigSetting BucketName {wcfResponse.ResultMessage + wcfResponse.ErrorMessage}");


                //ApiGateway:::Endpoint
                TypeConfigSetting = new
                {
                    ConfigFile = "LazylogConfig.txt",
                    Category = "ApiGateway",
                    Key = "Endpoint",
                    Value = dataManager.GetValue(DataManager.Category.ApiGateway, DataManager.Key.Endpoint)
                };

                jt = JToken.Parse(JsonConvert.SerializeObject(TypeConfigSetting));
                command = jt.ToString(Newtonsoft.Json.Formatting.Indented);

                taskString = dataManager.Execute
                    ("ExecuterRest"
                    , "TypeConfigSetting"
                    , command
                    , CsLib.RequestType.POST
                    , $"https://{ip}:9090"
                    , @"/LazyServer/LazyCommand/PostCmd"
                    , LogClient.Config.Instance.GetValue(LogClient.Category.Api, LogClient.Key.AccessKey)
                    , LogClient.Config.Instance.GetValue(LogClient.Category.Api, LogClient.Key.SecretKey)
                    , 60);

                response = await taskString;
                wcfResponse = JsonConvert.DeserializeObject<WcfResponse>(response);

                if (!wcfResponse.IsSuccess)
                    throw new Exception($"error : TypeConfigSetting Endpoint {wcfResponse.ResultMessage + wcfResponse.ErrorMessage}");

                // config setting required end 
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void buttonLoadTemplate_Click(object sender, EventArgs e)
        {
            LoadTextData();
        }

        private async Task DbSave()
        {
            try
            {
                foreach (DataGridViewRow item in dgvServerList.Rows)
                {
                    if (bool.Parse(item.Cells["CheckBox"].Value.ToString()))
                    {
                        string publicIp = item.Cells["PublicIp"].Value.ToString();
                        if (publicIp == null && publicIp.Length == 0)
                        {
                            throw new Exception("publicip is null");
                        }
                        else
                        {
                            var p = new List<KeyValuePair<string, string>>();
                            p.Add(new KeyValuePair<string, string>("serverName", item.Cells["Name"].Value.ToString()));
                            p.Add(new KeyValuePair<string, string>("serverInstanceNo", item.Cells["InstanceNo"].Value.ToString()));
                            p.Add(new KeyValuePair<string, string>("serverPublicIp", item.Cells["PublicIp"].Value.ToString()));
                            p.Add(new KeyValuePair<string, string>("serverPrivateIp", item.Cells["PrivateIp"].Value.ToString()));
                            p.Add(new KeyValuePair<string, string>("serverPort", textBoxPort.Text.Trim()));
                            p.Add(new KeyValuePair<string, string>("serverUserId", textBoxId.Text.Trim()));
                            p.Add(new KeyValuePair<string, string>("serverPassword", TranString.EncodeRijndael(
                                textBoxPassword.Text.Trim(),
                                LogClient.Config.Instance.GetCryptionKey()
                                )));
                            await fileDb.UpSertTable(FileDb.TableName.TBL_SERVER, p);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async void buttonDbSave_Click(object sender, EventArgs e)
        {
            try
            {
                await DbSave();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void buttonShowDetailLog_Click(object sender, EventArgs e)
        {
            FormPreview formPreview = FormPreview.Instance;
            formPreview.GroupBoxText = "Set SqlServer Log";
            formPreview.Script = sbResultAll.ToString();
            formPreview.StartPosition = FormStartPosition.CenterScreen;
            formPreview.ShowDialog();
        }

        private void buttonClearLog_Click(object sender, EventArgs e)
        {
            sbResultAll.Clear();
            MessageBox.Show("log cleared!");
        }

        private async void buttonServerListReload_Click(object sender, EventArgs e)
        {
            await ServerListLoad();
        }

        private void comboBoxCollation_TextUpdate(object sender, EventArgs e)
        {
            PsTemplateChange();
        }

        private Graphics graphics;
    }
}
