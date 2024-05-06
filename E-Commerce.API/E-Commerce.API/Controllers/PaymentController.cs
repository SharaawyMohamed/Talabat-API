using E_Commerce.Core.DataTransferObject_DTO.BasketDTO;
using E_Commerce.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace E_Commerce.API.Controllers
{
	public class PaymentController : APIBaseController
	{
		private readonly IPaymentService paymentservice;
		private readonly IConfiguration configuration;
		//const string endpointSecret = "whsec_5048b4a270f0f6745b72fc4e3aff35d4d08beb6b9f8b85d13aed142b9bff7ef9";

		public PaymentController(IPaymentService _paymentservice, IConfiguration _configuration)
		{
			paymentservice = _paymentservice;
			configuration = _configuration;
		}


		[HttpPost]
		public async Task<ActionResult<BasketDto>> CreatePaymentIntent(BasketDto input)
		{
			var basket = paymentservice.CreateOrUpdatePaymentIntentForExistingOrder(input);
			return Ok(basket);
		}


		[HttpPost("Webhook")]
		public async Task<IActionResult> Index()
		{
			var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
			try
			{
				var stripeEvent = EventUtility.ConstructEvent(json,
					Request.Headers["Stripe-Signature"], configuration["Stripe:endpointSecret"]);

				var paymentintent = stripeEvent.Data.Object as PaymentIntent;

				// Handle the event
				if (stripeEvent.Type == Events.PaymentIntentPaymentFailed)
				{
					await paymentservice.UpdatePaymentStatusFailed(paymentintent.Id);
				}
				else if (stripeEvent.Type == Events.PaymentIntentSucceeded)
				{
					await paymentservice.UpdatePaymentStatueSucceeded(paymentintent.Id);
				}
				// ... handle other event types
				else
				{
					Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
				}

				return Ok();
			}
			catch (StripeException e)
			{
				return BadRequest();
			}
		}

	}
}
