﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechStore.Models.ViewModels
{
	public class CartVM
	{
		public IEnumerable<Cart> CartList { get; set; }
		public OrderHeader OrderHeader { get; set; }
		// public double OrderTotal { get; set; }
	}
}
