using Microsoft.IdentityModel.Tokens;
using System;

namespace Scaffolder.API.Application.Security
{
    public class TokenProviderOptions
    {
        public TokenProviderOptions()
        {
            Path = "/api/token";
            Expiration = TimeSpan.FromMinutes(5);
        }

        public string Path { get; set; }

        public string Issuer { get; set; }

        public string Audience { get; set; }

        public TimeSpan Expiration { get; set; }

        public SigningCredentials SigningCredentials { get; set; }

        public String WorkingDirectory { get; set; }
    }
}
