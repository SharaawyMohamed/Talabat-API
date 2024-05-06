using E_Commerce.Core.Models.Order;
using E_Commerce.Core.Models.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace E_Commerce.Repository.Context
{
    public class StoreContextSeed
    {
        public static async Task Seed(ECommerceContext context)
        {
            if (!context.ProductBrands.Any())
            {
                var BrandData = File.ReadAllText("../E-Commerce.Repository/Data/DataSeed/brands.json");
                var Brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandData);
                if (Brands?.Count > 0)
                {
                    foreach (var i in Brands)
                    {
                        await context.Set<ProductBrand>().AddAsync(i);
                    }
                }
            }
            
            if (!context.DeliveryMethods.Any())
            {
                var deleviryData = File.ReadAllText("../E-Commerce.Repository/Data/DataSeed/delivery.json");
                var delivery = JsonSerializer.Deserialize<List<DeliveryMethod>>(deleviryData);
                if (delivery?.Count > 0)
                {
                    foreach (var i in delivery)
					{
                        await context.Set<DeliveryMethod>().AddAsync(i);
                    }
                }
            }

            // Seeding Product Type
            if (!context.ProductTypes.Any())
            {
                var TypeData = File.ReadAllText("../E-Commerce.Repository/Data/DataSeed/types.json");
                var Types = JsonSerializer.Deserialize<List<ProductType>>(TypeData);
                if (Types?.Count > 0)
                {
                    foreach (var i in Types)
                    {
                        await context.Set<ProductType>().AddAsync(i);
                    }
                }
            }

            // Seeding Product
            if (!context.Products.Any())
            {
                var ProductData = File.ReadAllText("../E-Commerce.Repository/Data/DataSeed/products.json");
                var Products = JsonSerializer.Deserialize<List<Product>>(ProductData);
                if (Products?.Count > 0)
                {
                    foreach (var i in Products)
                    {
                        await context.Set<Product>().AddAsync(i);
                    }
                }
            }

            await context.SaveChangesAsync();
        }
    }
}
