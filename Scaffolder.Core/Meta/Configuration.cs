using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using Newtonsoft.Json;

namespace Scaffolder.Core.Meta
{
    public enum StorageType
    {
        FileSystem,
        FTP,
        AzureStorage,
        SSH
    }
    
    public class StorageConfiguration
    {
        public StorageType Type { get; set; }

        public String Url { get; set; }

        public dynamic Connection { get; set; }
    }

    public class User
    {
        public String Login { get; set; }
        public String Password { get; set; }
        public bool Administrator { get; set; }
    }

    public class Configuration
    {
        public StorageConfiguration StorageConfiguration { get; set; }

        public List<User> Users { get; set; }

        public bool Save(String path)
        {
            var json = JsonConvert.SerializeObject(this, Formatting.Indented);
            File.WriteAllText(path, json);
            return true;
        }

        public static Configuration Load(String path)
        {
            var json = File.ReadAllText(path);
            var configuration = JsonConvert.DeserializeObject<Configuration>(json);
            return configuration;
        }

        public static Configuration Create()
        {
            return new Configuration
            {
                StorageConfiguration = new StorageConfiguration
                {
                    Type = StorageType.FileSystem,
                    Url = "http://example.com/storage/",
                    Connection = new
                    {
                        Path="/var/www/example.com/storage/"
                    }
                },
                Users = new List<User>
                {
                    new User {Login = "admin", Password = "admin", Administrator = true},
                    new User {Login = "manager", Password = "manager", Administrator = false},
                }
            };
        }
       
    }
}
