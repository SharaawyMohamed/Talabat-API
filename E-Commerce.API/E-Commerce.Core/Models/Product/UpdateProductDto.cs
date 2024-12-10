using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Models.Product
{
	public class UpdateProductDto
	{
		public int Id { get; set; }
		public string? Name { get; set; }
		public string? Description { get; set; }
		[Column(TypeName = "decimal(8,4)")]
		public decimal? Price { get; set; }
		public IFormFile? Image { get; set; }
		public int? quantity { get; set; } = 1;
	}
}
