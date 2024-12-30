using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting;
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
	public class ProductController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IWebHostEnvironment _webHostEnvironment;
		public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
		{
			_unitOfWork = unitOfWork;
			_webHostEnvironment = webHostEnvironment;
		}
		public IActionResult Index()
		{
			List<Product> objProductList = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();
			return View(objProductList);
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
				seriesList = _unitOfWork.Product.GetAll()
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
				_unitOfWork.Product.Add(productVM.Product);
				_unitOfWork.Save();
				TempData["success"] = "Stock Order Added Successfully";
				return RedirectToAction("Index");
			} else
			{
				productVM.CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
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
				CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
				{
					Text = u.Name,
					Value = u.Id.ToString()
				}),
				Product = new Product()
			};
			productVM.Product = _unitOfWork.Product.Get(u => u.ProductId == id);
			return View(productVM);
			// not required to pass an object
		}
		[HttpPost]
		public IActionResult Edit(ProductVM productVM, IFormFile? file)
		{
			if (ModelState.IsValid)
			{
				_unitOfWork.Product.Update(productVM.Product);
				_unitOfWork.Save();
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
				CategoryList = _unitOfWork.Category.GetAll()
				.GroupBy(c => c.Name) // Group by the brand name to ensure uniqueness.
				.Select(g => g.First()) // Select the first entry in each group (to get the unique brand with its Id).
				.Select(c => new SelectListItem
				{
					Text = c.Name,               // Display brand name (e.g., "ASUS").
					Value = c.Id.ToString()      // Use the numeric Id as the value.
				})
				.ToList(), // Ensure the list is materialized before passing it to the ViewModel.
				Product = new Product()
			};


			if (id == null)
			{
				return View(productVM);
			}
			else
			{
				productVM.Product = _unitOfWork.Product.Get(u => u.ProductId == id);
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
					_unitOfWork.Product.Add(productVM.Product);
				}
				else
				{
					// Existing product: Update it
					_unitOfWork.Product.Update(productVM.Product);
				}

				// Save changes to the database
				_unitOfWork.Save();
				TempData["success"] = "Product saved successfully.";
				return RedirectToAction("Index");
			}
			else
			{
				// Repopulate CategoryList in case of validation failure
				productVM.CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
				{
					Text = u.Name,
					Value = u.Id.ToString()
				});

				return View(productVM);
			}
		}


		[HttpGet]
		public IActionResult GetAll()
		{
			List<Product> objProductList = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();
			return Json(new { data = objProductList });
		}
		/*public IActionResult Delete(string? id)
		{
			Product? product = _unitOfWork.Product.Get(c => c.ProductId == id);
			return View(product);
			// not required to pass an object
		}
		[HttpPost, ActionName("Delete")]
		public IActionResult DeletePost(string? id)
		{
			Product? product = _unitOfWork.Product.Get(c => c.ProductId == id);
			// _db.Categories.Remove(product);
			_unitOfWork.Product.Remove(product);

			_unitOfWork.Save();
			TempData["success"] = "Stock Order Deleted Successfully";
			return RedirectToAction("Index");
			// return View(product);
			// not required to pass an object
		}*/
		[HttpDelete]
		public IActionResult Delete(string? id)
		{
			var product = _unitOfWork.Product.Get(u => u.ProductId == id);
			if (product == null)
			{
				return Json(new { success = false, message = "Product Not Found" });
			}

			var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, product.ImageLink.TrimStart('\\'));
			if (System.IO.File.Exists(oldImagePath))
			{
				System.IO.File.Delete(oldImagePath);
			}

			_unitOfWork.Product.Remove(product);
			_unitOfWork.Save();
			TempData["success"] = "Product Deleted";
			return RedirectToAction("Index");
		}
	}
}