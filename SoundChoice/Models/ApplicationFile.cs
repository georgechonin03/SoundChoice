using System.ComponentModel.DataAnnotations;

namespace SoundChoice.Models
{
    /// <summary>
    /// Defining properties for the uploaded files
    /// </summary>
    public class ApplicationFile 
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(256)]
        [Required]
        public string Title { get; set; }

        [MaxLength(256)]
        public string? Type { get; set; }

        [MaxLength(256)]
        public string? Genre { get; set; }

        [MaxLength(256)]
        public double? BPM { get; set; }

        public byte[] Content { get; set; }
    }
}
