using Auth.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auth.Infrastructure.Configurations.DBConfigurations;

public class BasketProductConfiguration : IEntityTypeConfiguration<BasketProduct>
{
    public void Configure(EntityTypeBuilder<BasketProduct> builder)
    {
        builder.HasKey(bp => bp.Id);

        builder
            .HasOne(bp => bp.Basket)
            .WithMany(b => b.Products)
            .HasForeignKey(bp => bp.BasketId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(bp => bp.Product)
            .WithMany()
            .HasForeignKey(bp => bp.ProductId);

        builder
            .Property(bp => bp.Quantity)
            .IsRequired();
    }
}