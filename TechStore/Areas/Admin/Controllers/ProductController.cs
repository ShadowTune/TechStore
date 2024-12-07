using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.ProjectModel;
using System.Collections.Generic;
using TechStore.DataAccess.Data;
using TechStore.DataAccess.Repository;
using TechStore.DataAccess.Repository.IRepository;
using TechStore.Models;
using TechStore.Models.ViewModels;

namespace TechStore.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class ProductController : Controller
	{
		private readonly IUnitOfWork _unitofwork;
		private readonly IWebHostEnvironment _webHostEnvironment;
		public ProductController(IUnitOfWork unitofwork, IWebHostEnvironment webHostEnvironment)
		{
			_unitofwork = unitofwork;
			_webHostEnvironment = webHostEnvironment;
		}
		public IActionResult Index()
		{
			List<Product> objProductList = _unitofwork.Product.GetAll(includeProperties: "Category").ToList();
			return View(objProductList);
		}
		/*public IActionResult Add(string? selectedBrand = null)
		{
			// Fetch all distinct brands from the database
			var brands = _unitofwork.Category.GetAll()
				.Select(c => c.Name)
				.Distinct()
				.ToList();

			// Initialize an empty list for series
			List<SelectListItem> seriesList = new List<SelectListItem>();

			// If a brand is selected, fetch its corresponding series
			if (!string.IsNullOrEmpty(selectedBrand))
			{
				seriesList = _unitofwork.Product.GetAll()
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
			ProductVM productVM = new()
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

				// Initialize an empty product object
				Product = new Product
				{
					Brand = selectedBrand // Retain the selected brand in the model
				}
			};

			return View(productVM);
		}
		[HttpPost]
		public IActionResult Add(ProductVM productVM)
		{
			if (ModelState.IsValid)
			{
				_unitofwork.Product.Add(productVM.Product);
				_unitofwork.Save();
				TempData["success"] = "Stock Order Added Successfully";
				return RedirectToAction("Index");
			} else
			{
				productVM.CategoryList = _unitofwork.Category.GetAll().Select(u => new SelectListItem
				{
					Text = u.Name,
					Value = u.Id.ToString()
				});
				return View(productVM);
			}
			// not required to pass an object
		}

		public IActionResult Edit(string id)
		{
			ProductVM productVM = new()
			{
				CategoryList = _unitofwork.Category.GetAll().Select(u => new SelectListItem
				{
					Text = u.Name,
					Value = u.Id.ToString()
				}),
				Product = new Product()
			};
			productVM.Product = _unitofwork.Product.Get(u => u.ProductId == id);
			return View(productVM);
			// not required to pass an object
		}
		[HttpPost]
		public IActionResult Edit(ProductVM productVM, IFormFile? file)
		{
			if (ModelState.IsValid)
			{
				_unitofwork.Product.Update(productVM.Product);
				_unitofwork.Save();
				TempData["success"] = "Stock Order Updated Successfully";
				return RedirectToAction("Index");
			} 
			return View(productVM);
			// not required to pass an object
		}
		*/

		public IActionResult UpdateAdd(string? id)
		{
			ProductVM productVM = new()
			{
				CategoryList = _unitofwork.Category.GetAll().Distinct().Select(u => new SelectListItem
				{
					Text = u.Name,
					Value = u.Id.ToString()
				}),
				Product = new Product()
			};

			if (id == null)
			{
				return View(productVM);
			}
			else
			{
				productVM.Product = _unitofwork.Product.Get(u => u.ProductId == id);
				return View(productVM);
			}
			// not required to pass an object
		}
		[HttpPost]
		public IActionResult UpdateAdd(ProductVM productVM, IFormFile? file)
		{
			if (ModelState.IsValid)
			{
				string wwwRootPath = _webHostEnvironment.WebRootPath;

				// Handle file upload
				if (file != null)
				{
					string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
					string productPath = Path.Combine(wwwRootPath, @"images\product");

					if (!string.IsNullOrEmpty(productVM.Product.ImageLink))
					{
						var oldImagePath = Path.Combine(wwwRootPath, productVM.Product.ImageLink.TrimStart('\\'));
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
					productVM.Product.ImageLink = @"\images\product\" + fileName;
				}
				// Add or update the product
				if (string.IsNullOrEmpty(productVM.Product.ProductId))
				{
					// New product
					productVM.Product.ProductId = Guid.NewGuid().ToString();
					_unitofwork.Product.Add(productVM.Product);
				}
				else
				{
					// Existing product: Update it
					_unitofwork.Product.Update(productVM.Product);
				}

				// Save changes to the database
				_unitofwork.Save();
				TempData["success"] = "Product saved successfully.";
				return RedirectToAction("Index");
			}
			else
			{
				// Repopulate CategoryList in case of validation failure
				productVM.CategoryList = _unitofwork.Category.GetAll().Select(u => new SelectListItem
				{
					Text = u.Name,
					Value = u.Id.ToString()
				});

				return View(productVM);
			}
		}



		public IActionResult Delete(string? id)
		{
			Product? product = _unitofwork.Product.Get(c => c.ProductId == id);
			return View(product);
			// not required to pass an object
		}
		[HttpPost, ActionName("Delete")]
		public IActionResult DeletePost(string? id)
		{
			Product? product = _unitofwork.Product.Get(c => c.ProductId == id);
			// _db.Categories.Remove(product);
			_unitofwork.Product.Remove(product);

			_unitofwork.Save();
			TempData["success"] = "Stock Order Deleted Successfully";
			return RedirectToAction("Index");
			// return View(product);
			// not required to pass an object
		}

		[HttpGet]
		public IActionResult GetAll()
		{
			List<Product> productList = _unitofwork.Product.GetAll(includeProperties: "Category").ToList();
			return Json(new {data = productList});
		}

	}
}