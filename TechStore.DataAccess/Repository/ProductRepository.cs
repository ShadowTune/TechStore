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
			var product2 = _db.Products.FirstOrDefault(u => u.ProductId == product.ProductId);
			if (product2 != null)
			{
				product2.ProductId = product.ProductId;
				product2.RegularPrice = product.RegularPrice;
				product2.DiscountPrice = product.DiscountPrice;	
				product2.Series = product.Series;
				product2.Model = product.Model;
				product2.DisplayOrder = product.DisplayOrder;
				product2.Specification = product.Specification;	
				product2.BrandId = product.BrandId;
				if (product.ImageLink != null)
				{
					product2.ImageLink = product.ImageLink;
				}
			}

		}
	}
}
