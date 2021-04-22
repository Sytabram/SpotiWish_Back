using System;
using System.ComponentModel.DataAnnotations;

namespace SpotiWish_back.Model
{
    public class PlayList
    {
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }

        [Required] 
        public byte[] Thumbnail { get; set; }
        
        [Required] 
        public DateTime CreatDate { get; set; }
        
        [Required] 
        public Music[] Musics { get; set; }
        
        [Required] 
        public String Descrition { get; set; }

        [Required] 
        public Artist[] Artists { get; set; }
        
    }
}