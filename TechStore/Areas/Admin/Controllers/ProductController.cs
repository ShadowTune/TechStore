using Microsoft.AspNetCore.Mvc;
using TechStore.DataAccess.Data;
using TechStore.DataAccess.Repository.IRepository;
using TechStore.Models;

namespace TechStore.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class ProductController : Controller
	{
		private readonly IUnitOfWork _unitofwork;
		public ProductController(IUnitOfWork unitofwork)
		{
			_unitofwork = unitofwork;
		}
		public IActionResult Index()
		{
			List<Product> objProductList = _unitofwork.Product.GetAll().ToList();
			return View(objProductList);
		}
		public IActionResult Add(string? id)
		{
			// Product product = _db.Categories.FirstOrDefault(c => c.Id == id);
			return View();
			// not required to pass an object
		}
		[HttpPost]
		public IActionResult Add(Product product)
		{
			if (ModelState.IsValid)
			{
				_unitofwork.Product.Add(product);
				_unitofwork.Save();
				TempData["success"] = "Stock Order Added Successfully";
				return RedirectToAction("Index");
			}
			return View();
			// not required to pass an object
		}

		public IActionResult Edit(string? id)
		{
			if (id == null)
			{
				return NotFound();
			}
			Product? product = _unitofwork.Product.Get(c => c.ProductId == id);
			if (product == null)
			{
				return NotFound();
			}
			return View(product);
			// not required to pass an object
		}
		[HttpPost]
		public IActionResult Edit(Product product)
		{
			if (ModelState.IsValid)
			{
				_unitofwork.Product.Update(product);
				_unitofwork.Save();
				TempData["success"] = "Stock Order Updated Successfully";
				return RedirectToAction("Index");
			}
			return View();
			// not required to pass an object
		}

		public IActionResult Delete(string? id)
		{
			if (id == null)
			{
				return NotFound();
			}
			Product? product = _unitofwork.Product.Get(c => c.ProductId == id);
			if (product == null) 
			{ 
				return NotFound(); 
			}
			return View(product);
			// not required to pass an object
		}
		[HttpPost, ActionName("Delete")]
		public IActionResult DeletePost(string? id)
		{
			Product? product = _unitofwork.Product.Get(c => c.ProductId == id);
			if (product == null)
			{
				return NotFound();
			}
			// _db.Categories.Remove(product);
			_unitofwork.Product.Remove(product);
			_unitofwork.Save();
			TempData["success"] = "Stock Order Deleted Successfully";
			return RedirectToAction("Index");
			// return View(product);
			// not required to pass an object
		}
	}
}
