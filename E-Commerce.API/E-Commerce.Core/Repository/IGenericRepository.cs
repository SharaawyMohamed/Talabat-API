using E_Commerce.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Repository
{
    public interface IGenericRepository<TEntity,TKey> where TEntity :BaseEntity<TKey>
	{
		#region Without Specifications
		public Task<IReadOnlyList<TEntity>> GetAllAsync();
		public Task<TEntity> GetByIdAsync(int Id);
		#endregion

		#region With Specifications
		public Task<IReadOnlyList<TEntity>> GetAllWithSpecAsync(ISpecifications<TEntity,TKey> Spec);
		public Task<TEntity> GetByIdWithSpecAsync(ISpecifications<TEntity,TKey> Spec);
		#endregion
		public Task<int> CountNumperWithSpec(ISpecifications<TEntity,TKey> Spec);
		public Task AddAsync(TEntity item);
		public void Update(TEntity item);
		public void Delete(TEntity item);
	}
}
