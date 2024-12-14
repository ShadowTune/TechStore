using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechStore.Models
{
	public class Company
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
		public int CompanyId { get ; set; }
		[Required]
		[ValidateNever]
		public string Name { get; set; }
		[ValidateNever]
		public string? Branch { get; set; }
		[ValidateNever]
		public string? State { get; set; }
		[ValidateNever]
		public string? Support { get; set; }
	}
}
