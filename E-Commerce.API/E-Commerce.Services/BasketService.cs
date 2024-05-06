using AutoMapper;
using E_Commerce.Core.DataTransferObject_DTO.BasketDTO;
using E_Commerce.Core.Models.Basket;
using E_Commerce.Core.Repository;
using E_Commerce.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services
{
	public class BasketService : IBasketService
	{
		private readonly IBasketRepository basketrepo;
		private readonly IMapper mapper;

		public BasketService(IBasketRepository _basketrepo, IMapper _mapper)
		{
			basketrepo = _basketrepo;
			mapper = _mapper;
		}

		public async Task<bool> DeleteBasketAsync(string Id)
		{
			return await basketrepo.DeleteBasketAsync(Id);
		}

		public async Task<BasketDto?> GetBasketAsync(string Id)
		{
			var basket = await basketrepo.GetCustomerBasketAsync(Id);
			return basket is null ? null : mapper.Map<CustomerBasket, BasketDto>(basket);
		}

		public async Task<BasketDto?> UpdateBasketAsync(BasketDto basket)
		{
			var maped = mapper.Map<BasketDto,CustomerBasket>(basket); 
			var toreturn=await basketrepo.UpdateBasketAsync(maped);
			return toreturn is null ? null : mapper.Map<CustomerBasket,BasketDto>(toreturn);
		}
	}
}
