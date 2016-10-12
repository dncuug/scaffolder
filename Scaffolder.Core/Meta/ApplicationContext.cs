using System;
using System.IO;

namespace Scaffolder.Core.Meta
{
    public class ApplicationContext
    {
        public Configuration Configuration { get; set; }
        public Schema Schema { get; set; }
        public String Name { get; set; }
        public String Location { get; set; }

        public static ApplicationContext Load(String workingDirectory)
        {
            var schemaPath = workingDirectory + "db.json";
            var extendedSchemaPath = workingDirectory + "db_ex.json";
            var configurationPath = workingDirectory + "configuration.json";

            if (!File.Exists(configurationPath))
            {
                Configuration.Create().Save(configurationPath);
            }

            if (!File.Exists(schemaPath))
            {
                new Schema().Save(schemaPath);
            }

            return new ApplicationContext
            {
                Configuration = Configuration.Load(configurationPath),
                Schema = Schema.Load(schemaPath, extendedSchemaPath),
                Name = Path.GetFileName(workingDirectory),
                Location = workingDirectory
            };

        }
    }
}
