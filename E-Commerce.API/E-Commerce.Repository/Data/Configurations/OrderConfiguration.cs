using E_Commerce.Core.Models.Order;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Repository.Data.Configurations
{
	public class OrderConfiguration : IEntityTypeConfiguration<Order>
	{
		public void Configure(EntityTypeBuilder<Order> builder)
		{
			builder.HasMany(oi=>oi.OrderItems)
				.WithOne()
				.OnDelete(DeleteBehavior.Cascade);// whene delete order then delete all order items 

			builder.Property(p => p.SubTotal).HasColumnType("decimal(18,5)");

			builder.OwnsOne(order => order.ShippingAddress, o => o.WithOwner());// to dont create shippingAddress table in database
		}

	}
}
