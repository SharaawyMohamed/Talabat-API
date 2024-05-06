using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Models.Basket
{
	public class CustomerBasket
	{
		public string Id { get; set; }
		public int? DeliveryMethodId { get; set; }
		public decimal ShippingPrice { get; set; }
		public string? PaymentIntentId { get; set; }
		public string? ClinetSecret { get; set; }
		public List<BasketItem> basketItems { get; set; } = new List<BasketItem>();
    }
}
