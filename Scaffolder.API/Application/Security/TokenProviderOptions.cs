using System;
using Microsoft.IdentityModel.Tokens;
using Scaffolder.Core.Meta;

namespace Scaffolder.API.Application.Security
{
    public class TokenProviderOptions
    {
        public string Path { get; set; } = "/token";

        public string Issuer { get; set; }

        public string Audience { get; set; }

        public TimeSpan Expiration { get; set; } = TimeSpan.FromMinutes(5);

        public SigningCredentials SigningCredentials { get; set; }

        public Configuration Configuration { get; set; }
    }
}
