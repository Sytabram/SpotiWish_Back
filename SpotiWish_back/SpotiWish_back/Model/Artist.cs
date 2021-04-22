using System;
using System.ComponentModel.DataAnnotations;

namespace SpotiWish_back.Model
{
    public class Artist
    {
        [Required]
        public string Name { get; set; }

        [Required] 
        public byte[] ProfilThumbnail { get; set; }
        
        [Required] 
        public byte[] BackGroundThumbnail { get; set; }
        
        [Required] 
        public Album[] Albums { get; set; }

        [Required] 
        public int TimeOfHeard { get; set; }
    }
}