using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using digitalsign.application.Security.Interface;
using digitalsign.common.Settings;
using digitalsign.persistence.Models;
using Microsoft.IdentityModel.Tokens;

namespace digitalsign.application.Security
{
    public class JwtTokenHandler : IJwtTokenHandler
    {
        private readonly JwtSettings _jwtSettings;
        private readonly TokenValidationParameters _tokenValidationTokenParameters;

        public JwtTokenHandler(JwtSettings jwtSettings, 
            TokenValidationParameters tokenValidationParameters) 
        {
            _jwtSettings = jwtSettings;
            _tokenValidationTokenParameters = tokenValidationParameters;
        }
        public (string jwtToken, RefreshToken refreshToken) BuildJWT(ApplicationUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("id", user.Id),
            }),
                Expires = DateTime.UtcNow.Add(_jwtSettings.TokenLifeTime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var refreshToken = new RefreshToken
            {
                JwtId = token.Id,
                User = user,
                CreationDate = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddMonths(6)
            };
            return (tokenHandler.WriteToken(token), new RefreshToken
            {
                JwtId = token.Id,
                User = user,
                CreationDate = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddMonths(6)
            });
        }

        public ClaimsPrincipal GetClaimsPrincipalFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = _tokenValidationTokenParameters.IssuerSigningKey,
                    ValidateLifetime = false // we check expired tokens here
                }, out var validatedToken);
                return !IsJwtWithValidSecurityAlgorithm(validatedToken) ? null : principal;
            }
            catch
            {
                return null;
            }
        }

        private bool IsJwtWithValidSecurityAlgorithm(SecurityToken validatedToken)
        {
            return validatedToken is JwtSecurityToken jwtSecurityToken &&
            jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
