using System;
using digitalsign.common.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace digitalsign_api.Installers
{
    public class SettingsInstaller : IInstaller
    {
        public void InstallService(IServiceCollection services, IConfiguration configuration, IServiceProvider serviceProvider)
        {
            var jwtSettings = new JwtSettings();
            configuration.Bind(nameof(JwtSettings), jwtSettings);
            services.AddSingleton(jwtSettings);
        }
    }
}
