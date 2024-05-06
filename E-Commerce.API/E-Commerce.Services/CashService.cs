using E_Commerce.Core.Services;
using StackExchange.Redis;
using System.Text.Json;


namespace E_Commerce.Services
{
	public class CashService : ICashService
	{
		private readonly IDatabase database;
		public CashService(IConnectionMultiplexer connection)
		{
			database = connection.GetDatabase();
		}
		public async Task<string?> GetCashResponseAsync(string key)
		{
			var response = await database.StringGetAsync(key);
			return (response.IsNullOrEmpty) ? null: response.ToString();
		}

		public async Task SetCashResponseAsync(string key, object response, TimeSpan timespan)
		{
			var serializedresponse = JsonSerializer.Serialize(response
				, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
			await database.StringSetAsync(key, serializedresponse, timespan);
		}
	}
}
