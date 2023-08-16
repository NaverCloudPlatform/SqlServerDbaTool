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
using HaTool.Model.NCloud;
using LogClient;
using HaTool.Config;
using CsLib;
using Newtonsoft.Json;
using HaTool.Global;
using Newtonsoft.Json.Linq;
using HaTool.Model;

namespace HaTool.Server
{
    public partial class UcCreateServer : UserControl
    {
        private static readonly Lazy<UcCreateServer> lazy =
            new Lazy<UcCreateServer>(() => new UcCreateServer(), LazyThreadSafetyMode.ExecutionAndPublication);

        public static UcCreateServer Instance { get { return lazy.Value; } }

        LogClient.Config logClientConfig = LogClient.Config.Instance;
        DataManager dataManager = DataManager.Instance;
        FileDb fileDb = FileDb.Instance;
        FormNcpRestPreview formNcpRestPreview = FormNcpRestPreview.Instance;
        
        private serverInstance CheckedServer = new serverInstance();
        
        public UcCreateServer()
        {
            InitializeComponent();
            formNcpRestPreview.ScriptCompleteEvent += PreviewActionCompleted;
        }

        private async void LoadData(object sender, EventArgs e)
        {
            dataManager.LoadUserData();

            try
            {
                List<Task> tasks = new List<Task>();
                tasks.Add(GetRegionList());
                tasks.Add(GetZoneList("1"));
                //tasks.Add(GetServerImageProductList("SPSW0WINNTEN0043A", "1"));
                //tasks.Add(GetServerProductList("SPSW0WINNTEN0043A", "1", "2"));
                tasks.Add(GetServerImageProductList("SPSW0WINNTEN0050A", "1"));
                tasks.Add(GetServerProductList("SPSW0WINNTEN0050A", "1", "2"));
                tasks.Add(GetAccessControlGroupList());
                await Task.WhenAll(tasks);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async Task GetAccessControlGroupList()
        {
            try
            {
                string endpoint = dataManager.GetValue(DataManager.Category.ApiGateway, DataManager.Key.Endpoint);
                string action = @"/server/v2/getAccessControlGroupList";
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
                    getAccessControlGroupList getAccessControlGroupList = JsonConvert.DeserializeObject<getAccessControlGroupList>(response, options);
                    if (getAccessControlGroupList.getAccessControlGroupListResponse.returnCode.Equals("0"))
                    {
                        comboBoxACG1.Items.Clear();
                        comboBoxACG2.Items.Clear();
                        comboBoxACG3.Items.Clear();
                        comboBoxACG4.Items.Clear();
                        comboBoxACG5.Items.Clear();

                        foreach (var a in getAccessControlGroupList.getAccessControlGroupListResponse.accessControlGroupList)
                        {
                            var item = new accessControlGroup
                            {
                                accessControlGroupConfigurationNo = a.accessControlGroupConfigurationNo,
                                accessControlGroupName = a.accessControlGroupName,
                                accessControlGroupDescription = a.accessControlGroupDescription,
                                isDefault = a.isDefault,
                                createDate = a.createDate
                            };
                            comboBoxACG1.Items.Add(item);
                            comboBoxACG2.Items.Add(item);
                            comboBoxACG3.Items.Add(item);
                            comboBoxACG4.Items.Add(item);
                            comboBoxACG5.Items.Add(item);

                        }
                    }
                }




            }
            catch (Exception)
            {
                throw;
            }
            comboBoxZone.SelectedIndex = 0;
        }

        private async Task GetServerProductList(
            string serverImageProductCode
            , string regionNo
            , string zoneNo
            )
        {
            try
            {
                string endpoint = dataManager.GetValue(DataManager.Category.ApiGateway, DataManager.Key.Endpoint);
                string action = @"/server/v2/getServerProductList";
                List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>();
                parameters.Add(new KeyValuePair<string, string>("responseFormatType", "json"));
                parameters.Add(new KeyValuePair<string, string>("serverImageProductCode", serverImageProductCode));
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


                comboBoxServer.InvokeIfRequired(s => {
                    if (response.Contains("responseError"))
                    {
                        hasError hasError = JsonConvert.DeserializeObject<hasError>(response, options);
                        throw new Exception(hasError.responseError.returnMessage);
                    }
                    else
                    {
                        getServerProductList getServerProductList = JsonConvert.DeserializeObject<getServerProductList>(response, options);
                        if (getServerProductList.getServerProductListResponse.returnCode.Equals("0"))
                        {
                            s.Items.Clear();
                            foreach (var a in getServerProductList.getServerProductListResponse.productList)
                            {
                                var item = new srvProduct
                                {
                                    productCode = a.productCode,
                                    productName = a.productName,
                                    productType = new codeCodeName
                                    {
                                        code = a.productType.code,
                                        codeName = a.productType.codeName
                                    },
                                    productDescription = a.productDescription,
                                    infraResourceType = new codeCodeName
                                    {
                                        code = a.infraResourceType.code,
                                        codeName = a.infraResourceType.codeName
                                    },
                                    cpuCount = a.cpuCount,
                                    memorySize = a.memorySize,
                                    baseBlockStorageSize = a.baseBlockStorageSize,

                                    osInformation = a.osInformation,
                                    diskType = new codeCodeName
                                    {
                                        code = a.diskType.code,
                                        codeName = a.diskType.codeName
                                    },
                                    dbKindCode = a.dbKindCode,
                                    addBlockStorageSize = a.addBlockStorageSize,
                                };
                                s.Items.Add(item);
                            }
                        }
                    }
                    if (s.Items.Count > 0)
                        s.SelectedIndex = 0;
                    else
                    {
                        s.Text = "not exists";
                    }
                        
                });

            }
            catch (Exception)
            {
                throw;
            }
            
        }

        private async Task GetServerImageProductList(string productCode, string regionNo)
        {
            try
            {
                string endpoint = dataManager.GetValue(DataManager.Category.ApiGateway, DataManager.Key.Endpoint);
                string action = @"/server/v2/getServerImageProductList";
                List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>();
                parameters.Add(new KeyValuePair<string, string>("responseFormatType", "json"));
                parameters.Add(new KeyValuePair<string, string>("blockStorageSize", "100"));
                //parameters.Add(new KeyValuePair<string, string>("productCode", "SPSW0WINNTEN0043A")); // SPSW0WINNTEN0050A
                parameters.Add(new KeyValuePair<string, string>("productCode", "SPSW0WINNTEN0050A")); // 
                parameters.Add(new KeyValuePair<string, string>("regionNo", regionNo));
                SoaCall soaCall = new SoaCall();
                var task = soaCall.WebApiCall(endpoint, RequestType.POST, action, parameters, LogClient.Config.Instance.GetValue(Category.Api, Key.AccessKey), LogClient.Config.Instance.GetValue(Category.Api, Key.SecretKey));
                string response = await task;

                JsonSerializerSettings options = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore
                };


                comboBoxServerImage.InvokeIfRequired(s => {
                    if (response.Contains("responseError"))
                    {
                        hasError hasError = JsonConvert.DeserializeObject<hasError>(response, options);
                        throw new Exception(hasError.responseError.returnMessage);
                    }
                    else
                    {
                        getServerImageProductList getServerImageProductList = JsonConvert.DeserializeObject<getServerImageProductList>(response, options);
                        if (getServerImageProductList.getServerImageProductListResponse.returnCode.Equals("0"))
                        {
                            s.Items.Clear();
                            foreach (var a in getServerImageProductList.getServerImageProductListResponse.productList)
                            {
                                var item = new imgProduct
                                {
                                    productCode = a.productCode,
                                    productName = a.productName,
                                    productType = new codeCodeName
                                    {
                                        code = a.productType.code,
                                        codeName = a.productType.codeName
                                    },
                                    productDescription = a.productDescription,
                                    infraResourceType = new codeCodeName
                                    {
                                        code = a.infraResourceType.code,
                                        codeName = a.infraResourceType.codeName
                                    },
                                    cpuCount = a.cpuCount,
                                    memorySize = a.memorySize,
                                    baseBlockStorageSize = a.baseBlockStorageSize,
                                    platformType = new codeCodeName
                                    {
                                        code = a.platformType.code,
                                        codeName = a.platformType.codeName
                                    },
                                    osInformation = a.osInformation,
                                    dbKindCode = a.dbKindCode,
                                    addBlockStorageSize = a.addBlockStorageSize,
                                };
                                s.Items.Add(item);
                            }
                        }
                    }
                    if (s.Items.Count > 0)
                        s.SelectedIndex = 0;
                    else
                    {
                        s.Text = "not exists";
                    }
                });
            }
            catch (Exception)
            {
                throw;
            }
            
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
                    if (s.Items.Count > 0)
                        s.SelectedIndex = 0;
                    else
                    {
                        s.Text = "not exists";
                    }
                });
            }
            catch (Exception)
            {
                throw;
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
                            // regionNo 1 Korea
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
            //MessageBox.Show((comboBoxRegion.SelectedItem as region).regionCode);
        }


        private bool PreconditionCheck()
        {
            string userDataFinal = dataManager.GetValue(DataManager.Category.InitScript, DataManager.Key.userDataFinal);
            if (userDataFinal.Trim().Length == 0)
                throw new Exception("The startup script is not set. Please set InitScript in Config Tab.");
            if (comboBoxServer.Text == "not exists")
                throw new Exception("There is no corresponding server spec.");
            return true; 
        }


        private async void buttonCreateServer_Click(object sender, EventArgs e)
        {
            try
            {
                if (!PreconditionCheck())
                    return;

                buttonCreateServer.Text = "requested";
                buttonCreateServer.Enabled = false; 
                InputCheck(false);
                MessageBox.Show("server create started please wait 10 min.");
                
                string command = GetCreateServerInstancesJsonCommand();
                formNcpRestPreview.Action = @"/server/v2/createServerInstances";
                formNcpRestPreview.Command = command;
                formNcpRestPreview.Callback = true;
                await formNcpRestPreview.RestCall();
                await ResponseFileDbSave(formNcpRestPreview.Result);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                buttonCreateServer.Text = "Create Server";
                buttonCreateServer.Enabled = true;
            }
        }

        private async Task ResponseFileDbSave(string response)
        {
            try
            {
                List<serverInstance> serverInstances = new List<serverInstance>();
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
                    createServerInstances createServerInstances = JsonConvert.DeserializeObject<createServerInstances>(response, options);
                    if (createServerInstances.createServerInstancesResponse.returnCode.Equals("0"))
                    {
                        serverInstances.Clear();
                        foreach (var a in createServerInstances.createServerInstancesResponse.serverInstanceList)
                        {
                            var item = new serverInstance
                            {
                                serverName = a.serverName,
                                serverInstanceNo = a.serverInstanceNo,
                                publicIp = a.publicIp,
                                privateIp = a.privateIp,
                                region = new region
                                {
                                    regionNo = a.region.regionNo,
                                    regionCode = a.region.regionCode,
                                    regionName = a.region.regionName
                                },
                                zone = new zone
                                {
                                    zoneNo = a.zone.zoneNo,
                                    zoneName = a.zone.zoneName,
                                    zoneCode = a.zone.zoneCode,
                                    zoneDescription = a.zone.zoneDescription,
                                    regionNo = a.zone.regionNo
                                },
                                serverImageProductCode = a.serverImageProductCode,
                                serverProductCode = a.serverProductCode,
                                feeSystemTypeCode = "FXSUM",
                                loginKeyName = a.loginKeyName,
                                // where is acg list ?

                            };
                            serverInstances.Add(item);
                        }
                        if (createServerInstances.createServerInstancesResponse.totalRows == 0)
                        {
                            CheckedServer = new serverInstance();
                            new Exception ("createServerInstances response error.");
                        }
                        else
                        {
                            foreach (var a in serverInstances)
                            {
                                var p = new List<KeyValuePair<string, string>>();
                                p.Add(new KeyValuePair<string, string>("serverName", a.serverName));
                                p.Add(new KeyValuePair<string, string>("serverInstanceNo", a.serverInstanceNo));
                                p.Add(new KeyValuePair<string, string>("serverPublicIp", a.publicIp));
                                p.Add(new KeyValuePair<string, string>("serverPrivateIp", a.privateIp));
                                p.Add(new KeyValuePair<string, string>("regionNo", a.region.regionNo));
                                p.Add(new KeyValuePair<string, string>("zoneNo", a.zone.zoneNo));
                                p.Add(new KeyValuePair<string, string>("serverImageProductCode", a.serverImageProductCode));
                                p.Add(new KeyValuePair<string, string>("serverProductCode", a.serverProductCode));
                                p.Add(new KeyValuePair<string, string>("feeSystemTypeCode", a.feeSystemTypeCode));
                                p.Add(new KeyValuePair<string, string>("loginKeyName", a.loginKeyName));

                                if (comboBoxACG1.Text.Equals(""))
                                    p.Add(new KeyValuePair<string, string>("accessControlGroupConfigurationNoList_1", "NULL"));
                                else
                                    p.Add(new KeyValuePair<string, string>("accessControlGroupConfigurationNoList_1", (comboBoxACG1.SelectedItem as accessControlGroup).accessControlGroupConfigurationNo));

                                if (comboBoxACG2.Text.Equals(""))
                                    p.Add(new KeyValuePair<string, string>("accessControlGroupConfigurationNoList_2", "NULL"));
                                else
                                    p.Add(new KeyValuePair<string, string>("accessControlGroupConfigurationNoList_2", (comboBoxACG2.SelectedItem as accessControlGroup).accessControlGroupConfigurationNo));

                                if (comboBoxACG3.Text.Equals(""))
                                    p.Add(new KeyValuePair<string, string>("accessControlGroupConfigurationNoList_3", "NULL"));
                                else
                                    p.Add(new KeyValuePair<string, string>("accessControlGroupConfigurationNoList_3", (comboBoxACG3.SelectedItem as accessControlGroup).accessControlGroupConfigurationNo));

                                if (comboBoxACG4.Text.Equals(""))
                                    p.Add(new KeyValuePair<string, string>("accessControlGroupConfigurationNoList_4", "NULL"));
                                else
                                    p.Add(new KeyValuePair<string, string>("accessControlGroupConfigurationNoList_4", (comboBoxACG4.SelectedItem as accessControlGroup).accessControlGroupConfigurationNo));

                                if (comboBoxACG5.Text.Equals(""))
                                    p.Add(new KeyValuePair<string, string>("accessControlGroupConfigurationNoList_5", "NULL"));
                                else
                                    p.Add(new KeyValuePair<string, string>("accessControlGroupConfigurationNoList_5", (comboBoxACG5.SelectedItem as accessControlGroup).accessControlGroupConfigurationNo));

                                await fileDb.UpSertTable(FileDb.TableName.TBL_SERVER, p);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        private async void PreviewActionCompleted(object sender, ScriptArgs e)
        {
            await ResponseFileDbSave(e.Script);
        }

        private void buttonCommandPreview_Click(object sender, EventArgs e)
        {
            try
            {
                InputCheck(false);
                string command = GetCreateServerInstancesJsonCommand();

                formNcpRestPreview.Action = @"/server/v2/createServerInstances";
                formNcpRestPreview.Command = command;
                formNcpRestPreview.Callback = true;
                formNcpRestPreview.StartPosition = FormStartPosition.CenterScreen;
                formNcpRestPreview.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void ComboBoxRegionChanged(object sender, EventArgs e)
        {
            try
            {
                if (comboBoxServerImage.Items.Count > 0)
                {
                    ComboBox c = (ComboBox)sender;
                    string regionNo = (c.SelectedItem as region).regionNo;
                    await GetZoneList(regionNo);
                    string serverImageProductCode = (comboBoxServerImage.SelectedItem as imgProduct).productCode;
                    string zoneNo = (comboBoxZone.SelectedItem as zone).zoneNo;
                    await GetServerProductList(serverImageProductCode, regionNo, zoneNo);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void buttonServerNameCheck_Click(object sender, EventArgs e)
        {
            try
            {
                await ServerNameCheck();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        private async Task ServerNameCheck(bool message = true)
        {
            try
            {
                if (textBoxServerName.Text.Length < 3)
                    throw new Exception("check server name first");

                List<serverInstance> serverInstances = new List<serverInstance>();

                string endpoint = dataManager.GetValue(DataManager.Category.ApiGateway, DataManager.Key.Endpoint);
                string action = @"/server/v2/getServerInstanceList";
                List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>();
                parameters.Add(new KeyValuePair<string, string>("responseFormatType", "json"));
                parameters.Add(new KeyValuePair<string, string>("searchFilterName", "serverName"));
                parameters.Add(new KeyValuePair<string, string>("searchFilterValue", textBoxServerName.Text.Trim()));
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
                                serverInstanceStatus = new codeCodeName
                                {
                                    code = a.serverInstanceStatus.code,
                                    codeName = a.serverInstanceStatus.codeName
                                },
                                serverInstanceOperation = new codeCodeName
                                {
                                    code = a.serverInstanceOperation.code,
                                    codeName = a.serverInstanceOperation.codeName
                                }
                            };
                            serverInstances.Add(item);
                        }
                        if (getServerInstanceList.getServerInstanceListResponse.totalRows == 0)
                        {
                            CheckedServer = new serverInstance();
                            if (message)
                                MessageBox.Show("You can use the name as the server name.");
                        }
                        else
                        {
                            bool matched = false;
                            foreach (var a in serverInstances)
                            {
                                if (a.serverName.Equals(textBoxServerName.Text.Trim(), StringComparison.OrdinalIgnoreCase))
                                {
                                    matched = true;
                                    CheckedServer = a;
                                    if (message)
                                        MessageBox.Show($"You have a server with that name. serverInstanceNo : {CheckedServer.serverInstanceNo}");
                                }
                            }
                            if (!matched)
                                if (message)
                                    MessageBox.Show("You can use the name as the server name.");
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        private string GetCreateServerInstancesJsonCommand()
        {
            try
            {
                List<KeyValuePair<string, string>> listKeyValueParameters = GetParameters();
                listKeyValueParameters.Add(new KeyValuePair<string, string>("responseFormatType", "json"));
                listKeyValueParameters.Add(new KeyValuePair<string, string>("userData", TranString.EncodeBase64(dataManager.GetValue(DataManager.Category.InitScript, DataManager.Key.userDataFinal))));
                Dictionary<string, string> dicParameters = new Dictionary<string, string>();

                foreach (var a in listKeyValueParameters)
                {
                    if (a.Value == null || a.Value.Equals("NULL", StringComparison.OrdinalIgnoreCase))
                    {
                    }
                    else
                        dicParameters.Add(a.Key, a.Value);
                }
                JToken jt = JToken.Parse(JsonConvert.SerializeObject(dicParameters));
                return jt.ToString(Newtonsoft.Json.Formatting.Indented).Replace("_",".");
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
                await ServerNameCheck(false);
                await Save();
                MessageBox.Show("saved");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void InputCheck(bool checkServerInstanceNo)
        {
            if(checkServerInstanceNo)
                if (CheckedServer.serverInstanceNo == null)
                    throw new Exception("check server name first");

            if (textBoxServerName.Text.Length < 3)
                throw new Exception("check server name first");

            if (comboBoxACG1.Text.Equals("")
                && comboBoxACG2.Text.Equals("")
                && comboBoxACG3.Text.Equals("")
                && comboBoxACG4.Text.Equals("")
                && comboBoxACG5.Text.Equals(""))
                throw new Exception("check acg first");
        }

        private List<KeyValuePair<string, string>> GetParameters()
        {
            var p = new List<KeyValuePair<string, string>>();
            p.Add(new KeyValuePair<string, string>("serverName", textBoxServerName.Text));
            p.Add(new KeyValuePair<string, string>("serverInstanceNo", CheckedServer.serverInstanceNo));
            p.Add(new KeyValuePair<string, string>("serverPublicIp", CheckedServer.publicIp));
            p.Add(new KeyValuePair<string, string>("serverPrivateIp", CheckedServer.privateIp));
            p.Add(new KeyValuePair<string, string>("regionNo", (comboBoxRegion.SelectedItem as region).regionNo));
            p.Add(new KeyValuePair<string, string>("zoneNo", (comboBoxZone.SelectedItem as zone).zoneNo));
            p.Add(new KeyValuePair<string, string>("serverImageProductCode", (comboBoxServerImage.SelectedItem as imgProduct).productCode));
            p.Add(new KeyValuePair<string, string>("serverProductCode", (comboBoxServer.SelectedItem as srvProduct).productCode));
            p.Add(new KeyValuePair<string, string>("feeSystemTypeCode", "FXSUM"));
            p.Add(new KeyValuePair<string, string>("loginKeyName", dataManager.GetValue(DataManager.Category.LoginKey, DataManager.Key.Name)));

            if (comboBoxACG1.Text.Equals(""))
                p.Add(new KeyValuePair<string, string>("accessControlGroupConfigurationNoList_1", "NULL"));
            else
                p.Add(new KeyValuePair<string, string>("accessControlGroupConfigurationNoList_1", (comboBoxACG1.SelectedItem as accessControlGroup).accessControlGroupConfigurationNo));

            if (comboBoxACG2.Text.Equals(""))
                p.Add(new KeyValuePair<string, string>("accessControlGroupConfigurationNoList_2", "NULL"));
            else
                p.Add(new KeyValuePair<string, string>("accessControlGroupConfigurationNoList_2", (comboBoxACG2.SelectedItem as accessControlGroup).accessControlGroupConfigurationNo));

            if (comboBoxACG3.Text.Equals(""))
                p.Add(new KeyValuePair<string, string>("accessControlGroupConfigurationNoList_3", "NULL"));
            else
                p.Add(new KeyValuePair<string, string>("accessControlGroupConfigurationNoList_3", (comboBoxACG3.SelectedItem as accessControlGroup).accessControlGroupConfigurationNo));

            if (comboBoxACG4.Text.Equals(""))
                p.Add(new KeyValuePair<string, string>("accessControlGroupConfigurationNoList_4", "NULL"));
            else
                p.Add(new KeyValuePair<string, string>("accessControlGroupConfigurationNoList_4", (comboBoxACG4.SelectedItem as accessControlGroup).accessControlGroupConfigurationNo));

            if (comboBoxACG5.Text.Equals(""))
                p.Add(new KeyValuePair<string, string>("accessControlGroupConfigurationNoList_5", "NULL"));
            else
                p.Add(new KeyValuePair<string, string>("accessControlGroupConfigurationNoList_5", (comboBoxACG5.SelectedItem as accessControlGroup).accessControlGroupConfigurationNo));

            return p; 
        }

        private async Task Save()
        {
            try
            {
                InputCheck(true);
                List<KeyValuePair<string, string>> p = GetParameters();
                await fileDb.UpSertTable(FileDb.TableName.TBL_SERVER, p);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async void buttonDbDelete_Click(object sender, EventArgs e)
        {
            try
            {
                await Delete();
                MessageBox.Show("deleted");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private async Task Delete()
        {
            try
            {
                if (CheckedServer.serverInstanceNo == null)
                    throw new Exception("check server name first");

                var p = new List<KeyValuePair<string, string>>();
                p.Add(new KeyValuePair<string, string>("serverName", textBoxServerName.Text));
                await fileDb.DeleteTable(FileDb.TableName.TBL_SERVER, p);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
