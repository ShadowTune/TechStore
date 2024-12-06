﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TechStore.DataAccess.Data;

#nullable disable

namespace TechStore.DataAccess.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TechStore.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DisplayOrder")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Series")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DisplayOrder = 12,
                            Name = "LENOVO",
                            Series = "LOQ"
                        },
                        new
                        {
                            Id = 2,
                            DisplayOrder = 5,
                            Name = "ASUS",
                            Series = "TUF"
                        },
                        new
                        {
                            Id = 3,
                            DisplayOrder = 3,
                            Name = "ACER",
                            Series = "NITRO"
                        },
                        new
                        {
                            Id = 4,
                            DisplayOrder = 4,
                            Name = "DELL",
                            Series = "ALIENWARE"
                        },
                        new
                        {
                            Id = 5,
                            DisplayOrder = 6,
                            Name = "MSI",
                            Series = "TITAN"
                        },
                        new
                        {
                            Id = 6,
                            DisplayOrder = 2,
                            Name = "HP",
                            Series = "VICTUS"
                        },
                        new
                        {
                            Id = 7,
                            DisplayOrder = 6,
                            Name = "ASUS",
                            Series = "ROG"
                        },
                        new
                        {
                            Id = 8,
                            DisplayOrder = 15,
                            Name = "LENOVO",
                            Series = "LEGION"
                        });
                });

            modelBuilder.Entity("TechStore.Models.Product", b =>
                {
                    b.Property<string>("ProductId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<double>("DiscountPrice")
                        .HasColumnType("float");

                    b.Property<int>("DisplayOrder")
                        .HasColumnType("int");

                    b.Property<string>("ImageLink")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("RegularPrice")
                        .HasColumnType("float");

                    b.Property<string>("Series")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Specification")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProductId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            ProductId = "MP2N6HJ1",
                            Brand = "LENOVO",
                            DiscountPrice = 1059.99,
                            DisplayOrder = 30,
                            ImageLink = "",
                            Model = "15IAX9",
                            RegularPrice = 1099.99,
                            Series = "LOQ",
                            Specification = "abcd"
                        },
                        new
                        {
                            ProductId = "M62N6HJ1",
                            Brand = "LENOVO",
                            DiscountPrice = 2059.9899999999998,
                            DisplayOrder = 20,
                            ImageLink = "",
                            Model = "PRO 5i",
                            RegularPrice = 2099.9899999999998,
                            Series = "LEGION",
                            Specification = "abcd"
                        },
                        new
                        {
                            ProductId = "A72N6HJ2",
                            Brand = "ASUS",
                            DiscountPrice = 1259.99,
                            DisplayOrder = 25,
                            ImageLink = "",
                            Model = "A15",
                            RegularPrice = 1399.99,
                            Series = "TUF",
                            Specification = "abcd"
                        },
                        new
                        {
                            ProductId = "RP2N6HJ1",
                            Brand = "ASUS",
                            DiscountPrice = 2399.9899999999998,
                            DisplayOrder = 10,
                            ImageLink = "",
                            Model = "STRIX G15",
                            RegularPrice = 2599.9899999999998,
                            Series = "ROG",
                            Specification = "abcd"
                        },
                        new
                        {
                            ProductId = "GT2N6HJ1",
                            Brand = "MSI",
                            DiscountPrice = 4959.9899999999998,
                            DisplayOrder = 10,
                            ImageLink = "",
                            Model = "GT77",
                            RegularPrice = 5099.9899999999998,
                            Series = "TITAN",
                            Specification = "abcd"
                        },
                        new
                        {
                            ProductId = "HP2N6HJ1",
                            Brand = "HP",
                            DiscountPrice = 1159.99,
                            DisplayOrder = 20,
                            ImageLink = "",
                            Model = "15",
                            RegularPrice = 1199.99,
                            Series = "VICTUS",
                            Specification = "abcd"
                        },
                        new
                        {
                            ProductId = "AC2N6HJ1",
                            Brand = "ACER",
                            DiscountPrice = 1150.99,
                            DisplayOrder = 20,
                            ImageLink = "",
                            Model = "AN15",
                            RegularPrice = 1159.99,
                            Series = "NITRO",
                            Specification = "abcd"
                        },
                        new
                        {
                            ProductId = "DL2N6HJ1",
                            Brand = "DELL",
                            DiscountPrice = 4100.9899999999998,
                            DisplayOrder = 5,
                            ImageLink = "",
                            Model = "RM15",
                            RegularPrice = 4159.9899999999998,
                            Series = "ALIENWARE",
                            Specification = "abcd"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
