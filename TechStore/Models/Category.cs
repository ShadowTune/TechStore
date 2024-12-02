using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TechStore.Models
{
	public class Category
	{
		public int Id { get; set; }
		[Required]
		[DisplayName("Brand Name")]
		public string Name { get; set; }
		[DisplayName("Stock Orders")]
		public int DisplayOrder { get; set; }
	}
}
