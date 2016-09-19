using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Scaffolder.API.Application;

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
    }
}
