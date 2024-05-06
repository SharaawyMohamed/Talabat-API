using AutoMapper;
using E_Commerce.API.Helper.Resolvers;
using E_Commerce.Core.DataTransferObject_DTO.OrderDTO;
using E_Commerce.Core.Models.Order;

namespace E_Commerce.API.Helper.Profiles
{
    public class OrderProfile:Profile
	{
        public OrderProfile()
        {
            CreateMap<ShippingAddress, AddressDto>().ReverseMap();

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(D => D.ProductId, O => O.MapFrom(S => S.OrderItemProduct.ProductId))
                .ForMember(D => D.ProductName, O => O.MapFrom(S => S.OrderItemProduct.ProductName))
                .ForMember(D => D.PictureUrl, O =>O.MapFrom<OrderPicturUrlResolver>());

            CreateMap<Order, OrderResultDto>()
                .ForMember(D => D.DeliveryMethod, O => O.MapFrom(S => S.DeliveryMethod.ShortName))
                .ForMember(D => D.ShippingPrice, O => O.MapFrom(S => S.DeliveryMethod.Price));

        }
    }
}
