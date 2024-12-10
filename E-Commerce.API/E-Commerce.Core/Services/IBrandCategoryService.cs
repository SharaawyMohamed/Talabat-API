using E_Commerce.Core.DataTransferObject_DTO.ProductDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Services
{
	public interface IBrandCategoryService
	{
		Task<IEnumerable<BrandTypeDTO>>? GetAllBrandsOrCategoriesAsync();
		Task<BrandTypeDTO>? GetBrandOrCategoryByIdAsync(int Id);
		Task<bool> UpdateBrandOrCategory(int Id,CreateBrandTypeDto entity);
		Task<bool> DeleteBrandOrCategory(int Id);
		Task<bool> AddBrandOrCategory(CreateBrandTypeDto entity);

	}
}
