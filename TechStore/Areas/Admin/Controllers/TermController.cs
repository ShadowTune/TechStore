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
	public class TermController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		// private readonly IWebHostEnvironment _webHostEnvironment;
		public TermController(IUnitOfWork unitofwork)
		{
			_unitOfWork = unitofwork;
			// _webHostEnvironment = webHostEnvironment;
		}
		public IActionResult Description(int id = 1)
		{
			Term term = _unitOfWork.Term.Get(u => u.TermId == id);
			return View(term);
		}

		public IActionResult UpdateAdd(int id = 1)
		{
			Term term = _unitOfWork.Term.Get(u => u.TermId == id);
			return View(term);
			// not required to pass an object
		}
		[HttpPost]
		public IActionResult UpdateAdd(Term term)
		{
			_unitOfWork.Term.Update(term);
			_unitOfWork.Term.Save();
			TempData["success"] = "Terms Updated Successfully";
			return RedirectToAction("Description");
		}
	}
}
