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

        }

        DbSet<User> Users { get; set; }
        DbSet<Advert> Adverts { get; set; }
        DbSet<Comment> Comments { get; set; }

        
    }
}
