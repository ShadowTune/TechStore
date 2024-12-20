using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechStore.Models;

namespace TechStore.DataAccess.Repository.IRepository
{
	public interface ICookieRepository : IRepository<Cookie>
	{
		void Update(Cookie cookie);
		void Save();
	}
}
