using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechStore.Models
{
	public class Category
	{
		[Key]
		public int Id { get; set; }
		[Required]
		[DisplayName("Brand Name")]
		public string Name { get; set; }
		[DisplayName("Series Name")]
		public string Series { get; set; }
		[DisplayName("Stock Orders")]
		[Range(1, 100, ErrorMessage = "Order must be in between 1-100")]
		public int DisplayOrder { get; set; }
	}
}
