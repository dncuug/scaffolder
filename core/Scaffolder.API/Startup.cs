using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Scaffolder.API.Application;
using Scaffolder.API.Application.Security;
using System;
using System.Linq;
using System.Text;

namespace Scaffolder.API
{
    public class Startup
    {
        private static String _workingDirectory;

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            var mvc = services.AddMvc();

            mvc.AddJsonOptions(opt =>
            {
                opt.SerializerSettings.Formatting = Formatting.Indented;
            });

            var appSettings = Configuration.GetSection("AppSettings");
            var workingDirectorySection = appSettings.GetSection("WorkingDirectory");
            _workingDirectory = workingDirectorySection.Value;

            services.Configure<AppSettings>(appSettings);

            var s = services.Select(o => o.ServiceType == typeof(AppSettings)).ToList();

            //Add Cors support to the service
            services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="loggerFactory"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseDefaultFiles();
            app.UseStaticFiles();

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            //Add CORS middleware before MVC
            app.UseCors("CorsPolicy");

            var configurationPath = _workingDirectory + "configuration.json";
            var configuration = Scaffolder.Core.Meta.Configuration.Load(configurationPath);

            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration.SecretKey));

            var tokenValidationParameters = new TokenValidationParameters
            {
                // The signing key must match!
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,

                // Validate the JWT Issuer (iss) claim
                ValidateIssuer = true,
                ValidIssuer = AppSettings.Issuer,

                // Validate the JWT Audience (aud) claim
                ValidateAudience = true,
                ValidAudience = AppSettings.Audience,

                // Validate the token expiry
                ValidateLifetime = true,

                // If you want to allow a certain amount of clock drift, set that here:
                ClockSkew = TimeSpan.Zero
            };

            app.UseJwtBearerAuthentication(new JwtBearerOptions
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                TokenValidationParameters = tokenValidationParameters
            });

            var options = new TokenProviderOptions
            {
                Issuer = AppSettings.Issuer,
                Audience = AppSettings.Audience,
                SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256),
                Configuration = configuration
            };

            app.UseMiddleware<TokenProviderMiddleware>(Options.Create(options));

            app.UseMvc();
        }
    }
}
