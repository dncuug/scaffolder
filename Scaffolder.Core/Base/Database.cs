using System;
using System.Collections.Generic;
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
        }

        public List<Table> Tables { get; set; }

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

        public static Database Load(String path)
        {
            var json = System.IO.File.ReadAllText(path);
            var obj = JsonConvert.DeserializeObject<Database>(json);
            return obj;
        }
    }
}
