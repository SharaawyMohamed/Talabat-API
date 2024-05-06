using E_Commerce.Core.Models.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Repository.Context.Configurations
{
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
	{
		public void Configure(EntityTypeBuilder<Product> builder)
		{
			builder.HasOne(p => p.ProductBrand)
				 .WithMany()
				 .HasForeignKey(k => k.BrandId);

			builder.HasOne(p => p.ProductType)
				.WithMany()
				.HasForeignKey(k => k.TypeId);

			builder.Property(des=>des.Description).IsRequired();
			builder.Property(url=>url.PictureUrl).IsRequired();
		}
	}
}
