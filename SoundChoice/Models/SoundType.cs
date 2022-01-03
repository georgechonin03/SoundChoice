namespace SoundChoice.Models
{
    /// <summary>
    /// Additional properties for tagging a file.
    /// </summary>
    public class SoundType
    {
        public bool Effect { get; set; }
        public bool Loop { get; set; }
        public bool OneShot { get; set; }
        public bool Sample { get; set; }
    }
}
