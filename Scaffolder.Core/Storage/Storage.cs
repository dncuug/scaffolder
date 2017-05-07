using Scaffolder.Core.Meta;
using System;

namespace Scaffolder.Core.Storage
{
    public abstract class Storage
    {
        protected Storage(string locationUrl)
        {
            Url = locationUrl;
        }

        public abstract StorageType Type { get; }

        public string Url { get; set; }

        public abstract string Upload(byte[] bytes, string extension = "");
        
        public abstract byte[] Get(string name);

        public static Storage GetStorage(StorageType type, string locationUrl, dynamic connection)
        {
            switch (type)
            {
                case StorageType.FileSystem: return new FileSystemStorage(connection, locationUrl);
                case StorageType.FTP: return new FtpStorage(connection, locationUrl);
                case StorageType.AzureStorage: return new AzureBlobStorage(connection, locationUrl);
                case StorageType.SSH: return new SshStorage(connection, locationUrl);
                default: throw new NotSupportedException();
            }
        }
    }
}
