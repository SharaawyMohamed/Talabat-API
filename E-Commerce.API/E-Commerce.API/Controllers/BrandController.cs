using AutoMapper;
using E_Commerce.API.Errors;
using E_Commerce.Core.Models.Product;
using E_Commerce.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers
{
	public class BrandController:APIBaseController
	{
		private readonly IBrandService _brandService;
		public BrandController(IBrandService brandService)
		{
			_brandService = brandService;
		}
		[HttpGet("Brands")]
		public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetAllBrands()
		{
			var response = await _brandService.GetAllBrandsAsync();
			if (!response.Any()) return Ok(new ApiResponce(200,"There not brands found!"));

			return Ok(response);
		}
	}
}
