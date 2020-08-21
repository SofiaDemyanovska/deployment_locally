using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Entities;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Interfaces;

namespace SoftServe.ITAcademy.BackendDubbingProject.Administration.Infrastructure.FileSystem
{
    internal class FileSystemRepository : IFileSystemRepository
    {
        public async Task WriteToFileSystemAsync(Audio audio, string path)
        {
            await File.WriteAllBytesAsync(path, audio.AudioFile);
        }

        public void Delete(string folderPath, IEnumerable<string> names)
        {
            Parallel.ForEach(names, name =>
            {
                var pathToAudio = Path.Combine(folderPath, name);

                File.Delete(pathToAudio);
            });
        }
    }
}