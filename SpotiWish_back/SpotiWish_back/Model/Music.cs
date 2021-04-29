using System;
using System.ComponentModel.DataAnnotations;

namespace SpotiWish_back.Model
{
    public class Music
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public Artist Author { get; set; }
        
        public byte[] Thumbnail { get; set; }
        
        public int TimeOfPlays { get; set; }
        
        public DateTime ReleaseDate { get; set; }
        
        public byte[] song { get; set; }

        public Album Album { get; set; }
        
        public String Style { get; set; }
    }
}