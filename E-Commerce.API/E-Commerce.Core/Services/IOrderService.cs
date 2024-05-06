using E_Commerce.Core.DataTransferObject_DTO.OrderDTO;
using E_Commerce.Core.Models.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Services
{
	public interface IOrderService
	{
		public Task<IEnumerable<DeliveryMethod>> GetDeliveryMethodsAsync();
		public Task<OrderResultDto> CreateOrderAsync(OrderDto order);
		public Task<OrderResultDto> GetOrderAsync(Guid Id,string Email);
		public Task<IReadOnlyList<OrderResultDto>> GetAllOrdersAsync(string Email);
	}
}
