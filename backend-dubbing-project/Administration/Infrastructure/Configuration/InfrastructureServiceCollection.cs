using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Interfaces;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Infrastructure.Database;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Infrastructure.FileSystem;

namespace SoftServe.ITAcademy.BackendDubbingProject.Administration.Infrastructure.Configuration
{
    public class InfrastructureServiceCollection : IInfrastructureServiceCollection
    {
        public void RegisterDependencies(IServiceCollection services)
        {
            const string connection = "Data Source=dubbing.db";

            services.AddDbContext<DubbingContext>(options =>
                options.UseSqlite(connection, b => b.MigrationsAssembly("Web")));

            services.AddScoped<DbContext, DubbingContext>();

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddScoped<IFileSystemRepository, FileSystemRepository>();
        }
    }
}