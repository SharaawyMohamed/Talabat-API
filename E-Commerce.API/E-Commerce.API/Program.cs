using E_Commerce.API.Errors;
using E_Commerce.API.Extentions;
using E_Commerce.API.MiddelWares;

namespace E_Commerce.API
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			#region Services

			builder.Services.AddApplicationServices(builder.Configuration);
			builder.Services.AddIdentityService(builder.Configuration);
			builder.Services.AddControllers();
			builder.Services.AddSwaggerService();

			#endregion

			var app = builder.Build();


			#region Update Database Migrations
			await DbInitililzer.InitilizeDbAsync(app);
			#endregion

			if (app.Environment.IsDevelopment())
			{
				app.UseMiddleware<ExceptionMeddleWare>();
				app.UseSwaggerMiddlWair();
			}

			app.UseStaticFiles();
			app.UseStatusCodePagesWithReExecute("/error/{0}");
			app.UseHttpsRedirection();
			app.UseAuthentication();
			app.UseAuthorization();

			app.MapControllers();
			app.UseCors("AngularApp");
			app.Run();
		}
	}
}
