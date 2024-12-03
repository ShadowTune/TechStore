using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TechStore.Models
{
	public class Category
	{
		public int Id { get; set; }
		[Required]
		[MaxLength(30)]
		[DisplayName("Brand Name")]
		public string Name { get; set; }
		[DisplayName("Stock Orders")]
		[Range(1, 100, ErrorMessage = "Order must be in between 1-100")]
		public int DisplayOrder { get; set; }
	}
}
