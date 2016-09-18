using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Scaffolder.API.Application
{
    public class ControllerBase : Controller
    {
        protected Scaffolder.Core.Base.Database Database { get; private set; }

        private readonly AppSettings _settings;

        public ControllerBase(IOptions<AppSettings> settings)
        {
            _settings = settings.Value;

            var configurationFilePath = _settings.ConfigurationFilePath;
            var extendedConfigurationFilePath = _settings.ExtendedConfigurationFilePath;

            Database = Scaffolder.Core.Base.Database.Load(configurationFilePath, extendedConfigurationFilePath);
        }
    }
}
