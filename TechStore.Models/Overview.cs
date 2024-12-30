using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechStore.Models
{
	public class Overview
	{
		[Key]
		public int OverviewId { get; set; }
		[Required]
		[ValidateNever]
		public string? Description { get; set; }
		[ValidateNever]
		public string? ImageLink { get; set; }
	}
}
