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


namespace lazylog
{
    class ExecuterRest
    {
        Config config = Config.Instance; 
        string logFilename = string.Empty;
        static readonly string logDirectory = "ExecuteLogRest";
        static string logDirectoryFullname = string.Empty; 
        string logFileFullname = string.Empty;
        string cmdType = string.Empty;
        WcfRestServer.WcfResponse wcfResponse = new WcfRestServer.WcfResponse();

        public ExecuterRest(string cmdType)
        {
            this.cmdType = cmdType; 
            logFilename = DateTime.Now.ToString("yyyyMMddHHmmss")+ "_" + Guid.NewGuid()+".log";
            logDirectoryFullname = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, logDirectory);
            logFileFullname = Path.Combine(logDirectoryFullname, logFilename);
            LogDirectoryCheckAndCreate();
        }
        
        public WcfRestServer.WcfResponse Execute (string cmdText)
        {
            try
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

                Common.FileLogWriteLine(logFileFullname, $"cmdType : {cmdType}");
                Common.FileLogWriteLine(logFileFullname, $"cmdText : {cmdText}");

                if (cmdType.Equals("TypeMonController",StringComparison.OrdinalIgnoreCase))
                {
                    var typeMonController = JsonConvert.DeserializeObject<TypeMonController>(cmdText);
                    if (typeMonController.MonName == null || typeMonController.MonName.Trim().Equals(""))
                    {
                        wcfResponse.IsSuccess = false;
                        wcfResponse.ResultMessage = "";
                        wcfResponse.ErrorMessage = "TypeMissMatch or MonName is empty";
                        return wcfResponse;
                    }
                    MonManager(typeMonController.MonName, typeMonController.StopStart);
                }

                if (cmdType.Equals("TypeRestTest", StringComparison.OrdinalIgnoreCase))
                {
                    try
                    {
                        var typeRestTest = JsonConvert.DeserializeObject<TypeRestTest>(cmdText);
                        wcfResponse.IsSuccess = true;
                        wcfResponse.ResultMessage = $"para1 :{typeRestTest.para1}, para2 : {typeRestTest.para2}";
                        wcfResponse.ErrorMessage = "";
                        return wcfResponse;
                    }
                    catch (Exception ex)
                    {
                        wcfResponse.IsSuccess = false; 
                        wcfResponse.ResultMessage = "";
                        wcfResponse.ErrorMessage = ex.Message;
                        return wcfResponse;
                    }
                }

                if (cmdType.Equals("TypeObjectStorage", StringComparison.OrdinalIgnoreCase))
                {
                    var typeObjectStorage = JsonConvert.DeserializeObject<TypeObjectStorage>(cmdText);
                    if (typeObjectStorage.LocalFileFullname == null || typeObjectStorage.LocalFileFullname.Trim().Equals(""))
                    {
                        wcfResponse.IsSuccess = false;
                        wcfResponse.ResultMessage = "";
                        wcfResponse.ErrorMessage = "TypeMissMatch or LocalFileFullname is empty";
                        return wcfResponse;
                    }

                    if (typeObjectStorage.UploadDownload.Equals("Upload", StringComparison.OrdinalIgnoreCase))
                        new Thread(
                            () => ObjectUpload(LogClient.Config.Instance.GetValue(LogClient.Category.Api, LogClient.Key.AccessKey)
                            , LogClient.Config.Instance.GetValue(LogClient.Category.Api, LogClient.Key.SecretKey)
                            , typeObjectStorage.ServiceUrl
                            , typeObjectStorage.BucketName
                            , typeObjectStorage.LocalFileFullname
                            , typeObjectStorage.RemoteFileFullname)
                        ).Start();

                    if (typeObjectStorage.UploadDownload.Equals("Download", StringComparison.OrdinalIgnoreCase))
                        new Thread(
                            () => ObjectDownload(LogClient.Config.Instance.GetValue(LogClient.Category.Api, LogClient.Key.AccessKey)
                            , LogClient.Config.Instance.GetValue(LogClient.Category.Api, LogClient.Key.SecretKey)
                            , typeObjectStorage.ServiceUrl
                            , typeObjectStorage.BucketName
                            , typeObjectStorage.LocalFileFullname
                            , typeObjectStorage.RemoteFileFullname)
                        ).Start();
                }
                
                wcfResponse.IsSuccess = true;
                if (wcfResponse.ResultMessage == null || wcfResponse.ResultMessage.Trim().Equals(""))
                    wcfResponse.ResultMessage = logFileFullname ; 
            }
            catch (Exception ex)
            {
                wcfResponse.IsSuccess = false;
                wcfResponse.ErrorMessage = ex.Message; // exception message 
            }

            Common.FileLogWriteLine(logFileFullname, $"wcfResponse : {JsonConvert.SerializeObject(wcfResponse)}");
            return wcfResponse;
        }
        
        private void ObjectUpload(string accessKey, string secretKey, string serviceUrl, string bucketName, string localFileFullname, string remoteFileFullname)
        {
            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
            try
            {
                string filename = Path.GetFileName(localFileFullname);
                ObjectStorage o = new ObjectStorage(accessKey, secretKey, serviceUrl);
                o.UploadProgressEvent += ProgressBar;
                o.UploadObjectAsync(bucketName, localFileFullname, remoteFileFullname, cancelTokenSource.Token, 0).Wait();
            }
            catch (Exception ex)
            {
                Common.FileLogWriteLine(logFileFullname, $"exception info : {ex.Message}, {ex.StackTrace}");
            }
            finally
            {
                Common.FileLogWriteLine(logFileFullname, "async cmd completed");
            }
        }

        private void ObjectDownload(string accessKey, string secretKey, string serviceUrl, string bucketName, string localFileFullname, string remoteFileFullname)
        {
            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
            try
            {
                string filename = Path.GetFileName(localFileFullname);
                ObjectStorage o = new ObjectStorage(accessKey, secretKey, serviceUrl);
                o.UploadProgressEvent += ProgressBar;
                o.DownloadObjectAsync(bucketName, localFileFullname, remoteFileFullname, cancelTokenSource.Token, 0).Wait();
            }
            catch (Exception ex)
            {
                Common.FileLogWriteLine(logFileFullname, $"exception info : {ex.Message}, {ex.StackTrace}");
            }
            finally
            {
                Common.FileLogWriteLine(logFileFullname, "async cmd completed");
            }
        }

        private void ProgressBar(object sender, S3ProgressArgs e)
        {
            Common.FileLogWriteLine(logFileFullname, string.Format($"{e.TransferredBytes} / {e.TotalBytes}, {e.PercentDone} %"));
        }
        
        private void MonManager(string MonName, string StopStart)
        {
            try
            {
                bool isMonExists = false;
                foreach (var a in Service1.appInstances)
                {
                    if (a.Key.Equals(MonName, StringComparison.OrdinalIgnoreCase))
                        isMonExists = true;
                }
                if (isMonExists)
                {
                    foreach (var a in Service1.appInstances)
                    {
                        if (a.Key.Equals(MonName, StringComparison.OrdinalIgnoreCase))
                        {
                            if (StopStart.Equals("Stop", StringComparison.OrdinalIgnoreCase))
                                ((IManager)a.Value).Stop();
                            if (StopStart.Equals("Start", StringComparison.OrdinalIgnoreCase))
                                ((IManager)a.Value).Start();
                        }
                    }
                    wcfResponse.IsSuccess = true;
                    wcfResponse.ResultMessage = $"MonName : { MonName}, {StopStart} completed";
                }
                else
                {
                    wcfResponse.IsSuccess = false;
                    wcfResponse.ResultMessage = $"MonName : [{MonName}] does not exist ";
                }
            }
            catch (Exception ex)
            {
                wcfResponse.IsSuccess = false;
                wcfResponse.ResultMessage = ex.Message;
            }
        }
        
        private void LogDirectoryCheckAndCreate()
        {
            if (!Directory.Exists(logDirectoryFullname))
                Directory.CreateDirectory(logDirectoryFullname);
            Common.FileDeleteFromDate(logDirectoryFullname, "*.log", new TimeSpan(24 * 7, 0, 0));
        }

    }
}
