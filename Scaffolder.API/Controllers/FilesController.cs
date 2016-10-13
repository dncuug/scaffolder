using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(ActiveAuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class FilesController : Scaffolder.API.Application.ControllerBase
    {
        public FilesController(IOptions<AppSettings> settings)
            : base(settings)
        {
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Get(string name)
        {
            var link = ApplicationContext.Configuration.StorageConfiguration.Url + name;
            return Redirect(link);
        }

        [HttpPost]
        public async Task<IActionResult> Post(ICollection<IFormFile> files)
        {
            Storage storage = Storage.GetStorage(ApplicationContext.Configuration.StorageConfiguration.Type, ApplicationContext.Configuration.StorageConfiguration.Connection);

            var file = this.Request.Form.Files.FirstOrDefault();

            if (file != null && file.Length > 0)
            {
                String fileName;
                var extension = Path.GetExtension(file.FileName);

                using (var fileStream = new MemoryStream())
                {
                    await file.CopyToAsync(fileStream);
                    fileName = storage.Upload(fileStream.ToArray(), extension);
                }

                var result = new
                {
                    Name = fileName,
                    Url = ApplicationContext.Configuration.StorageConfiguration.Url + fileName
                };

                return Ok(result);
            }

            return BadRequest();
        }
    }
}
