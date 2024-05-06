using E_Commerce.Core.Models.Order;

namespace E_Commerce.Core.DataTransferObject_DTO.OrderDTO
{
	public class OrderItemDto
	{
		public Guid Id { get; set; }
		public int Quantity { get; set; }
		public decimal Price { get; set; }
		public int ProductId { get; set; }
		public string ProductName { get; set; }
		public string PictureUrl { get; set; }
	}
}