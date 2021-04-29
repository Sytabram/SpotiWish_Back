using System.ComponentModel.DataAnnotations;

namespace SpotiWish_back.Model
{
    public class User
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public string Email { get; set; }
        
        public string Password { get; set; }
        
        public byte[] Thumbnail { get; set; }
        
        public PlayList[] Playlists { get; set; }
        
        public int Subscription { get; set; }
        
        public bool IsAdmin { get; set; }
    }
} 