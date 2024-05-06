using Order = E_Commerce.Core.Models.Order.Order;


namespace E_Commerce.Core.Specification
{
	public class OrderWithPaymentIntentIdSpec : BaseSpecification<Order,Guid>
	{
		public OrderWithPaymentIntentIdSpec(string PaymentIntentId)
			: base(order => order.PaymentIntentId == PaymentIntentId)
		{
			IncludeExpressions.Add(order => order.DeliveryMethod);
			IncludeExpressions.Add(order => order.OrderItems);
		}
	}
}
