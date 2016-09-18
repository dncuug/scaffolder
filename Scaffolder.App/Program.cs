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
            var connectionString = File.ReadAllText("c:/pub/connection.conf");

            var builder = new SqlServerModelBuild(connectionString);

            var database = builder.Build();
            
            var json = JsonConvert.SerializeObject(database, Formatting.Indented);
            Console.WriteLine(json);

            Console.ReadLine();
        }
    }
}
