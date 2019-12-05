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

namespace HaTool.Server
{
    public partial class UcSetAgentKey : UserControl
    {
        private static readonly Lazy<UcSetAgentKey> lazy =
            new Lazy<UcSetAgentKey>(() => new UcSetAgentKey(), LazyThreadSafetyMode.ExecutionAndPublication);

        public static UcSetAgentKey Instance { get { return lazy.Value; } }

        LogClient.Config logClientConfig = LogClient.Config.Instance;
        DataManager dataManager = DataManager.Instance;
        FileDb fileDb = FileDb.Instance;

        //List<serverInstance> serverInstances = new List<serverInstance>();

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
            dgvServerList.CellContentClick += new DataGridViewCellEventHandler(ControlHelpers.dgvLineColorChange);
        }

        public UcSetAgentKey()
        {
            InitializeComponent();
            InitDgv();
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

                await fileDb.ReadTable(FileDb.TableName.TBL_SERVER);
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
            radioButtonInitailAgentKeySetting.Checked = true;
            textBoxOldAccessKey.Enabled = false;
            textBoxOldSecretKey.Enabled = false;
            textBoxNewAccessKey.Enabled = false;
            textBoxNewSecretKey.Enabled = false;
            dataManager.LoadUserData();

            try
            {
                await ServerListLoad();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void buttonSetAgentKey_Click(object sender, EventArgs e)
        {
            try
            {
                buttonSetAgentKey.Enabled = false;
                bool isChangeKey = radioButtonModifyAgentKey.Checked;

                int checkBoxCount = 0;

                foreach (DataGridViewRow item in dgvServerList.Rows)
                {
                    if (bool.Parse(item.Cells["CheckBox"].Value.ToString()))
                    {
                        checkBoxCount++;
                    }
                }
                if (checkBoxCount == 0)
                    throw new Exception("select one or more servers");

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
                            string oldAccessKey = string.Empty;
                            string oldSecretKey = string.Empty;
                            string newAccessKey = string.Empty;
                            string newSecretKey = string.Empty;
                            //string getCryptionKey = string.Empty; 
                            //string keyTag = string.Empty;
                            //string ciphertext = string.Empty; 

                            if (isChangeKey)
                            {
                                oldAccessKey = textBoxOldAccessKey.Text;
                                oldSecretKey = textBoxOldSecretKey.Text;
                                newAccessKey = textBoxNewAccessKey.Text;
                                newSecretKey = textBoxNewSecretKey.Text;
                            }
                            else
                            {
                                oldAccessKey = LogClient.Config.Instance.GetValue(LogClient.Category.Api, LogClient.Key.AccessKey);
                                oldSecretKey = LogClient.Config.Instance.GetValue(LogClient.Category.Api, LogClient.Key.SecretKey);
                                newAccessKey = oldAccessKey;
                                newSecretKey = oldSecretKey;
                            }

                            //getCryptionKey = LogClient.Config.Instance.GetValue(LogClient.Category.Encryption, LogClient.Key.GetCryptionKey);
                            //keyTag = LogClient.Config.Instance.GetValue(LogClient.Category.Encryption, LogClient.Key.KeyTag);
                            //ciphertext = LogClient.Config.Instance.GetValue(LogClient.Category.Encryption, LogClient.Key.Ciphertext);


                            bool isSuccess = false;
                            string errorMessage = "";
                            for (int i = 0; i < 5; i++)
                            {
                                try
                                {

                                    WcfResponse wcfResponse;
                                    string response = string.Empty;
                                    string command = newAccessKey + ":::" + newSecretKey;
                                    var task = dataManager.Execute
                                        ("ExecuterRest"
                                        , "TypeKeySetting"
                                        , command
                                        , CsLib.RequestType.POST
                                        , $"https://{publicIp}:9090"
                                        , @"/LazyServer/LazyCommand/PostCmd"
                                        , oldAccessKey
                                        , oldSecretKey
                                        , 10);


                                    response = await task;
                                    wcfResponse = JsonConvert.DeserializeObject<WcfResponse>(response);

                                    JsonSerializerSettings options = new JsonSerializerSettings
                                    {
                                        NullValueHandling = NullValueHandling.Ignore,
                                        MissingMemberHandling = MissingMemberHandling.Ignore
                                    };

                                    TypeKeySetting TypeKeySetting = JsonConvert.DeserializeObject<TypeKeySetting>(response, options);
                                    if (TypeKeySetting.IsSuccess)
                                    {
                                        isSuccess = true;
                                        errorMessage = TypeKeySetting.ErrorMessage;
                                        break;
                                    }




                                }
                                catch (Exception ex)
                                {
                                    if (ex.Message.ToString().Contains("A task was canceled."))
                                    {
                                        Task delay = Task.Delay(1000);
                                        await delay;
                                    }
                                    else
                                    {
                                        throw new Exception(ex.Message);
                                    }
                                }
                            }

                            if (!isSuccess)
                                if (errorMessage.Length==0)
                                    throw new Exception("try again!");
                                else
                                    throw new Exception(errorMessage);

                        }
                    }
                }
                MessageBox.Show("key setting completed");
                //await ServerListLoad();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                buttonSetAgentKey.Enabled = true;
            }
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



        private void StatusChange(string value)
        {
            buttonServerListReload.InvokeIfRequired(async s =>
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

        private void radioButtonInitailAgentKeySetting_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton r = (RadioButton)sender;
            if (r.Name.Equals(radioButtonModifyAgentKey.Name))
            {
                //panelKeyInfo.Show();
                textBoxOldAccessKey.Enabled = true;
                textBoxOldSecretKey.Enabled = true;
                textBoxNewAccessKey.Enabled = true;
                textBoxNewSecretKey.Enabled = true;
            }
            else
            {
                //panelKeyInfo.Hide();
                textBoxOldAccessKey.Enabled = false;
                textBoxOldSecretKey.Enabled = false;
                textBoxNewAccessKey.Enabled = false;
                textBoxNewSecretKey.Enabled = false;
            }
        }

    }
}
