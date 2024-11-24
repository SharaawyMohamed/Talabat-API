using AutoMapper;
using E_Commerce.Core.DataTransferObject_DTO.BasketDTO;
using E_Commerce.Core.DataTransferObject_DTO.OrderDTO;
using E_Commerce.Core.Repository;
using E_Commerce.Core.Services;
using Product = E_Commerce.Core.Models.Product.Product;
using CustomerBasket = E_Commerce.Core.Models.Basket.CustomerBasket;
using Microsoft.Extensions.Configuration;
using Stripe;
using E_Commerce.Core.Models.Order;
using Microsoft.EntityFrameworkCore.Storage;
using E_Commerce.Core.Models.Basket;
using E_Commerce.Core.Specification;
namespace E_Commerce.Services
{
    public class PaymentService : IPaymentService
	{
		private readonly IUnitOfWork unitofwork;
		private readonly IBasketRepository basketservice;
		private readonly IMapper mapper;
		private readonly IConfiguration configuration;

		public PaymentService(IUnitOfWork _unitofwork, Core.Repository.IBasketRepository _basketservice, IMapper _mapper, IConfiguration _configuration)
		{
			unitofwork = _unitofwork;
			basketservice = _basketservice;
			mapper = _mapper;
			configuration = _configuration;
		}
		public async Task<BasketDto> CreateOrUpdatePaymentIntentForExistingOrder(BasketDto basket)
		{
			StripeConfiguration.ApiKey = configuration["Stripe:SecretKey"];
			//1. Calculate Amount
			// Total=Price*Quantity
			foreach (var item in basket.basketItems)
			{
				// Check Product Price
				var product = await unitofwork.Repository<Product, int>().GetByIdAsync(item.Id);
				if (product?.Price != item.Price)
				{
					item.Price = product.Price;
				}
			}
			// Calculate Total Price
			var total = basket.basketItems.Sum(p => p.Price * p.Quantity);
			// Retreve Shipping Price
			if (!basket.DeliveryMethodId.HasValue) throw new Exception("No Delivery Method Selected");

			var deliverymethod = await unitofwork.Repository<DeliveryMethod, int>().GetByIdAsync(basket.DeliveryMethodId.Value);
			var ShippingPrice = deliverymethod.Price;
			basket.ShippingPrice = deliverymethod.Price;

			// Calculate Amount in the smalest unite ex) 1$ => 100c
			long amount = (long)(total * 100 + ShippingPrice * 100);

			//Create Or Update 

			var service = new PaymentIntentService();
			PaymentIntent paymentIntent;
			if (string.IsNullOrEmpty(basket.PaymentIntentId))
			{

				// Create
				var options = new PaymentIntentCreateOptions()
				{
					Amount = amount,
					Currency = "USD",
					PaymentMethodTypes = new List<string> { "card" }
				};
				paymentIntent = await service.CreateAsync(options);
				basket.PaymentIntentId = paymentIntent.Id;
				basket.ClinetSecret = paymentIntent.ClientSecret;
			}
			else
			{
				// Update
				var options = new PaymentIntentUpdateOptions()
				{
					Amount = amount,
				};

				await service.UpdateAsync(basket.PaymentIntentId, options);
			}

			await basketservice.UpdateBasketAsync(mapper.Map<BasketDto, CustomerBasket>(basket));
			return basket;
		}

		public async Task<BasketDto> CreateOrUpdatePaymentIntentForNewOrder(string basketId)
		{
			StripeConfiguration.ApiKey = configuration["Stripe:SecretKey"];

			// 0 Get Basket 
			var basket = await basketservice.GetCustomerBasketAsync(basketId);
			//1. Calculate Amount
			// Total=Price*Quantity
			foreach (var item in basket.basketItems)
			{
				// Check Product Price
				var product = await unitofwork.Repository<Product, int>().GetByIdAsync(item.Id);
				if (product?.Price != item.Price)
				{
					item.Price = product.Price;
				}
			}
			// Calculate Total Price
			var total = basket.basketItems.Sum(p => p.Price * p.Quantity);
			// Retreve Shipping Price
			if (!basket.DeliveryMethodId.HasValue) throw new Exception("No Delivery Method Selected");

			var deliverymethod = await unitofwork.Repository<DeliveryMethod, int>().GetByIdAsync(basket.DeliveryMethodId.Value);
			var ShippingPrice = deliverymethod.Price;
			basket.ShippingPrice = deliverymethod.Price;

			// Calculate Amount in the smalest unite ex) 1$ => 100c
			long amount = (long)(total * 100 + ShippingPrice * 100);

			//Create Or Update 

			var service = new PaymentIntentService();
			PaymentIntent paymentIntent;
			if (string.IsNullOrEmpty(basket.PaymentIntentId))
			{
				// Create
				var options = new PaymentIntentCreateOptions()
				{
					Amount = amount,
					Currency = "USD",
					PaymentMethodTypes = new List<string> { "card" }
				};
				paymentIntent = await service.CreateAsync(options);
				basket.PaymentIntentId = paymentIntent.Id;
				basket.ClinetSecret = paymentIntent.ClientSecret;
			}
			else
			{
				// Update
				var options = new PaymentIntentUpdateOptions()
				{
					Amount = amount,
				};

				await service.UpdateAsync(basket.PaymentIntentId, options);
			}

			await basketservice.UpdateBasketAsync(basket);
			return mapper.Map<CustomerBasket, BasketDto>(basket);
		}

		public async Task<OrderResultDto> UpdatePaymentStatueSucceeded(string PaymentIntentId)
		{

			//1. Get Order By Payment Id
			var Spec = new OrderWithPaymentIntentIdSpec(PaymentIntentId);
			var order = await unitofwork.Repository<Order, Guid>().GetByIdWithSpecAsync(Spec);
			if (order is null) throw new Exception($"There is No Order By ID : {PaymentIntentId}");
			//2. Update Payment Status 
			order.paymentStatus = PaymentStatus.Received;
			unitofwork.Repository<Order, Guid>().Update(order);
			//3. ShaveChanges
			await unitofwork.CompleteAsync();
			//4. DeleteBrandOrCategory Customer Basket
			await basketservice.DeleteBasketAsync(order.BasketId);
			return mapper.Map<Order, OrderResultDto>(order);
		}

		public async Task<OrderResultDto> UpdatePaymentStatusFailed(string PaymentIntentId)
		{

			//1. Get Order By Payment Id
			var Spec = new OrderWithPaymentIntentIdSpec(PaymentIntentId);
			var order = await unitofwork.Repository<Order, Guid>().GetByIdWithSpecAsync(Spec);
			if(order is null)throw new Exception($"There is No Order By ID : {PaymentIntentId}");
			//2. Update Payment Status 
			order.paymentStatus = PaymentStatus.Failed;
		    unitofwork.Repository<Order,Guid>().Update(order);
			//3. ShaveChanges
			await unitofwork.CompleteAsync();

			return mapper.Map<Order, OrderResultDto>(order);	
		}
	}
}
