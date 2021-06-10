using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace SpotiWish_back.Model
{
    public class User : IdentityUser<int>
    {
        public byte[] Thumbnail { get; set; }
        
        public ICollection<PlayList> Playlists { get; set; }
        
        public int Subscription { get; set; }
        
        
    }

    public class UserDTO
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public string Email { get; set; }
        
        public string Password { get; set; }
        
        public byte[] Thumbnail { get; set; }
        
        public ICollection<SimplePlayListDTO> Playlists { get; set; }
        
        public int Subscription { get; set; }
        
        public bool IsAdmin { get; set; }
    }
    
    public class SimpleUserDTO
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public byte[] Thumbnail { get; set; }
    }

    public class RegisterUserDTO
    {

        [StringLength(20)]
        public string Name { get; set; }
        
        [EmailAddress]
        public string Email { get; set; }
        
        public string Password { get; set; }

        public bool IsAdmin { get; set; }
    }

    public class LoginUserDTO
    {
        [StringLength(20)]
        public string Name { get; set; }
        
        public string Password { get; set; }
    }
} 