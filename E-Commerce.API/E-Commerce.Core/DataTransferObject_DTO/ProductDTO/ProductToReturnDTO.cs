using E_Commerce.Core.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce.API.DataTransferObject_DTO.ProductDTO
{
    public class ProductToReturnDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }
        public int TypeId { get; set; }
        public string ProductType { get; set; }
        public int BrandId { get; set; }
        public string ProductBrand { get; set; }
    }
}
