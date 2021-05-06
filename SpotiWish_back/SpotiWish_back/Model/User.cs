using System.ComponentModel.DataAnnotations;

namespace SpotiWish_back.Model
{
    public class User
    {
        [Required]
        public int Id { get; set; }
        
        [StringLength(20)]
        public string Name { get; set; }
        
        [EmailAddress]
        public string Email { get; set; }
        
        public string Password { get; set; }
        
        public byte[] Thumbnail { get; set; }
        
        public SimplePlayListDTO[] Playlists { get; set; }
        
        public int Subscription { get; set; }
        
        public bool IsAdmin { get; set; }
    }

    public class UserDTO
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public string Email { get; set; }
        
        public string Password { get; set; }
        
        public byte[] Thumbnail { get; set; }
        
        public SimplePlayListDTO[] Playlists { get; set; }
        
        public int Subscription { get; set; }
        
        public bool IsAdmin { get; set; }
    }

    public class CreatUserDTO
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public string Email { get; set; }
        
        public string Password { get; set; }
    }
    public class SimpleUserDTO
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public byte[] Thumbnail { get; set; }
    }

    public class RegisterUserDTO
    {
        [Required]
        public int Id { get; set; }
        
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