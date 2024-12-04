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
		[Required]
		[DisplayName("Series")]
		public string Series { get; set; }
		[Required]
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
	}
}
