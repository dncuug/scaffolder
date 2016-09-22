using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Scaffolder.Core.Base;
using Scaffolder.Core.Data;
using System;

namespace Scaffolder.API.Application
{
    public class ControllerBase : Controller
    {
        protected Database DatabaseModel { get; private set; }

        protected readonly SqlServerDatabase _db;
        protected readonly AppSettings _settings;

        public ControllerBase(IOptions<AppSettings> settings)
        {
            _settings = settings.Value;

            var configurationFilePath = _settings.WorkingDirectory + "db.json";
            var extendedConfigurationFilePath = _settings.WorkingDirectory  + "db_ex.json";

            if (System.IO.File.Exists(configurationFilePath))
            {
                DatabaseModel = Database.Load(configurationFilePath, extendedConfigurationFilePath);
            }
            else
            {
                DatabaseModel = new Database
                {
                    Description = "",
                    Name = "EmptyModel",
                    Generated = DateTime.Now,
                    Title = "Empty Model"
                };
            }

            

            var connectionString = System.IO.File.ReadAllText(_settings.WorkingDirectory + "connection.conf");
            _db = new SqlServerDatabase(connectionString);
        }
    }
}
