using AutoMapper;
using E_Commerce.API.DataTransferObject_DTO.ProductDTO;
using E_Commerce.API.Errors;
using E_Commerce.API.Helper;
using E_Commerce.Core.Models.Product;
using E_Commerce.Core.Repository;
using E_Commerce.Core.Services;
using E_Commerce.Core.Specification;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers
{
	public class ProductController : APIBaseController
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly IProductServices _productService;
		public ProductController(
			IUnitOfWork unitOfWork,
			IMapper mapper,
			IProductServices productService
			)
		{

			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_productService = productService;
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

		[HttpPost("AddProduct")]
		public async Task<ActionResult<CreateProductDto>> AddProduct([FromForm] CreateProductDto product)
		{
			var brand = await _unitOfWork.Repository<ProductBrand, int>().GetByIdAsync(product.BrandId);
			if (brand == null) return NotFound("Brand id is not valid.");
			var category = await _unitOfWork.Repository<ProductType, int>().GetByIdAsync(product.TypeId);
			if (category == null) return Ok("Category id is not valid.");

			return Ok(await _productService.AddProductAsync(product));

		}

		[HttpDelete("DeleteProduct")]
		public async Task<ActionResult<ProductToReturnDTO>> DeleteProduct(int Id)
		{
			var product = await _unitOfWork.Repository<Product, int>().GetByIdAsync(Id);
			if (product == null)
			{
				return NotFound("In valid product Id.");
			}
			var response = await _productService.DeleteProductAsync(Id);
			return Ok(response ? "Product deleted successfully." : "Internal server error.");
		}

		[HttpPut("UpdateProduct")]
		public async Task<ActionResult<ProductToReturnDTO>> UpdateProduct([FromForm] UpdateProductDto productDto)
		{
			var product = await _unitOfWork.Repository<Product, int>().GetByIdAsync(productDto.Id);
			if (product == null)
			{
				return NotFound("In valid product Id.");
			}

			return Ok(await _productService.UpdateProductAsync(productDto));
		}
	}
}
