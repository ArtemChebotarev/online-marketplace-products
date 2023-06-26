using Microsoft.EntityFrameworkCore;
using OnlineMarketplace.Products.DAL.Models;

namespace OnlineMarketplace.Products.IntegrationTests.Infrastructure
{
    public class ProductsTestsContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public ProductsTestsContext(DbContextOptions<ProductsTestsContext> options) : base(options)
        {
            ChangeTracker.LazyLoadingEnabled = false;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductEntityTestsConfiguration());
        }
    }
}
