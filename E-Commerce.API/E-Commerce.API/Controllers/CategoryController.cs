using E_Commerce.API.Errors;
using E_Commerce.Core.DataTransferObject_DTO.ProductDTO;
using E_Commerce.Core.Services;
using E_Commerce.Services;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers
{
	public class CategoryController : APIBaseController
	{
		private readonly CategoryService _categoryService;

		public CategoryController(CategoryService categoryService)
		{
			_categoryService = categoryService;
		}

		[HttpGet("GetAllCategories")]
		public async Task<ActionResult<IReadOnlyList<BrandTypeDTO>>> GetAllCategories()
		{
			var response = await _categoryService.GetAllBrandsOrCategoriesAsync()!;
			if (response == null) return NotFound(new ApiResponce(200, "There is not categories found!"));

			return Ok(response);
		}

		[HttpDelete("DeleteCategory/{Id}")]
		public async Task<ActionResult<ApiResponce>> DeleteCategory(int Id)
		{
			var response = await _categoryService.DeleteBrandOrCategory(Id);
			if (!response) return new ApiResponce(StatusCodes.Status400BadRequest, "Bad request.");

			return Ok(new ApiResponce(200, "Category has been deleted successfully."));
		}

		[HttpGet("GetCategoryById/{Id}")]
		public async Task<ActionResult<BrandTypeDTO>> GetCategoryById(int Id)
		{
			var category = await _categoryService.GetBrandOrCategoryByIdAsync(Id)!;
			if (category == null)
			{
				return NotFound(new ApiResponce(StatusCodes.Status404NotFound, $"Invalid Id {Id}"));
			}
			return Ok(category);
		}

		[HttpPut("UpdateCategory")]
		public async Task<ActionResult<BrandTypeDTO>> UpdateCategory([FromRoute] int Id, [FromForm] CreateBrandTypeDto category)
		{
			var updateState = await _categoryService.UpdateBrandOrCategory(Id, category);
			return !updateState ? NotFound(new ApiResponce(StatusCodes.Status404NotFound, $"Invalid category id {Id}"))
				: Ok(new ApiResponce(StatusCodes.Status200OK, "Category updated successfully."));
		}

		[HttpPost("AddCategory")]
		public async Task<ActionResult> CreateCategory(CreateBrandTypeDto category)
		{
			var categoryState = await _categoryService.AddBrandOrCategory(category);
			return categoryState ? Ok(new ApiResponce(StatusCodes.Status200OK, "Category created successfully."))
				: BadRequest(new ApiResponce(StatusCodes.Status400BadRequest, "Bad request"));
		}

	}
}
