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
                    // Make it more elegant
                    throw new Exception("The file has an invalid name or extension. Please try again.");
                }
                else
                    upload.File.CopyTo(fileStream);
            }
            //Code that saves the file's information to the database
            string mainConnection = _configuration.GetConnectionString("DefaultConnection");
            SqlConnection sqlConnection = new SqlConnection(mainConnection);
            string sqlQuery = "INSERT INTO [dbo].[ApplicationFile] VALUES (@Path,@Title,@Type, @Genre, @BPM)";
            SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection);
            sqlConnection.Open();

            sqlCommand.Parameters.AddWithValue("@Path",Path.Combine(path, fileName));
            sqlCommand.Parameters.AddWithValue("@Title", fileName);
            sqlCommand.Parameters.AddWithValue(@"Type", upload.Type);
            sqlCommand.Parameters.AddWithValue(@"Genre", upload.Genre);
            sqlCommand.Parameters.AddWithValue(@"BPM", upload.BPM);
            
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();


            return RedirectToAction("Index", "Home");
        }
    }
}
