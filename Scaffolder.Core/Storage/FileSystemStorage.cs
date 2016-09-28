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

        public FileSystemStorage(dynamic connection)
        {
            _location = connection.Location;
        }

        public override StorageType Type => StorageType.FileSystem;

        public override string Upload(byte[] bytes)
        {
            var name = Guid.NewGuid().ToString();

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
