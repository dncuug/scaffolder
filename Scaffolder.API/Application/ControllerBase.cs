using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Scaffolder.Core.Engine.Sql;
using Scaffolder.Core.Meta;
using System;
using Scaffolder.Core.Base;
using Scaffolder.Core.Data;
using Scaffolder.Core.Engine.MySql;

namespace Scaffolder.API.Application
{
    public class ControllerBase : Controller
    {
        protected Schema Schema { get; private set; }

        protected readonly AppSettings Settings;

        public ControllerBase(IOptions<AppSettings> settings)
        {
            Settings = settings.Value;

            var configurationFilePath = Settings.WorkingDirectory + "db.json";
            var extendedConfigurationFilePath = Settings.WorkingDirectory + "db_ex.json";

            if (System.IO.File.Exists(configurationFilePath))
            {
                Schema = Schema.Load(configurationFilePath, extendedConfigurationFilePath);
            }
            else
            {
                Schema = new Schema
                {
                    Description = "",
                    Name = "EmptySchema",
                    Generated = DateTime.Now,
                    Title = "Empty Schema"
                };
            }
        }

        protected IRepository CreateRepository(Table table)
        {
            var connectionString = System.IO.File.ReadAllText(Settings.WorkingDirectory + "connection.conf");

	        //var db = new SqlDatabase(connectionString);
            //var queryBuilder = new SqlQueryBuilder();

	        var db = new MySqlDatabase(connectionString);
	        var queryBuilder = new MySqlQueryBuilder();

	        return new Repository(db, queryBuilder, table);
        }
    }
}
