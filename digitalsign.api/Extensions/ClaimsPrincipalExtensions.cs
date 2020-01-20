using System;
using System.Security.Claims;

namespace digitalsign_api.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static Guid GetUserId(this ClaimsPrincipal principal)
        {
            if (principal == null) throw new ArgumentNullException(nameof(principal));
            var claim = principal.FindFirstValue(ClaimTypes.NameIdentifier);
            return Guid.Parse(claim);
        }
    }
}
