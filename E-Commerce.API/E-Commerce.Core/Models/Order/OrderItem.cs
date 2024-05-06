using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Core.Models.Order
{
	public class OrderItem:BaseEntity<Guid>
	{
        public int Quantity { get; set; }
		public decimal Price { get; set; }
		public OrderItemProduct OrderItemProduct { get; set; }

	}
}