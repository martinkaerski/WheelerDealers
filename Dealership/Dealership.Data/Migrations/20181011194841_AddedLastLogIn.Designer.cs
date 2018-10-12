﻿// <auto-generated />
using System;
using Dealership.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Dealership.Data.Migrations
{
    [DbContext(typeof(DealershipContext))]
    [Migration("20181011194841_AddedLastLogIn")]
    partial class AddedLastLogIn
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Dealership.Data.Models.BodyType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(25);

                    b.Property<byte>("NumberOfDoors");

                    b.HasKey("Id");

                    b.ToTable("Chassis");

                    b.HasData(
                        new { Id = 1, IsDeleted = false, Name = "Sedan", NumberOfDoors = (byte)4 },
                        new { Id = 2, IsDeleted = false, Name = "Coupe", NumberOfDoors = (byte)2 },
                        new { Id = 3, IsDeleted = false, Name = "Cabrio", NumberOfDoors = (byte)2 },
                        new { Id = 4, IsDeleted = false, Name = "Touring", NumberOfDoors = (byte)4 },
                        new { Id = 5, IsDeleted = false, Name = "Suv", NumberOfDoors = (byte)5 },
                        new { Id = 6, IsDeleted = false, Name = "Hatchback", NumberOfDoors = (byte)5 }
                    );
                });

            modelBuilder.Entity("Dealership.Data.Models.Brand", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(25);

                    b.HasKey("Id");

                    b.ToTable("Brands");
                });

            modelBuilder.Entity("Dealership.Data.Models.Car", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BodyTypeId");

                    b.Property<int>("BrandId");

                    b.Property<int>("ColorId");

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<short>("EngineCapacity");

                    b.Property<int>("FuelTypeId");

                    b.Property<int>("GearBoxId");

                    b.Property<short>("HorsePower");

                    b.Property<bool>("IsDeleted");

                    b.Property<bool>("IsSold");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasMaxLength(25);

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<decimal>("Price");

                    b.Property<DateTime>("ProductionDate");

                    b.Property<int?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("BodyTypeId");

                    b.HasIndex("BrandId");

                    b.HasIndex("ColorId");

                    b.HasIndex("FuelTypeId");

                    b.HasIndex("GearBoxId");

                    b.HasIndex("UserId");

                    b.ToTable("Cars");
                });

            modelBuilder.Entity("Dealership.Data.Models.CarsExtras", b =>
                {
                    b.Property<int>("CarId");

                    b.Property<int>("ExtraId");

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedOn");

                    b.HasKey("CarId", "ExtraId");

                    b.HasIndex("ExtraId");

                    b.ToTable("CarsExtras");
                });

            modelBuilder.Entity("Dealership.Data.Models.Color", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ColorTypeId");

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(25);

                    b.HasKey("Id");

                    b.HasIndex("ColorTypeId");

                    b.ToTable("Colors");
                });

            modelBuilder.Entity("Dealership.Data.Models.ColorType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(25);

                    b.HasKey("Id");

                    b.ToTable("ColorTypes");

                    b.HasData(
                        new { Id = 1, IsDeleted = false, Name = "Acrylic" },
                        new { Id = 2, IsDeleted = false, Name = "Metalic" },
                        new { Id = 3, IsDeleted = false, Name = "Pearlescent" },
                        new { Id = 4, IsDeleted = false, Name = "Matte" },
                        new { Id = 5, IsDeleted = false, Name = "Xirallic" }
                    );
                });

            modelBuilder.Entity("Dealership.Data.Models.Extra", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(25);

                    b.HasKey("Id");

                    b.ToTable("Extras");
                });

            modelBuilder.Entity("Dealership.Data.Models.FuelType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(25);

                    b.HasKey("Id");

                    b.ToTable("FuelTypes");

                    b.HasData(
                        new { Id = 1, IsDeleted = false, Name = "Diesel" },
                        new { Id = 2, IsDeleted = false, Name = "Gasoline" },
                        new { Id = 3, IsDeleted = false, Name = "LPG" },
                        new { Id = 4, IsDeleted = false, Name = "Hybrid" },
                        new { Id = 5, IsDeleted = false, Name = "Electic" }
                    );
                });

            modelBuilder.Entity("Dealership.Data.Models.Gearbox", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<int>("GearTypeId");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<byte>("NumberOfGears");

                    b.HasKey("Id");

                    b.HasIndex("GearTypeId");

                    b.ToTable("Gearboxes");

                    b.HasData(
                        new { Id = 1, GearTypeId = 1, IsDeleted = false, NumberOfGears = (byte)3 },
                        new { Id = 2, GearTypeId = 1, IsDeleted = false, NumberOfGears = (byte)4 },
                        new { Id = 3, GearTypeId = 1, IsDeleted = false, NumberOfGears = (byte)5 },
                        new { Id = 4, GearTypeId = 1, IsDeleted = false, NumberOfGears = (byte)6 },
                        new { Id = 5, GearTypeId = 1, IsDeleted = false, NumberOfGears = (byte)7 },
                        new { Id = 6, GearTypeId = 1, IsDeleted = false, NumberOfGears = (byte)8 },
                        new { Id = 7, GearTypeId = 2, IsDeleted = false, NumberOfGears = (byte)4 },
                        new { Id = 8, GearTypeId = 2, IsDeleted = false, NumberOfGears = (byte)5 },
                        new { Id = 9, GearTypeId = 2, IsDeleted = false, NumberOfGears = (byte)6 }
                    );
                });

            modelBuilder.Entity("Dealership.Data.Models.GearType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(25);

                    b.HasKey("Id");

                    b.ToTable("GearTypes");

                    b.HasData(
                        new { Id = 1, IsDeleted = false, Name = "Automatic" },
                        new { Id = 2, IsDeleted = false, Name = "Manual" }
                    );
                });

            modelBuilder.Entity("Dealership.Data.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime>("LastLoggedIn");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(25);

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Dealership.Data.Models.Car", b =>
                {
                    b.HasOne("Dealership.Data.Models.BodyType", "BodyType")
                        .WithMany("Cars")
                        .HasForeignKey("BodyTypeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Dealership.Data.Models.Brand", "Brand")
                        .WithMany("Cars")
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Dealership.Data.Models.Color", "Color")
                        .WithMany("Cars")
                        .HasForeignKey("ColorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Dealership.Data.Models.FuelType", "FuelType")
                        .WithMany("Cars")
                        .HasForeignKey("FuelTypeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Dealership.Data.Models.Gearbox", "GearBox")
                        .WithMany("Cars")
                        .HasForeignKey("GearBoxId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Dealership.Data.Models.User")
                        .WithMany("FavoriteCars")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Dealership.Data.Models.CarsExtras", b =>
                {
                    b.HasOne("Dealership.Data.Models.Car", "Car")
                        .WithMany("CarsExtras")
                        .HasForeignKey("CarId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Dealership.Data.Models.Extra", "Extra")
                        .WithMany("CarsExtras")
                        .HasForeignKey("ExtraId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Dealership.Data.Models.Color", b =>
                {
                    b.HasOne("Dealership.Data.Models.ColorType", "ColorType")
                        .WithMany("Colors")
                        .HasForeignKey("ColorTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Dealership.Data.Models.Gearbox", b =>
                {
                    b.HasOne("Dealership.Data.Models.GearType", "GearType")
                        .WithMany("Gearboxes")
                        .HasForeignKey("GearTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
