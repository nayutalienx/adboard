﻿using DataAccessLayer.Models;
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

            

            builder.HasOne<Category>(advert => advert.Category)
                .WithMany(category => category.Adverts)
                .HasForeignKey(advert => advert.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
