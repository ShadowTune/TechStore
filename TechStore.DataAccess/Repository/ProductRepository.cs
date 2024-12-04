using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechStore.DataAccess.Data;
using TechStore.DataAccess.Repository.IRepository;
using TechStore.Models;

namespace TechStore.DataAccess.Repository
{
	public class ProductRepository : Repository<Product>, IProductRepository
	{
		private ApplicationDbContext _db;
		public ProductRepository(ApplicationDbContext db) : base(db)
		{ 
			_db = db;
		}

		public void Update(Product product)
		{
			_db.Products.Update(product);
		}
	}
}
