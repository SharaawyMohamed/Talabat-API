using E_Commerce.API.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers
{
	[Route("error/{Code}")]
	[ApiController]
	[ApiExplorerSettings(IgnoreApi =true)]
	public class ErrorController : ControllerBase
	{
		public ActionResult Error(int Code)
		{
			return NotFound(new ApiResponce(Code));
		}
	}
}
