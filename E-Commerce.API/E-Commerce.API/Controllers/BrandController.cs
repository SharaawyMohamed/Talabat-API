using AutoMapper;
using E_Commerce.API.Errors;
using E_Commerce.Core.DataTransferObject_DTO.ProductDTO;
using E_Commerce.Core.Models.Product;
using E_Commerce.Core.Services;
using E_Commerce.Services;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers
{
	public class BrandController : APIBaseController
	{
		private readonly BrandService _brandService;
		public BrandController(BrandService brandService)
		{
			_brandService = brandService;
		}
		[HttpGet("GetAllBrands")]
		public async Task<ActionResult<IReadOnlyList<BrandTypeDTO>>> GetAllBrands()
		{
			var response = await _brandService.GetAllBrandsOrCategoriesAsync()!;
			if (response == null)
			{
				return NotFound(new ApiResponce(StatusCodes.Status404NotFound, "There is not found brand yet!"));
			}
			return Ok(response);
		}

		[HttpGet("GetBrandById")]
		public async Task<ActionResult<BrandTypeDTO>> GetBrandById(int Id)
		{
			var brand = await _brandService.GetBrandOrCategoryByIdAsync(Id)!;
			if (brand == null)
			{
				return NotFound(new ApiResponce(StatusCodes.Status404NotFound, $"Invalid brand id {Id}"));
			}
			return Ok(brand);
		}

		[HttpPut("UpdateBrand")]
		public async Task<ActionResult> UpdateBrand(BrandTypeDTO brand)
		{
			var brandState = await _brandService.UpdateBrandOrCategory(brand);

			return brandState ? Ok(new ApiResponce(StatusCodes.Status200OK, "Brand updated successfully.")) :
				BadRequest(new ApiResponce(StatusCodes.Status400BadRequest, "Bad request."));
		}

		[HttpDelete("DeleteBrand")]
		public async Task<ActionResult> DeleteBrand(int Id)
		{
			var brandState = await _brandService.DeleteBrandOrCategory(new BrandTypeDTO { Id = Id })!;
			return brandState ? Ok(new ApiResponce(StatusCodes.Status200OK, "Brand deleted successfully."))
				: BadRequest(new ApiResponce(StatusCodes.Status400BadRequest, "Bad request"));
		}

		[HttpPost("AddBrand")]
		public async Task<ActionResult> AddBrand(string brandName)
		{
			var brandState = await _brandService.AddBrandOrCategory(new BrandTypeDTO { Name = brandName });
			return brandState ? Ok(new ApiResponce(StatusCodes.Status200OK, "Brand created successfully.")) : BadRequest(new ApiResponce(StatusCodes.Status400BadRequest, "Bad request!"));
		}
	}
}
