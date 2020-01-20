using System;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace digitalsign_api.Installers
{
    public class AuthenicationInstaller : IInstaller
    {       
        public void InstallService(IServiceCollection services, IConfiguration configuration, IServiceProvider serviceProvider)
        {
            //var sp = services.BuildServiceProvider();
            //var JwtSettings = sp.GetService<JwtSettings>();
            ////storing token validation parameters
            //var tokenValidationParameters = new TokenValidationParameters
            //{
            //    ValidateIssuerSigningKey = true,
            //    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(JwtSettings.Secret)),
            //    ValidateAudience = false,
            //    ValidateIssuer = false,
            //    RequireExpirationTime = false,
            //    ValidateLifetime = true,
            //    ClockSkew = TimeSpan.Zero
            //};

            //services.AddSingleton(tokenValidationParameters);

            ////settings the authentication schema to use json webtokens
            //services.AddAuthentication(x => {
            //    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            //    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //})
            //.AddJwtBearer(x => {
            //    x.SaveToken = true;
            //    x.TokenValidationParameters = tokenValidationParameters; 
            //});
            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication("Bearer", options =>
                {
                    options.Authority = "http://localhost:5050";
                    options.RequireHttpsMetadata = false;
                    options.ApiName = "DigitalSign";
                    options.ApiSecret = "secret";
                    options.EnableCaching = true;
                });
        }
    }
}
