using System.Collections.Generic;
using System.Threading.Tasks;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.DTOs;

namespace SoftServe.ITAcademy.BackendDubbingProject.Administration.Core
{
    public interface IAdministrationService
    {
        Task<List<PerformanceDTO>> GetAllPerformancesAsync();

        Task<List<SpeechDTO>> GetAllSpeechesAsync();

        Task<List<AudioDTO>> GetAllAudiosAsync();

        Task<List<LanguageDTO>> GetAllLanguagesAsync();

        Task<PerformanceDTO> GetPerformanceByIdAsync(int id);

        Task<SpeechDTO> GetSpeechByIdAsync(int id);

        Task<AudioDTO> GetAudioByIdAsync(int id);

        Task<LanguageDTO> GetLanguageByIdAsync(int id);

        Task CreatePerformanceAsync(PerformanceDTO performanceDTO);

        Task CreateSpeechAsync(SpeechDTO speechDTO);

        Task CreateAudioAsync(AudioDTO audioDTO);

        Task CreateLanguageAsync(LanguageDTO languageDTO);

        Task UpdatePerformanceAsync(int id, PerformanceDTO performanceDTO);

        Task UpdateSpeechAsync(int id, SpeechDTO speechDTO);

        Task UpdateAudioAsync(int id, AudioDTO audioDTO);

        Task UpdateLanguageAsync(int id, LanguageDTO languageDTO);

        Task DeletePerformanceAsync(int id);

        Task DeleteSpeechAsync(int id);

        Task DeleteLanguageAsync(int id);

        Task DeleteAudio(int id);

        Task DeleteFileAsync(int id);

        void DeleteAudioFiles(IEnumerable<string> fileNames);

        Task<List<LanguageDTO>> GetLanguagesByPerformanceIdAsync(int id);

        Task<List<SpeechDTO>> GetSpeechesByPerformanceIdAsync(int id);

        Task<List<AudioDTO>> GetAudiosBySpeechIdAsync(int id);

        Task UploadAudioAsync(AudioFileDTO audioFileDTO);

        Task UploadWaitingAudioAsync(AudioFileDTO audioFileDTO);
    }
}