using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SpotiWish_back.Model
{
    public class Music
    {
        [Required]
        public int Id { get; set; }
        
        [StringLength(20)]
        public string Name { get; set; }
        
        public Artist Author { get; set; }//todo add
        
        public byte[] Thumbnail { get; set; }
        
        public int TimeOfPlays { get; set; }
        
        public ICollection<PlayList> Playlists { get; set; }
      
        public ICollection<Album> Albums { get; set; }
        
        public DateTime ReleaseDate { get; set; }
        
        public byte[] song { get; set; }

        [StringLength(20)]
        public string Style { get; set; }//todo delete
    }

    public class MusicDTO
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public ArtistDTO Author { get; set; }
        
        public byte[] Thumbnail { get; set; }
        
        public int TimeOfPlays { get; set; }
        
        public DateTime ReleaseDate { get; set; }
        
        public byte[] song { get; set; }
        
        public string Style { get; set; }
        
        public ICollection<SimplePlayListDTO> Playlists { get; set; }
      
        public ICollection<SimpleAlbumDTO> Albums { get; set; }

    }
    public class SimpleMusicDTO
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public int TimeOfPlays { get; set; }
        
        public DateTime ReleaseDate { get; set; }
        
        public byte[] Thumbnail { get; set; }
    }

    public class CRUDMusicDTO
    {
        public string Name { get; set; }
        
        public int TimeOfPlays { get; set; }
        
        public DateTime ReleaseDate { get; set; }
        
        public string Style { get; set; }
        public List<int> AlbumId { get; set; }
        public List<int> PlaylistId { get; set; }
     
    }
}