using DataAccessLayer.EntityFramework.Configurations;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.EntityFramework
{
    public class AdboardContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder
                .UseLazyLoadingProxies()
                .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Adboard;Trusted_Connection=True");

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.ApplyConfiguration(new AdvertConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new CommentConfiguration());

            // relations

            modelBuilder.Entity<Advert>()
                .HasOne<User>(a => a.Author)
                .WithMany(u => u.Adverts)
                .HasForeignKey(a => a.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Comment>()
                .HasKey(c => new { c.AdvertId, c.AuthorId });


        }

        DbSet<User> Users { get; set; }
        DbSet<Advert> Adverts { get; set; }
        DbSet<Comment> Comments { get; set; }
    }
}
