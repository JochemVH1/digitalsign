using System;
using digitalsign.common.Settings;
using digitalsign_api.Installers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace digitalsign_api
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IServiceProvider serviceProvider)
        {
            Configuration = configuration;
            _serviceProvider = serviceProvider;
        }

        public IConfiguration Configuration { get; }

        private readonly IServiceProvider _serviceProvider;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.InstallerServicesInAssembly(Configuration, _serviceProvider);
        }   

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseRouting();
            app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();
            
            var swaggerSettings = new SwaggerSettings();
            Configuration.GetSection(nameof(swaggerSettings)).Bind(swaggerSettings);

            app.UseSwagger(options => {
                options.RouteTemplate = swaggerSettings.JsonRoute;
            });

            app.UseSwaggerUI(options => {
                options.SwaggerEndpoint(swaggerSettings.UIEndpoint, swaggerSettings.Description);
            });


            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}
