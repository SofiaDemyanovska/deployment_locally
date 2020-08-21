using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.DTOs;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Entities;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Interfaces;

namespace SoftServe.ITAcademy.BackendDubbingProject.Administration.Core
{
    internal class AdministrationService : IAdministrationService
    {
        private readonly IPerformanceService _performanceService;
        private readonly ISpeechService _speechService;
        private readonly IAudioService _audioService;
        private readonly ILanguageService _languageService;
        private readonly IMapper _mapper;

        public AdministrationService(
            IPerformanceService performanceService,
            ISpeechService speechService,
            IAudioService audioService,
            ILanguageService languageService,
            IMapper mapper)
        {
            _performanceService = performanceService;
            _speechService = speechService;
            _audioService = audioService;
            _languageService = languageService;
            _mapper = mapper;
        }

        public async Task<List<PerformanceDTO>> GetAllPerformancesAsync()
        {
            var performances = await _performanceService.GetAllAsync();

            return _mapper.Map<List<Performance>, List<PerformanceDTO>>(performances);
        }

        public async Task<List<SpeechDTO>> GetAllSpeechesAsync()
        {
            var speeches = await _speechService.GetAllAsync();

            return _mapper.Map<List<Speech>, List<SpeechDTO>>(speeches);
        }

        public async Task<List<AudioDTO>> GetAllAudiosAsync()
        {
            var audios = await _audioService.GetAllAsync();

            return _mapper.Map<List<Audio>, List<AudioDTO>>(audios);
        }

        public async Task<List<LanguageDTO>> GetAllLanguagesAsync()
        {
            var languages = await _languageService.GetAllAsync();

            return _mapper.Map<List<Language>, List<LanguageDTO>>(languages);
        }

        public async Task<PerformanceDTO> GetPerformanceByIdAsync(int id)
        {
            var performance = await _performanceService.GetByIdAsync(id);

            return _mapper.Map<Performance, PerformanceDTO>(performance);
        }

        public async Task<SpeechDTO> GetSpeechByIdAsync(int id)
        {
            var speech = await _speechService.GetByIdAsync(id);

            return _mapper.Map<Speech, SpeechDTO>(speech);
        }

        public async Task<AudioDTO> GetAudioByIdAsync(int id)
        {
            var audio = await _audioService.GetByIdAsync(id);

            return _mapper.Map<Audio, AudioDTO>(audio);
        }

        public async Task<LanguageDTO> GetLanguageByIdAsync(int id)
        {
            var language = await _languageService.GetByIdAsync(id);

            return _mapper.Map<Language, LanguageDTO>(language);
        }

        public async Task CreatePerformanceAsync(PerformanceDTO performanceDTO)
        {
            var performance = _mapper.Map<PerformanceDTO, Performance>(performanceDTO);

            await _performanceService.CreateAsync(performance);

            performanceDTO.Id = performance.Id;
        }

        public async Task CreateSpeechAsync(SpeechDTO speechDTO)
        {
            var speech = _mapper.Map<SpeechDTO, Speech>(speechDTO);

            await _speechService.CreateAsync(speech);

            speechDTO.Id = speech.Id;
        }

        public async Task CreateAudioAsync(AudioDTO audioDTO)
        {
            var audio = _mapper.Map<AudioDTO, Audio>(audioDTO);

            await _audioService.CreateAsync(audio);

            audioDTO.Id = audio.Id;
        }

        public async Task CreateLanguageAsync(LanguageDTO languageDTO)
        {
            var language = _mapper.Map<LanguageDTO, Language>(languageDTO);

            await _languageService.CreateAsync(language);

            languageDTO.Id = language.Id;
        }

        public async Task UpdatePerformanceAsync(int id, PerformanceDTO performanceDTO)
        {
            var performance = _mapper.Map<PerformanceDTO, Performance>(performanceDTO);

            await _performanceService.UpdateAsync(id, performance);
        }

        public async Task UpdateSpeechAsync(int id, SpeechDTO speechDTO)
        {
            var speech = _mapper.Map<SpeechDTO, Speech>(speechDTO);

            await _speechService.UpdateAsync(id, speech);
        }

        public async Task UpdateAudioAsync(int id, AudioDTO audioDTO)
        {
            var audio = _mapper.Map<AudioDTO, Audio>(audioDTO);

            await _audioService.UpdateAsync(id, audio);
        }

        public async Task UpdateLanguageAsync(int id, LanguageDTO languageDTO)
        {
            var language = _mapper.Map<LanguageDTO, Language>(languageDTO);

            await _languageService.UpdateAsync(id, language);
        }

        public async Task DeletePerformanceAsync(int id)
        {
            await _performanceService.DeleteAsync(id);
        }

        public async Task DeleteSpeechAsync(int id)
        {
            await _speechService.DeleteAsync(id);
        }

        public async Task DeleteLanguageAsync(int id)
        {
            await _languageService.DeleteAsync(id);
        }

        public async Task DeleteAudio(int id)
        {
            await _audioService.DeleteAsync(id);
        }

        public void DeleteAudioFiles(IEnumerable<string> fileNames)
        {
            _audioService.DeleteAudioFiles(fileNames);
        }

        public async Task DeleteFileAsync(int id)
        {
            await _audioService.DeleteFileAsync(id);
        }

        public async Task<List<LanguageDTO>> GetLanguagesByPerformanceIdAsync(int id)
        {
            var languages = await _performanceService.GetLanguagesAsync(id);

            return _mapper.Map<List<Language>, List<LanguageDTO>>(languages);
        }

        public async Task<List<SpeechDTO>> GetSpeechesByPerformanceIdAsync(int id)
        {
            var listOfSpeeches = await _performanceService.GetChildrenByIdAsync(id);

            return _mapper.Map<List<Speech>, List<SpeechDTO>>(listOfSpeeches);
        }

        public async Task<List<AudioDTO>> GetAudiosBySpeechIdAsync(int id)
        {
            var listOfAudios = await _speechService.GetChildrenByIdAsync(id);

            return _mapper.Map<List<Audio>, List<AudioDTO>>(listOfAudios);
        }

        public async Task UploadAudioAsync(AudioFileDTO audioFileDTO)
        {
            var audio = _mapper.Map<AudioFileDTO, Audio>(audioFileDTO);

            await _audioService.UploadAsync(audio, audioFileDTO);
        }

        public async Task UploadWaitingAudioAsync(AudioFileDTO audioFileDTO)
        {
            var audio = _mapper.Map<AudioFileDTO, Audio>(audioFileDTO);

            audio.FileName = "waiting.mp3";

            await _audioService.UploadAsync(audio, audioFileDTO);
        }
    }
}