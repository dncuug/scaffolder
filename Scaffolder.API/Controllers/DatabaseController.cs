using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Scaffolder.API.Application;

namespace Scaffolder.API.Controllers
{
    [Authorize(ActiveAuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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

       
    }
}
