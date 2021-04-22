using System;
using System.ComponentModel.DataAnnotations;

namespace SpotiWish_back.Model
{
    public class Album
    {
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }

        [Required] 
        public byte[] Thumbnail { get; set; }
        
        [Required] 
        public TimeSpan TotalTime { get; set; }
        
        [Required] 
        public int TotalHeard { get; set; }
        
        [Required] 
        public int YearReleased { get; set; }

        [Required] 
        public Artist Artists { get; set; }
        
        [Required] 
        public Music[] Musics { get; set; }
    }
}