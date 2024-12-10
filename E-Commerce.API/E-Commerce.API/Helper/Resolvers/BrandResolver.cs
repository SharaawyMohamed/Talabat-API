using AutoMapper;
using E_Commerce.API.DataTransferObject_DTO.ProductDTO;
using E_Commerce.Core.DataTransferObject_DTO.ProductDTO;
using E_Commerce.Core.Models.Product;

namespace E_Commerce.API.Helper.Resolvers
{
	public class BrandResolver:IValueResolver<ProductBrand,BrandTypeDTO,string>
	{
		private readonly IConfiguration configuration;
		// to reach App setting {base url}
		public BrandResolver(IConfiguration _configuration)
		{
			configuration = _configuration;
		}
		public string Resolve(ProductBrand source, BrandTypeDTO destination, string destMember, ResolutionContext context)
		{
			if (!string.IsNullOrEmpty(source.ImageUrl))
			{
				return $"{configuration["ApiBaseUrl"]}{source.ImageUrl}";
			}
			return string.Empty; 
		}
	}
}
