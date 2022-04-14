using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using SoundChoice.Models;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace SoundChoice.Controllers
{
    public class UploadController : Controller
    {
        private IHostingEnvironment _environment;
        private string[] _permittedExtensions = { ".mp3", ".wav", ".m4a", ".flac", ".wma", ".aac", ".ogg" };
        public UploadController(IHostingEnvironment environment)
        {
            _environment = environment;
        }
        public IActionResult Upload()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Upload(AudioFiles upload)
        {
            string path = Path.Combine(this._environment.WebRootPath, "Uploads");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            //Combining the user-generated title with the corresponding extension
            string fileName = $"{upload.Title}{Path.GetExtension(upload.File.FileName)}";

            //Checking whether the file extension is permitted.
            var ext = Path.GetExtension(fileName);
            if (string.IsNullOrEmpty(ext) || !_permittedExtensions.Contains(ext))
            {
                throw new Exception("The file has an invalid name or extension. Please try again.");
            }
            else
            {
                using (var fileStream = new FileStream(Path.Combine(path, fileName),
                FileMode.Create,
                FileAccess.Write))
                {
                    upload.File.CopyTo(fileStream);
                }
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
