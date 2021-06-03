using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SpotiWish_back.Model;

namespace SpotiWish_back.Data
{
    public class SpotiWishDataContext : IdentityDbContext
    {
        public new DbSet<User> Users { get; set; }
        public DbSet<Music> Musics { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<PlayList> PlayLists{ get; set; }

        public SpotiWishDataContext(DbContextOptions<SpotiWishDataContext> options)
            : base(options) 
        {
    
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Album>(entity =>
            {
                entity.HasMany(u => u.Artists)
                    .WithMany(p => p.Albums);
            });
            base.OnModelCreating(modelBuilder);
        }
 
    }
}