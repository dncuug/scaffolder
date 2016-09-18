using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace Scaffolder.Core.Base
{
    public class Database : BaseObject
    {
        public Database()
        {
            Name = "Database";
            Title = "Database";
            Description = "";
            Tables = new List<Table>();
            Generated = DateTime.Now;
        }

        public List<Table> Tables { get; set; }
        public DateTime Generated { get; set; }
        public bool ExtendedConfigurationLoaded { get; private set; }

        public bool Save(String path)
        {
            var json = AsJson();
            System.IO.File.WriteAllText(path, json);
            return true;
        }

        public string AsJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        private bool LoadextendedConfiguration(string extendedConfigurationFilePath)
        {
            if (File.Exists(extendedConfigurationFilePath))
            {
                var json = System.IO.File.ReadAllText(extendedConfigurationFilePath);
                var database = JsonConvert.DeserializeObject<Database>(json);

                foreach (var t in database.Tables)
                {

                }

                ExtendedConfigurationLoaded = true;
            }

            return ExtendedConfigurationLoaded;
        }

        public static Database Load(String configurationFilePath, String extendedConfigurationFilePath = "")
        {
            var json = System.IO.File.ReadAllText(configurationFilePath);
            var database = JsonConvert.DeserializeObject<Database>(json);
            database.LoadextendedConfiguration(extendedConfigurationFilePath);
            return database;
        }

        public Table GetTable(string name)
        {
            return Tables.SingleOrDefault(o => o.Name.ToLower() == name.ToLower());
        }
    }
}
