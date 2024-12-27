using E_Commerce.Core.DataTransferObject_DTO.OrderDTO;
using FluentValidation;

namespace E_Commerce.API.Validations
{
	public class OrderValidator:AbstractValidator<OrderDto>
	{
        public OrderValidator()
        {
			RuleFor(order => order.BasketId).NotEmpty();
			RuleFor(order => order.Email).NotEmpty().EmailAddress();
			RuleFor(order => order.DeliveryMethodId).NotNull();
			RuleFor(order => order.ShippingAddress).NotNull().SetValidator(new AddressValidator());
		}
    }
}
