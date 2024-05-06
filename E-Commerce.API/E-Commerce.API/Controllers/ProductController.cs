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
	//[Authorize]
	public class ProductController : APIBaseController
	{
		private readonly IUnitOfWork unitofwork;
		private readonly IProductServices productserice;
		private readonly IMapper mapper;
		public ProductController(IUnitOfWork _unitofwork, IProductServices _productserice, IMapper _mapper)
		{

			unitofwork = _unitofwork;
			productserice = _productserice;
			mapper = _mapper;
		}
		
		[HttpGet]
		[Cash(50)]
		[ProducesResponseType(typeof(ProductToReturnDTO), statusCode: 200)]
		[ProducesResponseType(typeof(ApiResponce), statusCode: 400)]
		// we use from query because front end cant count the boject in request body
		public async Task<ActionResult<IReadOnlyList<ProductToReturnDTO>>> GetAllProducts([FromQuery] ProductSpecParameter parameters)
		{
			var Spec = new ProductWithBrandTypeSpec(parameters);
			var Products = await unitofwork.Repository<Product, int>().GetAllWithSpecAsync(Spec);
			if (Products is null) return NotFound(new ApiResponce(404));
			var MapedProducts = mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDTO>>(Products);

			var CountSpec = new ProductWithFiltiratonForCountAsync(parameters);
			var ReturnObject = new Pagination<ProductToReturnDTO>()
			{
				PageIndex = parameters.PageIndex,
				PageSize = parameters.PageSize,
				Count = await unitofwork.Repository<Product, int>().CountNumperWithSpec(CountSpec),
				Data = MapedProducts,
			};
			return Ok(ReturnObject);

		}

		[HttpGet("{Id}")]
		[ProducesResponseType(typeof(ProductToReturnDTO), StatusCodes.Status200OK)]// when we found response then the response type will be of {ProductToReturnDTO}
		[ProducesResponseType(typeof(ApiResponce), StatusCodes.Status404NotFound)]// when we Not found response then the response type will be of {ApiResponce}
		public async Task<ActionResult<ProductToReturnDTO>> GetProductById(int Id)
		{
			var Spec = new ProductWithBrandTypeSpec(Id);
			var Product = await unitofwork.Repository<Product, int>().GetByIdWithSpecAsync(Spec);
			if (Product is null) return NotFound(new ApiResponce(404));
			var MapedProduct = mapper.Map<Product, ProductToReturnDTO>(Product);
			return Ok(MapedProduct);
		}

		// Get All Types
		[HttpGet("Types")]
		public async Task<ActionResult<IReadOnlyList<ProductType>>> GetAllTypes()
			=> Ok(await unitofwork.Repository<ProductType, int>().GetAllAsync());


		// Get All Brands
		[HttpGet("Brands")]
		public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetAllBrands()
			=> Ok(await unitofwork.Repository<ProductBrand, int>().GetAllAsync());

	}
}
