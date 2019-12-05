using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Newtonsoft.Json;
using System.Threading;
using System.IO;
using Amazon.Runtime;


namespace CsLib
{
    public class ObjectStorage
    {
        public event EventHandler<S3ProgressArgs> UploadProgressEvent;

        public string AccessKey { get; set; }
        public string SecretKey { get; set; }
        public string ServiceUrl { get; set; }
        private long CurrentTransferredBytes { get; set; }
        private long ContentTotalBytes { get; set; }
        private long PreviousReportBytes { get; set; } = 0;

        private long ProgressUpdateInterval = 20 * (int)Math.Pow(2, 20);

        public ObjectStorage() { }
        public ObjectStorage(string accessKey, string secretKey, string serviceUrl)
        {
            AccessKey = accessKey;
            SecretKey = secretKey;
            ServiceUrl = serviceUrl;
        }

        public async Task<bool> IsExistsBucket(string bucketName)
        {
            bool ReturnValue = false;

            if (string.IsNullOrEmpty(bucketName))
                throw new ArgumentNullException(nameof(bucketName), "bucketName is null");

            if (bucketName.Length < 1)
                throw new ArgumentOutOfRangeException(nameof(bucketName), "bucketName is blank");

            try
            {
                using (IAmazonS3 client = new AmazonS3Client(AccessKey, SecretKey, new AmazonS3Config
                {
                    ServiceURL = ServiceUrl
                    , Timeout = new TimeSpan(0, 0, 3)
                    , ReadWriteTimeout = new TimeSpan(0, 0, 3)
                }))
                {
                    var task = client.ListBucketsAsync();
                    ListBucketsResponse response = await task;
                    foreach (S3Bucket b in response.Buckets)
                    {
                        if (bucketName == b.BucketName)
                        {
                            ReturnValue = true;
                            break;
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return ReturnValue;
        }

        public bool CreateBucket(string bucketName)
        {
            bool ReturnValue = false;

            if (string.IsNullOrEmpty(bucketName))
                throw new ArgumentNullException(nameof(bucketName), "bucketName is null");

            if (bucketName.Length < 1)
                throw new ArgumentOutOfRangeException(nameof(bucketName), "bucketName is blank");

            try
            {
                using (IAmazonS3 client = new AmazonS3Client(AccessKey, SecretKey, new AmazonS3Config { ServiceURL = ServiceUrl }))
                {
                    PutBucketRequest request = new PutBucketRequest();
                    request.BucketName = bucketName;
                    client.PutBucket(request);
                }
                ReturnValue = true;
            }
            catch (AmazonS3Exception ex)
            {
                if (ex.ErrorCode == "BucketAlreadyOwnedByYou")
                    ReturnValue = true;
                throw ex;
            }
            catch (Exception)
            {
                throw;
            }
            return ReturnValue;
        }

        public async Task<bool> UploadObjectAsync(string bucketName, string localFullFilename, string key, CancellationToken token, int sleepMiliSec, bool publicReadTrueOrFalse = false)
        {
            bool bReturn = true;
            Task<bool> task;
            try
            {
                if (sleepMiliSec == 0)
                    task = UploadObjectFullThrottlingAsync(bucketName, localFullFilename, key, token, publicReadTrueOrFalse);
                else
                    task = UploadObjectBandWidthThrottlingAsync(bucketName, localFullFilename, key, token, sleepMiliSec, publicReadTrueOrFalse);

                bReturn = await task;
            }
            catch (Exception)
            {
                throw;
            }
            return bReturn;
        }

        public async Task PutACLAsync(string bucketName, string key, S3CannedACL s3CannedACL, CancellationToken token)
        {
            try
            {
                using (IAmazonS3 client = new AmazonS3Client(AccessKey, SecretKey, new AmazonS3Config
                {
                    ServiceURL = ServiceUrl
                }))
                {
                    Task task = client.PutACLAsync(new PutACLRequest
                    {
                        BucketName = bucketName,
                        Key = key,
                        CannedACL = s3CannedACL
                    }, token);
                    await task;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public PutACLResponse PutACL(string bucketName, string key, S3CannedACL s3CannedACL, CancellationToken token)
        {
            PutACLResponse putACLResponse;
            try
            {
                using (IAmazonS3 client = new AmazonS3Client(AccessKey, SecretKey, new AmazonS3Config
                {
                    ServiceURL = ServiceUrl
                }))
                {
                    putACLResponse = client.PutACL(new PutACLRequest
                    {
                        BucketName = bucketName,
                        Key = key,
                        CannedACL = s3CannedACL
                    });
                }
                return putACLResponse;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<bool> DeleteFileAsync(string bucketName, string key)
        {
            bool bReturn = false;
            try
            {
                using (IAmazonS3 s3Client = new AmazonS3Client(AccessKey, SecretKey, new AmazonS3Config
                {
                    ServiceURL = ServiceUrl,
                    Timeout = new TimeSpan(0, 0, 0, 30, 0)
                }))
                {
                    DeleteObjectsRequest deleteObjectsRequest = new DeleteObjectsRequest();
                    deleteObjectsRequest.BucketName = bucketName;
                    deleteObjectsRequest.AddKey(key);
                    var task = s3Client.DeleteObjectsAsync(deleteObjectsRequest);
                    var response = await task; 
                    var responseErrors = response.DeleteErrors;
                    foreach (var error in responseErrors)
                    {
                        throw new Exception($"object storage file delete error, filename : {error.Key}, message : {error.Message}");
                    }
                }
            }
            catch (Exception)
            {
                bReturn = false;
                throw;
            }
            return bReturn;
        }


        public async Task<bool> DownloadObjectAsync(string bucketName, string localFullFilename, string key, CancellationToken token, int sleepMiliSec = 0)
        {
            bool bReturn = true;
            try
            {
                using (IAmazonS3 s3Client = new AmazonS3Client(AccessKey, SecretKey, new AmazonS3Config
                {
                    ServiceURL = ServiceUrl,
                    BufferSize = 64 * (int)Math.Pow(2, 10),
                    ProgressUpdateInterval = this.ProgressUpdateInterval,
                    Timeout = new TimeSpan(1, 0, 0, 0, 0)
                }))
                {
                    //ProgressUpdateInterval setting error....why?
                    TransferUtility t = new TransferUtility(s3Client);
                    TransferUtilityDownloadRequest request = new TransferUtilityDownloadRequest
                    {
                        BucketName = bucketName,
                        Key = key,
                        FilePath = localFullFilename.Replace(@"/", @"\")
                    };
                    request.WriteObjectProgressEvent += ProgressEventCallback;
                    Task task = t.DownloadAsync(request, token);
                    await task;
                }
            }
            catch (Exception)
            {
                if (token.IsCancellationRequested)
                    bReturn = false;
                else
                    throw;
            }

            return bReturn;
        }

        public async Task<List<ObjectStorageFile>> List(string bucketName, string key = "")
        {
            List<ObjectStorageFile> objectStorageFiles = new List<ObjectStorageFile>();
            try
            {
                objectStorageFiles.Clear();
                using (IAmazonS3 s3Client = new AmazonS3Client(AccessKey, SecretKey, new AmazonS3Config
                {
                    ServiceURL = ServiceUrl,
                    BufferSize = 64 * (int)Math.Pow(2, 10),
                    ProgressUpdateInterval = 20 * (int)Math.Pow(2, 20),
                    Timeout = new TimeSpan(1, 0, 0, 0, 0)
                }))
                {
                    ListObjectsRequest request = new ListObjectsRequest
                    {
                        BucketName = bucketName,
                        MaxKeys = 2,
                        Prefix = key
                    };
                    do
                    {
                        ListObjectsResponse response = await s3Client.ListObjectsAsync(request);
                        foreach (var o in response.S3Objects)
                        {
                            objectStorageFiles.Add(
                                new ObjectStorageFile
                                {
                                    Name = o.Key,
                                    Length = o.Size,
                                    LastWriteTime = string.Format("{0:yyyy-MM-dd HH:mm:ss}", o.LastModified)
                                });
                        }
                        if (response.IsTruncated)
                        {
                            request.Marker = response.NextMarker;
                        }
                        else
                        {
                            request = null;
                        }
                    } while (request != null);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return objectStorageFiles;
        }



        private async Task<bool> UploadObjectFullThrottlingAsync(string bucketName, string localFullFilename, string key, CancellationToken token, bool publicReadTrueOrFalse = false)
        {


            bool bReturn = true;
            try
            {
                long contentLength = new FileInfo(localFullFilename).Length;
                using (IAmazonS3 s3Client = new AmazonS3Client(AccessKey, SecretKey, new AmazonS3Config
                {
                    ServiceURL = ServiceUrl,
                    BufferSize = 64 * (int)Math.Pow(2, 10),
                    ProgressUpdateInterval = this.ProgressUpdateInterval,
                    Timeout = new TimeSpan(1, 0, 0, 0, 0)
                }))
                {
                    TransferUtility t = new TransferUtility(s3Client);
                    TransferUtilityUploadRequest request = new TransferUtilityUploadRequest
                    {
                        BucketName = bucketName,
                        Key = key,
                        FilePath = localFullFilename,
                        PartSize = 50 * (long)Math.Pow(2, 20)
                    };
                    if (publicReadTrueOrFalse)
                        request.CannedACL = S3CannedACL.PublicRead;
                    request.UploadProgressEvent += ProgressEventCallback;
                    Task task = t.UploadAsync(request, token);
                    await task;
                }
            }
            catch (Exception)
            {

                if (token.IsCancellationRequested)
                    bReturn = false;
                else
                    throw;
            }

            return bReturn;
        }

        private async Task<bool> UploadObjectBandWidthThrottlingAsync(string bucketName, string localFullFilename, string key, CancellationToken token, int sleepMiliSec, bool publicReadTrueOrFalse = false)
        {
            bool bReturn = true;
            List<UploadPartResponse> uploadResponses = new List<UploadPartResponse>();
            InitiateMultipartUploadRequest initiateRequest = new InitiateMultipartUploadRequest
            {
                BucketName = bucketName,
                Key = key
            };

            if (publicReadTrueOrFalse)
                initiateRequest.CannedACL = S3CannedACL.PublicRead;

            IAmazonS3 s3Client = new AmazonS3Client(AccessKey, SecretKey, new AmazonS3Config
            {
                ServiceURL = ServiceUrl,
                BufferSize = 64 * (int)Math.Pow(2, 10),
                ProgressUpdateInterval = this.ProgressUpdateInterval,
                Timeout = new TimeSpan(1, 0, 0, 0, 0)
            });
            InitiateMultipartUploadResponse initResponse =
                await s3Client.InitiateMultipartUploadAsync(initiateRequest, token);
            long contentLength = new FileInfo(localFullFilename).Length;
            ContentTotalBytes = contentLength;
            long partSize = 5 * (long)Math.Pow(2, 20);

            try
            {
                long filePosition = 0;
                for (int i = 1; filePosition < contentLength; i++)
                {
                    if (token.IsCancellationRequested)
                    {
                        bReturn = false;
                        break;
                    }
                    var task = Task.Run(() => Thread.Sleep(sleepMiliSec));
                    await task;

                    UploadPartRequest uploadRequest = new UploadPartRequest
                    {
                        BucketName = bucketName,
                        Key = key,
                        UploadId = initResponse.UploadId,
                        PartNumber = i,
                        PartSize = partSize,
                        FilePosition = filePosition,
                        FilePath = localFullFilename
                    };
                    uploadRequest.StreamTransferProgress += ProgressEventCallback;
                    uploadResponses.Add(await s3Client.UploadPartAsync(uploadRequest, token));

                    filePosition += partSize;
                    CurrentTransferredBytes = filePosition;

                }
                CompleteMultipartUploadRequest completeRequest = new CompleteMultipartUploadRequest
                {
                    BucketName = bucketName,
                    Key = key,
                    UploadId = initResponse.UploadId
                };
                completeRequest.AddPartETags(uploadResponses);
                CompleteMultipartUploadResponse completeUploadResponse =
                    await s3Client.CompleteMultipartUploadAsync(completeRequest, token);
            }
            catch (Exception)
            {
                AbortMultipartUploadRequest abortMPURequest = new AbortMultipartUploadRequest
                {
                    BucketName = bucketName,
                    Key = key,
                    UploadId = initResponse.UploadId
                };

                await s3Client.AbortMultipartUploadAsync(abortMPURequest);

                throw;
            }
            finally
            {
                s3Client.Dispose();
            }
            return bReturn;
        }

        private void ProgressEventCallback(object sender, StreamTransferProgressArgs e)
        {
            double percentDone = Math.Round(((double)(e.TransferredBytes + CurrentTransferredBytes) * 100) / ContentTotalBytes);
            percentDone = percentDone >= 100 ? 100 : percentDone;
            long transferredBytes = (e.TransferredBytes + CurrentTransferredBytes) > ContentTotalBytes ? ContentTotalBytes : e.TransferredBytes + CurrentTransferredBytes;

            UploadProgressEvent?.Invoke(this, new S3ProgressArgs
            {
                TransferredBytes = transferredBytes,
                TotalBytes = ContentTotalBytes,
                PercentDone = (int)percentDone
            });
        }

        private void ProgressEventCallback(object sender, TransferProgressArgs e)
        {
            double percentDone = Math.Round(((double)(e.TransferredBytes) * 100) / e.TotalBytes);
            percentDone = percentDone >= 100 ? 100 : percentDone;
            long transferredBytes = e.TransferredBytes > e.TotalBytes ? e.TotalBytes : e.TransferredBytes;

            if ((PreviousReportBytes + this.ProgressUpdateInterval) < transferredBytes || transferredBytes == e.TotalBytes)
            {
                if (transferredBytes != e.TotalBytes)
                    PreviousReportBytes = transferredBytes;

                UploadProgressEvent?.Invoke(this, new S3ProgressArgs
                {
                    TransferredBytes = transferredBytes,
                    TotalBytes = e.TotalBytes,
                    PercentDone = (int)e.PercentDone
                });
            }
        }

    }


    public class S3ProgressArgs
    {
        public long TransferredBytes { get; set; }
        public long TotalBytes { get; set; }
        public int PercentDone { get; set; }
    }


    public class ObjectStorageFile
    {
        public string Name { get; set; }
        public long Length { get; set; }
        public string LastWriteTime { get; set; }
    }
}

