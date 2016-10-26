using Scaffolder.Core.Meta;
using System;

namespace Scaffolder.Core.Storage
{
    public class AmazonS3 : Storage
    {
        public AmazonS3(string locationUrl)
            : base(locationUrl)
        {
        }

        public override StorageType Type => StorageType.AmazonS3;
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
