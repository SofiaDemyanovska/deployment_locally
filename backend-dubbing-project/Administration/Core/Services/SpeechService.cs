using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Entities;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Interfaces;

namespace SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Services
{
    internal class SpeechService : GenericService<Speech>, ISpeechService
    {
        private readonly IAudioService _audioService;

        public SpeechService(IRepository<Speech> repository, IAudioService audioService)
            : base(repository)
        {
            _audioService = audioService;
        }

        public async Task<List<Audio>> GetChildrenByIdAsync(int id)
        {
            var speech = await Repository.GetByIdWithChildrenAsync(id, "Audios");

            return speech?.Audios.ToList();
        }

        public override async Task DeleteAsync(int id)
        {
            var speech = await Repository.GetByIdWithChildrenAsync(id, "Audios");

            if (speech == null)
                return;

            await _audioService.DeleteAsync(id);

            await Repository.DeleteAsync(speech);
        }
    }
}