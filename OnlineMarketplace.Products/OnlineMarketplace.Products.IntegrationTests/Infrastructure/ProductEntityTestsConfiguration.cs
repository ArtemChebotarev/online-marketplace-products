using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineMarketplace.Products.DAL.Models;

namespace OnlineMarketplace.Products.IntegrationTests.Infrastructure
{
    public class ProductEntityTestsConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder
                .HasKey(p => p.Id);

            builder
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();

            builder
                .Property(p => p.Name)
                .IsRequired();

            builder
                .Property(p => p.SellerId)
                .IsRequired();

            builder
                .Property(p => p.Price)
                .IsRequired();

            builder.OwnsMany(p => p.Attributes, a =>
            {
                a.WithOwner().HasForeignKey(p => p.ProductId);
                a.HasKey(p => p.Id);
                a.Property(p => p.Key).IsRequired();
                a.Property(p => p.Value).IsRequired();
            });
        }
    }
}
