using Microsoft.AspNetCore.Mvc;
using SoundChoice.Models;

namespace SoundChoice.Controllers
{
    public class SoundController : Controller
    {
        private string _dir = @"F:\Uploaded"; 
        public IActionResult Upload()
        {
            return View();
        }
        public IActionResult UploadFile(ApplicationFile upload)
        {
            using (var fileStream = new FileStream(Path.Combine(_dir, $"{upload.Title}.mp3"),
                FileMode.Create,
                FileAccess.Write))
            {
                upload.File.CopyTo(fileStream);
            }
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
