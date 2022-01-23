using Microsoft.AspNetCore.Mvc;

namespace SoundChoice.Controllers
{
    public class UploadController : Controller
    { 
        public async Task<IActionResult> OnPostUploadAsync(List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);
            foreach (var formFile in files)
            {
                if(formFile.Length > 0)
                {
                    var filePath = Path.GetTempFileName();
                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }
            return Ok(new {count = files.Count,size});
        }
    }
}
