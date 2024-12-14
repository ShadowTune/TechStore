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
	public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
	{
		private ApplicationDbContext _db;
		public ApplicationUserRepository(ApplicationDbContext db) : base(db)
		{
			_db = db;
		}

		public void Add(Cart entity)
		{
			// throw new NotImplementedException();
		}

		public void Remove(Cart entity)
		{
			throw new NotImplementedException();
		}

		public void RemoveRange(IEnumerable<Cart> entity)
		{
			throw new NotImplementedException();
		}

	}
}