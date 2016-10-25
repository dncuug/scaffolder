using Scaffolder.Core.Meta;
using System;

namespace Scaffolder.Core.Storage
{
    public class SshStorage : Storage
    {
        public override StorageType Type => StorageType.SSH;

        public SshStorage(dynamic connection, String locationUrl)
            :base(locationUrl)
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
