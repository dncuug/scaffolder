using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using NLog.Extensions.Logging;
using Scaffolder.API.Application;
using Scaffolder.API.Application.Security;

namespace Scaffolder.API
{
    public class Startup
    {
        private static String _workingDirectory;
        private static String _secretKey;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            //var builder = new ConfigurationBuilder()
            //    .SetBasePath(env.ContentRootPath)
            //    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            //    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
            //    .AddEnvironmentVariables();

            //Configuration = builder.Build();

        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var mvc = services.AddMvc();

            mvc.AddJsonOptions(opt =>
            {
                opt.SerializerSettings.Formatting = Formatting.Indented;
            });

            var settings = Configuration.GetSection("AppSettings");

            _workingDirectory = settings["WorkingDirectory"];
            _secretKey = settings["SecretKey"];

            services.Configure<AppSettings>(settings);

            //Add Cors support to the service
            services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));

            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //add NLog to ASP.NET Core
            loggerFactory.AddNLog();

            //needed for non-NETSTANDARD platforms: configure nlog.config in your project root
            //env.ConfigureNLog("nlog.config");

            app.UseDefaultFiles();
            app.UseStaticFiles();

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            //Add CORS middleware before MVC
            app.UseCors("CorsPolicy");

            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_secretKey));

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

            //app.UseJwtBearerAuthentication(new JwtBearerOptions
            //{
            //    AutomaticAuthenticate = true,
            //    AutomaticChallenge = true,
            //    TokenValidationParameters = tokenValidationParameters,
            //    SaveToken = true
            //});

            var options = new TokenProviderOptions
            {
                Issuer = AppSettings.Issuer,
                Audience = AppSettings.Audience,
                SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256),
                WorkingDirectory = _workingDirectory
            };
           
            app.UseMiddleware<TokenProviderMiddleware>(Options.Create(options));

            app.UseMvc();
        }
    }
}
