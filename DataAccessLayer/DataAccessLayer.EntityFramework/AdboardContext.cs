using DataAccessLayer.EntityFramework.Configurations;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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

            //  relations 
            modelBuilder.Entity<Advert>()
                .HasOne<User>(a => a.Author)
                .WithMany(u => u.Adverts)
                .HasForeignKey(a => a.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Comment>()
                .HasOne<User>(c => c.Author)
                .WithMany(a => a.Comments)
                .HasForeignKey(c => c.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Advert>()
                .HasMany<Comment>(a => a.Comments)
                .WithOne(c => c.Advert)
                .HasForeignKey(c => c.AdvertId)
                .OnDelete(DeleteBehavior.Restrict);

        }

        DbSet<User> Users { get; set; }
        DbSet<Advert> Adverts { get; set; }
        DbSet<Comment> Comments { get; set; }

        
    }
}
