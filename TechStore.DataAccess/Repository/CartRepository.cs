using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TechStore.DataAccess.Data;
using TechStore.DataAccess.Repository.IRepository;
using TechStore.Models;

namespace TechStore.DataAccess.Repository
{
	public class CartRepository : Repository<Cart>, ICartRepository
	{
		private ApplicationDbContext _db;
		public CartRepository(ApplicationDbContext db) : base(db)
		{
			_db = db;
		}

		public void Add(Cart cart)
		{
			_db.Add(cart);
			// throw new NotImplementedException();
		}
		public void Save()
		{
			_db.SaveChanges();
			// throw new NotImplementedException();
		}

		public void Update(Cart cart)
		{
			_db.Carts.Update(cart);
			// hrow new NotImplementedException();
		}
	}
}