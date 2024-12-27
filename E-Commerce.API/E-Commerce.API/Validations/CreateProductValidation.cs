using E_Commerce.Core.Models.Product;
using FluentValidation;

namespace E_Commerce.API.Validations
{
	public class CreateProductValidation:AbstractValidator<CreateProductDto>
	{
        public CreateProductValidation()
        {
            RuleFor(x => x.Name).NotNull();
            RuleFor(x => x.Price).NotNull().GreaterThan(0);

        }
    }
}
