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
	public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
	{
		private ApplicationDbContext _db;
		public OrderHeaderRepository(ApplicationDbContext db) : base(db)
		{
			_db = db;
		}
		/*public void Save()
		{
			_db.SaveChanges();
			// throw new NotImplementedException();
		}*/

		public void Update(OrderHeader orderHeader)
		{
			_db.OrderHeaders.Update(orderHeader);
			// throw new NotImplementedException();
		}

		public void UpdateStatus(int orderHeaderId, string orderStatus, string? paymentStatus = null)
		{
			var orderFromDb = _db.OrderHeaders.FirstOrDefault(u => u.Id == orderHeaderId);
			// throw new NotImplementedException();
			if (orderFromDb != null)
			{
				orderFromDb.OrderStatus = orderStatus;
				if (!string.IsNullOrEmpty(paymentStatus))
				{	
					orderFromDb.PaymentStatus = paymentStatus;
				}
			}
		}

		public void UpdateStripePaymentId(int orderHeaderId, string sessionId, string paymentIntentId)
		{
			// throw new NotImplementedException();
			var orderFromDb = _db.OrderHeaders.FirstOrDefault(u => u.Id == orderHeaderId);
			// throw new NotImplementedException();
			if (!string.IsNullOrEmpty(sessionId))
			{
				orderFromDb.SessionId = sessionId;
			}
			if (!string.IsNullOrEmpty(paymentIntentId))
			{
				orderFromDb.PaymentIntentId = paymentIntentId;
				orderFromDb.PaymentDate = DateTime.Now;
			}
		}
	}
}