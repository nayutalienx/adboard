using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.EntityFramework.Configurations
{
    class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.Property(p => p.Text).HasMaxLength(300);

            builder.HasOne<Advert>(comment => comment.Advert)
                .WithMany(advert => advert.Comments)
                .HasForeignKey(comment => comment.AdvertId)
                .OnDelete(DeleteBehavior.Cascade);

            
        }
    }
}
