using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(digitalsign.usermanagent.Areas.Identity.IdentityHostingStartup))]
namespace digitalsign.usermanagent.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            //builder.ConfigureServices((context, services) =>
            //{
            //    services.AddDbContext<ApplicationUserDbContext>(options =>
            //        options.UseSqlite(context.Configuration.GetConnectionString("DefaultConnection"), 
            //        contextOptionsBuilder => contextOptionsBuilder.MigrationsAssembly(typeof(ApplicationUserDbContext).Assembly.FullName)));


            //});
        }
    }
}