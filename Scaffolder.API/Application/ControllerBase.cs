using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Scaffolder.Core.Data;
using Scaffolder.Core.Meta;
using System;
using Scaffolder.Core.Engine.Sql;

namespace Scaffolder.API.Application
{
    public class ControllerBase : Controller
    {
        protected Schema Schema { get; private set; }

        protected readonly SqlDatabase _db;
        protected readonly AppSettings _settings;

        public ControllerBase(IOptions<AppSettings> settings)
        {
            _settings = settings.Value;

            var configurationFilePath = _settings.WorkingDirectory + "db.json";
            var extendedConfigurationFilePath = _settings.WorkingDirectory  + "db_ex.json";

            if (System.IO.File.Exists(configurationFilePath))
            {
                Schema = Schema.Load(configurationFilePath, extendedConfigurationFilePath);
            }
            else
            {
                Schema = new Schema
                {
                    Description = "",
                    Name = "EmptyModel",
                    Generated = DateTime.Now,
                    Title = "Empty Model"
                };
            }

            

            var connectionString = System.IO.File.ReadAllText(_settings.WorkingDirectory + "connection.conf");
            _db = new SqlDatabase(connectionString);
        }
    }
}
