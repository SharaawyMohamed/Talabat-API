using E_Commerce.Core.Models.Basket;
using E_Commerce.Core.Repository;
using StackExchange.Redis;
using System.Text.Json;

namespace E_Commerce.Repository.Repository
{
    public class BasketRepository : IBasketRepository
	{
		private readonly IDatabase databaserdis;
		public BasketRepository(IConnectionMultiplexer connection)
		{
			databaserdis = connection.GetDatabase();
		}

		public async Task<bool> DeleteBasketAsync(string id)
		{
			return (await databaserdis.KeyDeleteAsync(id));
		}

		public async Task<CustomerBasket?> GetCustomerBasketAsync(string id)
		{
			var basket = await databaserdis.StringGetAsync(id);
			return basket.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(basket);
		}

		public async Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket)
		{
			var serilizerdbasket = JsonSerializer.Serialize(basket);
			var isupdated = await databaserdis.StringSetAsync(basket.Id, serilizerdbasket, TimeSpan.FromDays(10));
			return isupdated ? await GetCustomerBasketAsync(basket.Id) : null;
		}
	}
}
