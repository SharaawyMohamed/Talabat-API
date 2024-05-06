using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Runtime.CompilerServices;

namespace E_Commerce.API.Extentions
{
	public static class AddSwaggerExtention
	{
		public static WebApplication UseSwaggerMiddlWair(this WebApplication app)
		{
			app.UseSwagger();
			app.UseSwaggerUI();

			return app;
		}

		public static IServiceCollection AddSwaggerService(this IServiceCollection services)
		{
			// Un Complete Service	
			services.AddEndpointsApiExplorer();
			services.AddSwaggerGen(options =>
			{
				var scheme = new OpenApiSecurityScheme()
				{
					Description = "Standard Authorization header using the bearer scheme , e.g.\"bearer {token}\"",
					In = ParameterLocation.Header,
					Name = "Authorization",
					Type = SecuritySchemeType.ApiKey
				};

				options.AddSecurityDefinition("bearer", scheme);
				options.OperationFilter<SecurityRequirementsOperationFilter>();

			});


			return services;
		}
	}
}
