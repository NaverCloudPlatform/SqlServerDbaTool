using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using CsLib;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using LogClient;
using lazylog.Model;

namespace lazylog
{
    class KeyManager
    {
        Log log = Log.Instance;
        LogClient.Config logClientConfig = LogClient.Config.Instance;
        string cmdType = string.Empty;
        WcfRestServer.WcfResponse wcfResponse = new WcfRestServer.WcfResponse();

        public KeyManager(string cmdType)
        {
            this.cmdType = cmdType;
        }

        private bool HasCriticalString(string message)
        {
            bool returnValue = false;
            if (message.Contains("cryption"))
                returnValue = true;
            if (message.Contains("Cryption"))
                returnValue = true;
            if (message.Contains("SAPWD"))
                returnValue = true;
            if (message.Contains("net user"))
                returnValue = true;
            if (message.Contains("password"))
                returnValue = true;
            if (message.Contains("TypeKeySetting"))
                returnValue = true;
            if (message.Contains("TypeSqlIdPassSetting"))
                returnValue = true;

            return returnValue;
        }

        public WcfRestServer.WcfResponse Execute(string cmdText, bool isAuthSuccess)
        {
            try
            {
                if (cmdType == null || cmdType.Trim().Equals(""))
                {
                    wcfResponse.IsSuccess = false;
                    wcfResponse.ResultMessage = "";
                    wcfResponse.ErrorMessage = "cmType is empty";
                    return wcfResponse;
                }
                if (cmdText == null || cmdText.Trim().Equals(""))
                {
                    wcfResponse.IsSuccess = false;
                    wcfResponse.ResultMessage = "";
                    wcfResponse.ErrorMessage = "cmdText is empty";
                    return wcfResponse;
                }

                if (!HasCriticalString(cmdType) && !HasCriticalString(cmdText))
                {
                    log.Warn($"cmdType : {cmdType}");
                    log.Warn($"cmdText : {cmdText}");
                }
                else
                    log.Warn(string.Format("string has critical word, skipped log."));

                if (cmdType.Equals("TypeKeySetting", StringComparison.OrdinalIgnoreCase))
                {
                    if (!isAuthSuccess)
                        wcfResponse = FirstKeySetting(cmdText);
                    else
                        wcfResponse = ChangeKeySetting(cmdText);
                }
                else if (cmdType.Equals("TypeSqlIdPassSetting", StringComparison.OrdinalIgnoreCase))
                {
                    wcfResponse = SqlIdPassSetting(cmdText);
                }
                else if (cmdType.Equals("TypeConfigSetting", StringComparison.OrdinalIgnoreCase))
                {
                    wcfResponse = ConfigSetting(cmdText);
                }
                else if (cmdType.Equals("TypeConfigRead", StringComparison.OrdinalIgnoreCase))
                {
                    wcfResponse = ConfigRead(cmdText);
                }
                else
                {
                    wcfResponse.IsSuccess = false;
                    wcfResponse.ResultMessage = "";
                    wcfResponse.ErrorMessage = "unknown cmdType";
                    return wcfResponse;
                }
            }
            catch (Exception ex)
            {
                wcfResponse.IsSuccess = false;
                wcfResponse.ErrorMessage = ex.Message; // exception message 
            }


            if (!HasCriticalString(cmdType) && !HasCriticalString(cmdText))
                log.Warn($"wcfResponse : {JsonConvert.SerializeObject(wcfResponse)}");
            else
                log.Warn(string.Format("string has critical word, skipped log."));

            return wcfResponse;
        }

        private WcfRestServer.WcfResponse ConfigSetting(string cmdText)
        {

            string ConfigFile = string.Empty;
            string Category = string.Empty;
            string Key = string.Empty;
            string Value = string.Empty;

            try
            {
                var configSetting = JsonConvert.DeserializeObject<ConfigSetting>(cmdText);
                ConfigFile = configSetting.ConfigFile;
                Category = configSetting.Category;
                Key = configSetting.Key;
                Value = configSetting.Value;

                if (ConfigFile.Equals("LazylogConfig.txt", StringComparison.OrdinalIgnoreCase))
                {

                    string value = Config.Instance.GetValue(
                        (lazylog.Category)Enum.Parse(typeof(lazylog.Category), Category)
                        , (lazylog.Key)Enum.Parse(typeof(lazylog.Key), Key)
                        );

                    Config.Instance.SetValue(
                        (lazylog.Category)Enum.Parse(typeof(lazylog.Category), Category)
                        , (lazylog.Key)Enum.Parse(typeof(lazylog.Key), Key)
                        , Value
                        );

                    Config.Instance.SaveData();
                }
                else if (ConfigFile.Equals("LogClientConfig.txt", StringComparison.OrdinalIgnoreCase))
                {
                    string value = LogClient.Config.Instance.GetValue(
                        (LogClient.Category)Enum.Parse(typeof(LogClient.Category), Category)
                        , (LogClient.Key)Enum.Parse(typeof(LogClient.Key), Key)
                        );

                    LogClient.Config.Instance.SetValue(
                        (LogClient.Category)Enum.Parse(typeof(LogClient.Category), Category)
                        , (LogClient.Key)Enum.Parse(typeof(LogClient.Key), Key)
                        , Value
                        );

                    LogClient.Config.Instance.SaveLogClientData();
                }
                else
                {
                    log.Error($@"ConfigSetting Error ConfigFile : {ConfigFile}");
                    wcfResponse.IsSuccess = false;
                    wcfResponse.ResultMessage = "";
                    wcfResponse.ErrorMessage = "Unknown Config File";
                    return wcfResponse;
                }

                wcfResponse.IsSuccess = true;
                wcfResponse.ResultMessage = "Config Change Completed.";
                wcfResponse.ErrorMessage = "";

                if (!HasCriticalString(cmdType) && !HasCriticalString(cmdText))
                {
                    log.Warn($@"ConfigSetting Completed ConfigFile : {ConfigFile}, Category : {Category}, Key : {Key}, Value : {Value}");
                    log.Warn($"wcfResponse : {JsonConvert.SerializeObject(wcfResponse)}");
                }
                else
                    log.Warn(string.Format("string has critical word, skipped log."));

                return wcfResponse;
            }
            catch (Exception ex)
            {
                if (!HasCriticalString(cmdType) && !HasCriticalString(cmdText))
                {
                    log.Error($@"ConfigSetting Error ConfigFile : {ConfigFile}, Category : {Category}, Key : {Key}, Value : {Value}, {ex.Message}, {ex.StackTrace}");
                }
                else
                    log.Warn(string.Format("string has critical word, skipped log."));

                wcfResponse.IsSuccess = false;
                wcfResponse.ResultMessage = "";
                wcfResponse.ErrorMessage = ex.Message;
                if (!HasCriticalString(cmdType) && !HasCriticalString(cmdText))
                {
                    log.Warn($"wcfResponse : {JsonConvert.SerializeObject(wcfResponse)}");
                }
                else
                    log.Warn(string.Format("string has critical word, skipped log."));
                return wcfResponse;
            }
        }




        private WcfRestServer.WcfResponse ConfigRead(string cmdText)
        {
            string ConfigFile = string.Empty;
            string Category = string.Empty;
            string Key = string.Empty;
            string Value = string.Empty;

            try
            {
                var configSetting = JsonConvert.DeserializeObject<ConfigSetting>(cmdText);
                ConfigFile = configSetting.ConfigFile;
                Category = configSetting.Category;
                Key = configSetting.Key;
                Value = configSetting.Value;

                if (ConfigFile.Equals("LazylogConfig.txt", StringComparison.OrdinalIgnoreCase))
                {

                    string value = Config.Instance.GetValue(
                        (lazylog.Category)Enum.Parse(typeof(lazylog.Category), Category)
                        , (lazylog.Key)Enum.Parse(typeof(lazylog.Key), Key)
                        );
                    configSetting.Value = value;
                }
                else if (ConfigFile.Equals("LogClientConfig.txt", StringComparison.OrdinalIgnoreCase))
                {
                    string value = LogClient.Config.Instance.GetValue(
                        (LogClient.Category)Enum.Parse(typeof(LogClient.Category), Category)
                        , (LogClient.Key)Enum.Parse(typeof(LogClient.Key), Key)
                        );
                    configSetting.Value = value;
                }
                else
                {
                    log.Error($@"ConfigRead Error ConfigFile : {ConfigFile}, Category : {Category}, Key : {Key}, Value : {Value}");
                    wcfResponse.IsSuccess = false;
                    wcfResponse.ResultMessage = "";
                    wcfResponse.ErrorMessage = "Unknown Config File";
                    return wcfResponse;
                }
                JToken jt = JToken.Parse(JsonConvert.SerializeObject(configSetting));
                string resultMessage = jt.ToString(Newtonsoft.Json.Formatting.Indented);

                wcfResponse.IsSuccess = true;
                wcfResponse.ResultMessage = resultMessage;
                wcfResponse.ErrorMessage = "";
                log.Warn($@"ConfigRead Completed ConfigFile : {ConfigFile}, Category : {Category}, Key : {Key}, Value : {Value}");
                return wcfResponse;
            }
            catch (Exception ex)
            {
                log.Error($@"ConfigRead Error ConfigFile : {ConfigFile}, Category : {Category}, Key : {Key}, {ex.Message}, {ex.StackTrace}");
                wcfResponse.IsSuccess = false;
                wcfResponse.ResultMessage = "";
                wcfResponse.ErrorMessage = ex.Message;
                return wcfResponse;
            }

        }

        private WcfRestServer.WcfResponse FirstKeySetting(string cmdText)
        {
            try
            {
                if ((logClientConfig.GetValue(LogClient.Category.Api, LogClient.Key.AccessKey).Length == 0)
                    && (logClientConfig.GetValue(LogClient.Category.Api, LogClient.Key.SecretKey).Length == 0))
                {
                    return ChangeKeySetting(cmdText);
                }
                else
                {
                    wcfResponse.IsSuccess = false;
                    wcfResponse.ResultMessage = "";
                    wcfResponse.ErrorMessage = "The key has already been set and can not be changed.";
                    return wcfResponse;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private WcfRestServer.WcfResponse ChangeKeySetting(string cmdText)
        {
            try
            {
                string plainCmdText = cmdText;
                string[] plainCmdTextArray = plainCmdText.Split(new string[] { ":::" }, StringSplitOptions.None);
                logClientConfig.SetValue(LogClient.Category.Api, LogClient.Key.AccessKey, plainCmdTextArray[0]);
                logClientConfig.SetValue(LogClient.Category.Api, LogClient.Key.SecretKey, plainCmdTextArray[1]);
                logClientConfig.SaveLogClientData();

                wcfResponse.IsSuccess = true;
                wcfResponse.ResultMessage = "SecretKey Setting Completed.";
                wcfResponse.ErrorMessage = "";
                return wcfResponse;
            }
            catch (Exception ex)
            {
                log.Warn($"ChangeKeySettring error : {ex.Message}, {ex.StackTrace}");
                throw;
            }
        }


        private WcfRestServer.WcfResponse SqlIdPassSetting(string cmdText)
        {
            try
            {
                string plainCmdText = cmdText;
                TypeSqlIdPassSetting typeSqlIdPassSetting = JsonConvert.DeserializeObject<TypeSqlIdPassSetting>(plainCmdText);

                logClientConfig.SetValue(LogClient.Category.Repository, LogClient.Key.SqlId, typeSqlIdPassSetting.SqlId);
                logClientConfig.SetValue(LogClient.Category.Repository, LogClient.Key.SqlEncryptedPassword, typeSqlIdPassSetting.SqlEncryptedPassword);
                logClientConfig.SetValue(LogClient.Category.Repository, LogClient.Key.SqlDataSource, typeSqlIdPassSetting.SqlDataSource);
                logClientConfig.SetValue(LogClient.Category.Repository, LogClient.Key.SqlConnectTimeout, typeSqlIdPassSetting.SqlConnectTimeout);
                logClientConfig.SaveLogClientData();
                logClientConfig.PasswordChanged();

                wcfResponse.IsSuccess = true;
                wcfResponse.ResultMessage = "SqlId Pass Setting Completed.";
                wcfResponse.ErrorMessage = "";
                return wcfResponse;
            }
            catch (Exception ex)
            {
                log.Error($"SqlIdPassSetting error : {ex.Message}, {ex.StackTrace}");
                throw;
            }
        }
    }
}
