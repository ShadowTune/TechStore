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
				new Category { Id = 1, Name = "LENOVO", Series = "LOQ", DisplayOrder = 12},
				new Category { Id = 2, Name = "ASUS", Series = "TUF", DisplayOrder = 5 },
				new Category { Id = 3, Name = "ACER", Series = "NITRO", DisplayOrder = 3 },
				new Category { Id = 4, Name = "DELL", Series = "ALIENWARE", DisplayOrder = 4 },
				new Category { Id = 5, Name = "MSI", Series = "TITAN", DisplayOrder = 6 },
				new Category { Id = 6, Name = "HP", Series = "VICTUS", DisplayOrder = 2 },
				new Category { Id = 7, Name = "ASUS", Series = "ROG", DisplayOrder = 6 },
				new Category { Id = 8, Name = "LENOVO", Series = "LEGION", DisplayOrder = 15 }
			);
			modelBuilder.Entity<Product>().HasData(
				new Product { ProductId = "MP2N6HJ1", Brand = "LENOVO", Series = "LOQ", Model = "15IAX9", RegularPrice = 1099.99, DiscountPrice = 1059.99, DisplayOrder = 30, ImageLink = "", Specification = "abcd" },
				new Product { ProductId = "M62N6HJ1", Brand = "LENOVO", Series = "LEGION", Model = "PRO 5i", RegularPrice = 2099.99, DiscountPrice = 2059.99, DisplayOrder = 20, ImageLink = "", Specification = "abcd" },
				new Product { ProductId = "A72N6HJ2", Brand = "ASUS", Series = "TUF", Model = "A15", RegularPrice = 1399.99, DiscountPrice = 1259.99, DisplayOrder = 25, ImageLink = "", Specification = "abcd" },
				new Product { ProductId = "RP2N6HJ1", Brand = "ASUS", Series = "ROG", Model = "STRIX G15", RegularPrice = 2599.99, DiscountPrice = 2399.99, DisplayOrder = 10, ImageLink = "", Specification = "abcd" },
				new Product { ProductId = "GT2N6HJ1", Brand = "MSI", Series = "TITAN", Model = "GT77", RegularPrice = 5099.99, DiscountPrice = 4959.99, DisplayOrder = 10, ImageLink = "", Specification = "abcd" },
				new Product { ProductId = "HP2N6HJ1", Brand = "HP", Series = "VICTUS", Model = "15", RegularPrice = 1199.99, DiscountPrice = 1159.99, DisplayOrder = 20, ImageLink = "", Specification = "abcd" },
				new Product { ProductId = "AC2N6HJ1", Brand = "ACER", Series = "NITRO", Model = "AN15", RegularPrice = 1159.99, DiscountPrice = 1150.99, DisplayOrder = 20, ImageLink = "", Specification = "abcd" },
				new Product { ProductId = "DL2N6HJ1", Brand = "DELL", Series = "ALIENWARE", Model = "RM15", RegularPrice = 4159.99, DiscountPrice = 4100.99, DisplayOrder = 5, ImageLink = "", Specification = "abcd" }
			);
		}
	}
}
