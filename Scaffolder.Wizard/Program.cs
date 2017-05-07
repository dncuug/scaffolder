using System;
using System.Linq;
using Scaffolder.Core.Data;
using Scaffolder.Core.Engine;
using Scaffolder.Core.Meta;

namespace Scaffolder.Wizard
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new ApplicationContext
            {
                Configuration = Configuration.Create()
            };

            Console.WriteLine("0. Project name:");
            context.Configuration.Name = Console.ReadLine();

            Console.WriteLine("1. Working directory:");
            context.Location = Console.ReadLine();

            context.Location = NormalizePath(context.Location);

            Console.WriteLine("2. Database type:");

            var databaseEngines = Enum.GetValues(typeof(DatabaseEngine)).Cast<DatabaseEngine>().ToList();

            for (int i = 0; i < databaseEngines.Count; i++)
            {
                Console.WriteLine($"\t{i + 1}. {databaseEngines[i]}");
            }

            var databaseEngineId = Convert.ToInt32(Console.ReadLine());
            context.Configuration.Engine = databaseEngines[databaseEngineId];

            Console.WriteLine("3. Database connection string (https://connectionstrings.com/):");

            context.Configuration.ConnectionString = Console.ReadLine();

            Console.WriteLine("One moment please...");

            var engine = new Engine(context.Configuration.ConnectionString, context.Configuration.Engine);
            var builder = engine.CreateSchemaBuilder();
            context.Schema = new Schema(builder.Build());

            context.Save(context.Location);
        }

        private static string NormalizePath(string path)
        {
            path = path.Replace("\\", "/");

            if (path.ElementAt(path.Length - 1) != '/')
            {
                path += '/';
            }

            return path;
        }
    }
}