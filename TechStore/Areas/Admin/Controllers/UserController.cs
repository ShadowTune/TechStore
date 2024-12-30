using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
	public class UserController : Controller
	{
		private readonly ApplicationDbContext _db;
		// private readonly IWebHostEnvironment _webHostEnvironment;
		private readonly UserManager<IdentityUser> _userManager;
		public UserController(ApplicationDbContext db, UserManager<IdentityUser> userManager)
		{
			_db = db;
			_userManager = userManager;
			// _unitOfWork = unitofwork;
			// _webHostEnvironment = webHostEnvironment;
		}
		public IActionResult Index()
		{
			return View();
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

		/*public IActionResult UpdateAdd(int id)
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
		}*/

		public IActionResult RoleManagement(string userId)
		{
			string RoleId = _db.UserRoles.FirstOrDefault(u => u.UserId == userId).RoleId;
			RoleManagementVM RoleVM = new RoleManagementVM()
			{
				User = _db.ApplicationUsers.Include(u => u.Company).FirstOrDefault(u => u.Id == userId),
				RoleList = _db.Roles.Select(b => new SelectListItem
				{
					Text = b.Name,
					Value = b.Name
				}),
				CompanyList = _db.Companies.Select(b => new SelectListItem
				{
					Text = b.Name,
					Value = b.CompanyId.ToString()
				}),
			};
			RoleVM.User.Role = _db.Roles.FirstOrDefault(u => u.Id == RoleId).Name;
			return View(RoleVM);
		}

		[HttpPost]
		public IActionResult RoleManagement(RoleManagementVM roleManagementVM)
		{
			string RoleId = _db.UserRoles.FirstOrDefault(u => u.UserId == roleManagementVM.User.Id).RoleId;
			string oldRole = _db.Roles.FirstOrDefault(u => u.Id == RoleId).Name;
			if (!(roleManagementVM.User.Role == oldRole))
			{
				ApplicationUser user = _db.ApplicationUsers.FirstOrDefault(u => u.Id == roleManagementVM.User.Id);
				if (roleManagementVM.User.Role == SD.Role_company)
				{
					user.CompanyId = roleManagementVM.User.CompanyId;
				}
				if (oldRole == SD.Role_company)
				{
					user.CompanyId = null;
				}
				_db.SaveChanges();
				_userManager.RemoveFromRoleAsync(user, oldRole).GetAwaiter().GetResult();
				_userManager.AddToRoleAsync(user, roleManagementVM.User.Role).GetAwaiter().GetResult();
			}
			return RedirectToAction("Index");
		}

		// api calls
		[HttpGet]
		public IActionResult GetAll()
		{
			List<ApplicationUser> UserList = _db.ApplicationUsers.Include(u => u.Company).ToList();

			var userRoles = _db.UserRoles.ToList();
			var roles = _db.Roles.ToList();

			foreach (var user in UserList)
			{
				var roleId = userRoles.FirstOrDefault(u => u.UserId == user.Id).RoleId;
				user.Role = roles.FirstOrDefault(u => u.Id == roleId).Name;
				if (user.Company == null)
				{
					user.Company = new() { Name = "" };
				}
			}

			return Json(new { data = UserList });
		}

		/*public IActionResult Delete(int id)
		{
			Company? company = _db.Company.Get(c => c.CompanyId == id);
			return View(company);
			// not required to pass an object
		}*/

		[HttpPost]
		public IActionResult LockUnlock([FromBody]string id)
		{
			var userFromDb = _db.ApplicationUsers.FirstOrDefault(u => u.Id == id);
			if (userFromDb == null)
			{
				return Json(new {success = false, message = "Error while locking / unlocking"});
			}
			if (userFromDb.LockoutEnd != null && userFromDb.LockoutEnd > DateTime.Now)
			{
				userFromDb.LockoutEnd = DateTime.Now;
			} else
			{
				userFromDb.LockoutEnd = DateTime.Now.AddYears(1000);
			}
			_db.SaveChanges();
			return Json(new { success = true, message = "Operation Successful" });
		}
	}
}