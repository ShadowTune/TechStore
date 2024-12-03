using Microsoft.AspNetCore.Mvc;
using TechStore.DataAccess.Data;
using TechStore.DataAccess.Repository.IRepository;
using TechStore.Models;

namespace TechStore.Controllers
{
	public class CategoryController : Controller
	{
		private readonly ICategoryRepository _categoryRepo;
		public CategoryController(ICategoryRepository db)
		{
			_categoryRepo = db;
		}
		public IActionResult Index()
		{
			List<Category> objCategoryList = _categoryRepo.GetAll().ToList();
			return View(objCategoryList);
		}
		public IActionResult Add(int? id)
		{
			// Category category = _db.Categories.FirstOrDefault(c => c.Id == id);
			return View();
			// not required to pass an object
		}
		[HttpPost]
		public IActionResult Add(Category category)
		{
			if (category.Name == category.DisplayOrder.ToString())
			{
				ModelState.AddModelError("Name", "Name and Orders must be different");
			}
			if (ModelState.IsValid)
			{
				_categoryRepo.Add(category);
				_categoryRepo.Save();
				TempData["success"] = "Stock Order Added Successfully";
				return RedirectToAction("Index");
			}
			return View(category);
			// not required to pass an object
		}

		public IActionResult Edit(int? id)
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}
			Category category = _categoryRepo.Get(c => c.Id == id);
			return View(category);
			// not required to pass an object
		}
		[HttpPost]
		public IActionResult Edit(Category category)
		{
			if (category.Name == category.DisplayOrder.ToString())
			{
				ModelState.AddModelError("Name", "Name and Orders must be different");
			}
			if (category.Name.ToLower() == "test")
			{
				ModelState.AddModelError("Name", "Name is invalid");
			}
			if (ModelState.IsValid)
			{
				_categoryRepo.Update(category);
				_categoryRepo.Save();
				TempData["success"] = "Stock Order Updated Successfully";
				return RedirectToAction("Index");
			}
			return View(category);
			// not required to pass an object
		}

		public IActionResult Delete(int? id)
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}
			Category category = _categoryRepo.Get(c => c.Id == id);
			return View(category);
			// not required to pass an object
		}
		[HttpPost, ActionName("Delete")]
		public IActionResult DeletePost(int? id)
		{
			Category category = _categoryRepo.Get(c => c.Id == id);
			if (category == null)
			{
				return NotFound();
			}
			// _db.Categories.Remove(category);
			if (ModelState.IsValid)
			{
				_categoryRepo.Remove(category);
				_categoryRepo.Save();
				TempData["success"] = "Stock Order Deleted Successfully";
			}
			return RedirectToAction("Index");
			// return View(category);
			// not required to pass an object
		}
	}
}
