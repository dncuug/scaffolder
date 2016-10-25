using Scaffolder.Core.Meta;
using System;
using System.IO;

namespace Scaffolder.Core.Storage
{
    /// <summary>
    /// 
    /// </summary>
    public class FileSystemStorage : Storage
    {
        private readonly string _location;

        public FileSystemStorage(dynamic connection, String locationUrl)
            : base(locationUrl)
        {
            _location = connection.Path;

            if (String.IsNullOrEmpty(_location))
            {
                throw new ArgumentNullException($"FileSystemStorage location configuration unknow");
            }
        }

        public override StorageType Type => StorageType.FileSystem;

        public override string Upload(byte[] bytes, string extension = "")
        {
            var name = Guid.NewGuid() + extension;

            var path = Path.Combine(_location, name);
            File.WriteAllBytes(path, bytes);
            return name;
        }

        public override byte[] Get(string name)
        {
            var path = Path.Combine(_location, name);
            return File.ReadAllBytes(path);
        }
    }
}
