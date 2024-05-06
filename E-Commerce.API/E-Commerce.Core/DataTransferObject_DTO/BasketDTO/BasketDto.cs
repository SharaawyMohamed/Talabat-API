using E_Commerce.Core.Models.Basket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.DataTransferObject_DTO.BasketDTO
{
	public class BasketDto
	{
		public string Id { get; set; }
		public int? DeliveryMethodId { get; set; }
		public decimal ShippingPrice { get; set; }
		public List<BasketItemDto> basketItems { get; set; } = new List<BasketItemDto>();
		public string? PaymentIntentId { get; set; }
		public string? ClinetSecret { get; set; }

		public BasketDto(string id)
		{
			Id = id;
		}
	}
}
#region MyRegion
/*
 public string Id { get; set; }
		public string? DeliveryMethodId { get; set; }
		public decimal ShippingPrice { get; set; }
		public string? PaymentIntentId { get; set; }
		public string? ClinetSecret { get; set; }
		public List<BasketItem> basketItems { get; set; } = new List<BasketItem>();
        public CustomerBasket(string id)
        {
			Id = id;
        }
 */
#endregion