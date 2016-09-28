using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Scaffolder.Core.Meta;

namespace Scaffolder.Core.Storage
{
    public abstract class Storage
    {
        public abstract StorageType Type { get; }

        public abstract string Upload(byte[] bytes);

        public abstract byte[] Get(string name);

        public static Storage GetStorage(StorageType type, dynamic connection)
        {
            switch (type)
            {
                case StorageType.FileSystem: return new FileSystemStorage(connection);
                case StorageType.FTP: return new FtpStorage(connection);
                case StorageType.AzureStorage: return new AzureBlobStorage(connection);
                case StorageType.SSH: return new SshStorage(connection);
                default: throw new NotSupportedException();
            }
        }
    }
}
