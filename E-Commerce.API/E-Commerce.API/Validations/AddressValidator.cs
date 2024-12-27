using E_Commerce.Core.DataTransferObject_DTO.OrderDTO;
using FluentValidation;

namespace E_Commerce.API.Validations
{
	public class AddressValidator:AbstractValidator<AddressDto>
	{
        public AddressValidator()
        {
			RuleFor(address => address.Street).NotEmpty();
			RuleFor(address => address.City).NotEmpty();
			RuleFor(address => address.State).NotEmpty();
			RuleFor(address => address.Country).NotEmpty();
			RuleFor(address => address.PostalCode).NotEmpty().Matches("^[0-9]{5}$");
		}
    }
}
