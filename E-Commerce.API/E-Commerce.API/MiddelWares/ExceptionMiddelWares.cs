using E_Commerce.API.Errors;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Net;
using System.Text.Json;

namespace E_Commerce.API.MiddelWares
{
	public class ExceptionMeddleWare
	{
		private readonly RequestDelegate next;
		private readonly ILogger<ExceptionMeddleWare> logger;
		private readonly IHostEnvironment environment;

		public ExceptionMeddleWare(RequestDelegate _next, ILogger<ExceptionMeddleWare> _logger, IHostEnvironment _environment)
		{
			next = _next;
			logger = _logger;
			environment = _environment;
		}
		public async Task InvokeAsync(HttpContext context)
		{
			try
			{
				await next.Invoke(context);

			}
			catch (Exception e)
			{
				logger.LogError(e, e.Message);
				context.Response.ContentType = "application/json";
				context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

				var Response = environment.IsDevelopment() ?
					new ApiExceptionResponse(500, e.Message, e.StackTrace!.ToString()) :
					new ApiExceptionResponse(500);

				var Options = new  JsonSerializerOptions(){
					PropertyNamingPolicy = JsonNamingPolicy.CamelCase
				};
				var JsonResponse=JsonSerializer.Serialize(Response,Options);
				context.Response.WriteAsync(JsonResponse);
			}
		}
	}
}
