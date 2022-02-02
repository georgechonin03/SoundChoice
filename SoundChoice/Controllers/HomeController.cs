using Microsoft.AspNetCore.Mvc;
using SoundChoice.Models;
using System.Diagnostics;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace SoundChoice.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IHostingEnvironment _environment;
        public HomeController(ILogger<HomeController> logger, IHostingEnvironment environment)
        {
            _logger = logger;
            _environment = environment;
        }

        public IActionResult Index()
        {
            string path = Path.Combine(this._environment.WebRootPath, "Uploads");
            var model = new AudioFiles()
            {
                Files = Directory.GetFiles(path).Select(file => Path
                .GetFileName(file))
                .ToList()
            };
            /*
            DirectoryInfo dirInfo = new DirectoryInfo(@"C:\Users\georg\source\repos\SoundChoice\SoundChoice\wwwroot\Uploads\");
            var files = new ApplicationFile()
            {
                Files = dirInfo.GetFiles().ToList()
            };*/
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