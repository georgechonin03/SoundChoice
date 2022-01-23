using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace SoundChoice.Views.Sound
{
    public class UploadPhysicalModel : PageModel
    {
        [BindProperty]
        public UploadPhysicalModel FileUpload { get; set; }

        [Required]
        [Display(Name = "File")]
        public IFormFile FormFile { get; set; }
    }
}
