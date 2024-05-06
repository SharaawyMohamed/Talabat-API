using AutoMapper;
using AutoMapper.Execution;
using E_Commerce.API.DataTransferObject_DTO.ProductDTO;
using E_Commerce.Core.Models.Product;

namespace E_Commerce.API.Helper.Resolvers
{
    public class ProductPicturUrlResolver : IValueResolver<Product, ProductToReturnDTO, string>
    {
        private readonly IConfiguration configuration;
        // to reach App setting {base url}
        public ProductPicturUrlResolver(IConfiguration _configuration)
        {
            configuration = _configuration;
        }
        public string Resolve(Product source, ProductToReturnDTO destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
            {
                return $"{configuration["ApiBaseUrl"]}{source.PictureUrl}";
            }
            return string.Empty; ;
        }
    }
}
