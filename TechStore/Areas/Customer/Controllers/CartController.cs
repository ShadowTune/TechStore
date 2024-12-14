using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;
using System.Security.Claims;
using TechStore.DataAccess.Repository;
using TechStore.DataAccess.Repository.IRepository;
using TechStore.Models;
using TechStore.Models.ViewModels;
using TechStore.Utility;

namespace TechStore.Areas.Customer.Controllers
{
	[Area("Customer")]
	[Authorize]
	public class CartController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		[BindProperty]
		public CartVM CartVM { get; set; }

		public CartController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public IActionResult Index()
		{
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
			CartVM = new()
			{
				CartList = _unitOfWork.Cart.GetAll(u =>
				u.ApplicationUserId == userId, includeProperties: "Product"),
				OrderHeader = new()
			};
			foreach (var cart in CartVM.CartList)
			{
				cart.CartCost = GetCartCost(cart);
				CartVM.OrderHeader.OrderCost += (cart.CartCost * cart.Count);
			}
			return View(CartVM);
		} 

		public IActionResult Summary(CartVM cartVM)
		{
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

			cartVM = new CartVM()
			{
				CartList = _unitOfWork.Cart.GetAll(u => 
				u.ApplicationUser.Id == userId, includeProperties: "Product"),
				OrderHeader = new()
			};

			cartVM.OrderHeader.ApplicationUser = _unitOfWork.ApplicationUser.Get(u => u.Id == userId);

			// cartVM.OrderHeader.ApplicationUser = _unitOfWork.ApplicationUser.Get(u => u. == claim.Value);
			// cartVM.OrderHeader.ApplicationUser = cartVM.OrderHeader.UserName;
			cartVM.OrderHeader.CustomerPhone = cartVM.OrderHeader.ApplicationUser.PhoneNumber;
			cartVM.OrderHeader.City = cartVM.OrderHeader.ApplicationUser.City;
			cartVM.OrderHeader.State = cartVM.OrderHeader.ApplicationUser.State;
			cartVM.OrderHeader.CustomerName = cartVM.OrderHeader.ApplicationUser.Name;
			// cartVM.OrderHeader.PostalCode = cartVM.OrderHeader.ApplicationUser.PostalCode;


			foreach (var cart in cartVM.CartList)
			{
				cart.CartCost = GetCartCost(cart);

				cartVM.OrderHeader.OrderCost += cart.CartCost * cart.Count;
			}
			return View(cartVM);
		}

		[HttpPost]
		[ActionName("Summary")]
		public IActionResult SummaryPOST(CartVM cartVM)
		{
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
			/*CartVM = new CartVM()
			{
				CartList = _unitOfWork.Cart.GetAll(u =>
				u.ApplicationUserId == userId, includeProperties: "Product"),
				OrderHeader = new()
			};*/

			CartVM.CartList = _unitOfWork.Cart.GetAll(u =>
							u.ApplicationUserId == userId, includeProperties: "Product");

			// cartVM.OrderHeader.ApplicationUser = _unitOfWork.ApplicationUser.Get(u => u. == claim.Value);
			// cartVM.OrderHeader.ApplicationUser = cartVM.OrderHeader.UserName;
			// cartVM.OrderHeader.CustomerPhone = cartVM.OrderHeader.ApplicationUser.PhoneNumber;
			// cartVM.OrderHeader.City = cartVM.OrderHeader.ApplicationUser.City;
			// cartVM.OrderHeader.State = cartVM.OrderHeader.ApplicationUser.State;
			// CartVM.OrderHeader.CustomerName = CartVM.OrderHeader.ApplicationUser.Name;
			// cartVM.OrderHeader.PostalCode = cartVM.OrderHeader.ApplicationUser.PostalCode;
			CartVM.OrderHeader.OrderDate = System.DateTime.Now;
			CartVM.OrderHeader.ApplicationUserId = userId;
			 
			ApplicationUser applicationUser = _unitOfWork.ApplicationUser.Get(u => u.Id == userId);

			foreach (var cart in CartVM.CartList)
			{
				cart.CartCost = GetCartCost(cart);

				CartVM.OrderHeader.OrderCost += cart.CartCost * cart.Count;
			}
			if (applicationUser.CompanyId.GetValueOrDefault() == 0)
			{
				CartVM.OrderHeader.PaymentStatus = SD.PaymentStatusPending;
				CartVM.OrderHeader.OrderStatus = SD.StatusPending;
			} else {
				CartVM.OrderHeader.PaymentStatus = SD.PaymentStatusDelayedPayment;
				CartVM.OrderHeader.OrderStatus = SD.StatusApproved;
			}

			_unitOfWork.OrderHeader.Add(CartVM.OrderHeader);
			_unitOfWork.Save();

			foreach (var cart in CartVM.CartList)
			{
				OrderDetail orderDetail = new()
				{
					ProductId = cart.ProductId,
					OrderHeaderId = CartVM.OrderHeader.OrderHeaderId,
					OrderPrice = cart.CartCost,
					OrderCount = cart.Count
				};
				_unitOfWork.OrderDetail.Add(orderDetail);
				_unitOfWork.Save();
			}

			if (applicationUser.CompanyId.GetValueOrDefault() == 0)
			{
				StripeConfiguration.ApiKey = "sk_test_51QUhfDArRUxZNyrM4W49NP1r7b0eDC6ynRZPELM60OwIri4A3AhCOcpC3BLqmb445btrLHXWJerDtfYGbc7mYueG00J9jeQlQ2";
				var domain = "https://localhost:7007/";
				var options = new SessionCreateOptions
				{
					SuccessUrl = domain+ $"customer/cart/OrderConfirmation?orderheaderId={CartVM.OrderHeader.OrderHeaderId}",
					CancelUrl = domain+"customer/cart/index",
					LineItems = new List<SessionLineItemOptions>(),
					Mode = "payment",
				};

				foreach (var items in CartVM.CartList)
				{
					var sessionLineItem = new SessionLineItemOptions
					{
						PriceData = new SessionLineItemPriceDataOptions
						{
							UnitAmount = (long)items.CartCost * 100,
							Currency = "usd",
							ProductData = new SessionLineItemPriceDataProductDataOptions
							{
								Name = items.Product.Series.ToString() + " " + items.Product.Model.ToString()
							}
						},
						Quantity = items.Count
					};
					options.LineItems.Add(sessionLineItem);
				}
				var service = new SessionService();
				Session session = service.Create(options);
				_unitOfWork.OrderHeader.UpdateStripePaymentId(CartVM.
					OrderHeader.OrderHeaderId, session.Id, session.PaymentIntentId);
				
				_unitOfWork.Save();
				Response.Headers.Add("Location", session.Url);
				return new StatusCodeResult(303);
			}

			return RedirectToAction(nameof(OrderConfirmation), 
				new {orderheaderId = CartVM.OrderHeader.OrderHeaderId});
			
		}

		public IActionResult OrderConfirmation(int orderHeaderId)
		{
			OrderHeader orderHeader = _unitOfWork.OrderHeader.Get(u => 
				u.OrderHeaderId == orderHeaderId, includeProperties: "ApplicationUser");	

			if (orderHeader.PaymentStatus != SD.PaymentStatusDelayedPayment)
			{
				var service = new SessionService();
				Session session = service.Get(orderHeader.SessionId);
				if (session.PaymentStatus.ToLower() == "paid")
				{
					_unitOfWork.OrderHeader.UpdateStripePaymentId(
						orderHeaderId, session.Id, session.PaymentIntentId);

					_unitOfWork.OrderHeader.UpdateStatus(orderHeaderId, 
						SD.StatusApproved, SD.PaymentStatusApproved);

					_unitOfWork.Save();
				}
			}
			
			List<Cart> carts = _unitOfWork.Cart.GetAll(
				u => u.ApplicationUserId == orderHeader.ApplicationUserId).ToList();

			_unitOfWork.Cart.RemoveRange(carts);
			_unitOfWork.Save();
			return View(orderHeaderId);
		}

		private double GetCartCost(Cart cart)
		{
			if (cart.Count > 2)
			{
				return cart.Product.DiscountPrice;
			}
			return cart.Product.RegularPrice;
		}

		public IActionResult Plus(int cartId)
		{
			var cartFromDb = _unitOfWork.Cart.Get(u => u.CartId == cartId);
			cartFromDb.Count += 1;
			_unitOfWork.Cart.Update(cartFromDb);
			_unitOfWork.Cart.Save();
			return RedirectToAction("Index");
		}

		public IActionResult Minus(int cartId)
		{
			var cartFromDb = _unitOfWork.Cart.Get(u => u.CartId == cartId);
			if (cartFromDb.Count == 1)
			{
				_unitOfWork.Cart.Remove(cartFromDb);
			}
			else
			{
				cartFromDb.Count -= 1;
				_unitOfWork.Cart.Update(cartFromDb);
			}
			_unitOfWork.Cart.Save();
			return RedirectToAction("Index");
		}

		public IActionResult Remove(int cartId)
		{
			var cartFromDb = _unitOfWork.Cart.Get(u => u.CartId == cartId);
			_unitOfWork.Cart.Remove(cartFromDb);
			_unitOfWork.Cart.Save();
			return RedirectToAction("Index");
		}
	}
}
