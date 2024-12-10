using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.DataTransferObject_DTO.ProductDTO
{
	public class CreateBrandTypeDto
	{
		public string Name { get; set; }
		public IFormFile image { get; set; }
	}
}
