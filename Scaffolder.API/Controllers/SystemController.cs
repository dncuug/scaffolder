using System.Diagnostics;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Scaffolder.API.Application;

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

        [HttpGet]
        [Route("api/system/restart")]
        public IActionResult Restart()
        {
            var cmd = Settings.ApplicationRestartCommand;

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
