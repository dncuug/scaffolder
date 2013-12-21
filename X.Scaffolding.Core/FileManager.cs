using Microsoft.WindowsAzure.Storage;
using System;
using System.IO;
using System.Web;

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
        /// <param name="fileName"></param>
        /// <param name="storageUrl"></param>
        /// <param name="storageConnectionString"></param>
        /// <param name="blobContainerName"></param>
        /// <returns></returns>
        public static string UploadFile(byte[] bytes, string fileName, string storageUrl, string storageConnectionString, string blobContainerName = "")
        {
            if (String.IsNullOrEmpty(StorageUrl) || String.IsNullOrEmpty(StorageConnectionString))
            {
                throw new Exception("Storage url or storage connection string not initialized. Please initilaize FileManager by using Initialize() method.");
            }
            
            var url = String.Format("{0}{1}", storageUrl, fileName);

            var storageType = GetStorageType(storageConnectionString);

            switch (storageType)
            {
                case Storage.FileSystem:
                    {
                        var path = String.Format("{0}{1}", storageConnectionString, fileName);
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
                        var blockBlob = container.GetBlockBlobReference(fileName);

                        // Create or overwrite the blob with contents from a file.
                        var stream = new MemoryStream(bytes);
                        blockBlob.UploadFromStream(stream);

                        url = blockBlob.Uri.ToString();
                        break;
                    }

                case Storage.Ftp:
                    {
                        var path = storageConnectionString + fileName;
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
        /// <param name="fileName">Name for new file</param>
        /// <returns>url to file</returns>
        public static string UploadFile(byte[] bytes, string fileName)
        {
            return UploadFile(bytes, fileName, StorageUrl, StorageConnectionString, BlobContainerName);
        }

        /// <summary>
        /// Save file to website
        /// </summary>
        /// <param name="stream">Stream with file content</param>
        /// <param name="fileName">Name for new file</param>
        /// <returns>url to file</returns>
        public static string UploadFile(Stream stream, string fileName)
        {
            var memoryStream = new MemoryStream();
            stream.CopyTo(memoryStream);
            var bytes = memoryStream.ToArray();

            return UploadFile(bytes, fileName, StorageUrl, StorageConnectionString, BlobContainerName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        /// <param name="currentFileName"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static string UploadPhoto(HttpPostedFileBase file, string currentFileName, Func<string, string> func)
        {
            if (file != null && file.ContentLength > 0)
            {
                var name = Guid.NewGuid() + Path.GetExtension(file.FileName);
                UploadFile(file.InputStream, name);
                func(name);
                return name;
            }

            func(currentFileName);
            return String.Empty;
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