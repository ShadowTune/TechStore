﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechStore.Models
{
	public class OrderHeader
	{   
		public int OrderHeaderId { get; set; }
		public string ApplicationUserId { get; set; }
		[ForeignKey("ApplicationUserId")]
		[ValidateNever]
		public ApplicationUser ApplicationUser { get; set; }


		public DateTime OrderDate { get; set; }
		public DateTime BoardDate { get; set; }	
		public double OrderCost { get; set; }

		public string? OrderStatus { get; set; }


		public string? PaymentStatus { get; set; }
		public string? TrackNumber { get; set; }
		public string? Courrier {  get; set; }

		public DateTime PaymentDate { get; set; }
		public DateOnly PaymentDueDate { get; set; }

		public string? SessionId { get; set; }
		public string? PaymentIntentId { get; set; }

		[Required]
		public string? CustomerPhone { get; set; }

		[Required]
		public string? CustomerName { get; set; }

		[Required]
		public string? City { get; set; }

		[Required]
		public string? State { get; set; }

	}
}
