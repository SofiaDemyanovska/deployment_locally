using System.Collections.Generic;
using System.Threading.Tasks;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Entities;

namespace SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Interfaces
{
    internal interface IPerformanceService
    {
        Task<Performance> GetByIdAsync(int id);

        Task<List<Speech>> GetChildrenByIdAsync(int id);

        Task<List<Performance>> GetAllAsync();

        Task CreateAsync(Performance entity);

        Task UpdateAsync(int id, Performance newEntity);

        Task DeleteAsync(int id);

        Task<List<Language>> GetLanguagesAsync(int id);
    }
}