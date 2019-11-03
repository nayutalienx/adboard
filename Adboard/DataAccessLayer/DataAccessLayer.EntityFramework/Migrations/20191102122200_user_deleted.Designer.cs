﻿// <auto-generated />
using System;
using DataAccessLayer.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DataAccessLayer.EntityFramework.Migrations
{
    [DbContext(typeof(AdboardContext))]
    [Migration("20191102122200_user_deleted")]
    partial class user_deleted
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.0-rtm-35687")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DataAccessLayer.Models.Address", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("AdvertId");

                    b.Property<string>("Area")
                        .HasMaxLength(30);

                    b.Property<string>("City")
                        .HasMaxLength(30);

                    b.Property<string>("Country")
                        .HasMaxLength(30);

                    b.Property<string>("HouseNumber")
                        .HasMaxLength(30);

                    b.Property<string>("Street")
                        .HasMaxLength(30);

                    b.HasKey("Id");

                    b.HasIndex("AdvertId")
                        .IsUnique();

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("DataAccessLayer.Models.Advert", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("CategoryId");

                    b.Property<DateTime>("CreatedDateTime");

                    b.Property<string>("Description");

                    b.Property<string>("Header")
                        .HasMaxLength(30);

                    b.Property<long>("Price");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Adverts");
                });

            modelBuilder.Entity("DataAccessLayer.Models.Category", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasMaxLength(30);

                    b.Property<long?>("ParentCategoryId");

                    b.HasKey("Id");

                    b.HasIndex("ParentCategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("DataAccessLayer.Models.Comment", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("AdvertId");

                    b.Property<DateTime>("CreatedDateTime");

                    b.Property<string>("Text")
                        .HasMaxLength(300);

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("AdvertId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("DataAccessLayer.Models.Photo", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("AdvertId");

                    b.Property<byte[]>("Data");

                    b.HasKey("Id");

                    b.HasIndex("AdvertId");

                    b.ToTable("Photos");
                });

            modelBuilder.Entity("DataAccessLayer.Models.Address", b =>
                {
                    b.HasOne("DataAccessLayer.Models.Advert", "Advert")
                        .WithOne("Location")
                        .HasForeignKey("DataAccessLayer.Models.Address", "AdvertId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DataAccessLayer.Models.Advert", b =>
                {
                    b.HasOne("DataAccessLayer.Models.Category", "Category")
                        .WithMany("Adverts")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DataAccessLayer.Models.Category", b =>
                {
                    b.HasOne("DataAccessLayer.Models.Category", "ParentCategory")
                        .WithMany("SubCategories")
                        .HasForeignKey("ParentCategoryId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("DataAccessLayer.Models.Comment", b =>
                {
                    b.HasOne("DataAccessLayer.Models.Advert", "Advert")
                        .WithMany("Comments")
                        .HasForeignKey("AdvertId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DataAccessLayer.Models.Photo", b =>
                {
                    b.HasOne("DataAccessLayer.Models.Advert", "Advert")
                        .WithMany("Photos")
                        .HasForeignKey("AdvertId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}