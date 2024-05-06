using AutoMapper;
using E_Commerce.API.DataTransferObject_DTO.ProductDTO;
using E_Commerce.API.Helper.Resolvers;
using E_Commerce.Core.DataTransferObject_DTO.BasketDTO;
using E_Commerce.Core.DataTransferObject_DTO.ProductDTO;
using E_Commerce.Core.Models.Basket;
using E_Commerce.Core.Models.Product;

namespace E_Commerce.API.Helper.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductBrand, BrandTypeDTO>();
            CreateMap<ProductType, BrandTypeDTO>();

            CreateMap<Product, ProductToReturnDTO>()
                .ForMember(D => D.ProductBrand, O => O.MapFrom(S => S.ProductBrand.Name))
                .ForMember(D => D.ProductType, O => O.MapFrom(S => S.ProductType.Name))
                .ForMember(D => D.PictureUrl, O => O.MapFrom<ProductPicturUrlResolver>());

			CreateMap<CustomerBasket, BasketDto>().ReverseMap();
			CreateMap<BasketItem, BasketItemDto>().ReverseMap();
		}
    }
}
