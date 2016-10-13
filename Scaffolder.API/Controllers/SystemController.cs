using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Scaffolder.API.Application;
using System.Diagnostics;
using System.IO;

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
                ApplicationContext.Configuration.Title,
                ApplicationContext.Configuration.Description,
                ApplicationContext.Schema.Generated,
                ApplicationContext.Schema.ExtendedConfigurationLoaded
            };
        }

        [HttpPost]
        public bool Post()
        {
            var builder = GetSchemaBuilder();
            var schema = builder.Build();
            
            schema.Save(Path.Combine(ApplicationContext.Location, "db.json"));

            return true;
        }

        [HttpGet("restart")]
        public IActionResult Restart()
        {
            var cmd = ApplicationContext.Configuration.ApplicationRestartCommand;

            try
            {
                var p = Process.Start(cmd);
                p.WaitForExit();
            }
            catch
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
