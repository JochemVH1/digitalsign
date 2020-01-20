using System;
using digitalsign.application.Services;
using digitalsign.application.Services.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace digitalsign_api.Installers
{
    public class DependencyInstaller : IInstaller
    {
        public void InstallService(IServiceCollection services, IConfiguration configuration, IServiceProvider serviceProvider)
        {
            services.AddScoped<IMessageService, MessageService>();
        }
    }
}
