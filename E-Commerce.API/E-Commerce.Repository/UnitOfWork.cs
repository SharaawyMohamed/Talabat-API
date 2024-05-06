using E_Commerce.Core.Models;
using E_Commerce.Core.Repository;
using E_Commerce.Repository.Context;
using System.Collections;
using System.Reflection.Metadata.Ecma335;


namespace E_Commerce.Repository
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly ECommerceContext context;
		private readonly Hashtable repositorys;
		public UnitOfWork(ECommerceContext _context)
		{
			context = _context;
			repositorys = new Hashtable();
		}
		public async Task<int> CompleteAsync()
		{
			return await context.SaveChangesAsync();
		}

		public async ValueTask DisposeAsync()
		{
			await context.DisposeAsync();
		}

		public IGenericRepository<TEntity,TKey> Repository<TEntity,TKey>() where TEntity : BaseEntity<TKey>
		{
			var key = typeof(TEntity).Name;
			if (!repositorys.ContainsKey(key))
			{
				var repo = new GenericRepository<TEntity,TKey>(context);
				repositorys.Add(key, repo);
			}
			return (repositorys[key] as GenericRepository<TEntity,TKey>)!	;
		}
	}
}
