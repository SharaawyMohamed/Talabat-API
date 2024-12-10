using AutoMapper;
using E_Commerce.Core.DataTransferObject_DTO.ProductDTO;
using E_Commerce.Core.Models.Product;

namespace E_Commerce.API.Helper.Resolvers
{
	public class CategoryResolver:IValueResolver<ProductType,BrandTypeDTO,string>
	{
        private readonly IConfiguration config;
        public CategoryResolver(IConfiguration configuration)
        {
            config = configuration;
        }

		public string Resolve(ProductType source, BrandTypeDTO destination, string destMember, ResolutionContext context)
		{
			if (!string.IsNullOrEmpty(source.ImageUrl))
			{
				return $"{config["ApiBaseUrl"]}{source.ImageUrl}";
			}
			return string.Empty; 
		}
	}
}
