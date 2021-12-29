using Microsoft.AspNetCore.Mvc;
using SoundChoice.Models;
using SoundChoice.Upload;

namespace SoundChoice.Controllers
{
    public class SoundController : Controller
    {

        private readonly ApplicationDbContext _db;
        private readonly BufferedSingleFileUploadDbModel model;

        public IActionResult Upload()
        {
            return View();
        }
        public async Task<IActionResult> OnPostUploadAsync()
        {
            using (var memoryStream = new MemoryStream())
            {
                await model.FileUpload.FormFile.CopyToAsync(memoryStream);
                //Upload file if less than 50mb
                if (memoryStream.Length < 52428800)
                {
                    var file = new ApplicationFile()
                    {
                        Content = memoryStream.ToArray()
                    };
                    _db.Add(file);
                    await _db.SaveChangesAsync();
                }
                else
                {
                    ModelState.AddModelError("File", "The file is too large.");
                }
            }
            return View();
        }
    }
}
