using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace digitalsign_api.Installers
{
    public interface IInstaller
    {
        void InstallService(IServiceCollection services, IConfiguration configuration, IServiceProvider serviceProvider);
    }
}
