using Google.Cloud.Storage.V1;
using Vision.Interfaces;
using Object = Google.Apis.Storage.v1.Data.Object;
using Vision.Exceptions;
using Google.Apis.Auth.OAuth2;
using Google;

namespace Vision.Datas
{
    public class GoogleDataObjectImpl : IDataObject
    {
        private static string bucketName = "csharp.gogle.cld.education";

        public async Task<bool> DoesExists(string remoteFullPath)
        {
            var storage = StorageClient.Create();
            try
            {
                if (remoteFullPath == bucketName)
                {
                    await storage.GetBucketAsync(remoteFullPath);
                    return true;
                }
                else
                {
                    await storage.GetObjectAsync(bucketName, remoteFullPath);
                    return true;
                }
            }
            catch (Exception ex) when (ex is ObjectNotFoundException || ex is GoogleApiException) //ObjectNotFoundException implements base class Exception from .net, but storage.GetObjectAsync throws a Google.GoogleApiException 
            {
                return false;
            }
        }

        public async Task Download(string remoteFullPath, string localFullPath)
        {
            if (await DoesExists(remoteFullPath))
            {
                var storage = StorageClient.Create();
                MemoryStream stream = new MemoryStream();
                Object file = await storage.DownloadObjectAsync(bucketName, remoteFullPath, stream);
                File.WriteAllBytes(localFullPath, stream.GetBuffer());
            }
            else
            {
                throw new ObjectNotFoundException();
            }
        }

        public async Task<string> Publish(string remoteFullPath, int expirationTime = 90)
        {
            if (await DoesExists(remoteFullPath))
            {


                UrlSigner urlSigner = UrlSigner.FromCredential(await GoogleCredential.GetApplicationDefaultAsync());
                // V4 is the default signing version.
                string url = await urlSigner.SignAsync(bucketName, remoteFullPath, TimeSpan.FromHours(expirationTime), HttpMethod.Get);
                return url;
            }
            else
            {
                throw new ObjectNotFoundException();
            }
        }

        public async Task Remove(string remoteFullPath, bool recursive = false)
        {
            var storage = StorageClient.Create();
            if (recursive)
            {
                foreach (var storageObject in storage.ListObjects(bucketName))
                {
                    string fileName = remoteFullPath.Replace(bucketName + "/", "");
                    if (storageObject.Name.StartsWith(fileName, StringComparison.OrdinalIgnoreCase))
                    {
                        await storage.DeleteObjectAsync(bucketName, storageObject.Name);
                    }
                }
            }
            if (await DoesExists(remoteFullPath))
            {
                await storage.DeleteObjectAsync(bucketName, remoteFullPath);
            }
        }

        public async Task Upload(string localFullPath, string remoteFullPath)
        {
            var storage = StorageClient.Create();
            using var fileStream = File.OpenRead(localFullPath);
            await storage.UploadObjectAsync(bucketName, remoteFullPath, null, fileStream);
        }

    }
}
