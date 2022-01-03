using System.ComponentModel.DataAnnotations;

namespace SoundChoice.Models.ViewModels
{
    /// <summary>
    /// Defines properties to use as tags when uploading a file.
    /// </summary>
    public class UploadViewModel
    {
        [Required]
        public string Title { get; set; }
        public string? Type { get; set; }
        public string? Genre { get; set; }
        public double? BPM { get; set; }

    }
}
