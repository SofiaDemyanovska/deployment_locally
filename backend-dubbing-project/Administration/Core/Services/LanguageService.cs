using System.Linq;
using System.Threading.Tasks;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Entities;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Interfaces;

namespace SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Services
{
    internal class LanguageService : GenericService<Language>, ILanguageService
    {
        private readonly IAudioService _audioService;

        public LanguageService(IRepository<Language> repository, IAudioService audioService)
            : base(repository)
        {
            _audioService = audioService;
        }

        public override async Task DeleteAsync(int id)
        {
            var language = await Repository.GetByIdWithChildrenAsync(id, "Audios");

            if (language == null)
                return;

            var namesList = language.Audios.Select(audio => audio.FileName).AsEnumerable();

            _audioService.DeleteAudioFiles(namesList);

            await Repository.DeleteAsync(language);
        }
    }
}