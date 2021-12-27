using System.ComponentModel.DataAnnotations;

namespace SoundChoice.Models.ViewModels
{
    public class UploadViewModel
    {
        [Required]
        public string Title { get; set; }
        public string? Type { get; set; }
        public string? Genre { get; set; }
        public double? BPM { get; set; }
        [Required]
        public FormFile File { get; set; }

    }
}
