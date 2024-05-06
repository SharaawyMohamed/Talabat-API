namespace E_Commerce.API.Errors
{
	public class ApiResponce
	{
		public int StatusCode { get; set; }
		public string? ErrorMessage { get; set; }
		public ApiResponce(int statusCode, string? errorMessage=null)
		{
			StatusCode = statusCode;
			ErrorMessage = errorMessage ?? GetDefaultMessageForStatusCode(StatusCode);
		}
	private string GetDefaultMessageForStatusCode(int statusCode)
	=> StatusCode switch
	{
		100 => "Continue",
		101 => "Switching Protocols",
		200 => "OK",
		201 => "Created",
		202 => "Accepted",
		204 => "No Content",
		301 => "Moved Permanently",
		302 => "Found",
		304 => "Not Modified",
		400 => "Bad Request",
		401 => "Unauthorized",
		403 => "Forbidden",
		404 => "Not Found",
		405 => "Method Not Allowed",
		500 => "Internal Server Error",
		501 => "Not Implemented",
		502 => "Bad Gateway",
		503 => "Service Unavailable",
		504 => "Gateway Timeout",
		_ => "Unknown Status Code"
	};
	}
}
