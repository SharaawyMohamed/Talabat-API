using E_Commerce.Core.DataTransferObject_DTO.OrderDTO;
using E_Commerce.Core.Models.Order;
using E_Commerce.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace E_Commerce.API.Controllers
{
	[Authorize]
	public class OrdersController : APIBaseController
	{
		private readonly IOrderService orderservic;

		public OrdersController(IOrderService _orderservic)
		{
			orderservic = _orderservic;
		}
		[HttpPost]
		public async Task<ActionResult<OrderResultDto>> Create(OrderDto input)
		{
			var order = await orderservic.CreateOrderAsync(input);

			return Ok(order);
		}
		[HttpGet("Orders")]
		public async Task<ActionResult<IEnumerable<OrderResultDto>>> GetAllOrders()
		{
			var email = User.FindFirstValue(ClaimTypes.Email);
			var orders = await orderservic.GetAllOrdersAsync(email);
			return Ok(orders);
		}
		
		[HttpGet("{Id}")]
		public async Task<ActionResult<OrderResultDto>> GetOrder(Guid Id)
		{
			var email = User.FindFirstValue(ClaimTypes.Email);
			var order = await orderservic.GetOrderAsync(Id,email);
			return Ok(order);
		}
		[HttpGet("Delivery")]
		public async Task<ActionResult<IEnumerable<DeliveryMethod>>> GetDeliveryMethods()
		{
			return Ok(await orderservic.GetDeliveryMethodsAsync());	
		}
	}
}
