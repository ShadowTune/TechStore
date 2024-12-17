using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.ProjectModel;
using System.Collections.Generic;
using TechStore.DataAccess.Data;
using TechStore.DataAccess.Repository;
using TechStore.DataAccess.Repository.IRepository;
using TechStore.Models;
using TechStore.Models.ViewModels;
using TechStore.Utility;

namespace TechStore.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = SD.Role_admin)]
	public class CompanyController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		// private readonly IWebHostEnvironment _webHostEnvironment;
		public CompanyController(IUnitOfWork unitofwork, IWebHostEnvironment webHostEnvironment)
		{
			_unitOfWork = unitofwork;
			// _webHostEnvironment = webHostEnvironment;
		}
		public IActionResult Index()
		{
			List<Company> objCompanyList = _unitOfWork.Company.GetAll().ToList();
			return View(objCompanyList);
		}
		/*public IActionResult Add(string? selectedBrand = null)
		{
			// Fetch all distinct brands from the database
			var brands = _unitOfWork.Category.GetAll()
				.Select(c => c.Name)
				.Distinct()
				.ToList();

			// Initialize an empty list for series
			List<SelectListItem> seriesList = new List<SelectListItem>();

			// If a brand is selected, fetch its corresponding series
			if (!string.IsNullOrEmpty(selectedBrand))
			{
				seriesList = _unitOfWork.Company.GetAll()
					.Where(p => p.Brand == selectedBrand) // Filter by the selected brand
					.Select(p => p.Series)
					.Distinct()
					.Select(series => new SelectListItem
					{
						Text = series,
						Value = series
					})
					.ToList();
			}

			// Create the ViewModel
			CompanyVM companyVM = new()
			{
				// Pass the brand dropdown
				CategoryList = brands.Select(b => new SelectListItem
				{
					Text = b,
					Value = b,
					Selected = b == selectedBrand // Pre-select the brand if already chosen
				}).ToList(),

				// Pass the filtered series list
				SeriesList = seriesList,

				// Initialize an empty company object
				Company = new Company
				{
					Brand = selectedBrand // Retain the selected brand in the model
				}
			};

			return View(companyVM);
		}
		[HttpPost]
		public IActionResult Add(CompanyVM companyVM)
		{
			if (ModelState.IsValid)
			{
				_unitOfWork.Company.Add(companyVM.Company);
				_unitOfWork.Save();
				TempData["success"] = "Stock Order Added Successfully";
				return RedirectToAction("Index");
			} else
			{
				companyVM.CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
				{
					Text = u.Name,
					Value = u.Id.ToString()
				});
				return View(companyVM);
			}
			// not required to pass an object
		}

		public IActionResult Edit(string id)
		{
			CompanyVM companyVM = new()
			{
				CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
				{
					Text = u.Name,
					Value = u.Id.ToString()
				}),
				Company = new Company()
			};
			companyVM.Company = _unitOfWork.Company.Get(u => u.CompanyId == id);
			return View(companyVM);
			// not required to pass an object
		}
		[HttpPost]
		public IActionResult Edit(CompanyVM companyVM, IFormFile? file)
		{
			if (ModelState.IsValid)
			{
				_unitOfWork.Company.Update(companyVM.Company);
				_unitOfWork.Save();
				TempData["success"] = "Stock Order Updated Successfully";
				return RedirectToAction("Index");
			} 
			return View(companyVM);
			// not required to pass an object
		}
		*/

		public IActionResult UpdateAdd(int id)
		{

			if (id == 0)
			{
				return View(new Company());
			}
			else
			{
				Company company = _unitOfWork.Company.Get(u => u.CompanyId == id);
				return View(company);
			}
			// not required to pass an object
		}
		[HttpPost]
		public IActionResult UpdateAdd(Company company)
		{
			if (ModelState.IsValid)
			{
				// Add or update the company
				if (company.CompanyId == 0)
				{
					// New company
					// company.CompanyId = Guid.NewGuid().ToString();
					_unitOfWork.Company.Add(company);
				}
				else
				{
					// Existing company: Update it
					_unitOfWork.Company.Update(company);
				}

				// Save changes to the database
				_unitOfWork.Save();
				TempData["success"] = "Company saved successfully.";
				return RedirectToAction("Index");
			}
			else
			{
				return View(company);
			}
		}


		[HttpGet]
		public IActionResult GetAll()
		{
			List<Company> CompanyList = _unitOfWork.Company.GetAll().ToList();
			return Json(new { data = CompanyList });
		}

		public IActionResult Delete(int id)
		{
			Company? company = _unitOfWork.Company.Get(c => c.CompanyId == id);
			return View(company);
			// not required to pass an object
		}
		[HttpPost, ActionName("Delete")]
		public IActionResult DeletePost(int id)
		{
			Company? company = _unitOfWork.Company.Get(c => c.CompanyId == id);
			// _db.Categories.Remove(company);
			_unitOfWork.Company.Remove(company);

			_unitOfWork.Save();
			TempData["success"] = "Stock Order Deleted Successfully";
			return RedirectToAction("Index");
			// return View(company);
			// not required to pass an object
		}
	}
}