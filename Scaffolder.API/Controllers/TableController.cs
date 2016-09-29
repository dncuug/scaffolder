using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Scaffolder.API.Application;
using Scaffolder.Core.Meta;
using System.Collections.Generic;
using System.Linq;

namespace Scaffolder.API.Controllers
{
    [Route("[controller]")]
    public class TableController : Scaffolder.API.Application.ControllerBase
    {
        public TableController(IOptions<AppSettings> settings)
            : base(settings)
        {
        }

        [HttpGet]
        public IEnumerable<dynamic> Get()
        {
            return Schema.Tables.Select(o => new
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
            return Schema.GetTable(name);
        }
    }
}
