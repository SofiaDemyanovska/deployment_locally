using System.Collections.Generic;
using System.Threading.Tasks;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.DTOs;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Entities;

namespace SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Interfaces
{
    internal interface IAudioService
    {
        Task<List<Audio>> GetAllAsync();

        Task<Audio> GetByIdAsync(int id);

        Task CreateAsync(Audio entity);

        Task UploadAsync(Audio audio, AudioFileDTO audioFileDTO);

        Task UpdateAsync(int id, Audio newEntity);

        Task DeleteAsync(int id);

        Task DeleteFileAsync(int id);

        void DeleteAudioFiles(IEnumerable<string> namesList);
    }
}