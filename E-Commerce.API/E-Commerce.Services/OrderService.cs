using AutoMapper;
using E_Commerce.Core.DataTransferObject_DTO.BasketDTO;
using E_Commerce.Core.DataTransferObject_DTO.OrderDTO;
using E_Commerce.Core.Models.Basket;
using E_Commerce.Core.Models.Order;
using E_Commerce.Core.Models.Product;
using E_Commerce.Core.Repository;
using E_Commerce.Core.Services;
using E_Commerce.Core.Specification;
using E_Commerce.Repository.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services
{
    public class OrderService : IOrderService
	{
		private readonly IBasketService basketservice;
		private readonly IUnitOfWork unitofwork;
		private readonly IMapper mapper;
		private readonly IPaymentService paymentservice;

		public OrderService(IBasketService _basketservice, IUnitOfWork _unitofwork, IMapper _mapper, IPaymentService _paymentservice)
		{
			basketservice = _basketservice;
			unitofwork = _unitofwork;
			mapper = _mapper;
			paymentservice = _paymentservice;
		}
		public async Task<OrderResultDto> CreateOrderAsync(OrderDto input)
		{
			//1. Get Basket 

			var basket = await basketservice.GetBasketAsync(input.BasketId);
			if (basket is null) throw new Exception($"No Basket With ID : {input.BasketId}.");

			//2. Create Items List and Get Order Items From Basket Items 
			var orderitems = new List<OrderItem>();
			foreach (var basketitem in basket.basketItems)
			{
				var product = await unitofwork.Repository<Product, int>().GetByIdAsync(basketitem.Id);
				if (product is not null)
				{
					var ProductItem = new OrderItemProduct()
					{
						PictureUrl = product.PictureUrl,
						ProductId = product.Id,
						ProductName = product.Name,
					};

					var OrderItem = new OrderItem()
					{
						OrderItemProduct = ProductItem,
						Price = product.Price,
						Quantity = basketitem.Quantity,
					};
					orderitems.Add(OrderItem);
				}
			}

			if (orderitems.Count == 0)
			{
				throw new Exception("There is not Basket Items Was Found");
			}

			//3. Get Delivery Method
			if (!input.DeliveryMethodId.HasValue)
			{
				throw new Exception("No Delivery Method Selected");
			}
			var deliveryMethod = await unitofwork.Repository<DeliveryMethod, int>().GetByIdAsync(input.DeliveryMethodId.Value);
			if (deliveryMethod is null) throw new Exception($"In Valid Delivery Method's ID : {input.DeliveryMethodId.Value}.");


			//4. Get Shipping Address
			var mapdedShippingAddress = mapper.Map<AddressDto, ShippingAddress>(input.ShippingAddress);

			//5. Calc SubTotal= Price * Quantity // for every order

			//Un Comoplete  : Create PaymentIntent
			var Spec = new OrderWithPaymentIntentIdSpec(basket.PaymentIntentId);
			var ExistOrder = await unitofwork.Repository<Order, Guid>().GetByIdWithSpecAsync(Spec);
			if(ExistOrder is not null)
			{
				unitofwork.Repository<Order, Guid>().Delete(ExistOrder);
				await paymentservice.CreateOrUpdatePaymentIntentForExistingOrder(basket);
			}
			else
			{
				basket = await paymentservice.CreateOrUpdatePaymentIntentForNewOrder(basket.Id); 
			}


			var subTotal = orderitems.Sum(item => (item.Price * item.Quantity));

			//6. map orderitems from OrderItemDTO => OrderItem
			//var mapedorderitem = mapper.Map<List<OrderItemDto>, List<OrderItem>>(orderitems);


			var order = new Order()
			{
				BuyerEmail = input.Email,
				ShippingAddress = mapdedShippingAddress,
				DeliveryMethod = deliveryMethod,
				OrderItems = orderitems,
				SubTotal = subTotal,
				PaymentIntentId=basket.PaymentIntentId,
				BasketId=basket.Id
			};

			await unitofwork.Repository<Order, Guid>().AddAsync(order);
			await unitofwork.CompleteAsync();
			return mapper.Map<Order, OrderResultDto>(order);
		}

		public async Task<IReadOnlyList<OrderResultDto>> GetAllOrdersAsync(string Email)
		{
			
			if (Email is null) throw new Exception($"User's Email Is Null");
			var spec = new OrderSpec(Email);
			var orders = await unitofwork.Repository<Order, Guid>().GetAllWithSpecAsync(spec);
			if (orders.Count() == 0) throw new Exception($"No Orders Yet For User : {Email}");
			return mapper.Map<IReadOnlyList<Order>, IReadOnlyList<OrderResultDto>>(orders);
		}

		public async Task<IEnumerable<DeliveryMethod>> GetDeliveryMethodsAsync()
		{
			return await unitofwork.Repository<DeliveryMethod, int>().GetAllAsync();
		}

		public async Task<OrderResultDto> GetOrderAsync(Guid Id, string Email)
		{
			if (Email is null) throw new Exception($"User's Email Is Null");

			var spec = new OrderSpec(Id, Email);
			var order = await unitofwork.Repository<Order, Guid>().GetByIdWithSpecAsync(spec);
			if (order is null) throw new Exception($"No Order With Id : {Id}");

			return mapper.Map<Order, OrderResultDto>(order);
		}


	}
}
