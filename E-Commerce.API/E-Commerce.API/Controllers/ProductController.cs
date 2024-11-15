using AutoMapper;
using E_Commerce.API.DataTransferObject_DTO;
using E_Commerce.API.DataTransferObject_DTO.ProductDTO;
using E_Commerce.API.Errors;
using E_Commerce.API.Helper;
using E_Commerce.Core.Models.Product;
using E_Commerce.Core.Repository;
using E_Commerce.Core.Services;
using E_Commerce.Core.Specification;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers
{
	public class ProductController : APIBaseController
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		public ProductController(IUnitOfWork unitOfWork, IMapper mapper)
		{

			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}
		[HttpGet("Products")]
		[Cash(50)]
		[ProducesResponseType(typeof(ProductToReturnDTO), statusCode: 200)]
		[ProducesResponseType(typeof(ApiResponce), statusCode: 400)]
		public async Task<ActionResult<IReadOnlyList<ProductToReturnDTO>>> GetAllProducts([FromQuery] ProductSpecParameter parameters)
		{

			var Spec = new ProductWithBrandTypeSpec(parameters);
			var Products = await _unitOfWork.Repository<Product, int>().GetAllWithSpecAsync(Spec);
			if (Products is null) return NotFound(new ApiResponce(404));
			var MapedProducts = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDTO>>(Products);

			var CountSpec = new ProductWithFiltiratonForCountAsync(parameters);
			var ReturnObject = new Pagination<ProductToReturnDTO>()
			{
				PageIndex = parameters.PageIndex,
				PageSize = parameters.PageSize,
				Count = await _unitOfWork.Repository<Product, int>().CountNumperWithSpec(CountSpec),
				Data = MapedProducts,
			};
			return Ok(ReturnObject);

		}

		[HttpGet("{Id}")]
		[ProducesResponseType(typeof(ProductToReturnDTO), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiResponce), StatusCodes.Status404NotFound)]
		public async Task<ActionResult<ProductToReturnDTO>> GetProductById(int Id)
		{
			var Spec = new ProductWithBrandTypeSpec(Id);
			var Product = await _unitOfWork.Repository<Product, int>().GetByIdWithSpecAsync(Spec);
			if (Product is null) return NotFound(new ApiResponce(404));
			var MapedProduct = _mapper.Map<Product, ProductToReturnDTO>(Product);
			return Ok(MapedProduct);
		}

		[HttpGet("Categories")]
		public async Task<ActionResult<IReadOnlyList<ProductType>>> GetAllCategories()
			=> Ok(await _unitOfWork.Repository<ProductType, int>().GetAllAsync());

		[HttpGet("Brands")]
		public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetAllBrands()
			=> Ok(await _unitOfWork.Repository<ProductBrand, int>().GetAllAsync());

	}
}
