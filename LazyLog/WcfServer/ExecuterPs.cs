using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Collections.ObjectModel;
using LogClient;

namespace lazylog
{
    class ExecuterPs
    {
        Log log = Log.Instance;
        WcfRestServer.WcfResponse wcfResponse = new WcfRestServer.WcfResponse();
        string cmdType = string.Empty;
        string cmdText = string.Empty;
        public ExecuterPs(string cmdType)
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

        public WcfRestServer.WcfResponse Execute(string cmdText)
        {
            if (cmdType == null || cmdType.Trim().Equals(""))
            {
                wcfResponse.IsSuccess = false;
                wcfResponse.ResultMessage = "";
                wcfResponse.ErrorMessage = "cmdType is empty";
                return wcfResponse;
            }
            if (cmdText == null || cmdText.Trim().Equals(""))
            {
                wcfResponse.IsSuccess = false;
                wcfResponse.ResultMessage = "";
                wcfResponse.ErrorMessage = "cmdText is empty";
                return wcfResponse;
            }

            if (cmdType.Equals("Out-String", StringComparison.OrdinalIgnoreCase))
                PowerShellOutString(cmdText);
            else
                PowerShellRunScript(cmdText);

            return wcfResponse;
        }


        private void PowerShellOutString(string PowerShellScript)
        {
            wcfResponse.IsSuccess = false;
            try
            {
                using (Runspace runspace = RunspaceFactory.CreateRunspace())
                {
                    runspace.Open();
                    Pipeline pipeline = runspace.CreatePipeline();
                    pipeline.Commands.AddScript(PowerShellScript);
                    pipeline.Commands.Add("Out-String");
                    Collection<PSObject> results = pipeline.Invoke();
                    StringBuilder sb = new StringBuilder();

                    foreach (PSObject obj in results)
                    {
                        sb.AppendLine(obj.ToString());
                    }
                    wcfResponse.ResultMessage = sb.ToString();
                    wcfResponse.IsSuccess = true;

                    if (!HasCriticalString(PowerShellScript))
                    {
                        log.Warn(string.Format(
                            "PowerShell Script : {0}, IsSuccess : {1}, ResultMessage : {2}"
                            , PowerShellScript
                            , wcfResponse.IsSuccess
                            , wcfResponse.ResultMessage
                            ));
                    }
                    else
                        log.Warn(string.Format("string has critical word, skipped log."));

                    sb.Clear();
                    if (pipeline.HadErrors)
                    {
                        wcfResponse.IsSuccess = false;
                        var errors = pipeline.Error.ReadToEnd();
                        foreach (var error in errors)
                        {
                            sb.AppendLine(error.ToString());
                        }
                        wcfResponse.ErrorMessage = sb.ToString();
                        wcfResponse.IsSuccess = false;

                        if (!HasCriticalString(PowerShellScript))
                        {
                            log.Error(string.Format(
                                "PowerShell Script : {0}, IsSuccess : {1}, ErrorMessage : {2}"
                                , PowerShellScript
                                , wcfResponse.IsSuccess
                                , wcfResponse.ErrorMessage
                                ));
                        }
                        else
                            log.Warn(string.Format("string has critical word, skipped log."));
                    }
                }
            }
            catch (Exception ex)
            {
                if (!HasCriticalString(PowerShellScript))
                {
                    log.Error(string.Format("{0}, {1}, {2}", ex.Message, ex.StackTrace, PowerShellScript));
                }
                else
                    log.Warn(string.Format("string has critical word, skipped log."));

                if (wcfResponse.ErrorMessage == null || wcfResponse.ErrorMessage.Trim() == "")
                    wcfResponse.ErrorMessage = ex.Message;
            }

        }

        private void PowerShellRunScript(string PowerShellScript)
        {
            wcfResponse.IsSuccess = false;
            try
            {
                using (PowerShell PowerShellInstance = PowerShell.Create())
                {
                    PowerShellInstance.AddScript(PowerShellScript);
                    Collection<PSObject> PSOutput = PowerShellInstance.Invoke();

                    StringBuilder sb = new StringBuilder();

                    foreach (PSObject outputItem in PSOutput)
                    {
                        if (outputItem != null)
                        {
                            sb.AppendLine(outputItem.BaseObject.ToString());
                        }
                    }

                    wcfResponse.ResultMessage = sb.ToString();
                    wcfResponse.IsSuccess = true;

                    if (!HasCriticalString(PowerShellScript))
                    {
                        log.Warn(string.Format(
                         "PowerShell Script : {0}, IsSuccess : {1}, ResultMessage : {2}"
                        , PowerShellScript
                        , wcfResponse.IsSuccess
                        , wcfResponse.ResultMessage
                        ));
                    }
                    else
                        log.Warn(string.Format("string has critical word, skipped log."));

                    sb.Clear();

                    if (PowerShellInstance.Streams.Error.Count > 0)
                    {
                        wcfResponse.IsSuccess = false;
                        foreach (var a in PowerShellInstance.Streams.Error)
                        {
                            sb.AppendLine(a.Exception.ToString());
                        }
                        wcfResponse.ErrorMessage = sb.ToString();
                        wcfResponse.IsSuccess = false;

                        if (!HasCriticalString(PowerShellScript))
                        {
                            log.Warn(string.Format(
                            "PowerShell Script : {0}, IsSuccess : {1}, ErrorMessage : {2}"
                            , PowerShellScript
                            , wcfResponse.IsSuccess
                            , wcfResponse.ErrorMessage
                            ));
                        }
                        else
                            log.Warn(string.Format("string has critical word, skipped log."));
                    }
                }
            }
            catch (Exception ex)
            {
                if (!HasCriticalString(PowerShellScript))
                {
                    log.Error(string.Format("{0}, {1}, {2}", ex.Message, ex.StackTrace, PowerShellScript));
                }
                else
                    log.Warn(string.Format("string has critical word, skipped log."));

                if (wcfResponse.ErrorMessage == null || wcfResponse.ErrorMessage.Trim() == "")
                    wcfResponse.ErrorMessage = ex.Message;
            }

        }
    }
}
