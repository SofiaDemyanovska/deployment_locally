using System.ComponentModel.DataAnnotations.Schema;

namespace SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Entities
{
    internal class Audio : BaseEntity
    {
        public string FileName { get; set; }

        public string OriginalText { get; set; }

        public int Duration { get; set; }

        public int SpeechId { get; set; }

        public Speech Speech { get; set; }

        public int LanguageId { get; set; }

        public Language Language { get; set; }

        [NotMapped]
        public byte[] AudioFile { get; set; }
    }
}