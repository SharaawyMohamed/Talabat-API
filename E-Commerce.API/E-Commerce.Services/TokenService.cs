using E_Commerce.Core.JwtToken;
using E_Commerce.Core.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services
{
    public class TokenService : ITokenService
    {
        //private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration configuration;

        public TokenService(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        public async Task<string> CreateTokenServiceAsync(ApplicationUser user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.DisplayName),
            };

            // Roles
            //var roles = await userManager.GetRolesAsync(user);
            //claims.AddRange(roles.Select(role=>new Claim(ClaimTypes.Role,role)));

            // Security key
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Create Token : we have two ways

            // 1 
            var tokendescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Issuer = configuration["Token:Issuer"],
                Audience = configuration["Token:Audience"],
                Expires = DateTime.Now.AddDays(double.Parse(configuration["Token:DurationInDays"])),// after this time ,should you Relogin
                SigningCredentials = credentials,
            };
            var Token = new JwtSecurityTokenHandler();
            var token = Token.CreateToken(tokendescriptor);
            return Token.WriteToken(token);
        }
    }
}
