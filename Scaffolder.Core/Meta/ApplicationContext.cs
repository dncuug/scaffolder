using System;
using System.IO;

namespace Scaffolder.Core.Meta
{
    public class ApplicationContext
    {
        public Configuration Configuration { get; set; }
        public Schema Schema { get; set; }
        public String Location { get; set; }

        public static ApplicationContext Load(String workingDirectory)
        {
            var schemaPath = Path.Combine(workingDirectory, "db.json");
            var extendedSchemaPath = Path.Combine(workingDirectory, "db_ex.json");
            var configurationPath = Path.Combine(workingDirectory, "configuration.json");

            if (!File.Exists(configurationPath))
            {
                Configuration.Create().Save(configurationPath);
            }

            if (!File.Exists(schemaPath))
            {
                new Schema().Save(schemaPath);
            }

            var applicationContext = new ApplicationContext
            {
                Configuration = Configuration.Load(configurationPath),
                Schema = Schema.Load(schemaPath, extendedSchemaPath),
                Location = workingDirectory
            };

            return applicationContext;

        }
    }
}
