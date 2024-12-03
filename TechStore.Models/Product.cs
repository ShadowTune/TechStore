using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechStore.Models
{
	public class Product
	{
		[Key]
		public string ProductId { get; set; }
		[Required]
		[MaxLength(30)]
		[DisplayName("Brand Name")]
		public string Brand { get; set; }
		[DisplayName("Series")]
		[Required]
		public string Series { get; set; }
		[Required]
		[DisplayName("Model")]
		public string Model { get; set; }
		[DisplayName("Regular Price")]
		[Required]
		[Range(499.99, 8999.99)]
		public double RegularPrice { get; set; }
		[DisplayName("Dicounted Price")]
		[Required]
		[Range(499.99, 8999.99)]
		public double DiscountPrice { get; set; }
		[DisplayName("Stock Orders")]
		[Range(1, 100, ErrorMessage = "Order must be in between 1-100")]
		[Required]
		public int DisplayOrder { get; set; }
	}
}
