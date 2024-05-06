using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Specification
{
	public class ProductSpecParameter
	{
		public SortParameter? Sort { get; set; }
		public int? TypeId { get; set; }
		public int? BrandId { get; set; }

		private int pagesize=10;
		public int PageSize
		{	
			get { return pagesize; }
			set { pagesize = value > 10 ? 10 : value; }
		}
		public int PageIndex { get; set; } = 1;

		private string? search;
		public string? Search
		{
			get { return search; }
			set { search = value.ToLower(); }
		}


	}
}
