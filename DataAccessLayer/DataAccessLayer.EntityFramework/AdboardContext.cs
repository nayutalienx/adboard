using Microsoft.EntityFrameworkCore;
using System;

namespace DataAccessLayer.EntityFramework
{
    public class AdboardContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder
                .UseLazyLoadingProxies()
                .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Dashboard;Trusted_Connection=True");

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder) { 
            
        }
    }
}
