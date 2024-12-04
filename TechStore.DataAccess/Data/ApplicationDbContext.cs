using Microsoft.EntityFrameworkCore;
using TechStore.Models;

namespace TechStore.DataAccess.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{
			
		}
		public DbSet<Category> Categories { get; set; }
		public DbSet<Product> Products { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<Category>().HasData(
				new Category { Id = 1, Name = "LENOVO", DisplayOrder = 12},
				new Category { Id = 2, Name = "ASUS", DisplayOrder = 5 },
				new Category { Id = 3, Name = "ACER", DisplayOrder = 3 },
				new Category { Id = 4, Name = "DELL", DisplayOrder = 4 },
				new Category { Id = 5, Name = "MSI", DisplayOrder = 6 },
				new Category { Id = 6, Name = "HP", DisplayOrder = 2 }
			);
			modelBuilder.Entity<Product>().HasData(
				new Product { ProductId = "MP2N6HJ1", Brand = "Lenovo", Series = "LOQ", Model = "15IAX9", RegularPrice = 1099.99, DiscountPrice = 1059.99, DisplayOrder = 30, BrandID = 1, ImageLink = "" },
				new Product { ProductId = "M62N6HJ1", Brand = "Lenovo", Series = "Legion", Model = "Pro 5i", RegularPrice = 2099.99, DiscountPrice = 2059.99, DisplayOrder = 20, BrandID = 1, ImageLink = "" },
				new Product { ProductId = "A72N6HJ2", Brand = "Asus", Series = "TUF", Model = "A15", RegularPrice = 1399.99, DiscountPrice = 1259.99, DisplayOrder = 25, BrandID = 2, ImageLink = "" },
				new Product { ProductId = "RP2N6HJ1", Brand = "Asus", Series = "ROG", Model = "Strix G15", RegularPrice = 2599.99, DiscountPrice = 2399.99, DisplayOrder = 10, BrandID = 2, ImageLink = "" },
				new Product { ProductId = "GT2N6HJ1", Brand = "MSI", Series = "Titan", Model = "GT77", RegularPrice = 5099.99, DiscountPrice = 4959.99, DisplayOrder = 10, BrandID = 5, ImageLink = "" },
				new Product { ProductId = "HP2N6HJ1", Brand = "HP", Series = "Victus", Model = "15", RegularPrice = 1199.99, DiscountPrice = 1159.99, DisplayOrder = 20, BrandID = 6, ImageLink = "" },
				new Product { ProductId = "AC2N6HJ1", Brand = "ACER", Series = "Nitro", Model = "AN15", RegularPrice = 1159.99, DiscountPrice = 1150.99, DisplayOrder = 20, BrandID = 3, ImageLink = "" },
				new Product { ProductId = "DL2N6HJ1", Brand = "DELL", Series = "Alienware", Model = "RM15", RegularPrice = 4159.99, DiscountPrice = 4100.99, DisplayOrder = 5, BrandID = 4, ImageLink = "" }
			);
		}
	}
}
