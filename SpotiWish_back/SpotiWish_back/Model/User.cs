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
        
        public PlayList[] Playlists { get; set; }
        
        public int Subscription { get; set; }
        
        public bool IsAdmin { get; set; }
    }
} 