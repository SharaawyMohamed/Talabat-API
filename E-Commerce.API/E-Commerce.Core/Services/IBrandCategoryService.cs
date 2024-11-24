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
		Task<bool> UpdateBrandOrCategory(BrandTypeDTO entity);
		Task<bool> DeleteBrandOrCategory(BrandTypeDTO entity);
		Task<bool> AddBrandOrCategory(BrandTypeDTO entity);

	}
}
