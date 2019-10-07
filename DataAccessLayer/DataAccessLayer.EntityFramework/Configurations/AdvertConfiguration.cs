using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.EntityFramework.Configurations
{
    class AdvertConfiguration : IEntityTypeConfiguration<Advert>
    {
        public void Configure(EntityTypeBuilder<Advert> builder) {
            builder.Property(p => p.Header).HasMaxLength(30);
            builder.Property(p => p.Category).HasMaxLength(30);
            builder.Property(p => p.SubCategory).HasMaxLength(30);

            builder.HasOne<User>(a => a.Author)
                .WithMany(u => u.Adverts)
                .HasForeignKey(a => a.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany<Comment>(a => a.Comments)
                .WithOne(c => c.Advert)
                .HasForeignKey(c => c.AdvertId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
