using System;
using System.Text;
using digitalsign.common.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace digitalsign_api.Installers
{
    public class AuthenicationInstaller : IInstaller
    {       
        public void InstallService(IServiceCollection services, IConfiguration configuration, IServiceProvider serviceProvider)
        {
            var sp = services.BuildServiceProvider();
            var JwtSettings = sp.GetService<JwtSettings>();
            //storing token validation parameters
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(JwtSettings.Secret)),
                ValidateAudience = false,
                ValidateIssuer = false,
                RequireExpirationTime = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            services.AddSingleton(tokenValidationParameters);

            //settings the authentication schema to use json webtokens
            services.AddAuthentication(x => {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x => {
                x.SaveToken = true;
                x.TokenValidationParameters = tokenValidationParameters; 
            });
        }
    }
}
