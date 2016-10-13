using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Scaffolder.API.Application;
using Scaffolder.Core.Meta;
using System.Collections.Generic;
using System.Linq;

namespace Scaffolder.API.Controllers
{
    [Authorize(ActiveAuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class TableController : Scaffolder.API.Application.ControllerBase
    {
        public TableController(IOptions<AppSettings> settings)
            : base(settings)
        {
        }

        [HttpGet]
        public IEnumerable<dynamic> Get()
        {
            return ApplicationContext.Schema.Tables.Select(o => new
            {
                o.Name,
                o.Title,
                o.Description,
                o.ShowInList
            }).ToList();
        }

        [HttpGet("{name}")]
        public Table Get(string name)
        {
            return ApplicationContext.Schema.GetTable(name);
        }
    }
}
