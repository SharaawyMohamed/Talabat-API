using E_Commerce.Core.DataTransferObject_DTO.ProductDTO;
using FluentValidation;

namespace E_Commerce.API.Validations
{
	public class BrandTypeValidator:AbstractValidator<BrandTypeDTO>
	{
        public BrandTypeValidator()
        {
            RuleFor(x => x.Id).NotNull().GreaterThan(-1);
            RuleFor(x => x.Name).NotNull().NotEmpty();
            RuleFor(x => x.ImageUrl).NotNull();
        }
    }
}
