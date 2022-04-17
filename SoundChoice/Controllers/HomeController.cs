using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoundChoice.Models;
using System.Diagnostics;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace SoundChoice.Controllers
{
    public class HomeController : Controller
    {
        private IHostingEnvironment _environment;
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger, IHostingEnvironment environment)
        {
            _logger = logger;
            _environment = environment;
        }

        /// <summary>
        /// Gets the files from the chosen path.
        /// </summary>
        /// <returns>A list of files.</returns>
        private AudioFiles GetAudioFiles(string path)
        {
            var model = new AudioFiles()
            {
                Files = Directory.GetFiles(path).Select(file => Path
                .GetFileName(file))
                .ToList()
            };
            return model;
        }
        /// <summary>
        /// Gets the files that meet the specified criteria
        /// </summary>
        /// <param name="path">The directory of the files</param>
        /// <param name="searchString">The criteria that is searched for</param>
        /// <returns>A list of files.</returns>
        private AudioFiles GetAudioFilesSearch(string path, string searchString)
        {
            var model = new AudioFiles()
            {
                Files = Directory.GetFiles(path).Select(file => Path
                .GetFileName(file)).Where(file => file.ToLower().Contains(searchString.ToLower())).ToList()
            };

            return model;
        }
        public IActionResult Index()
        {
            return View(GetAudioFiles(Path.Combine(_environment.WebRootPath, "Uploads")));
        }

        [HttpPost]
        public IActionResult Search(string searchString)
        {
            var path = Path.Combine(this._environment.WebRootPath, "Uploads");
            var audioFiles = GetAudioFilesSearch(path, searchString);
            return View(audioFiles);

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