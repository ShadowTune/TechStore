using Microsoft.AspNetCore.Mvc;
using TechStore.Data;
using TechStore.Models;

namespace TechStore.Controllers
{
	public class CategoryController : Controller
	{
		private readonly ApplicationDbContext _db;
		public CategoryController(ApplicationDbContext db)
		{
			_db = db;
		}
		public IActionResult Index()
		{
			List<Category> objCategoryList = _db.Categories.ToList();
			return View(objCategoryList);
		}
		public IActionResult Add(int? id)
		{
			Category category = _db.Categories.FirstOrDefault(c => c.Id == id);
			return View(category);
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
				_db.Categories.Add(category);
				_db.SaveChanges();
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
			Category category = _db.Categories.FirstOrDefault(c => c.Id == id);
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
				_db.Categories.Update(category);
				_db.SaveChanges();
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
			Category category = _db.Categories.Find(id);
			return View(category);
			// not required to pass an object
		}
		[HttpPost, ActionName("Delete")]
		public IActionResult DeletePost(int? id)
		{
			Category category = _db.Categories.Find(id);
			if (category == null)
			{
				return NotFound();
			}
			// _db.Categories.Remove(category);
			if (ModelState.IsValid)
			{
				_db.Categories.Remove(category);
				_db.SaveChanges();
				TempData["success"] = "Stock Order Deleted Successfully";
			}
			return RedirectToAction("Index");
			// return View(category);
			// not required to pass an object
		}
	}
}
