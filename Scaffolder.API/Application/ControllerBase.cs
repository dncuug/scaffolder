using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Scaffolder.Core.Data;

namespace Scaffolder.API.Application
{
    public class ControllerBase : Controller
    {
        protected Scaffolder.Core.Base.Database DatabaseModel { get; private set; }
        protected Scaffolder.Core.Data.SqlServerDatabase _db;

        protected readonly AppSettings _settings;

        public ControllerBase(IOptions<AppSettings> settings)
        {
            _settings = settings.Value;

            var configurationFilePath = _settings.WorkingDirectory + "db.json";
            var extendedConfigurationFilePath = _settings.WorkingDirectory  + "db_ex.json";;

            DatabaseModel = Scaffolder.Core.Base.Database.Load(configurationFilePath, extendedConfigurationFilePath);

            var connectionString = System.IO.File.ReadAllText(_settings.WorkingDirectory + "connection.conf");
            _db = new SqlServerDatabase(connectionString);
        }
    }
}
