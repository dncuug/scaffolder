using Scaffolder.Core;
using System;
using System.IO;
using Newtonsoft.Json;

namespace Scaffolder.App
{
    public class Program
    {
        public static void Main(string[] args)
        {
            const String workingDirectoryPath = "d:/tmp/";
            
            var connectionString = File.ReadAllText(workingDirectoryPath + "connection.conf");

            var builder = new SqlServerModelBuild(connectionString);

            var database = builder.Build();
            database.Save(workingDirectoryPath + "db.json");

            Console.WriteLine(database.AsJson());

            Console.ReadLine();
        }
    }
}
