using AutoMapper;
using E_Commerce.API.DataTransferObject_DTO.ProductDTO;
using E_Commerce.Core.DataTransferObject_DTO.OrderDTO;
using E_Commerce.Core.Models.Order;
using E_Commerce.Core.Models.Product;

namespace E_Commerce.API.Helper.Resolvers
{
	public class OrderPicturUrlResolver : IValueResolver<OrderItem, OrderItemDto, string>
	{
		private readonly IConfiguration configuration;
		// to reach App setting {base url}
		public OrderPicturUrlResolver(IConfiguration _configuration)
		{
			configuration = _configuration;
		}
		public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
		=> (string.IsNullOrEmpty(source.OrderItemProduct.PictureUrl)) ?
				 string.Empty : $"{configuration["ApiBaseUrl"]}{source.OrderItemProduct.PictureUrl}";
	}
}
