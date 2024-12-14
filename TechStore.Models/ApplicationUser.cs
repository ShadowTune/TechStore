using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechStore.Models
{
	public class ApplicationUser : IdentityUser
	{
		[Required]
		public string? Name { get; set; }

		[StringLength(100, ErrorMessage = "City name cannot exceed 100 characters.")]
		[Display(Name = "City")]
		public string? City { get; set; }

		[StringLength(100, ErrorMessage = "State name cannot exceed 100 characters.")]
		[Display(Name = "State")]
		public string? State { get; set; }

		public int? CompanyId { get; set; }
		[ForeignKey("CompanyId")]
		[ValidateNever]
		public Company Company { get; set; }
	}
}
