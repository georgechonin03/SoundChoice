namespace SoundChoice.Models
{
    public class AudioFiles : ApplicationFile
    {
        public List<string> Files { get; set; }
        public IFormFile File { get; set; }
    }
}
