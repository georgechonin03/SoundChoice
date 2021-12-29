﻿using System.ComponentModel.DataAnnotations;

namespace SoundChoice.Models
{
    public class ApplicationFile 
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string? Type { get; set; }
        public string? Genre { get; set; }
        public double? BPM { get; set; }
        public byte[] Content { get; set; }
    }
}
