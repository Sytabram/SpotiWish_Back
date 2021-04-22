﻿using System.ComponentModel.DataAnnotations;

namespace SpotiWish_back.Model
{
    public class User
    {
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Email { get; set; }

        [Required] 
        public string Password { get; set; }
        
        [Required] 
        public byte[] Thumbnail { get; set; }

        [Required] 
        public music[] Playlist { get; set; }
        
        [Required] 
        public int Subscription { get; set; }

        [Required]
        public bool IsAdmin { get; set; }
    }
} 