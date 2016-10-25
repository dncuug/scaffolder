using Scaffolder.Core.Meta;
using System;

namespace Scaffolder.Core.Storage
{
    public class FtpStorage : Storage
    {
        public FtpStorage(dynamic connection, String locationUrl)
            : base(locationUrl)
        {
           
        }

        public override StorageType Type => StorageType.FTP;
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
