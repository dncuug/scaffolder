using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Scaffolder.API.Application;
using Scaffolder.Core.Base;
using Scaffolder.Core.Engine.Sql;

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
            var builder = GetSchemaBuilder();
            var schema = builder.Build();

            schema.Save(Settings.WorkingDirectory + "db.json");

            return true;
        }

        private ISchemaBuilder GetSchemaBuilder()
        {
            var connectionString = System.IO.File.ReadAllText(Settings.WorkingDirectory + "connection.conf");
            var db = new SqlDatabase(connectionString);
            return new SqlSchemaBuilder(db);
        }
    }
}
