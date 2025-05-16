using Auth.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auth.Infrastructure.Configurations.DBConfigurations;

public class BasketConfiguration : IEntityTypeConfiguration<Basket>
{
    public void Configure(EntityTypeBuilder<Basket> builder)
    {
        builder.HasKey(b => b.Id);

        builder
            .HasOne(b => b.User)
            .WithMany()
            .HasForeignKey(b => b.UserId);

        builder
            .Property(b => b.TotalPrice);

        builder
            .HasMany(b => b.Products)
            .WithOne(bp => bp.Basket)
            .HasForeignKey(bp => bp.BasketId);
    }
}