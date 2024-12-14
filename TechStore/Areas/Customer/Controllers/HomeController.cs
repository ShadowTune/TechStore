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
			IEnumerable<Product> productList = _unitOfWork.Product.GetAll(includeProperties: "Category");
			return View(productList);
		}

		public IActionResult Details(string productId)
		{
			Cart cart = new()
			{
				Product = _unitOfWork.Product.Get(u => u.ProductId == productId, includeProperties: "Category"),
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
			var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
			cart.ApplicationUserId = userId;

			Cart cartFromDb = _unitOfWork.Cart.Get(u => u.ApplicationUserId == userId
			&& u.ProductId == cart.ProductId);

			if (cartFromDb != null)
			{
				cartFromDb.Count += cart.Count;
				_unitOfWork.Cart.Update(cart);
				_unitOfWork.Save();
			} else
			{
				_unitOfWork.Cart.Add(cart);
				_unitOfWork.Save();
				HttpContext.Session.SetInt32(SD.SessionCart,
					_unitOfWork.Cart.GetAll(u => u.ApplicationUserId == userId).Count());
			}

			// _unitOfWork.Cart.Add(cart);
			// Product product = _unitOfWork.Product.Get(u => u.ProductId == productId, includeProperties: "Category");
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
