using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechStore.Models
{
	public class OrderDetail
	{
		[Key]
		public int OrderId { get; set; }
		[Required]
		public int OrderHeaderId { get; set; }
		[ForeignKey("OrderHeaderId")]
		public OrderHeader OrderHeader { get; set; }

		[Required]
		public string ProductId { get; set; }
		[ForeignKey("ProductId")]
		public Product Product { get; set; }
		public int OrderCount { get; set; }
		public double OrderPrice { get; set; }

	}
}
