using E_Commerce.Core.Models.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
namespace E_Commerce.Core.Specification
{

    public class ProductWithBrandTypeSpec : BaseSpecification<Product,int>
	{
		// Constructor :is used to Get All Products
		public ProductWithBrandTypeSpec(ProductSpecParameter param)
			: base(p =>
			(!param.BrandId.HasValue || param.BrandId == p.BrandId) &&
			(!param.TypeId.HasValue || param.TypeId == p.TypeId) &&
			(string.IsNullOrEmpty(param.Search) || p.Name.ToLower().Contains(param.Search))
			)
		{
			ApplayPagination(param.PageSize, param.PageIndex);

			IncludeExpressions.Add(P => P.ProductType);
			IncludeExpressions.Add(P => P.ProductBrand);
			switch (param.Sort)
			{
				case SortParameter.NameAsc:
					SetOrderBy(name => name.Name);
					break;
				case SortParameter.NameDesc:
					SetOrderByDes(name => name.Name);
					break;
				case SortParameter.PriceAsc:
					SetOrderBy(price => price.Price);
					break;
				case SortParameter.PriceDesc:
					SetOrderByDes(price => price.Price);
					break;
				default:
					SetOrderBy(name => name.Name);
					break;
			}

		}

		// Constructor Is uset to Get Product By Id
		public ProductWithBrandTypeSpec(int Id) : base(P => P.Id == Id)
		{
			IncludeExpressions.Add(P => P.ProductType);
			IncludeExpressions.Add(P => P.ProductBrand);

		}
	}
}
