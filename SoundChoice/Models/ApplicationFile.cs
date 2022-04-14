using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace SoundChoice.Models
{
    public class ApplicationFile
    {
        [Key]
        public int Id { get; set; }
        public string Path { get; set; }
        [Required]
        public string Title { get; set; } 
        public string? Type { get; set; }
        public string? Genre { get; set; }
        public double? BPM { get; set; }
    }
}
