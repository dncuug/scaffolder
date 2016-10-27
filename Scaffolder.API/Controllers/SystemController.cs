using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Scaffolder.API.Application;
using System.Diagnostics;
using System.IO;
using Scaffolder.Core.Engine;

namespace Scaffolder.API.Controllers
{
    [Authorize(ActiveAuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class SystemController : Scaffolder.API.Application.ControllerBase
    {
        public SystemController(IOptions<AppSettings> settings)
            : base(settings)
        {
        }

        [HttpGet]
        public dynamic Get()
        {
            return new
            {
                ApplicationContext.Configuration.Name,
                ApplicationContext.Configuration.Title,
                ApplicationContext.Configuration.Description,
                ApplicationContext.Schema.Generated,
                ApplicationContext.Schema.ExtendedConfigurationLoaded
            };
        }

        [HttpPost]
        public bool Post()
        {
            var engine = new Engine(ApplicationContext.Configuration.ConnectionString, ApplicationContext.Configuration.Engine);
            var builder = engine.CreateSchemaBuilder();
            var schema = builder.Build();

            schema.Save(Path.Combine(ApplicationContext.Location, "db.json"));

            return true;
        }

        [HttpGet("restart")]
        public IActionResult Restart()
        {
            var command = ApplicationContext.Configuration.ApplicationRestartCommand;
            var executor = new Executor();
            var result = executor.Execute(command);

            if (result)
            {
                return Ok();
            }

            return StatusCode(500, "Error detecting during executing restart command");
        }
    }
}
