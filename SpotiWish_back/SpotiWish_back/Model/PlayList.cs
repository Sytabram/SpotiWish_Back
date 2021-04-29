using System;
using System.ComponentModel.DataAnnotations;

namespace SpotiWish_back.Model
{
    public class PlayList
    {
        public int Id { get; set; }
        
        
        public string Name { get; set; }

         
        public byte[] Thumbnail { get; set; }
        
         
        public DateTime CreatDate { get; set; }
        
         
        public Music[] Musics { get; set; }
        
         
        public String Descrition { get; set; }

         
        public Artist[] Artists { get; set; }
        
    }
}