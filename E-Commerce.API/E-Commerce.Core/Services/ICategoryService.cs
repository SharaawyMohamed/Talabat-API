using E_Commerce.Core.DataTransferObject_DTO.ProductDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Services
{
	public interface ICategoryService
	{
		Task<IReadOnlyList<BrandTypeDTO>> GetAllCategoriesAsync();
	}
}
