
using E_Commerce.API.DataTransferObject_DTO.ProductDTO;
using E_Commerce.Core.DataTransferObject_DTO.ProductDTO;
using E_Commerce.Core.Specification;

namespace E_Commerce.Core.Services
{
    public interface IProductServices
	{
	    Task<IEnumerable<ProductToReturnDTO>> GetAllProductsAsync(ProductSpecParameter param);
		Task<ProductToReturnDTO> GetProductAsync(int Id);

	}
}
