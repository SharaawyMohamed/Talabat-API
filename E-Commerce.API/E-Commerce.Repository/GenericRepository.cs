using E_Commerce.Core.Models;
using E_Commerce.Core.Models.Order;
using E_Commerce.Core.Repository;
using E_Commerce.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Repository
{
    public class GenericRepository<TEntity,TKey> : IGenericRepository<TEntity,TKey> where TEntity:BaseEntity<TKey>
	{
		private readonly ECommerceContext context;

		public GenericRepository(ECommerceContext _context)
        {
			context = _context;
		}

		#region WithoutSpecification
		public async Task<IReadOnlyList<TEntity>> GetAllAsync()
		{
			return await context.Set<TEntity>().ToListAsync();
		}

		public async Task<TEntity> GetByIdAsync(int Id)
		{
			return (await context.Set<TEntity>().FindAsync(Id))!;
		}

		#endregion


		#region With Specification
		public async Task<IReadOnlyList<TEntity>> GetAllWithSpecAsync(ISpecifications<TEntity,TKey> Spec)
		{//                                                   Entry Point      query
			return await ApplaySpec(Spec).ToListAsync();
		}
		// exception is here
		public async Task<TEntity?> GetByIdWithSpecAsync(ISpecifications<TEntity,TKey> Spec)
		{
			 var x= (await ApplaySpec(Spec).FirstOrDefaultAsync())!;
			return x;
		}

		public async Task<int> CountNumperWithSpec(ISpecifications<TEntity,TKey> Spec)
		=> await ApplaySpec(Spec).CountAsync();

		#endregion


		private IQueryable<TEntity> ApplaySpec(ISpecifications<TEntity,TKey> Spec)
		{
			return SpecificationEvaluator<TEntity,TKey>.BuildQuery(context.Set<TEntity>(), Spec);
		}

		public async Task AddAsync(TEntity item)
		{
			await context.Set<TEntity>().AddAsync(item);
		}

		public void Update(TEntity item)
		{
			context.Set<TEntity>().Update(item);
		}

		public void Delete(TEntity item)
		{
			context.Set<TEntity>().Remove(item);
		}
	}
}
