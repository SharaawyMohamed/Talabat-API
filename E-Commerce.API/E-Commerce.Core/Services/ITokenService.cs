using E_Commerce.Core.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.JwtToken
{
    public interface ITokenService
	{
		 Task<string> CreateTokenServiceAsync(ApplicationUser user);
	}
}
