using Microsoft.EntityFrameworkCore;
using TechStore.Models;

namespace TechStore.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{
			
		}
		public DbSet<Category> Categories { get; set; }
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
		}
	}
}
