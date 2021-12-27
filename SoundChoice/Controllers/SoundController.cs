using Microsoft.AspNetCore.Mvc;

namespace SoundChoice.Controllers
{
    public class SoundController : Controller
    {
        public IActionResult Upload()
        {
            return View();
        }
    }
}
