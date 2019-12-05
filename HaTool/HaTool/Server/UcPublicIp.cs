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

namespace HaTool.Server
{
    public partial class UcPublicIp : UserControl
    {
        private static readonly Lazy<UcPublicIp> lazy =
            new Lazy<UcPublicIp>(() => new UcPublicIp(), LazyThreadSafetyMode.ExecutionAndPublication);

        public static UcPublicIp Instance { get { return lazy.Value; } }

        LogClient.Config logClientConfig = LogClient.Config.Instance;
        DataManager dataManager = DataManager.Instance;
        FileDb fileDb = FileDb.Instance;
        bool initailLoadData = false;

        List<serverInstance> serverInstances = new List<serverInstance>();
        //List<publicIpInstance> publicIpInstances = new List<publicIpInstance>();

        DataGridViewCheckBoxColumn ColumnServerCheckBox;
        DataGridViewTextBoxColumn ColumnServerName;
        DataGridViewTextBoxColumn ColumnServerZoneNo;
        DataGridViewTextBoxColumn ColumnServerInstanceNo;
        DataGridViewTextBoxColumn ColumnServerPublicIp;
        DataGridViewTextBoxColumn ColumnServerPrivateIp;
        DataGridViewTextBoxColumn ColumnServerStatus;
        DataGridViewTextBoxColumn ColumnServerOperation;

        DataGridViewCheckBoxColumn ColumnIpCheckBox;
        DataGridViewTextBoxColumn ColumnIpInstanceNo;
        DataGridViewTextBoxColumn ColumnIpPublicIp;
        DataGridViewTextBoxColumn ColumnIpServerInstanceNo;
        DataGridViewTextBoxColumn ColumnIpServerName;
        DataGridViewTextBoxColumn ColumnIpStatus;
        DataGridViewTextBoxColumn ColumnIpOperation;


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

            ColumnIpCheckBox = new DataGridViewCheckBoxColumn();
            ColumnIpInstanceNo = new DataGridViewTextBoxColumn();
            ColumnIpPublicIp = new DataGridViewTextBoxColumn();
            ColumnIpServerInstanceNo = new DataGridViewTextBoxColumn();
            ColumnIpServerName = new DataGridViewTextBoxColumn();
            ColumnIpStatus = new DataGridViewTextBoxColumn();
            ColumnIpOperation = new DataGridViewTextBoxColumn();


            ColumnServerCheckBox.HeaderText = "CheckBox";
            ColumnServerName.HeaderText = "Name";
            ColumnServerZoneNo.HeaderText = "ZoneNo";
            ColumnServerInstanceNo.HeaderText = "InstanceNo";
            ColumnServerPublicIp.HeaderText = "PublicIp";
            ColumnServerPrivateIp.HeaderText = "PrivateIp";
            ColumnServerStatus.HeaderText = "Status";
            ColumnServerOperation.HeaderText = "Operation";

            ColumnIpCheckBox.HeaderText = "CheckBox";
            ColumnIpInstanceNo.HeaderText = "IpInstanceNo";
            ColumnIpPublicIp.HeaderText = "IpPublicIp";
            ColumnIpServerInstanceNo.HeaderText = "ServerInstanceNo";
            ColumnIpServerName.HeaderText = "ServerName";
            ColumnIpStatus.HeaderText = "IpStatus";
            ColumnIpOperation.HeaderText = "IpOperation";

            ColumnServerCheckBox.Name = "CheckBox";
            ColumnServerName.Name = "Name";
            ColumnServerZoneNo.Name = "ZoneNo";
            ColumnServerInstanceNo.Name = "InstanceNo";
            ColumnServerPublicIp.Name = "PublicIp";
            ColumnServerPrivateIp.Name = "PrivateIp";
            ColumnServerStatus.Name = "Status";
            ColumnServerOperation.Name = "Operation";

            ColumnIpCheckBox.Name = "CheckBox";
            ColumnIpInstanceNo.Name = "IpInstanceNo";
            ColumnIpPublicIp.Name = "IpPublicIp";
            ColumnIpServerInstanceNo.Name = "ServerInstnaceNao";
            ColumnIpServerName.Name = "ServerName";
            ColumnIpStatus.Name = "IpStatus";
            ColumnIpOperation.Name = "IpOperation";

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

            dgvPublicIpList.Columns.AddRange(new DataGridViewColumn[]
            {
                ColumnIpCheckBox      ,
                ColumnIpInstanceNo  ,
                ColumnIpPublicIp      ,
                ColumnIpServerInstanceNo,
                ColumnIpServerName,
                ColumnIpStatus        ,
                ColumnIpOperation

            });

            dgvServerList.AllowUserToAddRows = false;
            dgvServerList.RowHeadersVisible = false;
            dgvServerList.BackgroundColor = Color.White;
            dgvServerList.AutoResizeColumns();
            dgvServerList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvServerList.Columns["Operation"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvServerList.AllowUserToResizeRows = false;

            dgvPublicIpList.AllowUserToAddRows = false;
            dgvPublicIpList.RowHeadersVisible = false;
            dgvPublicIpList.BackgroundColor = Color.White;
            dgvPublicIpList.AutoResizeColumns();
            dgvPublicIpList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvPublicIpList.Columns["IpOperation"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvPublicIpList.AllowUserToResizeRows = false;

            ControlHelpers.dgvDesign(dgvServerList);
            ControlHelpers.dgvDesign(dgvPublicIpList);
            dgvServerList.CellContentClick += new DataGridViewCellEventHandler(ControlHelpers.dgvLineColorChange);
            dgvPublicIpList.CellContentClick += new DataGridViewCellEventHandler(ControlHelpers.dgvLineColorChange);
        }

        public UcPublicIp()
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
                                s.Rows[n].Cells["ZoneNo"].Value = a.Value.zoneNo + "("+serverInstance.zone.zoneCode+")";
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


        private async Task PublicIpListLoad(string regionNo = "1", string zoneNo = "3")
        {

            try
            {
                ControlHelpers.ButtonStatusChange(buttonPublicIpListReload, "Requested");

                string endpoint = dataManager.GetValue(DataManager.Category.ApiGateway, DataManager.Key.Endpoint);
                string action = @"/server/v2/getPublicIpInstanceList";
                List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>();
                parameters.Add(new KeyValuePair<string, string>("responseFormatType", "json"));
                parameters.Add(new KeyValuePair<string, string>("regionNo", regionNo));
                parameters.Add(new KeyValuePair<string, string>("zoneNo", zoneNo));

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

                getPublicIpInstanceList getPublicIpInstanceList = JsonConvert.DeserializeObject<getPublicIpInstanceList>(response, options);
                if (getPublicIpInstanceList.getPublicIpInstanceListResponse.returnCode.Equals("0"))
                {
                    dgvPublicIpList.InvokeIfRequired(s =>
                    {
                        s.Rows.Clear();
                        foreach (var a in getPublicIpInstanceList.getPublicIpInstanceListResponse.publicIpInstanceList)
                        {
                            int n = s.Rows.Add();
                            s.Rows[n].Cells["CheckBox"].Value = false;
                            s.Rows[n].Cells["IpInstanceNo"].Value = a.publicIpInstanceNo;
                            s.Rows[n].Cells["IpPublicIp"].Value = a.publicIp;
                            s.Rows[n].Cells["ServerInstnaceNao"].Value = a.serverInstanceAssociatedWithPublicIp.serverInstanceNo;
                            s.Rows[n].Cells["ServerName"].Value = a.serverInstanceAssociatedWithPublicIp.serverName;
                            s.Rows[n].Cells["IpStatus"].Value = a.publicIpInstanceStatus.code;
                            s.Rows[n].Cells["IpOperation"].Value = a.publicIpInstanceOperation.code;
                        }
                    });

                    if (getPublicIpInstanceList.getPublicIpInstanceListResponse.totalRows == 0)
                    {
                        throw new Exception("ip not founds");
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                ControlHelpers.ButtonStatusChange(buttonPublicIpListReload, "Reload");
            }
        }

        private async Task DeletePublicIpInstances(List<string> instanceNoList)
        {
            try
            {
                string endpoint = dataManager.GetValue(DataManager.Category.ApiGateway, DataManager.Key.Endpoint);
                string action = @"/server/v2/deletePublicIpInstances";
                List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>();
                parameters.Add(new KeyValuePair<string, string>("responseFormatType", "json"));

                int i = 0;
                foreach (var instanceNo in instanceNoList)
                {
                    i++;
                    string InstanceNoListKey = "publicIpInstanceNoList." + i;
                    string InstanceNoListValue = instanceNo;
                    parameters.Add(new KeyValuePair<string, string>(InstanceNoListKey, InstanceNoListValue));
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

                deletePublicIpInstances deletePublicIpInstances = JsonConvert.DeserializeObject<deletePublicIpInstances>(response, options);
                if (deletePublicIpInstances.deletePublicIpInstancesResponse.returnCode.Equals("0"))
                {
                    if (deletePublicIpInstances.deletePublicIpInstancesResponse.totalRows == 0)
                    {
                        throw new Exception("ip not founds");
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task DisassociatePublicIpFromServerInstance(string instanceNo)
        {
            try
            {
                string endpoint = dataManager.GetValue(DataManager.Category.ApiGateway, DataManager.Key.Endpoint);
                string action = @"/server/v2/disassociatePublicIpFromServerInstance";
                List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>();
                parameters.Add(new KeyValuePair<string, string>("responseFormatType", "json"));
                parameters.Add(new KeyValuePair<string, string>("publicIpInstanceNo", instanceNo));

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

                disassociatePublicIpFromServerInstance disassociatePublicIpFromServerInstance = JsonConvert.DeserializeObject<disassociatePublicIpFromServerInstance>(response, options);
                if (disassociatePublicIpFromServerInstance.disassociatePublicIpFromServerInstanceResponse.returnCode.Equals("0"))
                {
                    if (disassociatePublicIpFromServerInstance.disassociatePublicIpFromServerInstanceResponse.totalRows == 0)
                    {
                        throw new Exception("ip not founds");
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task StopServerInstances(List<string> instanceNoList)
        {
            try
            {
                string endpoint = dataManager.GetValue(DataManager.Category.ApiGateway, DataManager.Key.Endpoint);
                string action = @"/server/v2/stopServerInstances";
                List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>();
                parameters.Add(new KeyValuePair<string, string>("responseFormatType", "json"));

                int i = 0;
                foreach (var instanceNo in instanceNoList)
                {
                    i++;
                    string serverInstanceNoListKey = "serverInstanceNoList." + i;
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

                stopServerInstances stopServerInstances = JsonConvert.DeserializeObject<stopServerInstances>(response, options);
                if (stopServerInstances.stopServerInstancesResponse.returnCode.Equals("0"))
                {
                    serverInstances.Clear();
                    foreach (var a in stopServerInstances.stopServerInstancesResponse.serverInstanceList)
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
                    if (stopServerInstances.stopServerInstancesResponse.totalRows == 0)
                    {
                        MessageBox.Show("server not founds");
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task TerminateServerInstances(List<string> instanceNoList)
        {
            try
            {
                string endpoint = dataManager.GetValue(DataManager.Category.ApiGateway, DataManager.Key.Endpoint);
                string action = @"/server/v2/terminateServerInstances";
                List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>();
                parameters.Add(new KeyValuePair<string, string>("responseFormatType", "json"));

                int i = 0;
                foreach (var instanceNo in instanceNoList)
                {
                    i++;
                    string serverInstanceNoListKey = "serverInstanceNoList." + i;
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

                terminateServerInstances terminateServerInstances = JsonConvert.DeserializeObject<terminateServerInstances>(response, options);
                if (terminateServerInstances.terminateServerInstancesResponse.returnCode.Equals("0"))
                {
                    serverInstances.Clear();
                    foreach (var a in terminateServerInstances.terminateServerInstancesResponse.serverInstanceList)
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
                    if (terminateServerInstances.terminateServerInstancesResponse.totalRows == 0)
                    {
                        throw new Exception("server not founds");
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task StartServerInstances(List<string> instanceNoList)
        {
            try
            {
                string endpoint = dataManager.GetValue(DataManager.Category.ApiGateway, DataManager.Key.Endpoint);
                string action = @"/server/v2/startServerInstances";
                List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>();
                parameters.Add(new KeyValuePair<string, string>("responseFormatType", "json"));

                int i = 0;
                foreach (var instanceNo in instanceNoList)
                {
                    i++;
                    string serverInstanceNoListKey = "serverInstanceNoList." + i;
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

                startServerInstances startServerInstances = JsonConvert.DeserializeObject<startServerInstances>(response, options);
                if (startServerInstances.startServerInstancesResponse.returnCode.Equals("0"))
                {
                    serverInstances.Clear();
                    foreach (var a in startServerInstances.startServerInstancesResponse.serverInstanceList)
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
                    if (startServerInstances.startServerInstancesResponse.totalRows == 0)
                    {
                        throw new Exception("server not founds");
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
            dataManager.LoadUserData();

            try
            {
                initailLoadData = true;
                List<Task> tasks = new List<Task>();
                tasks.Add(GetRegionList());
                tasks.Add(GetZoneList("1"));
                tasks.Add(ServerListLoad());
                tasks.Add(PublicIpListLoad());
                await Task.WhenAll(tasks);
                initailLoadData = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private async Task GetRegionList()
        {
            try
            {
                string endpoint = dataManager.GetValue(DataManager.Category.ApiGateway, DataManager.Key.Endpoint);
                string action = @"/server/v2/getRegionList";
                List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>();
                parameters.Add(new KeyValuePair<string, string>("responseFormatType", "json"));
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
                    getRegionList getRegionList = JsonConvert.DeserializeObject<getRegionList>(response, options);
                    if (getRegionList.getRegionListResponse.returnCode.Equals("0"))
                    {
                        comboBoxRegion.Items.Clear();
                        foreach (var a in getRegionList.getRegionListResponse.regionList)
                        {
                            var item = new region
                            {
                                regionNo = a.regionNo,
                                regionCode = a.regionCode,
                                regionName = a.regionName
                            };
                            comboBoxRegion.Items.Add(item);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            comboBoxRegion.SelectedIndex = 0;
        }

        private async Task GetZoneList(string regionNo)
        {
            try
            {
                string endpoint = dataManager.GetValue(DataManager.Category.ApiGateway, DataManager.Key.Endpoint);
                string action = @"/server/v2/getZoneList";
                List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>();
                parameters.Add(new KeyValuePair<string, string>("responseFormatType", "json"));
                parameters.Add(new KeyValuePair<string, string>("regionNo", regionNo));
                SoaCall soaCall = new SoaCall();
                var task = soaCall.WebApiCall(endpoint, RequestType.POST, action, parameters, LogClient.Config.Instance.GetValue(Category.Api, Key.AccessKey), LogClient.Config.Instance.GetValue(Category.Api, Key.SecretKey));
                string response = await task;

                JsonSerializerSettings options = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore
                };

                comboBoxZone.InvokeIfRequired(s =>
                {
                    if (response.Contains("responseError"))
                    {
                        hasError hasError = JsonConvert.DeserializeObject<hasError>(response, options);
                        throw new Exception(hasError.responseError.returnMessage);
                    }
                    else
                    {
                        getZoneList getZoneList = JsonConvert.DeserializeObject<getZoneList>(response, options);
                        if (getZoneList.getZoneListResponse.returnCode.Equals("0"))
                        {
                            s.Items.Clear();
                            foreach (var a in getZoneList.getZoneListResponse.zoneList)
                            {
                                var item = new zone
                                {
                                    zoneNo = a.zoneNo,
                                    zoneName = a.zoneName,
                                    zoneCode = a.zoneCode,
                                    zoneDescription = a.zoneDescription,
                                    regionNo = a.regionNo
                                };
                                s.Items.Add(item);
                            }
                        }
                    }
                    s.SelectedIndex = 0;
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async void ComboBoxRegionChanged(object sender, EventArgs e)
        {
            try
            {
                if (!initailLoadData)
                {
                    string regionNo = string.Empty;
                    string zoneNo = string.Empty;

                    regionNo = (comboBoxRegion.SelectedItem as region).regionNo;
                    await GetZoneList(regionNo);
                    zoneNo = (comboBoxZone.SelectedItem as zone).zoneNo;

                    await PublicIpListLoad(
                        regionNo,
                        zoneNo
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void ComboBoxZoneChanged(object sender, EventArgs e)
        {
            try
            {
                if (!initailLoadData)
                {
                    string regionNo = string.Empty;
                    string zoneNo = string.Empty;

                    zoneNo = (comboBoxZone.SelectedItem as zone).zoneNo;
                    regionNo = (comboBoxRegion.SelectedItem as region).regionNo;

                    await PublicIpListLoad(
                        regionNo,
                        zoneNo
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void buttonCreatePublicIp_Click(object sender, EventArgs e)
        {
            try
            {
                int checkBoxCount = 0;

                foreach (DataGridViewRow item in dgvServerList.Rows)
                {
                    if (bool.Parse(item.Cells["CheckBox"].Value.ToString()))
                    {
                        checkBoxCount++;
                    }
                }
                if (checkBoxCount == 0)
                    throw new Exception("select server");

                DialogResult result = MessageBox.Show("Do you really want to run?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result != DialogResult.Yes)
                    return;

                ControlHelpers.ButtonStatusChange(buttonCreatePublicIp, "Requested");

                foreach (DataGridViewRow item in dgvServerList.Rows)
                {
                    if (bool.Parse(item.Cells["CheckBox"].Value.ToString()))
                    {
                        string publicIp = await CreatePublicIpInstance(item.Cells["InstanceNo"].Value.ToString());
                        if (publicIp != null && publicIp.Length > 0)
                        {
                            var p = new List<KeyValuePair<string, string>>();
                            p.Add(new KeyValuePair<string, string>("serverName", item.Cells["Name"].Value.ToString()));
                            p.Add(new KeyValuePair<string, string>("serverPublicIp", publicIp));
                            await fileDb.UpSertTable(FileDb.TableName.TBL_SERVER, p);
                        }
                    }
                }
                await ServerListLoad();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                ControlHelpers.ButtonStatusChange(buttonCreatePublicIp, "Create");
            }
        }

        private async Task<string> CreatePublicIpInstance(string serverInstanceNo)
        {
            string publicIp = string.Empty;
            try
            {
                string endpoint = dataManager.GetValue(DataManager.Category.ApiGateway, DataManager.Key.Endpoint);
                string action = @"server/v2/createPublicIpInstance";
                List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>();
                parameters.Add(new KeyValuePair<string, string>("responseFormatType", "json"));
                parameters.Add(new KeyValuePair<string, string>("serverInstanceNo", serverInstanceNo));

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

                createPublicIpInstance createPublicIpInstance = JsonConvert.DeserializeObject<createPublicIpInstance>(response, options);
                if (createPublicIpInstance.createPublicIpInstanceResponse.returnCode.Equals("0"))
                {
                    foreach (var a in createPublicIpInstance.createPublicIpInstanceResponse.publicIpInstanceList)
                        publicIp = a.publicIp;

                    if (createPublicIpInstance.createPublicIpInstanceResponse.totalRows == 0)
                        throw new Exception("createPublicIpInstance error");
                }

            }
            catch (Exception)
            {
                throw;
            }
            return publicIp;
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

        private async void buttonStopServer_Click(object sender, EventArgs e)
        {
            try
            {
                int checkBoxCount = 0;
                List<string> instanceNoList = new List<string>();
                foreach (DataGridViewRow item in dgvServerList.Rows)
                {
                    if (bool.Parse(item.Cells["CheckBox"].Value.ToString()))
                    {
                        instanceNoList.Add(item.Cells["InstanceNo"].Value.ToString());
                        checkBoxCount++;
                    }
                }
                if (checkBoxCount == 0)
                    throw new Exception("select server");

                DialogResult result = MessageBox.Show("Do you really want to run?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result != DialogResult.Yes)
                    return;

                ControlHelpers.ButtonStatusChange(buttonStopServer, "Requested");

                await StopServerInstances(instanceNoList);
                var task = ServerListLoad();
                await task;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                ControlHelpers.ButtonStatusChange(buttonStopServer, "Stop");
            }
        }

        private async void buttonTerminateServer_Click(object sender, EventArgs e)
        {
            try
            {
                int checkBoxCount = 0;
                List<string> instanceNoList = new List<string>();
                foreach (DataGridViewRow item in dgvServerList.Rows)
                {
                    if (bool.Parse(item.Cells["CheckBox"].Value.ToString()))
                    {
                        instanceNoList.Add(item.Cells["InstanceNo"].Value.ToString());
                        checkBoxCount++;
                    }
                }
                if (checkBoxCount == 0)
                    throw new Exception("select server");

                DialogResult result = MessageBox.Show("Do you really want to run?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result != DialogResult.Yes)
                    return;

                ControlHelpers.ButtonStatusChange(buttonTerminateServer, "Requested");

                try
                {
                    await TerminateServerInstances(instanceNoList);
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("Not found contract information") || ex.Message.Contains("server not founds"))
                    {
                        // Yum Yum
                    }
                    else if (ex.Message.Contains("additional storage"))
                    {
                        throw new Exception(@"Try deleting the assigned disk first from the 'Set Server Disk' menu.");
                    }
                    else
                        throw ex; 
                }

                var task = ServerListLoad();
                await task;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                ControlHelpers.ButtonStatusChange(buttonTerminateServer, "Terminate");
            }
        }

        private async void buttonStartServer_Click(object sender, EventArgs e)
        {
            try
            {
                int checkBoxCount = 0;
                List<string> instanceNoList = new List<string>();
                foreach (DataGridViewRow item in dgvServerList.Rows)
                {
                    if (bool.Parse(item.Cells["CheckBox"].Value.ToString()))
                    {
                        instanceNoList.Add(item.Cells["InstanceNo"].Value.ToString());
                        checkBoxCount++;
                    }
                }
                if (checkBoxCount == 0)
                    throw new Exception("select server");

                ControlHelpers.ButtonStatusChange(buttonStartServer, "Requested");
                var t1 = StartServerInstances(instanceNoList);
                await t1;
                var t2 = ServerListLoad();
                await t2;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                ControlHelpers.ButtonStatusChange(buttonStartServer, "Start");
            }
        }

        private async void buttonPublicIpListReload_Click(object sender, EventArgs e)
        {
            try
            {
                await PublicIpListLoad(
                        (comboBoxRegion.SelectedItem as region).regionNo,
                        (comboBoxZone.SelectedItem as zone).zoneNo
                    );
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("ip not founds")) { }
                else
                    MessageBox.Show(ex.Message);
            }
        }



        private async void buttonDeleteIp_Click(object sender, EventArgs e)
        {
            try
            {


                int checkBoxCount = 0;
                List<string> instanceNoList = new List<string>();
                foreach (DataGridViewRow item in dgvPublicIpList.Rows)
                {
                    if (bool.Parse(item.Cells["CheckBox"].Value.ToString()))
                    {
                        instanceNoList.Add(item.Cells["IpInstanceNo"].Value.ToString());
                        checkBoxCount++;
                    }
                }
                if (checkBoxCount == 0)
                    throw new Exception("select ip");

                DialogResult result = MessageBox.Show("Do you really want to run?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result != DialogResult.Yes)
                    return;

                ControlHelpers.ButtonStatusChange(buttonDeleteIp, "Requested");

                var t1 = DeletePublicIpInstances(instanceNoList);
                await t1;
                var t2 = PublicIpListLoad(
                        (comboBoxRegion.SelectedItem as region).regionNo,
                        (comboBoxZone.SelectedItem as zone).zoneNo
                    );

                await t2;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                ControlHelpers.ButtonStatusChange(buttonDeleteIp, "Delete");
            }
        }

        private async void buttonDisassociateIp_Click(object sender, EventArgs e)
        {
            try
            {
                int checkBoxCount = 0;

                foreach (DataGridViewRow item in dgvPublicIpList.Rows)
                {
                    if (bool.Parse(item.Cells["CheckBox"].Value.ToString()))
                    {
                        checkBoxCount++;
                    }
                }

                if (checkBoxCount == 0)
                    throw new Exception("select ip");

                DialogResult result = MessageBox.Show("Do you really want to run?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result != DialogResult.Yes)
                    return;

                ControlHelpers.ButtonStatusChange(buttonDisassociateIp, "Requested");

                List<string> instanceNoList = new List<string>();
                List<Task> tasks = new List<Task>();

                // disassociatepublicip
                foreach (DataGridViewRow item in dgvPublicIpList.Rows)
                {
                    if (bool.Parse(item.Cells["CheckBox"].Value.ToString()))
                    {

                        tasks.Add(DisassociatePublicIpFromServerInstance(item.Cells["IpInstanceNo"].Value.ToString()));
                    }
                }
                await Task.WhenAll(tasks);

                // update filedb 
                foreach (DataGridViewRow item in dgvPublicIpList.Rows)
                {
                    if (bool.Parse(item.Cells["CheckBox"].Value.ToString()))
                    {
                        var p = new List<KeyValuePair<string, string>>();
                        p.Add(new KeyValuePair<string, string>("serverName", item.Cells["ServerName"].Value.ToString()));
                        p.Add(new KeyValuePair<string, string>("serverPublicIp", ""));
                        await (fileDb.UpSertTable(FileDb.TableName.TBL_SERVER, p));
                    }
                }

                var task = PublicIpListLoad(
                    (comboBoxRegion.SelectedItem as region).regionNo,
                    (comboBoxZone.SelectedItem as zone).zoneNo
                );

                await task;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                ControlHelpers.ButtonStatusChange(buttonDisassociateIp, "Disassociate");
            }
        }

        private void buttonGetPassword_Click(object sender, EventArgs e)
        {
            try
            {
                
                string checkedServerInstanceNo = "";

                if (ControlHelpers.CheckBoxCheckedCnt(dgvServerList) != 1)
                    throw new Exception("select a server");

                foreach (DataGridViewRow item in dgvServerList.Rows)
                {
                    if (bool.Parse(item.Cells["CheckBox"].Value.ToString()))
                    {
                        checkedServerInstanceNo = item.Cells["InstanceNo"].Value.ToString();
                    }
                }

                var json = new
                {
                    responseFormatType = "json",
                    serverInstanceNo = checkedServerInstanceNo,
                    privateKey = "INPUT YOUR RSA PRIVATE KEY",
                };

                JToken jt = JToken.Parse(JsonConvert.SerializeObject(json));
                string command = jt.ToString(Newtonsoft.Json.Formatting.Indented);

                FormNcpRestPreview formNcpRestPreview = FormNcpRestPreview.Instance;
                formNcpRestPreview.TitleText = "Get Password";
                formNcpRestPreview.Action = @"/server/v2/getRootPassword";
                formNcpRestPreview.Command = command;
                formNcpRestPreview.Callback = false;
                formNcpRestPreview.Result = "";
                formNcpRestPreview.StartPosition = FormStartPosition.CenterScreen;
                formNcpRestPreview.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void buttonGetServerInfo_Click(object sender, EventArgs e)
        {
            try
            {
                int checkBoxCount = 0;
                string checkedServerInstanceNo = "";
                foreach (DataGridViewRow item in dgvServerList.Rows)
                {
                    if (bool.Parse(item.Cells["CheckBox"].Value.ToString()))
                    {
                        checkBoxCount++;
                        checkedServerInstanceNo = item.Cells["InstanceNo"].Value.ToString();
                    }
                }
                if (checkBoxCount != 1)
                    throw new Exception("select server");

                var josn = new
                {
                    responseFormatType = "json",
                    serverInstanceNoList_1 = checkedServerInstanceNo,
                };

                JToken jt = JToken.Parse(JsonConvert.SerializeObject(josn));
                string command = jt.ToString(Newtonsoft.Json.Formatting.Indented);

                FormNcpRestPreview formNcpRestPreview = FormNcpRestPreview.Instance;
                formNcpRestPreview.Action = @"/server/v2/getServerInstanceList";
                formNcpRestPreview.Command = command.Replace("_1", ".1");
                formNcpRestPreview.Callback = false;
                formNcpRestPreview.Result = "";
                await formNcpRestPreview.RestCall();
                formNcpRestPreview.StartPosition = FormStartPosition.CenterScreen;
                formNcpRestPreview.ShowDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void buttonAssociatePublicIpAndServer_Click(object sender, EventArgs e)
        {
            try
            {
                int checkBoxCount = 0;
                string checkedServerInstanceNo = "";
                foreach (DataGridViewRow item in dgvServerList.Rows)
                {
                    if (bool.Parse(item.Cells["CheckBox"].Value.ToString()))
                    {
                        checkBoxCount++;
                        checkedServerInstanceNo = item.Cells["InstanceNo"].Value.ToString();
                    }
                }
                if (checkBoxCount != 1)
                    throw new Exception("Please select one server");

                int ipCheckBoxCount = 0;
                string checkedIpInstanceNo = "";
                foreach (DataGridViewRow item in dgvPublicIpList.Rows)
                {
                    if (bool.Parse(item.Cells["CheckBox"].Value.ToString()))
                    {
                        ipCheckBoxCount++;
                        checkedIpInstanceNo = item.Cells["IpInstanceNo"].Value.ToString();
                    }
                }
                if (ipCheckBoxCount != 1)
                    throw new Exception("select one ip");

                DialogResult result = MessageBox.Show("Do you really want to run?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result != DialogResult.Yes)
                    return;

                ControlHelpers.ButtonStatusChange(buttonAssociatePublicIpAndServer, "Requested");

                var json = new
                {
                    responseFormatType = "json",
                    serverInstanceNo = checkedServerInstanceNo,
                    publicIpInstanceNo = checkedIpInstanceNo
                };

                JToken jt = JToken.Parse(JsonConvert.SerializeObject(json));
                string command = jt.ToString(Newtonsoft.Json.Formatting.Indented);

                FormNcpRestPreview formNcpRestPreview = FormNcpRestPreview.Instance;
                formNcpRestPreview.Action = @"/server/v2/associatePublicIpWithServerInstance";
                formNcpRestPreview.Callback = false;
                formNcpRestPreview.Command = command;
                formNcpRestPreview.Result = "";
                await formNcpRestPreview.RestCall();
                string response = formNcpRestPreview.Result;


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

                string publicIp = string.Empty;
                string serverName = string.Empty;

                associatePublicIpWithServerInstance associatePublicIpWithServerInstance = JsonConvert.DeserializeObject<associatePublicIpWithServerInstance>(response, options);
                if (associatePublicIpWithServerInstance.associatePublicIpWithServerInstanceResponse.returnCode.Equals("0"))
                {

                    foreach (var a in associatePublicIpWithServerInstance.associatePublicIpWithServerInstanceResponse.publicIpInstanceList)
                    {
                        publicIp = a.publicIp;
                        serverName = a.serverInstanceAssociatedWithPublicIp.serverName;
                    }

                    if (associatePublicIpWithServerInstance.associatePublicIpWithServerInstanceResponse.totalRows == 0)
                    {
                        throw new Exception("associatePublicIpWithServerInstance failed");
                    }
                }

                var p = new List<KeyValuePair<string, string>>();
                p.Add(new KeyValuePair<string, string>("serverName", serverName));
                p.Add(new KeyValuePair<string, string>("serverPublicIp", publicIp));
                await fileDb.UpSertTable(FileDb.TableName.TBL_SERVER, p);
                var task = ServerListLoad();
                await task;
                await PublicIpListLoad(
                    (comboBoxRegion.SelectedItem as region).regionNo,
                    (comboBoxZone.SelectedItem as zone).zoneNo
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                ControlHelpers.ButtonStatusChange(buttonAssociatePublicIpAndServer, "Associate");
            }
        }
    }
}
