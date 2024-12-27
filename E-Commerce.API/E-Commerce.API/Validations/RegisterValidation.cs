using E_Commerce.Services.UserServices;
using FluentValidation;

namespace E_Commerce.API.Validations
{
	public class RegisterValidation:AbstractValidator<RegisterDto>
	{
        public RegisterValidation()
        {
            RuleFor(x=>x.DisplayName).NotEmpty();
            RuleFor(x => x.Email).NotNull().EmailAddress();
            RuleFor(x => x.PhoneNumber).NotNull().NotNull().Length(10, 13);
            RuleFor(x => x.Password).NotNull();
        }
    }
}
