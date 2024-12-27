using E_Commerce.Core.DataTransferObject_DTO.ProductDTO;
using FluentValidation;

namespace E_Commerce.API.Validations
{
	public class CreateBrandTypeValidator:AbstractValidator<CreateBrandTypeDto>
	{
        public CreateBrandTypeValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty();
            RuleFor(x => x.image).NotNull();
        }
    }
}
