using Microsoft.AspNetCore.Mvc;
using SoundChoice.Models;
using System.Diagnostics;

namespace SoundChoice.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var model = new AudioFiles()
            {
                Files = Directory.GetFiles(@"C:\Users\georg\source\repos\SoundChoice\SoundChoice\wwwroot\Uploads\").Select(file =>
                 Path.GetFileName(file)).ToList()
            };
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}