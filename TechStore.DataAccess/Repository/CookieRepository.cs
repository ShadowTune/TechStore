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
	public class CookieRepository : Repository<Cookie>, ICookieRepository
	{
		private ApplicationDbContext _db;
		public CookieRepository(ApplicationDbContext db) : base(db)
		{
			_db = db;
		}
		public void Save()
		{
			_db.SaveChanges();
			// throw new NotImplementedException();
		}

		public void Update(Cookie cookie)
		{
			_db.Cookies.Update(cookie);
			// hrow new NotImplementedException();
		}
	}
}