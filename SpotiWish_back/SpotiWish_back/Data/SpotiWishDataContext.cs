using Microsoft.EntityFrameworkCore;
using SpotiWish_back.Model;

namespace SpotiWish_back.Data
{
    public class SpotiWishDataContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Music> Musics { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<PlayList> PlayLists{ get; set; }

        public SpotiWishDataContext(DbContextOptions<SpotiWishDataContext> options)
            : base(options) 
        {
    
        }

    }
}