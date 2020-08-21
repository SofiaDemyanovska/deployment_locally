using System.Collections.Generic;
using System.Threading.Tasks;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Entities;

namespace SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Interfaces
{
    internal interface ILanguageService
    {
        Task<List<Language>> GetAllAsync();

        Task<Language> GetByIdAsync(int id);

        Task CreateAsync(Language entity);

        Task UpdateAsync(int id, Language newEntity);

        Task DeleteAsync(int id);
    }
}