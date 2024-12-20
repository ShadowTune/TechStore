using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechStore.Models
{
	public class Term
	{
		[Key]
		public int TermId { get; set; }
		[Required]
		[ValidateNever]
		public string? Description { get; set; }
	}
}
