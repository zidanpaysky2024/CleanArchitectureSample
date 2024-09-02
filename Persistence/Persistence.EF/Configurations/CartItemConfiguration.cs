﻿using Architecture.Domain.Cart.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Architecture.Persistence.EF.Configurations
{
    public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder.HasKey(t => t.Id);
            builder.HasOne(t => t.Cart)
                .WithMany(p => p.CartItems)
                .HasForeignKey(d => d.CartId);
            builder.HasOne(t => t.ProductItem);

        }
    }
}
