using Microsoft.AspNetCore.Mvc;
using SoundChoice.Models;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace SoundChoice.Controllers
{
    public class UploadController : Controller
    {
        private IHostingEnvironment Environment;
        public UploadController(IHostingEnvironment _environment)
        {
            Environment = _environment;
        }
        private string _dir = @"F:\Uploaded"; 
        public IActionResult Upload()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Upload(ApplicationFile upload)
        {
            string path = Path.Combine(this.Environment.WebRootPath, "Uploads");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            /*
             * Add validation for types of audio files (wav; ogg; mp3; m4a...)
             */

            string fileName = Path.GetFileName($"{upload.Title}.mp3");
            using (var fileStream = new FileStream(Path.Combine(path, fileName),
                FileMode.Create,
                FileAccess.Write))
            {
                upload.File.CopyTo(fileStream);
            }
            ViewBag.Data = "data:audio/wav;base64," + Convert.ToBase64String(System.IO.File.ReadAllBytes(Path.Combine(path, fileName)));
            return RedirectToAction("Index", "Home");
        }

        /*
        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            await UploadFile(file);
            return View();
        }
        public async Task<bool> UploadFile(IFormFile file)
        {
            string path = "";
            bool isCopied = false;
            try
            {
                if(file.Length > 0)
                {
                    string fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                    path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"F:\Uploaded"));

                    using (var fileStream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                    {
                        await fileStream.CopyToAsync(fileStream);
                    }
                    isCopied = true;
                }
                else
                {
                    isCopied = false;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return isCopied;
        }*/
    }
}
