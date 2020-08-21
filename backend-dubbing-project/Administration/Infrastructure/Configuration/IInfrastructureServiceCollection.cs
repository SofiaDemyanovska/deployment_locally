using Microsoft.Extensions.DependencyInjection;

namespace SoftServe.ITAcademy.BackendDubbingProject.Administration.Infrastructure.Configuration
{
    public interface IInfrastructureServiceCollection
    {
        void RegisterDependencies(IServiceCollection services);
    }
}