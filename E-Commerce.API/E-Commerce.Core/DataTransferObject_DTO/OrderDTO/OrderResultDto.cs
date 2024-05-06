using E_Commerce.Core.Models.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.DataTransferObject_DTO.OrderDTO
{
	public class OrderResultDto
	{
		public Guid Id { get; set; }
		public string BuyerEmail { get; set; }
		public DateTime OrderDateTime { get; set; }
		public AddressDto ShippingAddress { get; set; }
		public string DeliveryMethod { get; set; }
		public IEnumerable<OrderItemDto> OrderItems { get; set; }
		public PaymentStatus PaymentStatus { get; set; }
		public decimal SubTotal { get; set; }
		public decimal ShippingPrice { get; set; }
		public string? PaymentIntentId { get; set; }
		public string? BasketId { get; set; }
		public decimal Total { get; set; }
	}
}
