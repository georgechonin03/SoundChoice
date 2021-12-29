using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SoundChoice.Models;
using SoundChoice.Models.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace SoundChoice.Upload
{
    public class BufferedSingleFileUploadDbModel : PageModel
    {
        [BindProperty]
        public BufferedSingleFileUploadDb FileUpload { get; set; }
    }

    public class BufferedSingleFileUploadDb
    {
        [Required]
        [Display(Name ="File")]
        public IFormFile FormFile { get; set; }
    }
}
