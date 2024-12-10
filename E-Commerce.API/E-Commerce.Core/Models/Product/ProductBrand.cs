using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Models.Product
{
    public class ProductBrand : BaseEntity<int>
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
    }
}
