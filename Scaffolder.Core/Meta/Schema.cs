using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Scaffolder.Core.Meta
{
    public class Schema : BaseObject
    {
        public Schema()
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
            var json = JsonConvert.SerializeObject(this, Formatting.Indented);
            File.WriteAllText(path, json);
            return true;
        }

        public static Schema Load(String configurationFilePath, String extendedConfigurationFilePath = "")
        {
            var json = File.ReadAllText(configurationFilePath);
            var schema = JsonConvert.DeserializeObject<Schema>(json);
            schema.LoadextendedConfiguration(extendedConfigurationFilePath);
            return schema;
        }

        public Table GetTable(string name)
        {
            return Tables.SingleOrDefault(o => String.Equals(o.Name, name, StringComparison.OrdinalIgnoreCase));
        }

        private bool LoadextendedConfiguration(string extendedConfigurationFilePath)
        {
            if (File.Exists(extendedConfigurationFilePath))
            {
                var json = File.ReadAllText(extendedConfigurationFilePath);
                var database = JsonConvert.DeserializeObject<Schema>(json);

                foreach (var t in database.Tables)
                {
                    var table = this.GetTable(t.Name);

                    if (table != null)
                    {
                        ObjectExtender.MapExtendInformation(t, table);

                        foreach (var c in t.Columns)
                        {
                            var column = table.GetColumn(c.Name);

                            if (column != null)
                            {
                                ObjectExtender.MapExtendInformation(c, column);
                            }
                        }
                    }
                }

                ExtendedConfigurationLoaded = true;
            }

            return ExtendedConfigurationLoaded;
        }
    }
}
