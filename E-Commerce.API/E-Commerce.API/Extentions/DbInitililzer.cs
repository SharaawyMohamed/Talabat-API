using E_Commerce.Core.Models.Identity;
using E_Commerce.Repository.Context;
using E_Commerce.Repository.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.API.Extentions
{
    public static class DbInitililzer
	{
		public static async Task InitilizeDbAsync(WebApplication app)
		{

			// update database Dynamically
			using var Scope = app.Services.CreateScope();// all services which have a life time scoped

			var Services = Scope.ServiceProvider;// services it self


			// Logger factory service : used to show object on console 
			var LoggerFactory = Services.GetRequiredService<ILoggerFactory>();
			try
			{
				var dbcontext = Services.GetRequiredService<ECommerceContext>();//as CLR to creat object of dbcontext exeplicityl
				await dbcontext.Database.MigrateAsync();

				var IdentityDbContext = Services.GetRequiredService<IdentityDataContext>();
				await IdentityDbContext.Database.MigrateAsync();

				var UserManager = Services.GetRequiredService<UserManager<ApplicationUser>>();//ask CLR to creat object of dbcontext exeplicityl

				#region Applay Data Seeding
				await StoreContextSeed.Seed(dbcontext);
				await IdentityDataSeed.SeedUserAsync(UserManager);
				#endregion
			}
			catch (Exception e)
			{
				var Logger = LoggerFactory.CreateLogger<Program>();
				Logger.LogError(e, "An Error Occurred During Update Database ");
			}
			}
	}
}
