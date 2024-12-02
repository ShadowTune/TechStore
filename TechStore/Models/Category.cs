﻿using System.ComponentModel.DataAnnotations;

namespace TechStore.Models
{
	public class Category
	{
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }	
		public int DisplayOrder { get; set; }
	}
}
