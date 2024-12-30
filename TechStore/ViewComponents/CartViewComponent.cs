﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TechStore.DataAccess.Repository.IRepository;
using TechStore.Utility;

namespace TechStore.ViewComponents
{
	public class CartViewComponent : ViewComponent
	{
		private readonly IUnitOfWork _unitOfWork;
		public CartViewComponent(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
			if (claim != null)
			{
				if (HttpContext.Session.GetInt32(SD.SessionCart) != null)
				{
					return View(HttpContext.Session.GetInt32(SD.SessionCart));
				}
				else
				{
					HttpContext.Session.SetInt32(SD.SessionCart, 
						_unitOfWork.Cart.GetAll(u => u.ApplicationUserId == claim.Value).ToList().Count);

					return View(HttpContext.Session.GetInt32(SD.SessionCart));
				}
			}
			else
			{
				HttpContext.Session.Clear();
				return View(0);
			}
		}
	}
}
