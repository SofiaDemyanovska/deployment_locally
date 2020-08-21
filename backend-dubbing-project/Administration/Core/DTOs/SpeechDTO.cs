using System.ComponentModel.DataAnnotations;

namespace SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.DTOs
{
    public class SpeechDTO
    {
        public int Id { get; set; }

        [Required]
        public int Order { get; set; }

        [Required]
        [StringLength(256)]
        public string Text { get; set; }

        public int Duration { get; set; }

        [Required]
        public int PerformanceId { get; set; }
    }
}