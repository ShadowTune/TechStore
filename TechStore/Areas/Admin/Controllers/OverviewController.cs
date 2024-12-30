using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using TechStore.DataAccess.Repository;
using TechStore.DataAccess.Repository.IRepository;
using TechStore.Models;
using TechStore.Models.ViewModels;
using TechStore.Utility;

namespace TechStore.Areas.Customer.Controllers
{
	[Area("Admin")]
	[Authorize]
	public class OverviewController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IWebHostEnvironment _webHostEnvironment;
		public OverviewController(IUnitOfWork unitofwork, IWebHostEnvironment webHostEnvironment)
		{
			_unitOfWork = unitofwork;
			_webHostEnvironment = webHostEnvironment;
		}
		public IActionResult Description(int id = 1)
		{
			Overview overview = _unitOfWork.Overview.Get(u => u.OverviewId == id);
			return View(overview);
		}

		public IActionResult UpdateAdd(int id = 1)
		{
			Overview overview = _unitOfWork.Overview.Get(u => u.OverviewId == id);
			return View(overview);
			// not required to pass an object
		}
		[HttpPost]
		public IActionResult UpdateAdd(Overview overview, IFormFile? file)
		{
			string wwwRootPath = _webHostEnvironment.WebRootPath;
			if (file != null)
			{
				string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
				string productPath = Path.Combine(wwwRootPath, @"images\product");

				if (!string.IsNullOrEmpty(overview.ImageLink))
				{
					var oldImagePath = Path.Combine(wwwRootPath, overview.ImageLink.TrimStart('\\'));
					if (System.IO.File.Exists(oldImagePath))
					{
						System.IO.File.Delete(oldImagePath);
					}
				}
				// Save the uploaded file
				using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
				{
					file.CopyTo(fileStream);
				}

				// Set the new image link
				overview.ImageLink = @"\images\product\" + fileName;
			}
			_unitOfWork.Overview.Update(overview);
			_unitOfWork.Overview.Save();
			TempData["success"] = "Overviews Updated Successfully";
			return RedirectToAction("Description");
		}
	}
}
