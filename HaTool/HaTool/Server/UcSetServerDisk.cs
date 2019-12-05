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
    public partial class UcSetServerDisk : UserControl
    {
        private static readonly Lazy<UcSetServerDisk> lazy =
            new Lazy<UcSetServerDisk>(() => new UcSetServerDisk(), LazyThreadSafetyMode.ExecutionAndPublication);

        public static UcSetServerDisk Instance { get { return lazy.Value; } }

        LogClient.Config logClientConfig = LogClient.Config.Instance;
        DataManager dataManager = DataManager.Instance;
        FileDb fileDb = FileDb.Instance;

        List<serverInstance> serverInstances = new List<serverInstance>();

        DataGridViewCheckBoxColumn ColumnServerCheckBox;
        DataGridViewTextBoxColumn  ColumnServerName;
        DataGridViewTextBoxColumn  ColumnServerZoneNo;
        DataGridViewTextBoxColumn  ColumnServerInstanceNo;
        DataGridViewTextBoxColumn  ColumnServerPublicIp;
        DataGridViewTextBoxColumn  ColumnServerPrivateIp;
        DataGridViewTextBoxColumn  ColumnServerStatus;
        DataGridViewTextBoxColumn  ColumnServerOperation;

        DataGridViewCheckBoxColumn ColumnStorageCheckBox;
        DataGridViewTextBoxColumn  ColumnStorageInstanceNo;
        DataGridViewTextBoxColumn  ColumnStorageServerName;
        DataGridViewTextBoxColumn  ColumnStorageName;
        DataGridViewTextBoxColumn  ColumnStorageSize;
        DataGridViewTextBoxColumn  ColumnStorageDetailType;
        DataGridViewTextBoxColumn  ColumnStorageDescription;
        DataGridViewTextBoxColumn  ColumnStorageInstanceStatus;
        DataGridViewTextBoxColumn  ColumnStorageInstanceOperation;

        //FormPreview formPreview = FormPreview.Instance;

        public string Sp_configure { get; set; }

        public string PsTemplate { get; set; }
        public string psTemplateChanged { get; set; }

        private void InitDgv()
        {
            ColumnServerCheckBox      = new DataGridViewCheckBoxColumn();
            ColumnServerName          = new DataGridViewTextBoxColumn();
            ColumnServerZoneNo        = new DataGridViewTextBoxColumn();
            ColumnServerInstanceNo    = new DataGridViewTextBoxColumn();
            ColumnServerPublicIp      = new DataGridViewTextBoxColumn();
            ColumnServerPrivateIp     = new DataGridViewTextBoxColumn();
            ColumnServerStatus        = new DataGridViewTextBoxColumn();
            ColumnServerOperation     = new DataGridViewTextBoxColumn();

            ColumnStorageCheckBox             = new DataGridViewCheckBoxColumn();
            ColumnStorageInstanceNo           = new DataGridViewTextBoxColumn();
            ColumnStorageServerName           = new DataGridViewTextBoxColumn();
            ColumnStorageName                 = new DataGridViewTextBoxColumn();
            ColumnStorageSize                 = new DataGridViewTextBoxColumn();
            ColumnStorageDetailType           = new DataGridViewTextBoxColumn();
            ColumnStorageDescription          = new DataGridViewTextBoxColumn();
            ColumnStorageInstanceStatus       = new DataGridViewTextBoxColumn();
            ColumnStorageInstanceOperation    = new DataGridViewTextBoxColumn();
            
            ColumnServerCheckBox.HeaderText   = "CheckBox";
            ColumnServerName.HeaderText       = "Name";
            ColumnServerZoneNo.HeaderText     = "ZoneNo";
            ColumnServerInstanceNo.HeaderText = "InstanceNo";
            ColumnServerPublicIp.HeaderText   = "PublicIp";
            ColumnServerPrivateIp.HeaderText  = "PrivateIp";
            ColumnServerStatus.HeaderText     = "Status";
            ColumnServerOperation.HeaderText  = "Operation";
            
            ColumnServerCheckBox.Name = "CheckBox";
            ColumnServerName.Name = "Name";
            ColumnServerZoneNo.Name = "ZoneNo";
            ColumnServerInstanceNo.Name = "InstanceNo";
            ColumnServerPublicIp.Name = "PublicIp";
            ColumnServerPrivateIp.Name = "PrivateIp";
            ColumnServerStatus.Name = "Status";
            ColumnServerOperation.Name = "Operation";
                                 
            ColumnStorageCheckBox.HeaderText = "CheckBox";
            ColumnStorageInstanceNo.HeaderText = "InstanceNo";
            ColumnStorageServerName.HeaderText = "ServerName";
            ColumnStorageName.HeaderText = "Name";
            ColumnStorageSize.HeaderText = "Size";
            ColumnStorageDetailType.HeaderText = "Type";
            ColumnStorageDescription.HeaderText = "Desc";
            ColumnStorageInstanceStatus.HeaderText = "Status";
            ColumnStorageInstanceOperation.HeaderText = "Operation";
            
            ColumnStorageCheckBox.Name = "CheckBox";
            ColumnStorageInstanceNo.Name = "InstanceNo";
            ColumnStorageServerName.Name = "ServerName";
            ColumnStorageName.Name = "Name";
            ColumnStorageSize.Name = "Size";
            ColumnStorageDetailType.Name = "Type";
            ColumnStorageDescription.Name = "Desc";
            ColumnStorageInstanceStatus.Name = "Status";
            ColumnStorageInstanceOperation.Name = "Operation";
            

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

            dgvStorageList.Columns.AddRange(new DataGridViewColumn[]
            {
                ColumnStorageCheckBox              ,      
                ColumnStorageInstanceNo            ,
                ColumnStorageName                  ,
                ColumnStorageSize                  ,
                ColumnStorageDetailType            ,
                ColumnStorageServerName            ,
                ColumnStorageDescription           , 
                ColumnStorageInstanceStatus        ,
                ColumnStorageInstanceOperation
            });



            dgvServerList.AllowUserToAddRows = false;
            dgvServerList.RowHeadersVisible = false;
            dgvServerList.BackgroundColor = Color.White;
            dgvServerList.AutoResizeColumns();
            dgvServerList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvServerList.Columns["Operation"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvServerList.AllowUserToResizeRows = false;

            dgvStorageList.AllowUserToAddRows = false;
            dgvStorageList.RowHeadersVisible = false;
            dgvStorageList.BackgroundColor = Color.White;
            dgvStorageList.AutoResizeColumns();
            dgvStorageList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvStorageList.Columns["Operation"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvStorageList.AllowUserToResizeRows = false;

            ControlHelpers.dgvDesign(dgvServerList);
            ControlHelpers.dgvDesign(dgvStorageList);
            dgvStorageList.CellContentClick += new DataGridViewCellEventHandler(ControlHelpers.dgvSingleCheckBox);
            dgvStorageList.CellContentClick += new DataGridViewCellEventHandler(ControlHelpers.dgvLineColorChange);
            dgvServerList.CellContentClick +=  new DataGridViewCellEventHandler(ControlHelpers.dgvSingleCheckBox);
            dgvServerList.CellContentClick +=  new DataGridViewCellEventHandler(ControlHelpers.dgvLineColorChange);
        }
        
        public UcSetServerDisk()
        {
            InitializeComponent();
            //formPreview.ScriptModifyEvent += PreviewClose;
            InitDgv();
        }

    
        private async Task GetBlockStorageInstanceList(string storageInstanceNo)
        {
            try
            {

                if (storageInstanceNo == null || storageInstanceNo.Length < 1)
                    throw new Exception("select one server");

                string endpoint = dataManager.GetValue(DataManager.Category.ApiGateway, DataManager.Key.Endpoint);
                string action = @"/server/v2/getBlockStorageInstanceList";
                List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>();
                parameters.Add(new KeyValuePair<string, string>("responseFormatType", "json"));
                parameters.Add(new KeyValuePair<string, string>("serverInstanceNo", storageInstanceNo));

                SoaCall soaCall = new SoaCall();
                var task = soaCall.WebApiCall(endpoint, RequestType.POST, action, parameters, LogClient.Config.Instance.GetValue(Category.Api, Key.AccessKey), LogClient.Config.Instance.GetValue(Category.Api, Key.SecretKey));
                string response = await task;

                JsonSerializerSettings options = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore
                };

                if (response.Contains("responseError"))
                {
                    hasError hasError = JsonConvert.DeserializeObject<hasError>(response, options);
                    throw new Exception(hasError.responseError.returnMessage);
                }
                else
                {
                    getBlockStorageInstanceList getBlockStorageInstanceList = JsonConvert.DeserializeObject<getBlockStorageInstanceList>(response, options);
                    if (getBlockStorageInstanceList.getBlockStorageInstanceListResponse.returnCode.Equals("0"))
                    {
                        dgvStorageList.InvokeIfRequired( s =>
                        {
                            s.Rows.Clear(); 
                            foreach (var a in getBlockStorageInstanceList.getBlockStorageInstanceListResponse.blockStorageInstanceList)
                            {
                                int n = s.Rows.Add();
                                s.Rows[n].Cells["CheckBox"].Value = false;
                                s.Rows[n].Cells["InstanceNo"].Value = a.blockStorageInstanceNo;
                                s.Rows[n].Cells["ServerName"].Value = a.serverName;
                                s.Rows[n].Cells["Name"].Value = a.blockStorageName;
                                s.Rows[n].Cells["Size"].Value = a.blockStorageSize;
                                s.Rows[n].Cells["Type"].Value = a.diskDetailType.code;
                                s.Rows[n].Cells["Desc"].Value = a.blockStorageInstanceDescription;
                                s.Rows[n].Cells["Status"].Value = a.blockStorageInstanceStatus.code;
                                s.Rows[n].Cells["Operation"].Value = a.blockStorageInstanceOperation.code;
                            }
                        });
                            
                        if (getBlockStorageInstanceList.getBlockStorageInstanceListResponse.totalRows == 0)
                        {
                            MessageBox.Show("storage not founds");
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task ServerListLoad()
        {
            //StatusChange("Requested");
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

        private async Task GetServerInstanceList(List<string> instanceNoList)
        {
            try
            {
                string endpoint = dataManager.GetValue(DataManager.Category.ApiGateway, DataManager.Key.Endpoint);
                string action = @"/server/v2/getServerInstanceList";
                List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>();
                parameters.Add(new KeyValuePair<string, string>("responseFormatType", "json"));

                int i = 0; 
                foreach (var instanceNo in instanceNoList)
                {
                    i++;
                    string serverInstanceNoListKey = "serverInstanceNoList."+i;
                    string serverInstanceNoListValue = instanceNo;
                    parameters.Add(new KeyValuePair<string, string>(serverInstanceNoListKey, serverInstanceNoListValue));
                }
                
                SoaCall soaCall = new SoaCall();
                var task = soaCall.WebApiCall(endpoint, RequestType.POST, action, parameters, LogClient.Config.Instance.GetValue(Category.Api, Key.AccessKey), LogClient.Config.Instance.GetValue(Category.Api, Key.SecretKey));
                string response = await task;

                JsonSerializerSettings options = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore
                };

                if (response.Contains("responseError"))
                {
                    hasError hasError = JsonConvert.DeserializeObject<hasError>(response, options);
                    throw new Exception(hasError.responseError.returnMessage);
                }
                else
                {
                    getServerInstanceList getServerInstanceList = JsonConvert.DeserializeObject<getServerInstanceList>(response, options);
                    if (getServerInstanceList.getServerInstanceListResponse.returnCode.Equals("0"))
                    {
                        serverInstances.Clear();
                        foreach (var a in getServerInstanceList.getServerInstanceListResponse.serverInstanceList)
                        {
                            var item = new serverInstance
                            {
                                serverInstanceNo = a.serverInstanceNo,
                                serverName = a.serverName,
                                publicIp = a.publicIp,
                                privateIp = a.privateIp,
                                serverInstanceStatus = a.serverInstanceStatus,
                                serverInstanceOperation = a.serverInstanceOperation
                            };
                            serverInstances.Add(item);
                        }
                        if (getServerInstanceList.getServerInstanceListResponse.totalRows == 0)
                        {
                            MessageBox.Show("server not founds");
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
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
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
 
        }

        private void LoadTextData()
        {
            try
            {
                textBoxStorageName.Text = dataManager.GetValue(DataManager.Category.SetDisk, DataManager.Key.Name).Trim();
                textBoxStorageSize.Text = dataManager.GetValue(DataManager.Category.SetDisk, DataManager.Key.Size).Trim();
                comboBoxType.Text = dataManager.GetValue(DataManager.Category.SetDisk, DataManager.Key.Type).Trim();
            }
            catch (Exception)
            {
                throw; 
            }
        }


        private async Task<WcfResponse> Execute(string psCmd, string serverIp, int timeoutSec)
        {
            StringBuilder resultMessageBackup = new StringBuilder();
            WcfResponse wcfResponse = new WcfResponse();
            try
            {

                var task = dataManager.Execute
                ("ExecuterPs"
                , "out-string"
                , psCmd
                , CsLib.RequestType.POST
                , $"https://{serverIp}:9090"
                , @"/LazyServer/LazyCommand/PostCmd"
                , LogClient.Config.Instance.GetValue(LogClient.Category.Api, LogClient.Key.AccessKey)
                , LogClient.Config.Instance.GetValue(LogClient.Category.Api, LogClient.Key.SecretKey)
                , timeoutSec
                );
                string response = await task;
                wcfResponse = JsonConvert.DeserializeObject<WcfResponse>(response);
            }
            catch (Exception)
            {
                throw; 
            }
            return wcfResponse;
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


        private async void buttonGetBlockStorageInfo_Click(object sender, EventArgs e)
        {
            try
            {
                await GetBlockStorageInfoLoad(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async Task GetBlockStorageInfoLoad()
        {
            try
            {
                ControlHelpers.ButtonStatusChange(buttonGetBlockStorageInfo, "Requested");
                int checkBoxCount = 0;

                foreach (DataGridViewRow item in dgvServerList.Rows)
                {
                    if (bool.Parse(item.Cells["CheckBox"].Value.ToString()))
                    {
                        checkBoxCount++;
                    }
                }
                if (checkBoxCount != 1)
                    throw new Exception("select one server");

                foreach (DataGridViewRow item in dgvServerList.Rows)
                {
                    if (bool.Parse(item.Cells["CheckBox"].Value.ToString()))
                    {
                        string obj = item.Cells["InstanceNo"].Value.ToString();
                        if (obj == null && obj.Length == 0)
                        {
                            throw new Exception("serverInstanceNo is null");
                        }
                        else
                        {
                            await GetBlockStorageInstanceList(item.Cells["InstanceNo"].Value.ToString());
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw; 
            }
            finally
            {
                ControlHelpers.ButtonStatusChange(buttonGetBlockStorageInfo, "Get BlockStorage Info");
            }

        }

        private async void buttonStorageDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int checkBoxCount = 0;

                foreach (DataGridViewRow item in dgvStorageList.Rows)
                {
                    if (bool.Parse(item.Cells["CheckBox"].Value.ToString()))
                    {
                        checkBoxCount++;
                    }
                }
                if (checkBoxCount != 1)
                    throw new Exception("select one Storage");

                DialogResult result = MessageBox.Show("Do you really want to run?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result != DialogResult.Yes)
                    return;

                ControlHelpers.ButtonStatusChange(buttonBlockStorageDelete, "Requested");

                foreach (DataGridViewRow item in dgvStorageList.Rows)
                {
                    if (bool.Parse(item.Cells["CheckBox"].Value.ToString()))
                    {
                        string obj = item.Cells["InstanceNo"].Value.ToString();
                        if (obj == null && obj.Length == 0)
                        {
                            throw new Exception("serverInstanceNo is null");
                        }
                        else
                        {
                            await DeleteBlockStorageInstances(item.Cells["InstanceNo"].Value.ToString());
                        }
                    }
                }
                var taskDelay = Task.Delay(1000);
                await taskDelay;
                await GetBlockStorageInfoLoad();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                ControlHelpers.ButtonStatusChange(buttonBlockStorageDelete, "Delete");
            }
        }

        private async Task DeleteBlockStorageInstances(string storageInstanceNo)
        {
            try
            {
                string endpoint = dataManager.GetValue(DataManager.Category.ApiGateway, DataManager.Key.Endpoint);
                string action = @"/server/v2/deleteBlockStorageInstances";
                List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>();
                parameters.Add(new KeyValuePair<string, string>("responseFormatType", "json"));
                parameters.Add(new KeyValuePair<string, string>("blockStorageInstanceNoList.1", storageInstanceNo));

                SoaCall soaCall = new SoaCall();
                var task = soaCall.WebApiCall(endpoint, RequestType.POST, action, parameters, LogClient.Config.Instance.GetValue(Category.Api, Key.AccessKey), LogClient.Config.Instance.GetValue(Category.Api, Key.SecretKey));
                string response = await task;

                JsonSerializerSettings options = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore
                };

                if (response.Contains("responseError"))
                {
                    hasError hasError = JsonConvert.DeserializeObject<hasError>(response, options);
                    throw new Exception(hasError.responseError.returnMessage);
                }
                else
                {
                    deleteBlockStorageInstances deleteBlockStorageInstances = JsonConvert.DeserializeObject<deleteBlockStorageInstances>(response, options);
                    if (deleteBlockStorageInstances.deleteBlockStorageInstancesResponse.returnCode.Equals("0"))
                    {
                        if (deleteBlockStorageInstances.deleteBlockStorageInstancesResponse.totalRows == 0)
                        {
                            MessageBox.Show("storage not founds");
                        }
                        else
                        {

                            MessageBox.Show("deleted");
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async void buttonStorageCreate_Click(object sender, EventArgs e)
        {
            try
            {
                int checkBoxCount = 0;
                string serverInstanceNo = string.Empty;

                foreach (DataGridViewRow item in dgvServerList.Rows)
                {
                    if (bool.Parse(item.Cells["CheckBox"].Value.ToString()))
                    {
                        checkBoxCount++;
                        serverInstanceNo = item.Cells["InstanceNo"].Value.ToString();
                    }
                }
                if (checkBoxCount != 1)
                    throw new Exception("select one server");

                DialogResult result = MessageBox.Show("Do you really want to run?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result != DialogResult.Yes)
                    return;

                ControlHelpers.ButtonStatusChange(buttonStorageCreate, "Requested");

                await GetBlockStorageInfoLoad();

                string endpoint = dataManager.GetValue(DataManager.Category.ApiGateway, DataManager.Key.Endpoint);
                string action = @"/server/v2/createBlockStorageInstance";
                List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>();
                parameters.Add(new KeyValuePair<string, string>("responseFormatType", "json"));
                parameters.Add(new KeyValuePair<string, string>("blockStorageName", textBoxStorageName.Text));
                parameters.Add(new KeyValuePair<string, string>("blockStorageSize", textBoxStorageSize.Text));
                parameters.Add(new KeyValuePair<string, string>("blockStorageDescription", textBoxDescription.Text));
                parameters.Add(new KeyValuePair<string, string>("serverInstanceNo", serverInstanceNo));
                parameters.Add(new KeyValuePair<string, string>("diskDetailTypeCode", comboBoxType.Text));
                
                SoaCall soaCall = new SoaCall();
                var task = soaCall.WebApiCall(endpoint, RequestType.POST, action, parameters, LogClient.Config.Instance.GetValue(Category.Api, Key.AccessKey), LogClient.Config.Instance.GetValue(Category.Api, Key.SecretKey));
                string response = await task;

                JsonSerializerSettings options = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore
                };

                if (response.Contains("responseError"))
                {
                    hasError hasError = JsonConvert.DeserializeObject<hasError>(response, options);
                    throw new Exception(hasError.responseError.returnMessage);
                }
                else
                {
                    createBlockStorageInstance createBlockStorageInstance = JsonConvert.DeserializeObject<createBlockStorageInstance>(response, options);
                    if (createBlockStorageInstance.createBlockStorageInstanceResponse.returnCode.Equals("0"))
                    {
                        if (createBlockStorageInstance.createBlockStorageInstanceResponse.totalRows == 0)
                        {
                            MessageBox.Show("server not founds");
                        }
                        else
                        {
                            MessageBox.Show("Successfully requested storage creation. Press Reload button and mount when Status and Operation status are ATTAC NULL.");
                        }
                    }
                }
                await GetBlockStorageInfoLoad();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                ControlHelpers.ButtonStatusChange(buttonStorageCreate, "Create");
            }
        }

        private async void buttonMountStorage_Click(object sender, EventArgs e)
        {
            try
            {
                int checkBoxCount = 0;
                string publicIp = string.Empty;

                foreach (DataGridViewRow item in dgvServerList.Rows)
                {
                    if (bool.Parse(item.Cells["CheckBox"].Value.ToString()))
                    {
                        checkBoxCount++;
                        publicIp = item.Cells["PublicIp"].Value.ToString();
                    }

                }
                if (checkBoxCount != 1)
                    throw new Exception("select one server");

                DialogResult result = MessageBox.Show("Do you really want to run?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result != DialogResult.Yes)
                    return;

                ControlHelpers.ButtonStatusChange(buttonMountStorage, "Requested");

                await GetBlockStorageInfoLoad();

                foreach (DataGridViewRow item in dgvStorageList.Rows)
                {
                    if (!item.Cells["Status"].Value.ToString().Equals("ATTAC"))
                        throw new Exception("check status and operation");
                    if (!item.Cells["Operation"].Value.ToString().Equals("NULL"))
                        throw new Exception("check status and operation");
                }

                var task = Execute(
                    dataManager.GetValue(DataManager.Category.SetDisk, DataManager.Key.PsPartitionFormat)
                    , publicIp
                    , 30);

                WcfResponse wcfResponse = await task;
                if (wcfResponse.IsSuccess)
                    MessageBox.Show("disk mount completed");
                else
                    throw new Exception(wcfResponse.ErrorMessage);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                ControlHelpers.ButtonStatusChange(buttonMountStorage, "Mount");
            }
        }

        private async void buttonServerGetDiskInfo_Click(object sender, EventArgs e)
        {
            try
            {
                int checkBoxCount = 0;
                string publicIp = string.Empty;

                foreach (DataGridViewRow item in dgvServerList.Rows)
                {
                    if (bool.Parse(item.Cells["CheckBox"].Value.ToString()))
                    {
                        checkBoxCount++;
                        publicIp = item.Cells["PublicIp"].Value.ToString();
                    }
                }
                if (checkBoxCount != 1)
                    throw new Exception("select one server");

                ControlHelpers.ButtonStatusChange(buttonServerGetDiskInfo, "Requested");

                var task = Execute(@"gdr -PSProvider 'FileSystem'"
                    , publicIp
                    , 30);

                WcfResponse wcfResponse = await task;
                if (wcfResponse.IsSuccess)
                    MessageBox.Show(wcfResponse.ResultMessage);
                else
                    throw new Exception(wcfResponse.ErrorMessage);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                ControlHelpers.ButtonStatusChange(buttonServerGetDiskInfo, "Server Get-Disk Info");
            }
        }

        private async void buttonStorageReload_Click(object sender, EventArgs e)
        {
            try
            {
                ControlHelpers.ButtonStatusChange(buttonStorageReload, "Requested");
                await GetBlockStorageInfoLoad();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                ControlHelpers.ButtonStatusChange(buttonStorageReload, "Reload");
            }
        }
    }
}
