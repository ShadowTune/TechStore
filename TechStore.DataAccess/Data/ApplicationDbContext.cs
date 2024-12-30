using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TechStore.Models;

namespace TechStore.DataAccess.Data
{
	public class ApplicationDbContext : IdentityDbContext<IdentityUser>
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{

		}
		public DbSet<Category> Categories { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<ApplicationUser> ApplicationUsers {  get; set; }
		public DbSet<Cart> Carts { get; set; }
		public DbSet<Company> Companies { get; set; }
		public DbSet<OrderHeader> OrderHeaders { get; set; }
		public DbSet<OrderDetail> OrderDetails { get; set; }
		public DbSet<Term> Terms { get; set; }
		public DbSet<Cookie> Cookies { get; set; }
		public DbSet<Privacy> Privacies { get; set; }
		public DbSet<Overview> Overviews { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<Category>().HasData(
				new Category { Id = 1, Name = "LENOVO", Series = "LOQ", DisplayOrder = 12 },
				new Category { Id = 2, Name = "ASUS", Series = "TUF", DisplayOrder = 5 },
				new Category { Id = 3, Name = "ACER", Series = "NITRO", DisplayOrder = 3 },
				new Category { Id = 4, Name = "DELL", Series = "ALIENWARE", DisplayOrder = 4 },
				new Category { Id = 5, Name = "MSI", Series = "TITAN", DisplayOrder = 6 },
				new Category { Id = 6, Name = "HP", Series = "VICTUS", DisplayOrder = 2 },
				new Category { Id = 7, Name = "ASUS", Series = "ROG", DisplayOrder = 6 }
			);

			modelBuilder.Entity<Company>().HasData(
				new Company { CompanyId = 1, Name = "LENOVO", Branch = "BANGKOK", State = "THAILAND", Support = "1-855-253-6686" },
				new Company { CompanyId = 2, Name = "ASUS", Branch = "KUALALAMPUR", State = "MALAYSIS", Support = "1-855-263-6686" },
				new Company { CompanyId = 3, Name = "ACER", Branch = "NYC", State = "US", Support = "1-855-783-6686" }
			);

			modelBuilder.Entity<Product>().HasData(
				new Product { ProductId = "09b4ab76-9b49-4278-8a31-e30b2bb244e5", BrandId = 1, Series = "LOQ", Model = "15IAX9", RegularPrice = 1099.99, DiscountPrice = 1059.99, DisplayOrder = 30, ImageLink = "", Specification = "abcd" },
				new Product { ProductId = "4aae79c9-e38e-43dc-83a5-79fe0bac94e5", BrandId = 2, Series = "TUF", Model = "A15", RegularPrice = 1399.99, DiscountPrice = 1259.99, DisplayOrder = 25, ImageLink = "", Specification = "abcd" },
				new Product { ProductId = "6b5786c6-3eed-4fb6-9183-6f5eaae7d897", BrandId = 7, Series = "TITAN", Model = "GT77", RegularPrice = 5099.99, DiscountPrice = 4959.99, DisplayOrder = 10, ImageLink = "", Specification = "abcd" },
				new Product { ProductId = "a55a96e0-660e-4407-ae45-bcb3b6f8a53e", BrandId = 6, Series = "VICTUS", Model = "15", RegularPrice = 1199.99, DiscountPrice = 1159.99, DisplayOrder = 20, ImageLink = "", Specification = "abcd" },
				new Product { ProductId = "b694c472-76e9-4e71-9ac0-ccd28944335c", BrandId = 3, Series = "NITRO", Model = "AN515", RegularPrice = 1159.99, DiscountPrice = 1150.99, DisplayOrder = 20, ImageLink = "", Specification = "abcd" },
				new Product { ProductId = "e612e509-77f0-4cd6-a784-0c75172db894", BrandId = 4, Series = "ALIENWARE", Model = "R15", RegularPrice = 4159.99, DiscountPrice = 4100.99, DisplayOrder = 5, ImageLink = "", Specification = "abcd" }
			);

			modelBuilder.Entity<Term>().HasData(
				new Term { TermId = 1, Description = "abcd" }
			);

			modelBuilder.Entity<Cookie>().HasData(
				new Cookie { CookieId = 1, Description = "abcd" }
			);

			modelBuilder.Entity<Privacy>().HasData(
				new Privacy { PrivacyId = 1, Description = "abcd" }
			);

			modelBuilder.Entity<Overview>().HasData(
				new Overview { OverviewId = 1, Description = "abcd", ImageLink = "" }
			);
		}
	}
}
