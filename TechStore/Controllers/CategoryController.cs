using Microsoft.AspNetCore.Mvc;

namespace TechStore.Controllers
{
	public class CategoryController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
