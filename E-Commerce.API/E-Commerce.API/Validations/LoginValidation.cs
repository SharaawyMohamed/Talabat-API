using E_Commerce.Services.UserServices;
using FluentValidation;

namespace E_Commerce.API.Validations
{
	public class LoginValidation:AbstractValidator<LoginDto>
	{
        public LoginValidation()
        {
            RuleFor(x => x.Email).NotNull().EmailAddress();
            RuleFor(x => x.Password).NotEmpty().MinimumLength(8)
                .Matches(@"[A-Z]")
                .Matches(@"[a-z]")
                .Matches(@"[0-9]")
                .Matches(@"[\W_]");
        }
    }
}
