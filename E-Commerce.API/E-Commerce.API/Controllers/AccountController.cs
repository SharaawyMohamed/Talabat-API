using E_Commerce.API.Errors;
using E_Commerce.API.Helper;
using E_Commerce.API.Validations;
using E_Commerce.Core.JwtToken;
using E_Commerce.Core.Models.Identity;
using E_Commerce.Services.UserServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers
{

	public class AccountController : APIBaseController
	{
		private readonly IUserService userservice;
		private readonly UserManager<ApplicationUser> usermanager;
		private readonly SignInManager<ApplicationUser> signInmanager;
		private readonly ITokenService tokenservice;

		public AccountController(IUserService _userservice,
			UserManager<ApplicationUser> _usermanager,
			SignInManager<ApplicationUser> _signInmanager,
			ITokenService _tokenservice)
		{
			userservice = _userservice;
			usermanager = _usermanager;
			signInmanager = _signInmanager;
			tokenservice = _tokenservice;
		}
		[HttpPost("Login")]
		public async Task<ActionResult<UserDto>> Login(LoginDto input)
		{
			var validator = new LoginValidation();
			var validatorResult = await validator.ValidateAsync(input);
			if (!validatorResult.IsValid)
			{
				var res= validatorResult.Errors
					.Select(error => new Error
					{
						Property = error.PropertyName,
						ErrorMessage = error.ErrorMessage
					}).ToList();
			// I want to return this list of errors ?
			}

			var user = await usermanager.FindByEmailAsync(input.Email);
			if (user is null) return Unauthorized(new ApiResponce(401, "Incorrect Email"));

			var correctpassword = await signInmanager.CheckPasswordSignInAsync(user, input.Password, false);
			return !correctpassword.Succeeded ? BadRequest(new ApiResponce(400, "Incoreect Password")) : new UserDto()
			{
				DisplayName = user.DisplayName,
				Email = user.Email,
				Token = await tokenservice.CreateTokenServiceAsync(user)
			};
		}
		[HttpPost("Register")]
		public async Task<ActionResult<UserDto>> Register(RegisterDto input)
		{
			var user = new ApplicationUser()
			{
				DisplayName = input.DisplayName,
				Email = input.Email,
				UserName = input.Email.Split('@')[0],
				PhoneNumber = input.PhoneNumber,
				PasswordHash = input.Password
			};
			//                                 password would be sotred in database {Hashed}
			var isCreate = await usermanager.CreateAsync(user, input.Password);
			return !isCreate.Succeeded ? BadRequest(new ApiResponce(400)) : new UserDto()
			{
				DisplayName = user.DisplayName,
				Email = user.Email,
				Token = await tokenservice.CreateTokenServiceAsync(user),
			};
		}
	}
}
