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
	public class PrivacyController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		// private readonly IWebHostEnvironment _webHostEnvironment;
		public PrivacyController(IUnitOfWork unitofwork)
		{
			_unitOfWork = unitofwork;
			// _webHostEnvironment = webHostEnvironment;
		}
		public IActionResult Description(int id = 1)
		{
			Privacy Privacy = _unitOfWork.Privacy.Get(u => u.PrivacyId == id);
			return View(Privacy);
		}

		public IActionResult UpdateAdd(int id = 1)
		{
			Privacy Privacy = _unitOfWork.Privacy.Get(u => u.PrivacyId == id);
			return View(Privacy);
			// not required to pass an object
		}
		[HttpPost]
		public IActionResult UpdateAdd(Privacy Privacy)
		{
			_unitOfWork.Privacy.Update(Privacy);
			_unitOfWork.Privacy.Save();
			TempData["success"] = "Privacies Updated Successfully";
			return RedirectToAction("Description");
		}
	}
}
