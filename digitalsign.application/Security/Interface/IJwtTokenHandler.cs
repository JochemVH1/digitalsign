using System.Security.Claims;
using digitalsign.persistence.Models;

namespace digitalsign.application.Security.Interface
{
    public interface IJwtTokenHandler
    {
        ClaimsPrincipal GetClaimsPrincipalFromToken(string token);
        (string jwtToken, RefreshToken refreshToken) BuildJWT(ApplicationUser user);
    }
}
