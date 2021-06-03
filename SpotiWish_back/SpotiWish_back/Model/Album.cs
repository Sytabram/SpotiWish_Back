﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SpotiWish_back.Model
{
    public class Album
    {
        [Required]
        public int Id { get; set; }
        
        [StringLength(20)]
        public string Name { get; set; }
        
        public byte[] Thumbnail { get; set; }
        
        public TimeSpan TotalTime { get; set; }
        
        public int TotalHeard { get; set; }
        
        public int YearReleased { get; set; }

        public ICollection<Artist> Artists { get; set; }

        public ICollection<Music> Musics { get; set; }
    }

    public class AlbumDTO
    {
        
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public byte[] Thumbnail { get; set; }
        
        public TimeSpan TotalTime { get; set; }
        
        public int TotalHeard { get; set; }
        
        public int YearReleased { get; set; }

        public ICollection<MusicDTO> Musics { get; set; }
    }
    public class SimpleAlbumDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] Thumbnail { get; set; }
    }

    public class CRUDAlbumDTO
    {
        public string Name { get; set; }
        
        public TimeSpan TotalTime { get; set; }
        
        public int TotalHeard { get; set; }
        
        public int YearReleased { get; set; }
    }
}