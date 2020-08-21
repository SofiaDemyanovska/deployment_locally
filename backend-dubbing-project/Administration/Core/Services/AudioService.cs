using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.DTOs;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Entities;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Interfaces;
using File = System.IO.File;

namespace SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Services
{
    internal class AudioService : GenericService<Audio>, IAudioService
    {
        private readonly IFileSystemRepository _fileSystemRepository;

        private readonly IRepository<Speech> _speechRepository;

        private readonly string _audioFilesFolderPath = Path.GetFullPath("../Web/AudioFiles/");

        public AudioService(
            IRepository<Audio> repository,
            IRepository<Speech> speechRepository,
            IFileSystemRepository fileSystemRepository)
            : base(repository)
        {
            _speechRepository = speechRepository;
            _fileSystemRepository = fileSystemRepository;
        }

        public override async Task CreateAsync(Audio entity)
        {
            var audio = await ChangeName(entity);

            audio = await ChangeDuration(entity);

            entity.Id = default(int);

            await Repository.AddAsync(audio);
        }

        public async Task UploadAsync(Audio audio, AudioFileDTO audioFileDTO)
        {
            using (var memStream = new MemoryStream())
            {
                audioFileDTO.File.CopyTo(memStream);

                audio.AudioFile = memStream.ToArray();

                if (audio.FileName != "waiting.mp3")
                {
                    audio.FileName = audioFileDTO.File.FileName;
                }
            }

            var path = Path.Combine(_audioFilesFolderPath, audio.FileName);

            await _fileSystemRepository.WriteToFileSystemAsync(audio, path);
        }

        public override async Task UpdateAsync(int id, Audio newEntity)
        {
            var oldEntity = await Repository.GetByIdAsync(id);

            if (oldEntity == null)
                throw new Exception($"{typeof(Audio)} entity with ID: {id} doesn't exist.");

            newEntity = await ChangeDuration(newEntity);

            if (oldEntity.FileName != newEntity.FileName)
            {
                var fileToRemovePath = Path.Combine(_audioFilesFolderPath, oldEntity.FileName);
                File.Delete(fileToRemovePath);

                newEntity = await ChangeName(newEntity);

                await Repository.UpdateAsync(oldEntity, newEntity);
            }
        }

        public override async Task DeleteAsync(int id)
        {
            var speech = await _speechRepository.GetByIdWithChildrenAsync(id, "Audios");

            var namesList = speech.Audios.Select(audio => audio.FileName).AsEnumerable();

            DeleteAudioFiles(namesList);
        }

        public async Task DeleteFileAsync(int id)
        {
            var audio = await Repository.GetByIdAsync(id);

            await Repository.DeleteAsync(audio);
        }

        public void DeleteAudioFiles(IEnumerable<string> namesList)
        {
            _fileSystemRepository.Delete(_audioFilesFolderPath, namesList);
        }

        private async Task<Audio> ChangeName(Audio entity)
        {
            var speech = await _speechRepository.GetByIdAsync(entity.SpeechId);

            var newFileName = $"{speech.PerformanceId}_{entity.SpeechId}_{entity.LanguageId}.mp3";
            var oldPath = Path.Combine(_audioFilesFolderPath, entity.FileName);
            var newPath = Path.Combine(_audioFilesFolderPath, newFileName);
            File.Move(oldPath, newPath);

            entity.FileName = newFileName;

            return entity;
        }

        private async Task<Audio> ChangeDuration(Audio entity)
        {
            var speech = await _speechRepository.GetByIdAsync(entity.SpeechId);

            var file = TagLib.File.Create(_audioFilesFolderPath + entity.FileName);
            var duration = file.Properties.Duration;
            entity.Duration = Convert.ToInt32(duration.TotalSeconds);

            if (speech.Duration >= entity.Duration)
                return entity;

            var newSpeech = new Speech {Id = speech.Id, Duration = entity.Duration};

            await _speechRepository.UpdateFieldAsync(newSpeech, "Duration");

            return entity;
        }
    }
}