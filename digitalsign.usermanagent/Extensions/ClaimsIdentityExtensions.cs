using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace digitalsign.usermanagent.Extensions
{
    public static class ClaimsIdentityExtensions
    {
        public static Guid GetUserId(this ClaimsPrincipal principal)
        {
            if (principal == null) throw new ArgumentNullException(nameof(principal));
            var claim = principal.FindFirstValue(JwtRegisteredClaimNames.Sub);
            return Guid.Parse(claim);
        }
    }
}
