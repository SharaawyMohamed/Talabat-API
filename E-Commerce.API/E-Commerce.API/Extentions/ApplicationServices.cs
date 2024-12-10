using E_Commerce.Core.Repository;
using E_Commerce.Repository.Context;
using E_Commerce.Repository;
using E_Commerce.Services.UserServices;
using E_Commerce.Core.JwtToken;
using E_Commerce.Repository.Repository;
using E_Commerce.Services;
using E_Commerce.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using System.Reflection;
using E_Commerce.API.Errors;

namespace E_Commerce.API.Extentions
{
	public static class ApplicationServices
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
		{
			// Service Registrations
			services.AddScoped<IProductServices, ProductServices>();
			services.AddScoped<IOrderService, OrderService>();
			services.AddScoped<IPaymentService, PaymentService>();
			services.AddScoped<ICashService, CashService>();
			services.AddScoped<ITokenService, TokenService>();
			services.AddScoped<IBasketService, BasketService>();
			services.AddScoped<IBasketRepository, BasketRepository>();
			services.AddScoped<IUserService, UserService>();
			services.AddScoped<IUnitOfWork, UnitOfWork>();
			services.AddScoped<IMedia, Media>();
			services.AddScoped<CategoryService>();
			services.AddScoped<BrandService>(); 


			// Redis Configuration
			services.AddSingleton<IConnectionMultiplexer>(option =>
			{
				var config = ConfigurationOptions.Parse(configuration.GetConnectionString("RedisConnection"));
				return ConnectionMultiplexer.Connect(config);
			});

			// Database Context Configuration
			services.AddDbContext<ECommerceContext>(option =>
				option.UseSqlServer(configuration.GetConnectionString("Default"))
			);

			// CORS Configurations
			services.AddCors(options =>
			{
				options.AddPolicy("AngularApp", policy =>
					policy.WithOrigins("http://localhost:4200")
						  .AllowAnyMethod()
						  .AllowAnyHeader()
						  .AllowCredentials()
				);
			});

			// API Behavior Configuration
			services.Configure<ApiBehaviorOptions>(options =>
			{
				options.InvalidModelStateResponseFactory = actionContext =>
				{
					var errors = actionContext.ModelState
						.Where(p => p.Value!.Errors.Count > 0)
						.SelectMany(p => p.Value!.Errors)
						.Select(e => e.ErrorMessage)
						.ToList();

					var validationErrorResponse = new ApiValidationResponceError
					{
						Errors = errors
					};
					return new BadRequestObjectResult(validationErrorResponse);
				};
			});

			// AutoMapper Configuration
			services.AddAutoMapper(Assembly.GetExecutingAssembly());

			return services;
		}
	}
}
