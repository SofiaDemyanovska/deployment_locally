using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Configuration;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Infrastructure.Configuration;
using SoftServe.ITAcademy.BackendDubbingProject.Streaming.Core.Hubs;
using Swashbuckle.AspNetCore.Swagger;

namespace SoftServe.ITAcademy.BackendDubbingProject.Web
{
    public class Startup
    {
        private const string CorsName = "AllowAllOrigins";
        private readonly IAdministrationServiceCollection _administrationServiceCollection;
        private readonly IInfrastructureServiceCollection _infrastructureServiceCollection;

        public Startup(
            IConfiguration configuration,
            IAdministrationServiceCollection administrationServiceCollection,
            IInfrastructureServiceCollection infrastructureServiceCollection)
        {
            Configuration = configuration;
            _administrationServiceCollection = administrationServiceCollection;
            _infrastructureServiceCollection = infrastructureServiceCollection;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(
                    CorsName,
                    builder =>
                    {
                        builder
                            .SetIsOriginAllowed(host => true)
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowCredentials();
                    });
            });

            AddAdministrationServices(services);

            AddInfrastructureServices(services);

            services.AddSignalR();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info {Title = "APIs", Version = "v1"});
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddJsonOptions(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        }

        private void AddAdministrationServices(IServiceCollection services)
        {
            _administrationServiceCollection.RegisterDependencies(services);
        }

        private void AddInfrastructureServices(IServiceCollection services)
        {
            _infrastructureServiceCollection.RegisterDependencies(services);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseCors(CorsName);

            app.UseSwagger();

            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "APIs V1"));

            app.UseFileServer(new FileServerOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "AudioFiles")),
                RequestPath = "/audio",
                EnableDirectoryBrowsing = true
            });

            app.UseSignalR(options => options.MapHub<StreamHub>("/StreamHub"));

            app.UseDefaultFiles();

            app.UseStaticFiles();

            app.UseMvc();
        }
    }
}