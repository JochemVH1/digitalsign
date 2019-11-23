using System;
using digitalsign.persistence.Context;
using digitalsign.persistence.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace digitalsign_api.Installers
{
    public class DatabaseInstaller : IInstaller
    {
        public void InstallService(IServiceCollection services, IConfiguration configuration, IServiceProvider serviceProvider)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
               options.UseSqlite(configuration.GetConnectionString("DefaultConnection"), 
                   contextOptionsBuilder => contextOptionsBuilder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
            
            services.AddDefaultIdentity<ApplicationUser>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
        }
    }
}
