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
	public class OrderDetail
	{
		public int Id { get; set; }
		[Required]
		public int OrderHeaderId { get; set; }
		[ForeignKey("OrderHeaderId")]
		[ValidateNever]
		public OrderHeader OrderHeader { get; set; }

		[Required]
		public string ProductId { get; set; }
		[ForeignKey("ProductId")]
		[ValidateNever]
		public Product Product { get; set; }
		public int OrderCount { get; set; }
		public double OrderPrice { get; set; }

	}
}
