using AutoMapper;
using E_Commerce.API.DataTransferObject_DTO.ProductDTO;
using E_Commerce.Core.DataTransferObject_DTO.ProductDTO;
using E_Commerce.Core.Models.Product;
using E_Commerce.Core.Repository;
using E_Commerce.Core.Services;
using E_Commerce.Core.Specification;
using System.Runtime.InteropServices;


namespace E_Commerce.Services
{
	public class ProductServices : IProductServices
	{
		private readonly IUnitOfWork unitofwork;
		private readonly IMedia _media;
		private readonly IMapper mapper;

		public ProductServices(IUnitOfWork _unitofwork, IMapper _mapper)
		{
			unitofwork = _unitofwork;
			mapper = _mapper;
		}

		public async Task<ProductToReturnDTO> AddProductAsync(CreateProductDto productDto)
		{
			var product = new Product
			{
				Name = productDto.Name,
				Description = productDto.Description,
				quantity = productDto.quantity,
				Price = productDto.Price,
				PictureUrl = _media.UploadFile(productDto.Image, "products"),
				TypeId = productDto.TypeId,
				BrandId = productDto.BrandId
			};
			await unitofwork.Repository<Product, int>().AddAsync(product);
			await unitofwork.CompleteAsync();

			var response = await unitofwork.Repository<Product, int>().GetByIdAsync(product.Id);

			return mapper.Map<ProductToReturnDTO>(response);
		}

		public async Task<bool> DeleteProductAsync(int Id)
		{
			var product = await unitofwork.Repository<Product, int>().GetByIdAsync(Id);
			unitofwork.Repository<Product, int>().Delete(product);
			await unitofwork.CompleteAsync();
			return true;
		}

		public async Task<IEnumerable<ProductToReturnDTO>> GetAllProductsAsync(ProductSpecParameter param)
		{
			var Spec = new ProductWithBrandTypeSpec(param);
			var products = await unitofwork.Repository<Product, int>().GetAllWithSpecAsync(Spec);
			return mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDTO>>(products);
		}
		
		public async Task<ProductToReturnDTO> GetProductAsync(int Id)
		{
			var Spec = new ProductWithBrandTypeSpec(Id);
			var product = await unitofwork.Repository<Product, int>().GetByIdWithSpecAsync(Spec);
			return mapper.Map<Product, ProductToReturnDTO>(product);
		}

		public async Task<ProductToReturnDTO> UpdateProductAsync(UpdateProductDto productDto)
		{
			var product = await unitofwork.Repository<Product, int>().GetByIdAsync(productDto.Id);

			if (productDto.Image != null && product.PictureUrl != null)
			{
				_media.DeleteFile("products", product.PictureUrl.Split('/')[^1]);
			}

			product.Name = productDto.Name ?? product.Name;
			product.Description = productDto.Description ?? product.Description;
			product.Price = productDto.Price ?? product.Price;
			product.quantity = productDto.quantity ?? product.quantity;

			if (productDto.Image != null)
			{
				product.PictureUrl = _media.UploadFile(productDto.Image, "products");
			}

			unitofwork.Repository<Product, int>().Update(product);
			await unitofwork.CompleteAsync();
			return mapper.Map<ProductToReturnDTO>(product);
		}
	}
}
