using AutoMapper;
using E_Commerce.API.DataTransferObject_DTO.ProductDTO;
using E_Commerce.Core.DataTransferObject_DTO.ProductDTO;
using E_Commerce.Core.Models.Product;
using E_Commerce.Core.Repository;
using E_Commerce.Core.Services;
using E_Commerce.Core.Specification;


namespace E_Commerce.Services
{
    public class ProductServices : IProductServices
	{
		private readonly IUnitOfWork unitofwork;
		private readonly IMapper mapper;

		public ProductServices(IUnitOfWork _unitofwork,IMapper _mapper)
		{
			unitofwork = _unitofwork;
			mapper = _mapper;
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
	}
}
