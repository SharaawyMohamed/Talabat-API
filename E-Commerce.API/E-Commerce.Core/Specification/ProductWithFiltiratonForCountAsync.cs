using E_Commerce.Core.Models.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Specification
{
    public class ProductWithFiltiratonForCountAsync: BaseSpecification<Product,int>
	{
		public ProductWithFiltiratonForCountAsync(ProductSpecParameter param)
			: base(p =>
			(!param.BrandId.HasValue || param.BrandId == p.BrandId) &&
			(!param.TypeId.HasValue || param.TypeId == p.TypeId)&&
			(string.IsNullOrEmpty(param.Search)|| p.Name.ToLower().Contains(param.Search))
			)
		{

		}
	}
}
