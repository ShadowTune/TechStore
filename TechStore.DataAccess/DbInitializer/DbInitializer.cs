using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechStore.DataAccess.DbInitializer;
using TechStore.DataAccess.Data;
using TechStore.DataAccess.Repository.IRepository;
using TechStore.Models;
using TechStore.Utility;

namespace TechStore.DataAccess.DbInitializer
{
	public class DbInitializer: IDbInitializer
	{ 
		// private readonly SignInManager<IdentityUser> _signInManager;
		private readonly UserManager<IdentityUser> _userManager;
		// private readonly IUserStore<IdentityUser> _userStore;
		// private readonly IUserEmailStore<IdentityUser> _emailStore;
		// private readonly ILogger<RegisterModel> _logger;
		// private readonly IEmailSender _emailSender;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly ApplicationDbContext _db;
		//private readonly IUnitOfWork _unitOfWork;
		public DbInitializer(UserManager<IdentityUser> userManager,
			RoleManager<IdentityRole> roleManager, ApplicationDbContext db)
		{
			_roleManager = roleManager;
			_userManager = userManager;
			_db = db;
		}

		public void Initialize() 
		{
			try
			{ 
				if (_db.Database.GetPendingMigrations().Count() > 0)
				{
					_db.Database.Migrate();
				}
			}
			catch (Exception ex) {}

			if (!_roleManager.RoleExistsAsync(SD.Role_customer).GetAwaiter().GetResult())
			{
				_roleManager.CreateAsync(new IdentityRole(SD.Role_customer)).GetAwaiter().GetResult();
				_roleManager.CreateAsync(new IdentityRole(SD.Role_admin)).GetAwaiter().GetResult();
				_roleManager.CreateAsync(new IdentityRole(SD.Role_company)).GetAwaiter().GetResult();
				_roleManager.CreateAsync(new IdentityRole(SD.Role_employee)).GetAwaiter().GetResult();

				_userManager.CreateAsync(new ApplicationUser
				{
					EmailConfirmed = true,
					UserName = "admin@techstore.com",
					Email = "admin@techstore.com",
					Name = "Jubair",
					PhoneNumber = "01990277590",
					City = "Chicago",
					State = "USA"
				}, "Admin@2003180").GetAwaiter().GetResult();

				ApplicationUser user = _db.ApplicationUsers.FirstOrDefault(u => u.Email == "admin@techstore.com");
				_userManager.AddToRoleAsync(user, SD.Role_admin).GetAwaiter().GetResult();
			}
			// throw new NotImplementedException();
			return;
		}
	}
}
