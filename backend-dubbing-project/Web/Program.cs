using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Configuration;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Infrastructure.Configuration;

namespace SoftServe.ITAcademy.BackendDubbingProject.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                    services.AddTransient<IAdministrationServiceCollection, AdministrationServiceCollection>())
                .ConfigureServices(services =>
                    services.AddTransient<IInfrastructureServiceCollection, InfrastructureServiceCollection>())
                .UseStartup<Startup>();
    }
}