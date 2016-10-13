using System;

namespace Scaffolder.API.Application
{
    public class AppSettings
    {
        public String WorkingDirectory { get; set; }

        public String SecretKey { get; set; }

        public const string Audience = "SystemAudience";

        public const string Issuer = "SystemIssuer";
    }
}
