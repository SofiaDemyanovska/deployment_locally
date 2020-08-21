using System.Collections.Generic;

namespace SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Entities
{
    internal class Language : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<Audio> Audios { get; set; }
    }
}