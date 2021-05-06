using System;
using System.ComponentModel.DataAnnotations;

namespace SpotiWish_back.Model
{
    public class PlayList
    {
        [Required]
        public int Id { get; set; }
        
        [StringLength(20)]
        public string Name { get; set; }

         
        public byte[] Thumbnail { get; set; }
        
         
        public DateTime CreatDate { get; set; }
        
         
        public Music[] Musics { get; set; }
        
         
        public string Descrition { get; set; }

         
        public Artist[] Artists { get; set; }
        
    }
    public class PlayListDTO{

        public int Id { get; set; }
        

        public string Name { get; set; }

         
        public byte[] Thumbnail { get; set; }
        
         
        public DateTime CreatDate { get; set; }
        
         
        public SimpleMusicDTO[] Musics { get; set; }
        
         
        public string Descrition { get; set; }

         
        public Artist[] Artists { get; set; }
    }

    public class SimplePlayListDTO
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public byte[] Thumbnail { get; set; }
    }

    public class CRUDPlayListDTO
    {
        public string Name { get; set; }
        public DateTime CreatDate { get; set; }
        public string Descrition { get; set; }
    }
}