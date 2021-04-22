using System;
using System.ComponentModel.DataAnnotations;

namespace SpotiWish_back.Model
{
    public class music
    {
        [Required]
        public string Name { get; set; }
        
        [Required]
        public Artist Author { get; set; }

        [Required] 
        public byte[] Thumbnail { get; set; }
        
        [Required] 
        public int TimeOfPlays { get; set; }

        [Required] 
        public DateTime ReleaseDate { get; set; }
        
        [Required] 
        public byte[] song { get; set; }

        [Required]
        public Album Album { get; set; }
        
        [Required]
        public String Style { get; set; }
    }
}