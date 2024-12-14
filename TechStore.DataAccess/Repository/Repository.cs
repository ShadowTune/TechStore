using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TechStore.DataAccess.Data;
using TechStore.DataAccess.Repository.IRepository;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
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
			_db.Products.Include(u => u.Category).Include(u => u.BrandId).Include(u => u.ProductId).Include(u => u.BrandId);
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

		public T Get(Expression<Func<T, bool>>? predicate = null, string? includeProperties = null, bool tracked = false)
		{
			if (tracked == true) { 
				IQueryable<T> query = dbSet.Where(predicate);
				if (!string.IsNullOrEmpty(includeProperties))
				{
					foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
					{
						query = query.Include(includeProperty.Trim());
					}
				}
				return query.FirstOrDefault();
			} else {
				IQueryable<T> query = dbSet.AsNoTracking().Where(predicate);
				if (!string.IsNullOrEmpty(includeProperties))
				{
					foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
					{
						query = query.Include(includeProperty.Trim());
					}
				}
				return query.FirstOrDefault();
			}
		}


		public IEnumerable<T> GetAll(Expression<Func<T, bool>>? predicate = null, string? includeProperties = null)
		{

			IQueryable<T> query = dbSet;
			if (predicate != null)
			{
				query = query.Where(predicate);
			}
			if (!string.IsNullOrEmpty(includeProperties)) 
			{
				foreach (var property in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
				{
					query = query.Include(property);
				}
			}
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