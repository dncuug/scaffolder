using System;
using System.Security.Claims;
using System.Security.Principal;

namespace Scaffolder.API.Application.Security
{
    public class ApplicationClaimsIdentity : ClaimsIdentity
    {
        public String ConfiguratoinLocation { get; private set; }

        public ApplicationClaimsIdentity(GenericIdentity genericIdentity, Claim[] claim, String configuratoinLocation)
            :base(genericIdentity, claim)
        {
            ConfiguratoinLocation = configuratoinLocation;
        }
    }
}
