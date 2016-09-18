using Scaffolder.Core;
using System;
using System.IO;

namespace Scaffolder.App
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var connectionString = File.ReadAllText("c:/pub/connection.conf");
            
            var builder = new SqlServerModelBuild(connectionString);
            
            var database = builder.Build();

            foreach (var t in database.Tables)
            {
                Console.WriteLine(t.Name);
            }

            Console.ReadLine();
        }
    }
}
