using System.ComponentModel.DataAnnotations;

namespace SoundChoice.Models.ViewModels
{
    public class UploadViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        [StringLength(100, ErrorMessage ="This field cannot be empty.")]
        public string Title { get; set; }
        [Required]
        [DataType(DataType.Upload)]
        public IFormFile File { get; set; }

    }
}
