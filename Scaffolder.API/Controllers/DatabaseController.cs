using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Scaffolder.API.Application;
using Scaffolder.Core;
using Scaffolder.Core.Sql;

namespace Scaffolder.API.Controllers
{
    [Route("[controller]")]
    public class DatabaseController : Scaffolder.API.Application.ControllerBase
    {
        public DatabaseController(IOptions<AppSettings> settings)
            : base(settings)
        {
        }

        [HttpGet]
        public dynamic Get()
        {
            return new
            {
                Schema.Name,
                Schema.Title,
                Schema.Description,
                Schema.Generated,
                Schema.ExtendedConfigurationLoaded
            };
        }

        [HttpPost]
        public bool Post()
        {
            var connectionString = System.IO.File.ReadAllText(_settings.WorkingDirectory + "connection.conf");

            var builder = new SqlSchemaBuilder(connectionString);
            var database = builder.Build();
            
            database.Save(_settings.WorkingDirectory + "db.json");

            return true;
        }
    }
}
