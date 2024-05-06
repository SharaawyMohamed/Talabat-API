using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Services
{
	public interface ICashService
	{
		public Task SetCashResponseAsync(string key, object responce, TimeSpan timespan);
		Task<string?> GetCashResponseAsync(string key);

    }
}
