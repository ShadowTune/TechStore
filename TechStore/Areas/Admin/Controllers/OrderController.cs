using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;
using System.Diagnostics;
using System.Security.Claims;
using TechStore.DataAccess.Repository;
using TechStore.DataAccess.Repository.IRepository;
using TechStore.Models;
using TechStore.Models.ViewModels;
using TechStore.Utility;

namespace TechStore.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize]
	public class OrderController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;

		[BindProperty]
		public OrderVM OrderVM { get; set; }
		public OrderController (IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Details(int Id)
		{
			OrderVM = new()
			{
				OrderHeader = _unitOfWork.OrderHeader.Get(u => u.Id == Id, includeProperties: "ApplicationUser"),
				OrderDetail = _unitOfWork.OrderDetail.GetAll(u => u.OrderHeaderId == Id, includeProperties: "Product")
			};
			return View(OrderVM);
		}

		[HttpPost]
		[Authorize(Roles = SD.Role_admin + ", " + SD.Role_employee)]
		public IActionResult UpdateOrderDetails()
		{
			/*OrderVM orderVM = new()
			{
				OrderHeader = _unitOfWork.OrderHeader.Get(u => u.Id == Id, includeProperties: "ApplicationUser"),
				OrderDetail = _unitOfWork.OrderDetail.GetAll(u => u.OrderHeaderId == Id, includeProperties: "Product")
			};*/
			var orderHeaderDb = _unitOfWork.OrderHeader.Get(u => u.Id == OrderVM.OrderHeader.Id);
			orderHeaderDb.CustomerName = OrderVM.OrderHeader.CustomerName;
			orderHeaderDb.CustomerPhone = OrderVM.OrderHeader.CustomerPhone;
			orderHeaderDb.City = OrderVM.OrderHeader.City;
			orderHeaderDb.State = OrderVM.OrderHeader.State;
			if (!string.IsNullOrEmpty(OrderVM.OrderHeader.Courier))
			{
				orderHeaderDb.Courier = OrderVM.OrderHeader.Courier;
			}
			if (!string.IsNullOrEmpty(OrderVM.OrderHeader.TrackNumber))
			{
				orderHeaderDb.TrackNumber = OrderVM.OrderHeader.TrackNumber;
			}

			_unitOfWork.OrderHeader.Update(orderHeaderDb);
			_unitOfWork.Save();
			TempData["success"] = "Order Details Updated Successfully";
			// return View(orderVM); 
			return RedirectToAction(nameof(Details), new { orderHeaderDb.Id });
		}

		[HttpPost]
		[Authorize(Roles = SD.Role_admin + ", " + SD.Role_employee)]
		public ActionResult StartProcessing()
		{
			_unitOfWork.OrderHeader.UpdateStatus(OrderVM.OrderHeader.Id, SD.StatusInProcess);
			_unitOfWork.Save();
			TempData["success"] = "Order Details Updated Successfully";
			return RedirectToAction(nameof(Details), new {OrderVM.OrderHeader.Id});
		}

		[HttpPost]
		[ActionName("Details")]
		public ActionResult OrderPayNow()
		{
			OrderVM.OrderHeader = _unitOfWork.OrderHeader.Get(u => u.Id == OrderVM.OrderHeader.Id, includeProperties: "ApplicationUser");
			OrderVM.OrderDetail = _unitOfWork.OrderDetail.GetAll(u => u.OrderHeaderId == OrderVM.OrderHeader.Id, includeProperties: "Product");
			var domain = "https://localhost:7007/";
			var options = new SessionCreateOptions
			{
				SuccessUrl = domain + $"admin/order/PaymentConfirmation?orderheaderId={OrderVM.OrderHeader.Id}",
				CancelUrl = domain + $"admin/order/details?orderId={OrderVM.OrderHeader.Id}",
				LineItems = new List<SessionLineItemOptions>(),
				Mode = "payment",
			};

			foreach (var items in OrderVM.OrderDetail)
			{
				var sessionLineItem = new SessionLineItemOptions
				{
					PriceData = new SessionLineItemPriceDataOptions
					{
						UnitAmount = (long)(items.OrderPrice * 100),
						Currency = "usd",
						ProductData = new SessionLineItemPriceDataProductDataOptions
						{
							Name = items.Product.Series.ToString() + " " + items.Product.Model.ToString()
						}
					},
					Quantity = items.OrderCount
				};
				options.LineItems.Add(sessionLineItem);
			}
			var stripeClient = new Stripe.StripeClient(StripeConfiguration.ApiKey);
			var service = new SessionService(stripeClient);
			Session session = service.Create(options);
			_unitOfWork.OrderHeader.UpdateStripePaymentId(OrderVM.OrderHeader.Id, session.Id, session.PaymentIntentId);

			_unitOfWork.Save();
			Response.Headers.Add("Location", session.Url);
			return new StatusCodeResult(303);
			// return RedirectToAction(nameof(Details), new { OrderVM.OrderHeader.Id });
		}

		public IActionResult PaymentConfirmation(int orderHeaderId)
		{
			OrderHeader orderHeader = _unitOfWork.OrderHeader.Get(u => u.Id == orderHeaderId);

			if (orderHeader.PaymentStatus == SD.PaymentStatusDelayedPayment)
			{
				var service = new SessionService();
				Session session = service.Get(orderHeader.SessionId);
				if (session.PaymentStatus.ToLower() == "paid")
				{
					_unitOfWork.OrderHeader.UpdateStripePaymentId(
						orderHeaderId, session.Id, session.PaymentIntentId);

					_unitOfWork.OrderHeader.UpdateStatus(orderHeaderId,
						orderHeader.OrderStatus, SD.PaymentStatusApproved);

					_unitOfWork.Save();
				}
			}
			return View(orderHeaderId);
		}

		[HttpPost]
		[Authorize(Roles = SD.Role_admin + ", " + SD.Role_employee)]
		public ActionResult ShipOrder()
		{
			var orderHeader = _unitOfWork.OrderHeader.Get(u => u.Id == OrderVM.OrderHeader.Id);
			orderHeader.TrackNumber = OrderVM.OrderHeader.TrackNumber;
			orderHeader.Courier = OrderVM.OrderHeader.Courier;
			orderHeader.OrderStatus = SD.StatusBoarded;
			orderHeader.BoardDate = DateTime.Now;	
			if (orderHeader.PaymentStatus == SD.PaymentStatusDelayedPayment)
			{
				orderHeader.PaymentDueDate = DateOnly.FromDateTime(DateTime.Now.AddDays(30));
			}
			_unitOfWork.OrderHeader.Update(orderHeader);
			_unitOfWork.Save();
			TempData["success"] = "Order Shipped Successfully";
			return RedirectToAction(nameof(Details), new { OrderVM.OrderHeader.Id });
		}

		[HttpPost]
		[Authorize(Roles = SD.Role_admin + ", " + SD.Role_employee)]
		public ActionResult OrderCancel()
		{
			var orderHeader = _unitOfWork.OrderHeader.Get(u => u.Id == OrderVM.OrderHeader.Id);
			/*orderHeader.TrackNumber = OrderVM.OrderHeader.TrackNumber;
			orderHeader.Courier = OrderVM.OrderHeader.Courier;
			orderHeader.OrderStatus = SD.StatusBoarded;
			orderHeader.BoardDate = DateTime.Now;*/
			if (orderHeader.PaymentStatus == SD.PaymentStatusApproved)
			{
				// orderHeader.PaymentDueDate = DateOnly.FromDateTime(DateTime.Now.AddDays(30));
				var stripeClient = new Stripe.StripeClient(StripeConfiguration.ApiKey);
				var service = new RefundService(stripeClient);
				var options = new RefundCreateOptions
				{
					Reason = RefundReasons.RequestedByCustomer,
					PaymentIntent = orderHeader.PaymentIntentId
				};
				Refund refund = service.Create(options);
				_unitOfWork.OrderHeader.UpdateStatus(orderHeader.Id, SD.StatusCancelled, SD.StatusRefunded);
			} else
			{
				_unitOfWork.OrderHeader.UpdateStatus(orderHeader.Id, SD.StatusCancelled, SD.StatusCancelled);
			}
			// _unitOfWork.OrderHeader.Update(orderHeader);
			_unitOfWork.Save();
			TempData["success"] = "Order Cancelled Successfully";
			return RedirectToAction(nameof(Details), new { OrderVM.OrderHeader.Id });
		}

		[HttpGet] 
		public IActionResult GetAll(string status)
		{
			IEnumerable<OrderHeader> orderHeaders;

			if (User.IsInRole(SD.Role_admin) || User.IsInRole(SD.Role_employee))
			{
				orderHeaders = _unitOfWork.OrderHeader.GetAll(includeProperties: "ApplicationUser").ToList();
			} else
			{
				var claimsIdentity = (ClaimsIdentity)User.Identity;
				var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
				orderHeaders = _unitOfWork.OrderHeader.GetAll(u => u.ApplicationUserId == claim, includeProperties: "ApplicationUser");
			}

			switch (status)
			{
				case "pending":
					orderHeaders = orderHeaders.Where(u => u.PaymentStatus == SD.PaymentStatusPending);
					break;

				case "inprocess":
					orderHeaders = orderHeaders.Where(u => u.OrderStatus == SD.StatusInProcess);
					break;

				case "completed":
					orderHeaders = orderHeaders.Where(u => u.OrderStatus == SD.StatusBoarded);
					break;

				case "approved":
					orderHeaders = orderHeaders.Where(u => u.OrderStatus == SD.StatusApproved);
					break;

				default:
					break;
			}

			return Json(new { data = orderHeaders });  
		}
	}
}
