using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Scaffolder.API.Application;
using Scaffolder.Core.Meta;

namespace Scaffolder.API.Controllers
{
    [Route("[controller]")]
    public class FilesController : Scaffolder.API.Application.ControllerBase
    {
        public FilesController(IOptions<AppSettings> settings)
            : base(settings)
        {
        }

        [HttpGet]
        public byte[] Get()
        {
           throw new NotImplementedException();
        }

        [HttpPost]
        public dynamic Post([FromBody]Payload payload)
        {
            throw new NotImplementedException();
        }
    }
}
