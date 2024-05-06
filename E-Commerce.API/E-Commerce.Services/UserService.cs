using E_Commerce.Core.JwtToken;
using E_Commerce.Core.Models.Identity;
using E_Commerce.Services.UserServices;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> usermanager;
        private readonly SignInManager<ApplicationUser> signinmanager;
        private readonly ITokenService tokenservice;

        public UserService(UserManager<ApplicationUser> _usermanager, SignInManager<ApplicationUser> _signinmanager, ITokenService _tokenservice)
        {
            usermanager = _usermanager;
            signinmanager = _signinmanager;
            tokenservice = _tokenservice;
        }
        public async Task<UserDto?> LoginAsync(LoginDto dto)
        {
            var user = await usermanager.FindByEmailAsync(dto.Email);
            if (user is not null)
            {
                var res = await signinmanager.CheckPasswordSignInAsync(user, dto.Password, false);
                if (res.Succeeded)
                {
                    return new UserDto()
                    {
                        DisplayName = user.DisplayName,
                        Email = user.Email,
                        Token = await tokenservice.CreateTokenServiceAsync(user)
                    };
                }
            }
            return null;
        }

        public Task<UserDto> RegisterAsync(RegisterDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
