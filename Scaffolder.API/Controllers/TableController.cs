using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Scaffolder.Core.Base;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Options;
using Scaffolder.API.Application;

namespace Scaffolder.API.Controllers
{
    [Route("[controller]")]
    public class TableController : Scaffolder.API.Application.ControllerBase
    {

        [HttpGet]
        public IEnumerable<BaseObject> Get()
        {
            return Database.Tables.Select(o => new BaseObject
            {
                Name = o.Name,
                Title = o.Title,
                Description = o.Description
            }).ToList();
        }

        [HttpGet("{name}")]
        public Table Get(string name)
        {
            return Database.Tables.SingleOrDefault(o => o.Name.ToLower() == name.ToLower());
        }

        public TableController(IOptions<AppSettings> settings) : base(settings)
        {
        }
    }
}
