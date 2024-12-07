using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace TechStore.Models
{
	public class Product
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Use Identity for numeric IDs or None for GUIDs
		public string? ProductId { get; set; }
		[Required]
		[ValidateNever]
		[DisplayName("Series")]
		public string Series { get; set; }
		[Required]
		[ValidateNever]
		[DisplayName("Model")]
		public string Model { get; set; }
		[Required]
		[DisplayName("Regular Price")]
		[Range(499.99, 8999.99)]
		public double RegularPrice { get; set; }
		[Required]
		[DisplayName("Dicounted Price")]
		[Range(499.99, 8999.99)]
		public double DiscountPrice { get; set; }
		[Required]
		[DisplayName("Stock Orders")]
		[Range(1, 100, ErrorMessage = "Order must be in between 1-100")]
		public int DisplayOrder { get; set; }
		[ValidateNever]
		public string? Specification { get; set; }
		[ValidateNever]
		public string? ImageLink { get; set; }
		[ValidateNever]
		public int BrandId { get; set; }
		[ForeignKey("BrandId")]
		[ValidateNever]
		public Category? Category { get; set; }
	}
}