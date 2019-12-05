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

namespace lazylog.Backup
{
    public class LogBackup : IManager
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
                    HaCompleteCheckYN = config.GetValue(Category.Backup, Key.FullBackupHaCompleteCheckYN);
                    Path = config.GetValue(Category.Backup, Key.Path);
                    maxtransfersize = config.GetValue(Category.Backup, Key.maxtransfersize);
                    buffercount = config.GetValue(Category.Backup, Key.buffercount);
                    compressionYN = config.GetValue(Category.Backup, Key.compressionYN);
                    bucketName = config.GetValue(Category.Backup, Key.BucketName);

                    if (!int.TryParse(config.GetValue(Category.Backup, Key.LogBackupIntervalSec), out int RunIntervalSec))
                        RunIntervalSec = 60 * 60 * 1;

                    if (!int.TryParse(config.GetValue(Category.Backup, Key.PurgeObjectLimitSec), out int PurgeObjectLimitSec))
                        PurgeObjectLimitSec = 60 * 60 * 24 * 3;

                    DateTime endTime = DateTime.Now.Add(new TimeSpan(0, 0, RunIntervalSec));

                    if (RunIntervalSec == 0)
                    {
                        IsRunning = false;
                        log.Warn("backupmon (log backup) can't start because LogBackupIntervalSec is 0");
                        break;
                    }
                    else
                    {
                        targets.Clear();
                        GetData();
                        if (!IsRunning) break;
                        ExecuteBackup();
                        if (!IsRunning) break;
                        if (PurgeObjectLimitSec > 0)
                            CopyBackup2ObjectStorage();
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

        public void Stop()
        {
            log.Warn("log backup stop");
            IsRunning = false;
            log.Warn("log backup stoped");
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
                    string localFileFullname = Path  +@"\"+ a.BACKUP_FILE_NAME;
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
            log.Warn("get log backup target");
            using (SqlConnection conn = new SqlConnection(config.GetConnectionString(InitialCatalog.Master)))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = logBackupTargetQuery;
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
            log.Warn("log backup Execute");

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
                        string filename = $"{a.SERVERNAME.Replace(@"/", "_")}__{a.DATABASE_NAME}__{DateTime.Now.ToString("yyyyMMddHHmmss")}.log";
                        string query = $@"backup log [{a.DATABASE_NAME}] to disk = '{Path}\{filename}' with init";
                        log.Warn(query);
                        a.BACKUP_FILE_NAME = filename;
                        cmd.CommandText = query;
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandTimeout = 60 * 60 * 1;
                        cmd.ExecuteNonQuery();
                    }
                    conn.Close();
                }
                if (!IsRunning) break;
            }
        }

        private string logBackupTargetQuery =
@"
SET NOCOUNT ON
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED 

--DECLARE @HACHECKYN VARCHAR(1)
--SET @HACHECKYN = 'Y'

IF (@HACHECKYN = 'Y')
BEGIN 
	SELECT 
		lower(@@servername) SERVERNAME
		, B.NAME DATABASE_NAME
	FROM 
		( 
		SELECT DISTINCT DATABASE_NAME 
		FROM MSDB.DBO.BACKUPSET 
		WHERE RECOVERY_MODEL = 'FULL'
		AND BACKUP_FINISH_DATE > GETDATE() -5
		) A 
		JOIN SYS.DATABASES B
		ON A.DATABASE_NAME = B.NAME
		JOIN SYS.DATABASE_MIRRORING C
		ON B.NAME = DB_NAME(C.DATABASE_ID)
	WHERE 
		NAME NOT IN ('TEMPDB', 'MODEL', 'LAZYLOG','MSDB', 'MASTER')
		AND HAS_DBACCESS(DB_NAME(C.DATABASE_ID)) = 1 
		AND C.MIRRORING_STATE_DESC IS NOT NULL
END
ELSE 
BEGIN 
	SELECT 
		lower(@@servername) SERVERNAME
		, B.NAME DATABASE_NAME
	FROM 
		( 
		SELECT DISTINCT DATABASE_NAME 
		FROM MSDB.DBO.BACKUPSET 
		WHERE RECOVERY_MODEL = 'FULL'
		AND BACKUP_FINISH_DATE > GETDATE() -5
		) A 
		JOIN SYS.DATABASES B
		ON A.DATABASE_NAME = B.NAME
		JOIN SYS.DATABASE_MIRRORING C
		ON B.NAME = DB_NAME(C.DATABASE_ID)
	WHERE 
		NAME NOT IN ('TEMPDB', 'MODEL', 'LAZYLOG','MSDB', 'MASTER')
		AND HAS_DBACCESS(DB_NAME(C.DATABASE_ID)) = 1 
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
