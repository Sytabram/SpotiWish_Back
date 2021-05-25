using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SpotiWish_back.Model
{
    public class Artist
    {
        [Required]
        public int Id { get; set; }

        [StringLength(20)]
        public string Name { get; set; }
        
        public byte[] ProfilThumbnail { get; set; }
        
        public byte[] BackGroundThumbnail { get; set; }
        
        public ICollection<SimpleAlbumDTO> Albums { get; set; }
        
        public int TimeOfHeard { get; set; }
    }

    public class ArtistDTO
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public byte[] ProfilThumbnail { get; set; }
        
        public byte[] BackGroundThumbnail { get; set; }
        
        public ICollection<SimpleAlbumDTO> Albums { get; set; }

        public int TimeOfHeard { get; set; }
    }
    public class SimpleArtistDTO
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public byte[] ProfilThumbnail { get; set; }
    }

    public class CRUDArtistDTO
    {
        public string Name { get; set; }
        public int TimeOfHeard { get; set; }
        public ICollection<SimpleAlbumDTO> Albums { get; set; }
    }
}