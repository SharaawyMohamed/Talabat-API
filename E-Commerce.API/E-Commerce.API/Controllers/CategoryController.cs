using E_Commerce.API.Errors;
using E_Commerce.Core.DataTransferObject_DTO.ProductDTO;
using E_Commerce.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers
{
	public class CategoryController:APIBaseController
	{
		private readonly ICategoryService _categoryService;

		public CategoryController(ICategoryService categoryService)
		{
			_categoryService = categoryService;
		}

		[HttpGet("Categories")]
		public async Task<ActionResult<IReadOnlyList<BrandTypeDTO>>> GetAllCategories()
		{
			var response = await _categoryService.GetAllCategoriesAsync();
			if (response == null) return Ok(new ApiResponce(200,"There is not categories found!"));

			return Ok(response);
		}
	}
}
