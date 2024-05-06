using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Models.Product
{
    public class Product : BaseEntity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        [Column(TypeName = "decimal(8,4)")]
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }

        [ForeignKey("ProductType")]
        public int? TypeId { get; set; }
        public ProductType ProductType { get; set; }


        [ForeignKey("ProductBrand")]
        public int? BrandId { get; set; }
        public ProductBrand ProductBrand { get; set; }

    }
}
