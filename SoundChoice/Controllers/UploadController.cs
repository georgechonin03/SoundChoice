using Microsoft.AspNetCore.Mvc;
using SoundChoice.Models;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace SoundChoice.Controllers
{
    public class UploadController : Controller
    {
        private IHostingEnvironment _environment;
        private string[] _permittedExtensions = { ".mp3", ".wav", ".m4a", ".flac", ".wma", ".aac", ".ogg" };
        private string[] _excludedCharacters = { "#", "%", "&", "{", "}", "/", @"\", "<", ">", "?", "$", "!", "'", ":", "@", "+", "`", "|", "=" ," "};
        public UploadController(IHostingEnvironment environment)
        {
            _environment = environment;
        }
        public IActionResult Upload()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Upload(ApplicationFile upload)
        {
            string path = Path.Combine(this._environment.WebRootPath, "Uploads");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            //string fileName = Path.GetFileName(upload.File.FileName);
            string fileName = $"{upload.Title}{Path.GetExtension(upload.File.FileName)}";
            var ext = Path.GetExtension(fileName);


            using (var fileStream = new FileStream(Path.Combine(path, fileName),
                FileMode.Create,
                FileAccess.Write))
            {
                if (string.IsNullOrEmpty(ext) || !_permittedExtensions.Contains(ext) || _excludedCharacters.Contains(fileName))
                {
                    throw new Exception("The file has an invalid name or extension. Please try again.");
                }
                else
                    upload.File.CopyTo(fileStream);
            }
            //ViewBag.Data = "data:audio/wav;base64," + Convert.ToBase64String(System.IO.File.ReadAllBytes(Path.Combine(path, fileName)));
            return RedirectToAction("Index", "Home");
        }
    }
}
