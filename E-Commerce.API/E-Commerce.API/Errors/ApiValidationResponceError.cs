using System.Security.Cryptography.X509Certificates;

namespace E_Commerce.API.Errors
{
	public class ApiValidationResponceError:ApiResponce
	{
        public IEnumerable<string> Errors { get; set; }
        public ApiValidationResponceError():base(400)
        {
            Errors = new List<string>();
        }
    }
}
