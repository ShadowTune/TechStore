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
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("BrandId")
                        .HasColumnType("int");

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

                    b.HasIndex("BrandId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            ProductId = "09b4ab76-9b49-4278-8a31-e30b2bb244e5",
                            BrandId = 1,
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
                            ProductId = "418f00cb-3c1b-4cca-b071-43b7458fe4c4",
                            BrandId = 8,
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
                            ProductId = "4aae79c9-e38e-43dc-83a5-79fe0bac94e5",
                            BrandId = 2,
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
                            ProductId = "6b5786c6-3eed-4fb6-9183-6f5eaae7d897",
                            BrandId = 7,
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
                            ProductId = "a55a96e0-660e-4407-ae45-bcb3b6f8a53e",
                            BrandId = 6,
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
                            ProductId = "b694c472-76e9-4e71-9ac0-ccd28944335c",
                            BrandId = 3,
                            DiscountPrice = 1150.99,
                            DisplayOrder = 20,
                            ImageLink = "",
                            Model = "AN515",
                            RegularPrice = 1159.99,
                            Series = "NITRO",
                            Specification = "abcd"
                        },
                        new
                        {
                            ProductId = "e612e509-77f0-4cd6-a784-0c75172db894",
                            BrandId = 4,
                            DiscountPrice = 4100.9899999999998,
                            DisplayOrder = 5,
                            ImageLink = "",
                            Model = "R15",
                            RegularPrice = 4159.9899999999998,
                            Series = "ALIENWARE",
                            Specification = "abcd"
                        });
                });

            modelBuilder.Entity("TechStore.Models.Product", b =>
                {
                    b.HasOne("TechStore.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });
#pragma warning restore 612, 618
        }
    }
}
