using E_Commerce.API.Errors;
using E_Commerce.Repository.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers
{
	public class BuggyController : APIBaseController
	{
		private readonly ECommerceContext context;

		public BuggyController(ECommerceContext _context)
        {
			context = _context;
		}
        [HttpGet("NotFound")]
		public ActionResult GetNotFoundRequest()
		{
			var product = context.Products.Find(-1);
			if(product == null) { return NotFound(new ApiResponce(404)); }
			return Ok(product);
		}
		[HttpGet("ServerError")]
		public ActionResult GetServrError()
		{
			var product = context.Products.Find(-1);
			var ConvertToString = product.ToString();
			// whil throw Null reference Exeption
			return Ok(new ApiResponce(500));
		}
		[HttpGet("BadRequest")]
		public ActionResult GetBadRequst()
		{
			return BadRequest();	
		}
		
		[HttpGet("BadRequest/{Id}")]
		public ActionResult GetBadRequst(int Id)
		{
			return Ok();	
		}
	}
}
