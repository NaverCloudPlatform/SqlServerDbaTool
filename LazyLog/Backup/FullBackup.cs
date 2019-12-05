using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogClient;
using System.Data;
using System.Threading;
using CsLib;
using System.IO;
using System.Globalization;

namespace lazylog.Backup
{
    public class FullBackup : IManager
    {
        Log log = Log.Instance;
        Config config = Config.Instance;
        List<Target> targets = new List<Target>();
        string IntervalSec = string.Empty;
        string HaCompleteCheckYN = string.Empty;
        string Path = string.Empty;
        string maxtransfersize = "";
        string buffercount = "";
        string compressionYN = string.Empty;
        string currentBackupFileName = string.Empty;
        string bucketName = string.Empty;

        public bool IsRunning { get; set; }

        public async void Start()
        {
            IsRunning = true;
            var task = Task.Delay(3000);
            await task;

            while (IsRunning)
            {
                try
                {
                    IntervalSec = config.GetValue(Category.Backup, Key.FullBackupIntervalSec);
                    HaCompleteCheckYN = config.GetValue(Category.Backup, Key.FullBackupHaCompleteCheckYN);
                    Path = config.GetValue(Category.Backup, Key.Path);
                    maxtransfersize = config.GetValue(Category.Backup, Key.maxtransfersize);
                    buffercount = config.GetValue(Category.Backup, Key.buffercount);
                    compressionYN = config.GetValue(Category.Backup, Key.compressionYN);
                    bucketName = config.GetValue(Category.Backup, Key.BucketName);

                    if (!int.TryParse(config.GetValue(Category.Backup, Key.PurgeLocalLimitSec), out int PurgeLocalLimitSec))
                        PurgeLocalLimitSec = 60 * 60 * 24 * 3;
                    if (!int.TryParse(config.GetValue(Category.Backup, Key.PurgeObjectLimitSec), out int PurgeObjectLimitSec))
                        PurgeObjectLimitSec = 60 * 60 * 24 * 3;
                    if (!int.TryParse(config.GetValue(Category.Backup, Key.FullBackupIntervalSec), out int RunIntervalSec))
                        RunIntervalSec = 60 * 60 * 24;

                    DateTime endTime = DateTime.Now.Add(new TimeSpan(0, 0, RunIntervalSec));

                    if (RunIntervalSec == 0)
                    {
                        IsRunning = false;
                        log.Warn("backupmon (full backup) can't start because FullBackupIntervalSec is 0");
                        break;
                    }
                    else
                    {
                        targets.Clear();
                        PurgeLocal(Path, PurgeLocalLimitSec);
                        if (!IsRunning) break;
                        if (PurgeObjectLimitSec > 0)
                            await PurgeObjectStorage($"backup/{config.GetServerName()}", PurgeObjectLimitSec);
                        if (!IsRunning) break;
                        GetData();
                        if (!IsRunning) break;
                        ExecuteBackup();
                        if (!IsRunning) break;
                        if (PurgeObjectLimitSec > 0)
                            CopyBackup2ObjectStorage();
                        if (!IsRunning) break;
                        while (DateTime.Now < endTime)
                        {
                            if (!IsRunning) break;
                            var sleepTask = Task.Delay(2000);
                            await sleepTask;
                        }
                    }
                }
                catch (Exception ex)
                {
                    log.Error(string.Format("{0}, {1}", ex.Message, ex.StackTrace));
                }
            }
        }

        private async Task PurgeObjectStorage(string path, int purgeLimitSec)
        {

            ObjectStorage o = new ObjectStorage(
                LogClient.Config.Instance.GetValue(LogClient.Category.Api, LogClient.Key.AccessKey)
                , LogClient.Config.Instance.GetValue(LogClient.Category.Api, LogClient.Key.SecretKey)
                , config.GetValue(Category.Backup, Key.ObjectStorageServiceUrl)
                );

            var task = o.List(bucketName, path);
            var files = await task;
            foreach (var file in files)
            {
                DateTime.TryParseExact(file.LastWriteTime, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date);
                string filenameWithExtention = file.Name.Replace(".full", "");
                string filename = filenameWithExtention.Replace(".log", "");
                string[] splitfilename = filename.Split(new string[] { "__" }, StringSplitOptions.None);
                if (splitfilename.Count() == 3)
                {
                    if (date < DateTime.Now.Add(new TimeSpan(0, 0, Math.Abs(purgeLimitSec) * -1)))
                    {
                        var task2 = o.DeleteFileAsync(bucketName, file.Name);
                        await task2;
                    }
                }
            }
        }

        private void PurgeLocal(string path, int purgeLimitSec)
        {

            try
            {

                if (path.Substring(path.Length - 1).Equals(@"\"))
                    path = path.Substring(0, Path.Length - 1);

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                string[] files = Directory.GetFiles(path);
                foreach (var file in files)
                {
                    FileInfo f = new FileInfo(file);
                    string extention = f.Extension;
                    string filenameWithExtention = f.Name;
                    filenameWithExtention = filenameWithExtention.Replace(".full", "");
                    string filename = filenameWithExtention.Replace(".log", "");
                    string[] splitfilename = filename.Split(new string[] { "__" }, StringSplitOptions.None);
                    if (splitfilename.Count() == 3)
                    {
                        if (long.TryParse(splitfilename[2], out long backupTime))
                        {
                            if (f.CreationTime < DateTime.Now.Add(new TimeSpan(0, 0, Math.Abs(purgeLimitSec) * -1))
                                && (extention.Equals(".full", StringComparison.OrdinalIgnoreCase) || extention.Equals(".log", StringComparison.OrdinalIgnoreCase)))
                                f.Delete();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error($"PurgeLocal exception info : {ex.Message}, {ex.StackTrace}");
            }
        }

        public void Stop()
        {
            log.Warn("full backup stop");
            IsRunning = false;
            log.Warn("full backup stoped");
        }

        public void CopyBackup2ObjectStorage()
        {
            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
            try
            {
                if (Path.Substring(Path.Length - 1).Equals(@"\"))
                    Path = Path.Substring(0, Path.Length - 1);

                if (!Directory.Exists(Path))
                    Directory.CreateDirectory(Path);

                foreach (var a in targets)
                {
                    try
                    {
                        string localFileFullname = Path + @"\" + a.BACKUP_FILE_NAME;
                        string remoteFileFullname = $@"backup/{config.GetServerName()}/" + a.BACKUP_FILE_NAME;
                        currentBackupFileName = localFileFullname;
                        if (!int.TryParse(config.GetValue(Category.Backup, Key.ObjectStorageBandWidthThrottleSleepMiliSec), out int sleepMiliSec))
                            sleepMiliSec = 0;

                        ObjectStorage o = new ObjectStorage(
                            LogClient.Config.Instance.GetValue(LogClient.Category.Api, LogClient.Key.AccessKey)
                            , LogClient.Config.Instance.GetValue(LogClient.Category.Api, LogClient.Key.SecretKey)
                            , config.GetValue(Category.Backup, Key.ObjectStorageServiceUrl)
                            );
                        o.UploadProgressEvent += ProgressLog;
                        o.UploadObjectAsync(bucketName, localFileFullname, remoteFileFullname, cancelTokenSource.Token, sleepMiliSec).Wait();
                        if (!IsRunning) break;
                    }
                    catch (Exception ex)
                    {
                        log.Warn($"objectStorage Upload Error filename : {currentBackupFileName}, exception info : {ex.Message}, {ex.StackTrace}");
                    }
                }
            }
            catch (Exception ex)
            {
                log.Warn($"objectStorage Upload Error filename : {currentBackupFileName}, exception info : {ex.Message}, {ex.StackTrace}");
            }
            finally
            {
                log.Warn($"objectStorage Upload Completed filename : {currentBackupFileName}");
            }
        }

        public void ProgressLog(object sender, S3ProgressArgs e)
        {
            log.Warn($"objectStorage Upload Progress Reported filename : {currentBackupFileName}, { e.TransferredBytes} / { e.TotalBytes}, { e.PercentDone} %");
        }

        public void GetData()
        {
            log.Warn("get fullbackup target");
            using (SqlConnection conn = new SqlConnection(config.GetConnectionString(InitialCatalog.Master)))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = fullBackupTargetQuery;
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("@HACHECKYN", SqlDbType.VarChar, 1).Value = HaCompleteCheckYN;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        targets.Add
                            (
                                new Target
                                {
                                    SERVERNAME = config.DatabaseValue<string>(reader["SERVERNAME"]),
                                    DATABASE_NAME = config.DatabaseValue<string>(reader["DATABASE_NAME"])
                                }
                            );
                        if (!IsRunning) break;
                    }
                }
                conn.Close();
            }
        }

        public void ExecuteBackup()
        {
            log.Warn("Fullbackup Execute");
            if (Path.Substring(Path.Length - 1).Equals(@"\"))
                Path = Path.Substring(0, Path.Length - 1);

            if (!Directory.Exists(Path))
                Directory.CreateDirectory(Path);

            foreach (var a in targets)
            {
                using (SqlConnection conn = new SqlConnection(config.GetConnectionString(InitialCatalog.Master)))
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        string stringmaxtransfersize = (maxtransfersize.Equals("0") || maxtransfersize.Equals("")) ? "" : $", MAXTRANSFERSIZE = {maxtransfersize}";
                        string stringbuffercount = (buffercount.Equals("0") || buffercount.Equals("")) ? "" : $", BUFFERCOUNT = {buffercount}";
                        string stringcompression = (!compressionYN.Equals("Y", StringComparison.OrdinalIgnoreCase)) ? "" : ", compression";
                        string option = stringcompression + stringmaxtransfersize + stringbuffercount;
                        string filename = $"{a.SERVERNAME.Replace(@"/", "-")}__{a.DATABASE_NAME}__{DateTime.Now.ToString("yyyyMMddHHmmss")}.full";
                        string query = $@"backup database [{a.DATABASE_NAME}] to disk = '{Path}/{filename}' with init{option}";
                        log.Warn(query);
                        a.BACKUP_FILE_NAME = filename;
                        cmd.CommandText = query;
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandTimeout = 60 * 60 * 24;
                        cmd.ExecuteNonQuery();
                    }
                    conn.Close();
                }
                if (!IsRunning) break;
            }
        }

        private string fullBackupTargetQuery =
@"
SET NOCOUNT ON
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED 

--DECLARE @HACHECKYN VARCHAR(1)
--SET @HACHECKYN = 'Y'

IF (@HACHECKYN = 'Y')
BEGIN 
	SELECT  
		lower(@@servername) SERVERNAME
		,DB_NAME(SD.[DATABASE_ID]) DATABASE_NAME
		,HAS_DBACCESS(DB_NAME(SD.[DATABASE_ID])) HAS_DBACCESS_STATE
		,SD.MIRRORING_STATE_DESC            
		,B.RECOVERY_MODEL_DESC
	FROM SYS.DATABASE_MIRRORING AS SD
		JOIN SYS.DATABASES B
		ON DB_NAME(SD.DATABASE_ID) = B.NAME 
	WHERE DB_NAME(SD.DATABASE_ID) NOT IN ('TEMPDB', 'MODEL', 'lazylog') 
		AND B.RECOVERY_MODEL_DESC = 'FULL'
		AND SD.MIRRORING_STATE_DESC IS NOT NULL
		AND HAS_DBACCESS(DB_NAME(SD.[DATABASE_ID])) = 1 
	UNION
	SELECT  
		lower(@@servername) SERVERNAME
		,DB_NAME(SD.[DATABASE_ID]) DATABASE_NAME
		,HAS_DBACCESS(DB_NAME(SD.[DATABASE_ID])) HAS_DBACCESS_STATE
		,SD.MIRRORING_STATE_DESC            
		,B.RECOVERY_MODEL_DESC
	FROM SYS.DATABASE_MIRRORING AS SD
		JOIN SYS.DATABASES B
		ON DB_NAME(SD.DATABASE_ID) = B.NAME 
	WHERE DB_NAME(SD.DATABASE_ID) NOT IN ('TEMPDB', 'MODEL', 'lazylog') 
		AND B.RECOVERY_MODEL_DESC <> 'FULL'
		AND HAS_DBACCESS(DB_NAME(SD.[DATABASE_ID])) = 1 
END 
ELSE
BEGIN 
	SELECT  
		lower(@@servername) SERVERNAME
		,DB_NAME(SD.[DATABASE_ID]) DATABASE_NAME
		,HAS_DBACCESS(DB_NAME(SD.[DATABASE_ID])) HAS_DBACCESS_STATE
		,SD.MIRRORING_STATE_DESC            
		,B.RECOVERY_MODEL_DESC
	FROM SYS.DATABASE_MIRRORING AS SD
		JOIN SYS.DATABASES B
		ON DB_NAME(SD.DATABASE_ID) = B.NAME 
	WHERE DB_NAME(SD.DATABASE_ID) NOT IN ('TEMPDB', 'MODEL', 'lazylog') 
		AND B.RECOVERY_MODEL_DESC = 'FULL'
		AND HAS_DBACCESS(DB_NAME(SD.[DATABASE_ID])) = 1 
	UNION
	SELECT  
		lower(@@servername) SERVERNAME
		,DB_NAME(SD.[DATABASE_ID]) DATABASE_NAME
		,HAS_DBACCESS(DB_NAME(SD.[DATABASE_ID])) HAS_DBACCESS_STATE
		,SD.MIRRORING_STATE_DESC            
		,B.RECOVERY_MODEL_DESC
	FROM SYS.DATABASE_MIRRORING AS SD
		JOIN SYS.DATABASES B
		ON DB_NAME(SD.DATABASE_ID) = B.NAME 
	WHERE DB_NAME(SD.DATABASE_ID) NOT IN ('TEMPDB', 'MODEL', 'lazylog') 
		AND B.RECOVERY_MODEL_DESC <> 'FULL'
		AND HAS_DBACCESS(DB_NAME(SD.[DATABASE_ID])) = 1 
END 
";
        public class Target
        {
            public string SERVERNAME { get; set; }
            public string DATABASE_NAME { get; set; }
            public string BACKUP_FILE_NAME { get; set; }
        }
    }
}
