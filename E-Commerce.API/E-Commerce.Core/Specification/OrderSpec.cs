using E_Commerce.Core.Models.Order;
using E_Commerce.Core.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Repository.Specification
{
	public class OrderSpec : BaseSpecification<Order,Guid>
	{
		public OrderSpec(string email)
			: base(order => order.BuyerEmail == email)
		{
			IncludeExpressions.Add(order => order.DeliveryMethod);
			IncludeExpressions.Add(order => order.OrderItems);
			OrderByDes = (o => o.OrderDateTime);
		}
		public OrderSpec(Guid Id, string email)
			: base(order => order.BuyerEmail == email && order.Id == Id)
		{
			IncludeExpressions.Add(order => order.DeliveryMethod);
			IncludeExpressions.Add(order => order.OrderItems);

		}
	}
}
