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
using HaTool.Global;

namespace HaTool.HighAvailability
{
    public partial class UcFailoverPolicy : UserControl
    {
        private static readonly Lazy<UcFailoverPolicy> lazy =
            new Lazy<UcFailoverPolicy>(() => new UcFailoverPolicy(), LazyThreadSafetyMode.ExecutionAndPublication);

        public static UcFailoverPolicy Instance { get { return lazy.Value; } }

        LogClient.Config logClientConfig = LogClient.Config.Instance;
        DataManager dataManager = DataManager.Instance;
        FileDb fileDb = FileDb.Instance;

        public UcFailoverPolicy()
        {
            InitializeComponent();
            InitDgv();
            
        }

        List<serverInstance> serverInstances = new List<serverInstance>();

        DataGridViewCheckBoxColumn ColumnServerCheckBox;
        DataGridViewTextBoxColumn ColumnServerName;
        DataGridViewTextBoxColumn ColumnServerZoneNo;
        DataGridViewTextBoxColumn ColumnServerInstanceNo;
        DataGridViewTextBoxColumn ColumnServerPublicIp;
        DataGridViewTextBoxColumn ColumnServerPrivateIp;
        DataGridViewTextBoxColumn ColumnServerStatus;
        DataGridViewTextBoxColumn ColumnServerOperation;

        private void InitDgv()
        {
            ColumnServerCheckBox = new DataGridViewCheckBoxColumn();
            ColumnServerName = new DataGridViewTextBoxColumn();
            ColumnServerZoneNo = new DataGridViewTextBoxColumn();
            ColumnServerInstanceNo = new DataGridViewTextBoxColumn();
            ColumnServerPublicIp = new DataGridViewTextBoxColumn();
            ColumnServerPrivateIp = new DataGridViewTextBoxColumn();
            ColumnServerStatus = new DataGridViewTextBoxColumn();
            ColumnServerOperation = new DataGridViewTextBoxColumn();

            ColumnServerCheckBox.HeaderText = "CheckBox";
            ColumnServerName.HeaderText = "Name";
            ColumnServerZoneNo.HeaderText = "ZoneNo";
            ColumnServerInstanceNo.HeaderText = "InstanceNo";
            ColumnServerPublicIp.HeaderText = "PublicIp";
            ColumnServerPrivateIp.HeaderText = "PrivateIp";
            ColumnServerStatus.HeaderText = "Status";
            ColumnServerOperation.HeaderText = "Operation";

            ColumnServerCheckBox.Name = "CheckBox";
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

            ControlHelpers.dgvDesign(dgvServerList);
            dgvServerList.CellContentClick += new DataGridViewCellEventHandler(ControlHelpers.dgvSingleCheckBox);
            dgvServerList.CellContentClick += new DataGridViewCellEventHandler(ControlHelpers.dgvLineColorChange);

        }
        private async void LoadData(object sender, EventArgs e)
        {
            try
            {
                textBox00Comment.ReadOnly = true;
                textBox00Comment.BorderStyle = 0;
                textBox00Comment.BackColor = this.BackColor;
                textBox00Comment.TabStop = false;
                textBox01Comment.ReadOnly = true;
                textBox01Comment.BorderStyle = 0;
                textBox01Comment.BackColor = this.BackColor;
                textBox01Comment.TabStop = false;

                lastMessage.ReadOnly = true;
                lastMessage.BorderStyle = 0;
                lastMessage.BackColor = this.BackColor;
                lastMessage.TabStop = false;
                lastMessage.Text = "If the collection interval is 0, no data action is taken.";

                dataManager.LoadUserData();
                List<Task> tasks = new List<Task>();
                tasks.Add(ServerListLoad());
                await Task.WhenAll(tasks);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        private async Task ServerListLoad()
        {
            try
            {
                ControlHelpers.ButtonStatusChange(buttonServerListReload, "Requested");
                await fileDb.ReadTable(FileDb.TableName.TBL_SERVER);

                List<string> instanceNoList = new List<string>();
                List<string> deleteServerNameList = new List<string>();

                foreach (var a in fileDb.TBL_SERVER.Data)
                {
                    if (a.Value.serverInstanceNo != "NULL")
                        instanceNoList.Add(a.Value.serverInstanceNo);
                }


                List<serverInstance> serverInstances = new List<serverInstance>();

                try
                {
                    serverInstances = await ServerOperation.GetServerInstanceList(instanceNoList);
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("server not found"))
                    {
                        // 
                    }
                    else
                        throw new Exception(ex.Message);
                }
                
                dgvServerList.InvokeIfRequired(async s =>
                {
                    try
                    {
                        s.Rows.Clear();
                        foreach (var a in fileDb.TBL_SERVER.Data)
                        {

                            var serverInstance = serverInstances.Find(x => x.serverName == a.Key.serverName);

                            if (serverInstance != null)
                            {
                                int n = s.Rows.Add();
                                s.Rows[n].Cells["CheckBox"].Value = false;
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
                        throw;
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
        
        private string GetPublicIp (string serverName)
        {
            string serverPublicIp = "";
            foreach (var a in fileDb.TBL_SERVER.Data)
            {
                if (a.Key.serverName.Equals(serverName, StringComparison.OrdinalIgnoreCase))
                    serverPublicIp = a.Value.serverPublicIp;
            }
            return serverPublicIp;
        }

        private async void buttonServerListReload_Click(object sender, EventArgs e)
        {
            try
            {
                await ServerListLoad();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private async void buttonLoadPolicy_Click(object sender, EventArgs e)
        {
            try
            {
                ControlHelpers.ButtonStatusChange(buttonLoadPolicy, "Requested");

                if (ControlHelpers.CheckBoxCheckedCnt(dgvServerList) != 1)
                    throw new Exception("select a server");

                string ip = string.Empty; 

                foreach (DataGridViewRow item in dgvServerList.Rows)
                {
                    if (bool.Parse(item.Cells["CheckBox"].Value.ToString()))
                    {
                        ip = item.Cells["PublicIp"].Value.ToString();
                        if (
                            !(
                            item.Cells["Status"].Value.ToString().Equals("RUN",StringComparison.OrdinalIgnoreCase) &&
                            item.Cells["Operation"].Value.ToString().Equals("NULL", StringComparison.OrdinalIgnoreCase)
                            )
                            )
                        {
                            throw new Exception("The server is not running. Please use after changing the server running state.");
                        }
                    }
                }

                if (ip.Length == 0)
                    throw new Exception("check public ip");

                List<string> TypeConfigReads = new List<string>();
                List<Task<string>> Tasks = new List<Task<string>>();
                string cmdText = string.Empty; 

                var typeConfigRead = new TypeConfigRead
                {
                    ConfigFile = "LazylogConfig.txt",
                    Category = "Ha",
                    Key = "HeartBeatIntervalSec"
                };
                cmdText = JsonConvert.SerializeObject(typeConfigRead);

                Task<string> HeartBeatIntervalSec = dataManager.Execute
                    ("ExecuterRest"
                    , "TypeConfigRead"
                    , cmdText
                    , CsLib.RequestType.POST
                    , $"https://{ip}:9090"
                    , @"/LazyServer/LazyCommand/PostCmd"
                    , LogClient.Config.Instance.GetValue(LogClient.Category.Api, LogClient.Key.AccessKey)
                    , LogClient.Config.Instance.GetValue(LogClient.Category.Api, LogClient.Key.SecretKey));

                Tasks.Add(HeartBeatIntervalSec);

                typeConfigRead = new TypeConfigRead
                {
                    ConfigFile = "LazylogConfig.txt",
                    Category = "Ha",
                    Key = "HeartBeatTimeLimitSec"
                };
                cmdText = JsonConvert.SerializeObject(typeConfigRead);
                Task<string> HeartBeatTimeLimitSec = dataManager.Execute
                    ("ExecuterRest"
                    , "TypeConfigRead"
                    , cmdText
                    , CsLib.RequestType.POST
                    , $"https://{ip}:9090"
                    , @"/LazyServer/LazyCommand/PostCmd"
                    , LogClient.Config.Instance.GetValue(LogClient.Category.Api, LogClient.Key.AccessKey)
                    , LogClient.Config.Instance.GetValue(LogClient.Category.Api, LogClient.Key.SecretKey));

                Tasks.Add(HeartBeatTimeLimitSec);

                await Task.WhenAll(Tasks);

                WcfResponse wcfResponse;
                wcfResponse = JsonConvert.DeserializeObject<WcfResponse>(HeartBeatIntervalSec.Result);
                int responseSuccessCnt = 0;

                if (wcfResponse.IsSuccess)
                {
                    string resultMessage = wcfResponse.ResultMessage;
                    typeConfigRead = JsonConvert.DeserializeObject<TypeConfigRead>(resultMessage);
                    textBoxHeartBeatIntervalSec.Text = typeConfigRead.Value;
                    responseSuccessCnt++;
                }

                wcfResponse = JsonConvert.DeserializeObject<WcfResponse>(HeartBeatTimeLimitSec.Result);
                if (wcfResponse.IsSuccess)
                {
                    string resultMessage = wcfResponse.ResultMessage;
                    typeConfigRead = JsonConvert.DeserializeObject<TypeConfigRead>(resultMessage);
                    textBoxHeartBeatTimeLimitSec.Text = typeConfigRead.Value;
                    responseSuccessCnt++;
                }

                if (responseSuccessCnt == 2)
                    MessageBox.Show("loaded");
                else
                    MessageBox.Show("server response error");

            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Unexpected character encountered while parsing value"))
                {
                    MessageBox.Show("server response error");
                }
                else
                {
                    MessageBox.Show(ex.Message);
                    await ServerListLoad();
                }
            }
            finally
            {
                ControlHelpers.ButtonStatusChange(buttonLoadPolicy, "Load Policy");
            }
        }


        private async void buttonSavePolicy_Click(object sender, EventArgs e)
        {
            try
            {
                ControlHelpers.ButtonStatusChange(buttonSavePolicy, "Requested");
                if (ControlHelpers.CheckBoxCheckedCnt(dgvServerList) != 1)
                    throw new Exception("select one server");

                string ip = string.Empty;

                foreach (DataGridViewRow item in dgvServerList.Rows)
                {
                    if (bool.Parse(item.Cells["CheckBox"].Value.ToString()))
                    {
                        ip = item.Cells["PublicIp"].Value.ToString();
                        if (
                                !(
                                item.Cells["Status"].Value.ToString().Equals("RUN", StringComparison.OrdinalIgnoreCase) &&
                                item.Cells["Operation"].Value.ToString().Equals("NULL", StringComparison.OrdinalIgnoreCase)
                                )
                            )
                        {
                            throw new Exception("The server is not running. Please use after changing the server running state.");
                        }
                    }
                }

                if (ip.Length == 0)
                    throw new Exception("check public ip");

                if (!long.TryParse(textBoxHeartBeatIntervalSec.Text, out long x2))
                    throw new Exception("Error HeartBeatTimeLimitSec is not numeric value");
                if (x2 < 0)
                    throw new Exception("Error HeartBeatTimeLimitSec value cannot be negative.");

                if (!long.TryParse(textBoxHeartBeatTimeLimitSec.Text, out long x3))
                    throw new Exception("Error HeartBeatTimeLimitSec is not numeric value");
                if (x3 < 0)
                    throw new Exception("Error HeartBeatTimeLimitSec value cannot be negative.");


                List<string> TypeConfigReads = new List<string>();
                List<Task<string>> Tasks = new List<Task<string>>();
                string cmdText = string.Empty;

                var typeConfigSetting = new TypeConfigSetting
                {
                    ConfigFile = "LazylogConfig.txt",
                    Category = "Ha",
                    Key = "HeartBeatIntervalSec",
                    Value = textBoxHeartBeatIntervalSec.Text.Trim()
                };
                cmdText = JsonConvert.SerializeObject(typeConfigSetting);

                Task<string> HeartBeatIntervalSec = dataManager.Execute
                    ("ExecuterRest"
                    , "TypeConfigSetting"
                    , cmdText
                    , CsLib.RequestType.POST
                    , $"https://{ip}:9090"
                    , @"/LazyServer/LazyCommand/PostCmd"
                    , LogClient.Config.Instance.GetValue(LogClient.Category.Api, LogClient.Key.AccessKey)
                    , LogClient.Config.Instance.GetValue(LogClient.Category.Api, LogClient.Key.SecretKey));

                Tasks.Add(HeartBeatIntervalSec);

                typeConfigSetting = new TypeConfigSetting
                {
                    ConfigFile = "LazylogConfig.txt",
                    Category = "Ha",
                    Key = "HeartBeatTimeLimitSec",
                    Value = textBoxHeartBeatTimeLimitSec.Text.Trim()
                };
                cmdText = JsonConvert.SerializeObject(typeConfigSetting);
                Task<string> HeartBeatTimeLimitSec = dataManager.Execute
                    ("ExecuterRest"
                    , "TypeConfigSetting"
                    , cmdText
                    , CsLib.RequestType.POST
                    , $"https://{ip}:9090"
                    , @"/LazyServer/LazyCommand/PostCmd"
                    , LogClient.Config.Instance.GetValue(LogClient.Category.Api, LogClient.Key.AccessKey)
                    , LogClient.Config.Instance.GetValue(LogClient.Category.Api, LogClient.Key.SecretKey));

                Tasks.Add(HeartBeatTimeLimitSec);

                await Task.WhenAll(Tasks);

                WcfResponse wcfResponse;
                wcfResponse = JsonConvert.DeserializeObject<WcfResponse>(HeartBeatIntervalSec.Result);
                int responseSuccessCnt = 0; 

                if (wcfResponse.IsSuccess)
                {
                    responseSuccessCnt++;
                }

                wcfResponse = JsonConvert.DeserializeObject<WcfResponse>(HeartBeatTimeLimitSec.Result);
                if (wcfResponse.IsSuccess)
                {
                    responseSuccessCnt++;
                }

                Task task = ApplyPolicy(ip);
                await task;

                if (responseSuccessCnt == 2)
                    MessageBox.Show("saved");
                else
                    MessageBox.Show("server response error");
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Unexpected character encountered while parsing value"))
                {
                    MessageBox.Show("server response error");
                }
                else
                {
                    MessageBox.Show(ex.Message);
                    await ServerListLoad();
                }
            }
            finally
            {
                ControlHelpers.ButtonStatusChange(buttonSavePolicy, "Save Policy");
            }
        }

        private async Task ApplyPolicy(string ip)
        {
            try
            {
                string cmdText = string.Empty;
                string response = string.Empty;
                WcfResponse wcfResponse;

                var typeMonController = new TypeMonController
                {
                    MonName = "HaManager",
                    StopStart = "Stop"
                };
                cmdText = JsonConvert.SerializeObject(typeMonController);
                Task<string> HaMangerStopTask = dataManager.Execute
                    ("ExecuterRest"
                    , "TypeMonController"
                    , cmdText
                    , CsLib.RequestType.POST
                    , $"https://{ip}:9090"
                    , @"/LazyServer/LazyCommand/PostCmd"
                    , LogClient.Config.Instance.GetValue(LogClient.Category.Api, LogClient.Key.AccessKey)
                    , LogClient.Config.Instance.GetValue(LogClient.Category.Api, LogClient.Key.SecretKey));

                response = await HaMangerStopTask;
                wcfResponse = JsonConvert.DeserializeObject<WcfResponse>(response);

                if (!wcfResponse.IsSuccess)
                    throw new Exception("Ha Manager Stop Failed");

                Task task = Task.Delay(3000);
                await task;

                typeMonController = new TypeMonController
                {
                    MonName = "HaManager",
                    StopStart = "Start"
                };
                cmdText = JsonConvert.SerializeObject(typeMonController);
                Task<string> HaMangerStartTask = dataManager.Execute
                    ("ExecuterRest"
                    , "TypeMonController"
                    , cmdText
                    , CsLib.RequestType.POST
                    , $"https://{ip}:9090"
                    , @"/LazyServer/LazyCommand/PostCmd"
                    , LogClient.Config.Instance.GetValue(LogClient.Category.Api, LogClient.Key.AccessKey)
                    , LogClient.Config.Instance.GetValue(LogClient.Category.Api, LogClient.Key.SecretKey));

                response = await HaMangerStartTask;
                wcfResponse = JsonConvert.DeserializeObject<WcfResponse>(response);

                if (!wcfResponse.IsSuccess)
                    throw new Exception("Ha Manager Start Failed");

            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Unexpected character encountered while parsing value"))
                {
                    MessageBox.Show("server response error");
                }
                else
                    MessageBox.Show(ex.Message);
            }
        }


    }
}
