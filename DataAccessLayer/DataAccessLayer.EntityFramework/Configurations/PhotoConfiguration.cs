using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.EntityFramework.Configurations
{
    class PhotoConfiguration : IEntityTypeConfiguration<Photo>
    {
        public void Configure(EntityTypeBuilder<Photo> builder)
        {
            builder.HasOne<Advert>(photo => photo.Advert)
                .WithMany(advert => advert.Photos)
                .HasForeignKey(photo => photo.AdvertId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
