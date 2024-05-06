using E_Commerce.Core.Repository;
using E_Commerce.Repository.Context;
using E_Commerce.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using E_Commerce.API.Errors;
using E_Commerce.Services.UserServices;
using E_Commerce.Core.JwtToken;
using E_Commerce.Repository.Repository;
using E_Commerce.Services;
using E_Commerce.Core.Services;
using TokenService = E_Commerce.Services.TokenService;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace E_Commerce.API.Extentions
{
    public static class ApplicationServices
	{                // this : it's mean that this method would be Extendtion Method 
		public static IServiceCollection AddApplicationServices(this IServiceCollection Services,IConfiguration configuration)
		{
			Services.AddScoped<IProductServices, ProductServices>();
			Services.AddScoped<IOrderService, OrderService>();
			Services.AddScoped<IPaymentService, PaymentService>();
			Services.AddScoped<ICashService, CashService>();
			Services.AddScoped<ITokenService, TokenService>();
			Services.AddScoped<IBasketService, BasketService>();
			Services.AddScoped<IBasketRepository, BasketRepository>();
			Services.AddScoped<IUserService, UserService>();
			Services.AddScoped<IUnitOfWork,UnitOfWork>();
			Services.AddAutoMapper(Assembly.GetExecutingAssembly());

			Services.AddSingleton<IConnectionMultiplexer>(option =>
			{
				var connection =ConfigurationOptions.Parse(configuration.GetConnectionString("RedisDefault"));
				return ConnectionMultiplexer.Connect(connection);
			});
			Services.AddDbContext<ECommerceContext>(option =>
			{
				option.UseSqlServer(configuration.GetConnectionString("Default"));
			});

			
			Services.Configure<ApiBehaviorOptions>(options =>
			{
				options.InvalidModelStateResponseFactory = (actioncontext) => {
					var errors = actioncontext.ModelState.Where(p => p.Value.Errors.Count() > 0)
					.SelectMany(E => E.Value.Errors)
					.Select(E=>E.ErrorMessage).ToList();
					var validerrorresponse = new ApiValidationResponceError()
					{
						Errors = errors
					};
					return new BadRequestObjectResult(validerrorresponse);
				};
			});
			//builder.Services.AddScoped<IGenericRepository<Product>, GenericRepository<Product>>();
			//builder.Services.AddScoped<IGenericRepository<ProductBrand>, GenericRepository<ProductBrand>>();
			//builder.Services.AddScoped<IGenericRepository<ProductType>, GenericRepository<ProductType>>();



			// Services for AutoMapper
			Services.AddAutoMapper(Assembly.GetExecutingAssembly());

			// Error Validation Services
			Services.Configure<ApiBehaviorOptions>(option =>
			{
				option.InvalidModelStateResponseFactory = (ActionContext) =>
				{
					var Errors = ActionContext.ModelState.Where(p => p.Value.Errors.Count > 0)
					.SelectMany(p => p.Value.Errors)
					.Select(E => E.ErrorMessage)
					.ToArray();

					var ValidationErrorResponce = new ApiValidationResponceError()
					{
						Errors = Errors
					};
					return new BadRequestObjectResult(ValidationErrorResponce);

				};
			});


			return Services;
		}
	}
}
