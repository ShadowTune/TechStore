using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechStore.Models
{
	public class Policy
	{
		[Key]
		public string? PolicyTerms{ get; set; }
	}
}
