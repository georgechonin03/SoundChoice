using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using SoundChoice.Models;
using System.Diagnostics;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace SoundChoice.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IHostingEnvironment _environment;
        private IConfiguration _configuration { get; }
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

        public IActionResult Index()
        {
            return View(GetAudioFiles(Path.Combine(this._environment.WebRootPath, "Uploads")));
        }
        
        public IActionResult Search(string searchString)
        {
            string mainConnection = _configuration.GetConnectionString("DefaultConnection");
            SqlModel sql = new SqlModel();
            sql.Connection = new SqlConnection(mainConnection);

            sql.Query = $"SELECT * FROM [Sound].[dbo].[ApplicationFile] WHERE [Title] LIKE '%{searchString}%'";
            sql.Command = new SqlCommand(sql.Query, sql.Connection);

            sql.Connection.Open(); 
            sql.Reader = sql.Command.ExecuteReader();
            while (sql.Reader.Read())
            {
                string path = (string)sql.Reader["Path"];
                return View(GetAudioFiles(path));
            }
            sql.Connection.Close();

            return RedirectToAction("Index");
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