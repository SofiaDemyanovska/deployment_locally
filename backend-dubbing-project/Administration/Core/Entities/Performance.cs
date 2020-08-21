using System.Collections.Generic;

namespace SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Entities
{
    internal class Performance : BaseEntity
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public ICollection<Speech> Speeches { get; set; }
    }
}