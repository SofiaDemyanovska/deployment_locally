using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Entities;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Interfaces;

namespace SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Services
{
    internal class PerformanceService : GenericService<Performance>, IPerformanceService
    {
        private readonly IAudioService _audioService;
        private readonly ISpeechService _speechService;
        private readonly ILanguageService _languageService;

        public PerformanceService(
            IRepository<Performance> repository,
            IAudioService audioService,
            ISpeechService speechService,
            ILanguageService languageService)
            : base(repository)
        {
            _audioService = audioService;
            _speechService = speechService;
            _languageService = languageService;
        }

        public async Task<List<Speech>> GetChildrenByIdAsync(int id)
        {
            var performance = await Repository.GetByIdWithChildrenAsync(id, "Speeches");

            return performance?.Speeches.ToList();
        }

        public override async Task DeleteAsync(int id)
        {
            var performance = await Repository.GetByIdWithChildrenAsync(id, "Speeches");

            if (performance == null)
                return;

            foreach (var speech in performance.Speeches)
            {
                await _audioService.DeleteAsync(speech.Id);
            }

            await Repository.DeleteAsync(performance);
        }

        public async Task<List<Language>> GetLanguagesAsync(int id)
        {
            var speeches = await GetChildrenByIdAsync(id);

            var speech = speeches.First();

            var audios = await _speechService.GetChildrenByIdAsync(speech.Id);

            var languagesIds = audios.Select(audio => audio.LanguageId).ToList();

            var listOfLanguages = new List<Language>();

            foreach (var langId in languagesIds)
            {
                listOfLanguages.Add(await _languageService.GetByIdAsync(langId));
            }

            return listOfLanguages;
        }
    }
}