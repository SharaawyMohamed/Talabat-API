using E_Commerce.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;

namespace E_Commerce.API.Helper
{
	public class CashAttribute : Attribute, IAsyncActionFilter
	{

		//                                  request                        next action
		private readonly int time;
		public CashAttribute(int _time) { time = _time; }
		public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			var cashkey = GenerateKeyFromRequst(context.HttpContext.Request);
			var cashservice = context.HttpContext.RequestServices.GetRequiredService<ICashService>();
			var cashresponse = await cashservice.GetCashResponseAsync(cashkey);
			if(cashresponse is not null)
			{
				var res = new ContentResult()
				{
					ContentType = "application/json",
					StatusCode = 200,
					Content=cashresponse
				};
				context.Result=res;
				return;
			}

			var executedcontext=await next();
			if(executedcontext.Result is OkObjectResult response) {
				await cashservice.SetCashResponseAsync(cashkey,response.Value,TimeSpan.FromSeconds(time));
			}
		}
		private string GenerateKeyFromRequst(HttpRequest request)
		{
			StringBuilder key = new StringBuilder();
			key.Append($"{request.Path}");
			foreach (var item in request.Query.OrderBy(x => x.Key))
			{
				key.Append(item);
			}
			return key.ToString();
		}
	}
}
