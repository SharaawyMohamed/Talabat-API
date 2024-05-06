using E_Commerce.API.Errors;
using E_Commerce.API.Extentions;
using E_Commerce.API.MiddelWares;
using E_Commerce.Core.Models;
using E_Commerce.Core.Repository;
using E_Commerce.Repository;
using E_Commerce.Repository.Context;
using E_Commerce.Repository.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System.Reflection;

namespace E_Commerce.API
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			#region Servieces

			// creat dependency injection 

			builder.Services.AddApplicationServices(builder.Configuration);
			builder.Services.AddIdentityService(builder.Configuration);

			// Add services to the container.
			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle


			  builder.Services.AddSwaggerService();
			//builder.Services.AddEndpointsApiExplorer();
			//builder.Services.AddSwaggerGen();


			#endregion

			var app = builder.Build();

			#region Update Database Migrations

			await DbInitililzer.InitilizeDbAsync(app);

			#endregion			#region PipeLines
			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseMiddleware<ExceptionMeddleWare>();
				app.UseSwaggerMiddlWair();// I'm Add this service 
			}
			app.UseStaticFiles();

			app.UseStatusCodePagesWithReExecute("/error/{0}");

			app.UseHttpsRedirection();

			app.UseAuthentication();
			app.UseAuthorization();


			app.MapControllers();

			app.Run();

		}
	}
}
