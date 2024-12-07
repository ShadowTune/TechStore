﻿using Microsoft.EntityFrameworkCore;
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
			_db.Products.Include(u => u.Category).Include(u => u.BrandId);
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

		public T Get(Expression<Func<T, bool>> predicate, string? includeProperties = null)
		{
			IQueryable<T> query = dbSet.Where(predicate);

			// Include default properties if `includeProperties` is null
			includeProperties ??= "Category";

			foreach (var property in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
			{
				query = query.Include(property);
			}

			return query.FirstOrDefault();
		}


		public IEnumerable<T> GetAll(string? includeProperties = null)
		{

			IQueryable<T> query = dbSet;
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