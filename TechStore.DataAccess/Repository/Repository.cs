using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TechStore.DataAccess.Data;
using TechStore.DataAccess.Repository.IRepository;
namespace TechStore.DataAccess.Repository
{
	public class Repository<T> : IRepository<T> where T : class
	{
		private readonly ApplicationDbContext _db;
		internal DbSet<T> dbSet;
		public Repository(ApplicationDbContext db)
		{
			_db = db;
			this.dbSet = _db.Set<T>();
		}
		public void Add(T entity)
		{
			dbSet.Add(entity);
			// throw new NotImplementedException();
		}

		/*public void Delete(T entity)
		{
			throw new NotImplementedException();
		}*/

		public T Get(Expression<Func<T, bool>> predicate)
		{
			IQueryable<T> query = dbSet;
			query = query.Where(predicate);
			return query.FirstOrDefault();
			// throw new NotImplementedException();
		}

		public IEnumerable<T> GetAll()
		{
			IQueryable<T> query = dbSet;
			return query.ToList();
			// throw new NotImplementedException();
		}
		public void Remove(T entity)
		{
			dbSet.Remove(entity);
		}

		public void RemoveRange(IEnumerable<T> entity)
		{
			dbSet.RemoveRange(entity);
			// throw new NotImplementedException();
		}
	}
}