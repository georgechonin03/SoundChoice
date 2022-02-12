using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using SoundChoice.Models;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace SoundChoice.Controllers
{
    public class UploadController : Controller
    {
        private IHostingEnvironment _environment;
        private string[] _permittedExtensions = { ".mp3", ".wav", ".m4a", ".flac", ".wma", ".aac", ".ogg" };
        private string[] _excludedCharacters = { "#", "%", "&", "{", "}", "/", @"\", "<", ">", "?", "$", "!", "'", ":", "@", "+", "`", "|", "=" ," "};
        //private char[] _excludedCharacters = { '#', '%', '&', '{', '}', '/', ' ', '<', '>', '?', '$', '!', '"', ':', '@', '+', '`', '|', '='};

        private IConfiguration _configuration { get; }
        public UploadController(IHostingEnvironment environment, IConfiguration configuration)
        {
            _environment = environment;
            _configuration = configuration;
        }
        public IActionResult Upload()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Upload(AudioFiles upload)
        {
            string path = Path.Combine(this._environment.WebRootPath, "Uploads");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            //Combining the user-generated title with the corresponding extension
            string fileName = $"{upload.Title}{Path.GetExtension(upload.File.FileName)}";
            var ext = Path.GetExtension(fileName);


            using (var fileStream = new FileStream(Path.Combine(path, fileName),
                FileMode.Create,
                FileAccess.Write))
            {
                if (string.IsNullOrEmpty(ext) || !_permittedExtensions.Contains(ext) || _excludedCharacters.Contains(fileName))
                {
                    // To-do: Make it more elegant
                    throw new Exception("The file has an invalid name or extension. Please try again.");
                }
                else
                    upload.File.CopyTo(fileStream);
            }
            //Code that saves the file's information to the database
            string mainConnection = _configuration.GetConnectionString("DefaultConnection");
            SqlModel sql = new SqlModel();
            sql.Connection = new SqlConnection(mainConnection);
            sql.Query = "INSERT INTO [dbo].[ApplicationFile] VALUES (@Path,@Title,@Type, @Genre, @BPM)";
            sql.Command = new SqlCommand(sql.Query, sql.Connection);

            sql.Connection.Open();
            sql.Command.Parameters.AddWithValue("@Path", Path.Combine(path, fileName));
            sql.Command.Parameters.AddWithValue("@Title", fileName);
            sql.Command.Parameters.AddWithValue(@"Type", upload.Type);
            sql.Command.Parameters.AddWithValue(@"Genre", upload.Genre);
            sql.Command.Parameters.AddWithValue(@"BPM", upload.BPM);

            sql.Command.ExecuteNonQuery();
            sql.Connection.Close();

            return RedirectToAction("Index", "Home");
        }
    }
}
