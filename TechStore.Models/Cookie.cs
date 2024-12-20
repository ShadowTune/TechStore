using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechStore.Models
{
	public class Cookie
	{
		[Key]
		public int CookieId { get; set; }
		[Required]
		[ValidateNever]
		public string? Description { get; set; }
	}
}
