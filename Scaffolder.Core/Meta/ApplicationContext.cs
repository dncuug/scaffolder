using System.IO;

namespace Scaffolder.Core.Meta
{
    public class ApplicationContext
    {
        public Configuration Configuration { get; set; }
        public Schema Schema { get; set; }
        public string Location { get; set; }

        public static ApplicationContext Load(string workingDirectory)
        {
            var configurationPath = Path.Combine(workingDirectory, "configuration.json");

            if (!File.Exists(configurationPath))
            {
                Configuration.Create().Save(configurationPath);
            }

            var applicationContext = new ApplicationContext
            {
                Configuration = Configuration.Load(configurationPath),
                Schema = new Schema(Schema.Load(workingDirectory)),
                Location = workingDirectory
            };

            return applicationContext;
        }

        public void Save(string workingDirectory)
        {
            var configurationPath = Path.Combine(workingDirectory, "configuration.json");

            Configuration.Save(configurationPath);
            Schema.Save(workingDirectory);
        }
    }
}
