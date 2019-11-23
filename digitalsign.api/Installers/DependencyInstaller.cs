using System;
using digitalsign.application.Security;
using digitalsign.application.Security.Interface;
using digitalsign.application.Services;
using digitalsign.application.Services.Interface;
using digitalsign.persistence.Repository;
using digitalsign.persistence.Repository.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace digitalsign_api.Installers
{
    public class DependencyInstaller : IInstaller
    {
        public void InstallService(IServiceCollection services, IConfiguration configuration, IServiceProvider serviceProvider)
        {
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<IJwtTokenHandler, JwtTokenHandler>();
            services.AddScoped<IIdentityService, IdentityService>();
        }
    }
}
