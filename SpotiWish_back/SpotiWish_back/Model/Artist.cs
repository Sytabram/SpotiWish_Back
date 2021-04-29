﻿using System;
using System.ComponentModel.DataAnnotations;

namespace SpotiWish_back.Model
{
    public class Artist
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public byte[] ProfilThumbnail { get; set; }
        
        public byte[] BackGroundThumbnail { get; set; }
        
        public Album[] Albums { get; set; }
        
        public int TimeOfHeard { get; set; }
    }
}