using System.ComponentModel.DataAnnotations;

namespace SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.DTOs
{
    public class LanguageDTO
    {
        public int Id { get; set; }

        [Required]
        [StringLength(32)]
        public string Name { get; set; }
    }
}