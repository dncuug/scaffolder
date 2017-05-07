using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Scaffolder.Core.Meta
{
    public class Schema : List<Table>
    {
        public Schema()
        {
        }

        public Schema(IEnumerable<Table> list)
        {
            AddRange(list);
        }

        public Table GetTable(string name)
        {
            return this.SingleOrDefault(o => String.Equals(o.Name, name, StringComparison.OrdinalIgnoreCase));
        }

        public bool Save(string directory)
        {
            foreach (var table in this)
            {
                var path = $"{directory}{table.Name}.entity.json";
                var json = JsonConvert.SerializeObject(table, Formatting.Indented);
                File.WriteAllText(path, json);
            }

            return true;
        }

        public static IEnumerable<Table> Load(string directory)
        {
            var tables = new List<Table>();

            var files = Directory.GetFiles(directory, "*.entity.json");

            foreach (var path in files)
            {
                var json = File.ReadAllText(path);
                var table = JsonConvert.DeserializeObject<Table>(json);
                var extendedConfigurationFilePath = $"{directory}{Path.GetFileNameWithoutExtension(path)}.extended.json";
                table = LoadextendedConfiguration(table, extendedConfigurationFilePath);

                tables.Add(table);
            }

            return tables;
        }

        private static Table LoadextendedConfiguration(Table table, string path)
        {
            if (File.Exists(path))
            {
                var json = File.ReadAllText(path);
                var extendedTable = JsonConvert.DeserializeObject<Table>(json);

                ObjectExtender.MapExtendInformation(table, extendedTable);

                foreach (var c in table.Columns)
                {
                    var column = extendedTable.GetColumn(c.Name);

                    if (column != null)
                    {
                        ObjectExtender.MapExtendInformation(c, column);
                    }
                }
            }

            return table;
        }
    }
}