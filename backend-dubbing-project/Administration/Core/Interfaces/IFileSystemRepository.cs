using System.Collections.Generic;
using System.Threading.Tasks;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Entities;

namespace SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Interfaces
{
    internal interface IFileSystemRepository
    {
        Task WriteToFileSystemAsync(Audio audio, string path);

        void Delete(string folderPath, IEnumerable<string> names);
    }
}