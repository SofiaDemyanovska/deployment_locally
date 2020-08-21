using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Interfaces;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Mapping;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Services;

namespace SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Configuration
{
    public class AdministrationServiceCollection : IAdministrationServiceCollection
    {
        public void RegisterDependencies(IServiceCollection services)
        {
            var mappingConfiguration = new MapperConfiguration(conf => { conf.AddProfile<MappingProfile>(); });
            var mapper = mappingConfiguration.CreateMapper();
            services.AddSingleton(mapper);

            services.AddScoped<IAdministrationService, AdministrationService>();

            services.AddScoped<IAudioService, AudioService>();
            services.AddScoped<ILanguageService, LanguageService>();
            services.AddScoped<IPerformanceService, PerformanceService>();
            services.AddScoped<ISpeechService, SpeechService>();
        }
    }
}