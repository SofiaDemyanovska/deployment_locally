using System.Collections.Generic;

namespace SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Entities
{
    internal class Speech : BaseEntity
    {
        public int Order { get; set; }

        public string Text { get; set; }

        public int Duration { get; set; }

        public int PerformanceId { get; set; }

        public Performance Performance { get; set; }

        public ICollection<Audio> Audios { get; set; }
    }
}