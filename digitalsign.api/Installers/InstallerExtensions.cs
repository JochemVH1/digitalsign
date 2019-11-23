using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace digitalsign_api.Installers
{
    public static class InstallerExtensions
    {
        public static void InstallerServicesInAssembly(this IServiceCollection services, IConfiguration configuration, IServiceProvider serviceProvider)
        {
            
            var installers = typeof(Startup).Assembly.ExportedTypes
                .Where(x =>
                typeof(IInstaller).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .Select(Activator.CreateInstance)
                .Cast<IInstaller>()
                .ToArray();
                
            int[] installOrder = new int[installers.Length ];
            int currentIndex = 4;
            int index = 0;
            installers.ToList()
                .ForEach(i => {
                    string typeName = i.GetType().Name;
                    if(typeName.Equals("MvcInstaller")) {
                        installOrder[index] = 1;
                    }else if(typeName.Equals("SettingsInstaller")) {
                        installOrder[index] = 2;
                    }else if(typeName.Equals("DependencyInstaller")) {
                        installOrder[index] = 3;
                    }else {
                        installOrder[index] = currentIndex;
                        currentIndex++;
                    }                   
                    index++;
                });
            Array.Sort(installOrder, installers);
            foreach (IInstaller installer in installers) {
                installer.InstallService(services, configuration, serviceProvider);
            }
        }
    }
}
