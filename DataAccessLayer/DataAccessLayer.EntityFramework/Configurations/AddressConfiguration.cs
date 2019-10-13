using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.EntityFramework.Configurations
{
    class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.Property(p => p.Country).HasMaxLength(30);
            builder.Property(p => p.Area).HasMaxLength(30);
            builder.Property(p => p.City).HasMaxLength(30);
            builder.Property(p => p.Street).HasMaxLength(30);
            builder.Property(p => p.HouseNumber).HasMaxLength(30);


        }
    }
}
