using Scaffolder.Core;
using System;
using System.IO;
using Newtonsoft.Json;
using Scaffolder.Core.Base;

namespace Scaffolder.App
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //var c1 = new TextColumn
            //{
            //    Name = "column 1"
            //};

            //var c2 = new FileColumn
            //{
            //    Name = "column 2",
            //    StorageConnectoinString = "StorageConnectoinString 2",
            //    StorageUrl = "StorageUrl 2"
            //};

            //var table = new Table("TST");
            //table.Columns.Add(c1);
            //table.Columns.Add(c2);

            //var json = JsonConvert.SerializeObject(table);

            //var table2 = JsonConvert.DeserializeObject<Table>(json);

            //var x = (table2.Columns[1] as FileColumn);


            //return;

            const String workingDirectoryPath = "c:/pub/";

            var connectionString = File.ReadAllText(workingDirectoryPath + "connection.conf");

            var builder = new SqlServerModelBuild(connectionString);

            var database = builder.Build();
            database.Save(workingDirectoryPath + "db.json");

            Console.WriteLine(database.AsJson());

            Console.ReadLine();
        }
    }
}
