using E_Commerce.Core.Models;
using E_Commerce.Core.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Repository
{
    // this class has functions that build all queries in dynamic wayx	
    public static class SpecificationEvaluator<TEntity,TKey> where TEntity : BaseEntity<TKey>
	{
		// Func To build Query
		public static IQueryable<TEntity> BuildQuery(IQueryable<TEntity> inputquery, ISpecifications<TEntity,TKey> Spec)
		{
			// input query= context.Set<T>()
			var Query= inputquery;

			// check filteration
			if(Spec.Criteria is not null)
			{
				// Spec.Criteria= Where(Func<T,bool>)
				Query = Query.Where(Spec.Criteria);
			}
			if(Spec.OrderBy is not null)
			{
				Query= Query.OrderBy(Spec.OrderBy);
			}
			
			if(Spec.OrderByDes is not null)
			{
				Query= Query.OrderByDescending(Spec.OrderByDes);
			}
			if (Spec.IsPaginated)
			{
				Query = Query.Skip(Spec.Skip).Take(Spec.Take);
			}
			

			// To Add Includes to my Query
			Query = Spec.IncludeExpressions.Aggregate(Query, (cur, expression) => cur.Include(expression));
			
			return Query;
		}
	}
}
