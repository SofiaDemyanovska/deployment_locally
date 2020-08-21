using System.ComponentModel.DataAnnotations;

namespace SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.DTOs
{
    public class PerformanceDTO
    {
        public int Id { get; set; }

        [Required]
        [StringLength(64)]
        public string Title { get; set; }

        [Required]
        [StringLength(256)]
        public string Description { get; set; }
    }
}