using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechStore.Models;

namespace TechStore.DataAccess.Repository.IRepository
{
	public interface IOrderHeaderRepository : IRepository<OrderHeader>
	{
		void Update(OrderHeader orderHeader);
		// void Save();
		void UpdateStatus(int orderHeaderId, string orderStatus, string? paymentStatus = null);
		void UpdateStripePaymentId(int orderHeaderId, string sessionId, string paymentIntentId);
	}
}
