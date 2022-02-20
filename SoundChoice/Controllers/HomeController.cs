using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SoundChoice.Models;
using System.Diagnostics;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace SoundChoice.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IHostingEnvironment _environment;
        private ApplicationDbContext _db;
        private IConfiguration _configuration { get; }
        public HomeController(ILogger<HomeController> logger, IHostingEnvironment environment, ApplicationDbContext db, IConfiguration configuration)
        {
            _logger = logger;
            _environment = environment;
            _db = db;
            _configuration = configuration;
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
        /// Method needs reworking. 
        /// </summary>
        /// <param name="path"></param>
        /// <returns>Must retrieve only a single file using its path</returns>
        private AudioFiles GetAudioFile(string path)
        {
            var model = new AudioFiles();
            return model;
        }
        [HttpGet]
        public IActionResult Index()
        {
            // Search("goodbye");
            return View(GetAudioFiles(Path.Combine(this._environment.WebRootPath, "Uploads")));
        }
        [HttpPost]
        public IActionResult Search(string searchString)
        {
            string mainConnection = _configuration.GetConnectionString("DefaultConnection");
            SqlModel sql = new SqlModel();
            sql.Connection = new SqlConnection(mainConnection);
            sql.Query = $"SELECT * FROM [Sound].[dbo].[ApplicationFile] WHERE [Title] LIKE '%{searchString}%' OR [Type] LIKE '%{searchString}%' OR [Genre] LIKE '%{searchString}%' OR [BPM] LIKE '%{searchString}%'";
            sql.Command = new SqlCommand(sql.Query, sql.Connection);

            sql.Connection.Open();
            sql.Reader = sql.Command.ExecuteReader();
            while (sql.Reader.Read())
            {
                string path = (string)sql.Reader["Path"];
                return View(GetAudioFile(path));
            }
            sql.Connection.Close();

            return RedirectToAction("Index");
        }


        /* [HttpPost]
         public IActionResult Search(string searchString)
         {

             var query = from x in _db.ApplicationFile select x;
             var model = new AudioFiles();
             if (!String.IsNullOrEmpty(searchString))
             {
                 query = query.Where(x => 
                 x.Title.Contains(searchString) || 
                 x.Type.Contains(searchString) ||
                 x.Genre.Contains(searchString));
             }
             return View(query.ToList());
         }*/


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