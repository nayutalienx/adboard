using DataAccessLayer.EntityFramework.Configurations;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DataAccessLayer.EntityFramework
{
    public class AdboardContext : DbContext
    {
        public AdboardContext(DbContextOptions<AdboardContext> options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLazyLoadingProxies();


        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AdvertConfiguration());
            modelBuilder.ApplyConfiguration(new CommentConfiguration());
            modelBuilder.ApplyConfiguration(new AddressConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new PhotoConfiguration());

        }

        DbSet<Advert> Adverts { get; set; }
        DbSet<Comment> Comments { get; set; }
        DbSet<Category> Categories { get; set; }
        DbSet<Address> Addresses { get; set; }
        DbSet<Photo> Photos { get; set; }
          



    }
}
