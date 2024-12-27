using E_Commerce.Services.UserServices;
using FluentValidation;

namespace E_Commerce.API.Validations
{
	public class UserValidation:AbstractValidator<UserDto>
	{
        public UserValidation()
        {
            RuleFor(x => x.Email).NotNull().EmailAddress();
            RuleFor(x => x.DisplayName).NotNull();
            RuleFor(x => x.Token).NotNull();
        }
    }
}
