using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Scaffolder.API.Application;
using Scaffolder.Core;

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
                DatabaseModel.Name,
                DatabaseModel.Title,
                DatabaseModel.Description,
                DatabaseModel.Generated,
                DatabaseModel.ExtendedConfigurationLoaded
            };
        }

        [HttpPost]
        public bool Post()
        {
            var connectionString = System.IO.File.ReadAllText(_settings.WorkingDirectory + "connection.conf");

            var builder = new SqlServerModelBuild(connectionString);
            var database = builder.Build();
            
            database.Save(_settings.WorkingDirectory + "db.json");

            return true;
        }
    }
}
