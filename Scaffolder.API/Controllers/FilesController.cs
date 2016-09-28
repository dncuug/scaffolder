using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Scaffolder.API.Application;
using Scaffolder.Core.Storage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

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
        public byte[] Get(string name)
        {
            Storage storage = Storage.GetStorage(Configuration.StorageConfiguration.Type, Configuration.StorageConfiguration.Connection);
            return storage.Get(name);
        }

        [HttpPost]
        public async Task<IActionResult> Post(ICollection<IFormFile> files)
        {
            var storage = Storage.GetStorage(Configuration.StorageConfiguration.Type, Configuration.StorageConfiguration.Connection);

            var file = files.FirstOrDefault();
            
            if (file != null && file.Length > 0)
            {
                String fileName;

                using (var fileStream = new MemoryStream())
                {
                    await file.CopyToAsync(fileStream);
                    fileName = storage.Upload(fileStream.ToArray());
                }

                return Created(Configuration.StorageConfiguration.Url + fileName, new object());
                //return Ok(_configuration.StorageConfiguration.Url + fileName);
            }

            return BadRequest();
        }
    }
}
