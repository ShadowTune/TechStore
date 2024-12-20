using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechStore.DataAccess.Repository;
using TechStore.DataAccess.Repository.IRepository;
using TechStore.Models;
using TechStore.Utility;

namespace TechStore.Areas.Customer.Controllers
{
	[Area("Admin")]
	[Authorize]
	public class CookieController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		// private readonly IWebHostEnvironment _webHostEnvironment;
		public CookieController(IUnitOfWork unitofwork)
		{
			_unitOfWork = unitofwork;
			// _webHostEnvironment = webHostEnvironment;
		}
		public IActionResult Description(int id = 1)
		{
			Cookie cookie = _unitOfWork.Cookie.Get(u => u.CookieId == id);
			return View(cookie);
		}

		public IActionResult UpdateAdd(int id = 1)
		{
			Cookie term = _unitOfWork.Cookie.Get(u => u.CookieId == id);
			return View(term);
			// not required to pass an object
		}
		[HttpPost]
		public IActionResult UpdateAdd(Cookie term)
		{
			_unitOfWork.Cookie.Update(term);
			_unitOfWork.Cookie.Save();
			TempData["success"] = "Cookies Updated Successfully";
			return RedirectToAction("Description");
		}
	}
}
