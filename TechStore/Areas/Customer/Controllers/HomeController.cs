using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechStore.DataAccess.Repository.IRepository;
using TechStore.Models;
using TechStore.Utility;

namespace TechStore.Areas.Customer.Controllers
{
	[Area("Customer")]
	[Authorize]
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly IUnitOfWork _unitOfWork;

		public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
		{
			_logger = logger;
			_unitOfWork = unitOfWork;
		}

		public IActionResult Index()
		{
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
			if (claim != null)
			{
				HttpContext.Session.SetInt32(SD.SessionCart, _unitOfWork.
					Cart.GetAll(u => u.ApplicationUserId == claim.Value).Count());
			}
			IEnumerable<Product> productList = _unitOfWork.Product.GetAll(includeProperties: "Category");
			return View(productList);
		}
		 
		public IActionResult Details(string productId)
		{
			Cart cart = new()
			{
				Product = _unitOfWork.Product.Get(u => 
				u.ProductId == productId, includeProperties: "Category"),
				Count = 0,
				ProductId = productId
			};
			// Product product = _unitOfWork.Product.Get(u => u.ProductId == productId, includeProperties: "Category");
			return View(cart);
		}

		[HttpPost]
		[Authorize]
		public IActionResult Details(Cart cart)
		{
			/*Cart cart = new()
			{
				Product = _unitOfWork.Product.Get(u => u.ProductId == productId, includeProperties: "Category"),
				Count = 1,
				ProductId = productId
			};*/
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
			cart.ApplicationUserId = claim.Value;

			Cart cartFromDb = _unitOfWork.Cart.Get(u => 
			u.ApplicationUserId == claim.Value && u.ProductId == cart.ProductId);

			if (cartFromDb != null)
			{
				cartFromDb.Count += cart.Count;
				_unitOfWork.Cart.Update(cartFromDb);
				_unitOfWork.Save();
			} else
			{
				_unitOfWork.Cart.Add(cart);
				_unitOfWork.Save();
				HttpContext.Session.SetInt32(SD.SessionCart, _unitOfWork.
					Cart.Get(u => u.ApplicationUserId == claim.Value).Count);
			}
			// _unitOfWork.Cart.Add(cart);
			// Product product = _unitOfWork.Product.Get(u => u.ProductId == productId, includeProperties: "Category");
			TempData["success"] = "Product Added To Cart";
			return RedirectToAction("Index");
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
