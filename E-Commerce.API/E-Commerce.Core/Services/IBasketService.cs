using E_Commerce.Core.DataTransferObject_DTO.BasketDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Services
{
	public interface IBasketService
	{
		public Task<BasketDto?> GetBasketAsync(string Id);
		public Task<BasketDto?> UpdateBasketAsync(BasketDto basket);
		public Task<bool> DeleteBasketAsync(string Id);
	}
}
