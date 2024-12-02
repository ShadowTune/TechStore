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
		public IActionResult Add()
		{
			return View();
			// not required to pass an object
		}
		[HttpPost]
		public IActionResult Add(Category category)
		{
			_db.Categories.Add(category);
			_db.SaveChanges();
			return RedirectToAction("Index");
			// not required to pass an object
		}
	}
}
