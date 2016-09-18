using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Scaffolder.API.Application;

namespace Scaffolder.API.Controllers
{
    [Route("[controller]")]
    public class DatabaseController : Scaffolder.API.Application.ControllerBase
    {
      

        [HttpGet]
        public dynamic Get()
        {
            return new 
            {
                Database.Name,
                Database.Title,
                Database.Description,
                Database.Generated,
                Database.ExtendedConfigurationLoaded
            };
        }


        public DatabaseController(IOptions<AppSettings> settings) : base(settings)
        {
        }
    }
}
