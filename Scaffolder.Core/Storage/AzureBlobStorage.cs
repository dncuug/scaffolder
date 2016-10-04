using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Scaffolder.Core.Meta;

namespace Scaffolder.Core.Storage
{
    public class AzureBlobStorage : Storage
    {
        public override StorageType Type => StorageType.AzureStorage;

        public AzureBlobStorage(dynamic connection)
        {

        }

        public override string Upload(byte[] bytes, string extension = "")
        {
            throw new NotImplementedException();
        }

        public override byte[] Get(string name)
        {
            throw new NotImplementedException();
        }
    }
}
