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
            services
                .AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
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
