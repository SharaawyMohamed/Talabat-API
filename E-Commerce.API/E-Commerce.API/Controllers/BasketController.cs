using E_Commerce.API.Errors;
using E_Commerce.Core.DataTransferObject_DTO.BasketDTO;
using E_Commerce.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers
{
    public class BasketController : APIBaseController
	{
		private readonly IBasketService basketservic;

		public BasketController(IBasketService _basketservic)
		{
			basketservic = _basketservic;
		}

		[HttpGet("{Id}")]
		public async Task<ActionResult<BasketDto>> GetCustomerBasket(string Id)
		{
			var basket = await basketservic.GetBasketAsync(Id);
			return basket is null ? new BasketDto(Id) : Ok(basket);
		}

		[HttpPost]
		public async Task<ActionResult<BasketDto>> UpdatCustomerBaskert(BasketDto basket)
		{
			var CreateOrUpdateBaskert = await basketservic.UpdateBasketAsync(basket);

			return CreateOrUpdateBaskert is not null ? Ok(CreateOrUpdateBaskert) : BadRequest(new ApiResponce(400));
		}

		[HttpDelete("{Id}")]
		public async Task<ActionResult<bool>> DeleteCustomerBasket(string Id)
		=>	 await basketservic.DeleteBasketAsync(Id);


	}
}
