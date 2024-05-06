using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.DataTransferObject_DTO.BasketDTO
{
	public class BasketItemDto
	{
		[Required]
		public int Id { get; set; }
		[Required]

		public string ProductName { get; set; }
		[Required]

		public string Description { get; set; }
		[Required]
		public decimal Price { get; set; }
		[Required]
		[Range(1,100)]
		public int Quantity { get; set; }
		[Required]

		public string PictureUlr { get; set; }
		[Required]

		public string Typename { get; set; }
		[Required]
		
		public string BrandName { get; set; }
	}
}
