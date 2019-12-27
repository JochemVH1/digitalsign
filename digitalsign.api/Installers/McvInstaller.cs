using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace digitalsign_api.Installers
{
    public class MvcInstaller : IInstaller
    {
        public void InstallService(IServiceCollection services, IConfiguration configuration, IServiceProvider serviceProvider)
        {
            services.AddControllers();
        }
    }
}
