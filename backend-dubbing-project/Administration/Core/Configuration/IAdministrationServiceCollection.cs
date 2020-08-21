using Microsoft.Extensions.DependencyInjection;

namespace SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Configuration
{
    public interface IAdministrationServiceCollection
    {
        void RegisterDependencies(IServiceCollection services);
    }
}