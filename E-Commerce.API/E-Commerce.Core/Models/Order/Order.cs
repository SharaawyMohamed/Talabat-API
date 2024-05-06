using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Models.Order
{
	public class Order:BaseEntity<Guid>
	{
        public string BuyerEmail { get; set; }
        public DateTime OrderDateTime { get; set; } = DateTime.Now;
        public ShippingAddress ShippingAddress { get; set; }    
        public DeliveryMethod DeliveryMethod{ get; set; }
        public int? DeliveryMethodId { get; set; }

        public IEnumerable<OrderItem> OrderItems { get; set; }

        public PaymentStatus paymentStatus { get; set; } = PaymentStatus.Pending;

        public decimal SubTotal { get; set; }
        public string? PaymentIntentId { get; set; }
        public string? BasketId { get; set; }
        public decimal CalcTotal() => SubTotal + DeliveryMethod.Price;
    }
}
