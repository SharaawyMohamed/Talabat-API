using E_Commerce.Core.JwtToken;
using E_Commerce.Core.Models.Identity;
using E_Commerce.Repository.Data;
using E_Commerce.Services;
using E_Commerce.Services.UserServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace E_Commerce.API.Extentions
{
    public static class IdentityServices
	{
		public static IServiceCollection AddIdentityService(this IServiceCollection services, IConfiguration configuration)
		{

			services.AddDbContext<IdentityDataContext>(option =>
			{
				option.UseSqlServer(configuration.GetConnectionString("IdentityDefault"));
			});

			services.AddIdentityCore<ApplicationUser>()
				.AddEntityFrameworkStores<IdentityDataContext>()
				.AddSignInManager<SignInManager<ApplicationUser>>();
			services.AddAuthentication(Options=>
			{
				Options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				Options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
				.AddJwtBearer(options=>
				{
					options.IncludeErrorDetails = true;
					options.TokenValidationParameters = new TokenValidationParameters()
					{
						ValidateIssuer = true,
						ValidIssuer = configuration["Token:Issuer"],

						ValidateIssuerSigningKey = true,
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:Key"])),

						ValidateAudience = true,
						ValidAudience = configuration["Token:Audience"],

						ValidateLifetime = true,


					};
				});
			return services;
		}
	}
}
