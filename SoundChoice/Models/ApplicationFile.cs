using System.ComponentModel.DataAnnotations;

namespace SoundChoice.Models
{
    public class ApplicationFile
    {
        [Required]
        public string Title { get; set; }
        public string? Type { get; set; }
        public string? Genre { get; set; }
        public double? BPM { get; set; }
        public IFormFile File { get; set; }
        public List<string> Files { get; set; }
    }
}
