using E_Commerce.Core.Models;
using E_Commerce.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Specification
{
	public class BaseSpecification<TEntity,TKey> : ISpecifications<TEntity,TKey> where TEntity: BaseEntity<TKey>
	{
		public Expression<Func<TEntity, bool>> Criteria { get; set; }

		public List<Expression<Func<TEntity, object>>> IncludeExpressions { get; set; } = new List<Expression<Func<TEntity, object>>>();
		public Expression<Func<TEntity, object>> OrderBy { get; set; }
		public Expression<Func<TEntity, object>> OrderByDes { get; set; }
		public int Take { get; set; }
		public int Skip { get; set; }
		public bool IsPaginated { get; set; }

		// GET ALL
		public BaseSpecification()
		{
		}

		// GET BY ID
		public BaseSpecification(Expression<Func<TEntity, bool>> criteria)
		{
			Criteria = criteria;

		}
		public void SetOrderBy(Expression<Func<TEntity, object>> OrderByExpression)
		{
			OrderBy = OrderByExpression;
		}
		public void SetOrderByDes(Expression<Func<TEntity, object>> OrderByDesExpression)
		{
			OrderByDes = OrderByDesExpression;
		}
		public void ApplayPagination(int PageSize, int PageIndex)
		{
			Take = PageSize;
			Skip=(PageIndex-1)*PageSize;
			IsPaginated = true;
		}
	}
}
