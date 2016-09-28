using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Scaffolder.Core.Base;
using Scaffolder.Core.Data;
using Scaffolder.Core.Engine.Sql;
using Scaffolder.Core.Meta;
using System;

namespace Scaffolder.API.Application
{
    public class ControllerBase : Controller
    {
        protected Configuration Configuration { get; private set; }
        protected Schema Schema { get; private set; }

        protected readonly AppSettings Settings;

        public ControllerBase(IOptions<AppSettings> settings)
        {
            Settings = settings.Value;

            var schemaPath = Settings.WorkingDirectory + "db.json";
            var extendedSchemaPath = Settings.WorkingDirectory + "db_ex.json";
            var configurationPath = Settings.WorkingDirectory + "configuration.json";

            if (!System.IO.File.Exists(configurationPath))
            {
                Configuration.Create().Save(configurationPath);
            }
            
            if (!System.IO.File.Exists(schemaPath))
            {
                var schema = new Schema
                {
                    Description = "",
                    Name = "EmptySchema",
                    Generated = DateTime.Now,
                    Title = "Empty Schema"
                };

                schema.Save(schemaPath);
            }

            Configuration = Configuration.Load(configurationPath);
            Schema = Schema.Load(schemaPath, extendedSchemaPath);
        }

        protected ISchemaBuilder GetSchemaBuilder()
        {
            var db = GetDatabase();

            return new SqlSchemaBuilder(db);
            //return new MySqlSchemaBuilder(db);
        }

        protected IRepository CreateRepository(Table table)
        {
            var db = GetDatabase();

            var queryBuilder = new SqlQueryBuilder();
            //var queryBuilder = new MySqlQueryBuilder();

            return new Repository(db, queryBuilder, table);
        }

        private IDatabase GetDatabase()
        {
            var connectionString = System.IO.File.ReadAllText(Settings.WorkingDirectory + "connection.conf");
            return new SqlDatabase(connectionString);
            //return new MySqlDatabase(connectionString);
        }
    }
}