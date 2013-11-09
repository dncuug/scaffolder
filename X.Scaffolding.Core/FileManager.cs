using Microsoft.WindowsAzure.Storage;
using System;
using System.IO;

namespace X.Scaffolding.Core
{
    /// <summary>
    /// Global application context
    /// </summary>
    public static class FileManager
    {
        public static String StorageUrl { get; set; }
        public static String StorageConnectionString { get; set; }
        public static String BlobContainerName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="storageUrl"></param>
        /// <param name="storageConnectionString"></param>
        /// <param name="blobContainerName"></param>
        public static void Initialize(string storageUrl, string storageConnectionString, string blobContainerName = "")
        {
            StorageUrl = storageUrl;
            StorageConnectionString = storageConnectionString;
            BlobContainerName = blobContainerName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="originalFileName"></param>
        /// <param name="storageUrl"></param>
        /// <param name="storageConnectionString"></param>
        /// <param name="blobContainerName"></param>
        /// <returns></returns>
        public static string UploadFile(byte[] bytes, string originalFileName, string storageUrl, string storageConnectionString, string blobContainerName = "")
        {
            if (String.IsNullOrEmpty(StorageUrl) || String.IsNullOrEmpty(StorageConnectionString))
            {
                throw new Exception("Storage url or storage connection string not initialized. Please initilaize FileManager by using Initialize() method.");
            }

            originalFileName = originalFileName.ToLower();
            var extension = Path.GetExtension(originalFileName);
            var name = Guid.NewGuid() + extension;

            var url = String.Format("{0}{1}", storageUrl, name);

            var storageType = GetStorageType(storageConnectionString);

            switch (storageType)
            {
                case Storage.FileSystem:
                    {
                        var path = String.Format("{0}{1}", storageConnectionString, name);
                        File.WriteAllBytes(path, bytes);
                        break;
                    }

                case Storage.WindowsAzureStorage:
                    {
                        //Upload to Windows Azure Storage
                        var storageAccount = CloudStorageAccount.Parse(storageConnectionString);

                        var blobClient = storageAccount.CreateCloudBlobClient();

                        // Retrieve a reference to a container. 
                        var container = blobClient.GetContainerReference(blobContainerName);

                        // Retrieve reference to a blob named "myblob".
                        var blockBlob = container.GetBlockBlobReference(name);

                        // Create or overwrite the blob with contents from a file.
                        var stream = new MemoryStream(bytes);
                        blockBlob.UploadFromStream(stream);

                        url = blockBlob.Uri.ToString();
                        break;
                    }

                case Storage.Ftp:
                    {
                        var path = storageConnectionString + name;
                        var ftp = new Ftp();
                        ftp.UploadFile(bytes, path);
                        break;
                    }
                case Storage.Unknown:
                    {
                        throw new Exception("Unknow storage type");
                        break;
                    }
            }

            return url;
        }

        /// <summary>
        /// Save file to website
        /// </summary>
        /// <param name="bytes">File content</param>
        /// <param name="originalFileName">Name for new file</param>
        /// <returns>url to file</returns>
        public static string UploadFile(byte[] bytes, string originalFileName)
        {
            return UploadFile(bytes, originalFileName, StorageUrl, StorageConnectionString, BlobContainerName);
        }

        private static Storage GetStorageType(string path)
        {
            if (String.IsNullOrEmpty(path))
            {
                return Storage.Unknown;
            }

            if (path.Contains("DefaultEndpointsProtocol"))
            {
                return Storage.WindowsAzureStorage;
            }

            if (path.Contains("ftp://"))
            {
                return Storage.Ftp;
            }

            return Storage.FileSystem;
        }
    }
}