using System;
using System.ComponentModel.DataAnnotations;

namespace SpotiWish_back.Model
{
    public class Music
    {
        [Required]
        public int Id { get; set; }
        
        [StringLength(20)]
        public string Name { get; set; }
        
        public Artist Author { get; set; }
        
        public byte[] Thumbnail { get; set; }
        
        public int TimeOfPlays { get; set; }
        
        public DateTime ReleaseDate { get; set; }
        
        public byte[] song { get; set; }

        [StringLength(20)]
        public string Style { get; set; }
    }

    public class MusicDTO
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public Artist Author { get; set; }
        
        public byte[] Thumbnail { get; set; }
        
        public int TimeOfPlays { get; set; }
        
        public DateTime ReleaseDate { get; set; }
        
        public byte[] song { get; set; }
        
        public string Style { get; set; }

    }
    public class SimpleMusicDTO
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public byte[] Thumbnail { get; set; }
    }

    public class CRUDMusicDTO
    {
        public string Name { get; set; }
        
        public int TimeOfPlays { get; set; }
        
        public DateTime ReleaseDate { get; set; }
        
        public string Style { get; set; }
     
    }
}