using Microsoft.EntityFrameworkCore;
using OnlineMarketplace.Products.DAL.Configurations;
using OnlineMarketplace.Products.DAL.Models;

namespace OnlineMarketplace.Products.DAL
{
    public class ProductsContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public ProductsContext(DbContextOptions<ProductsContext> options) : base(options)
        {
            ChangeTracker.LazyLoadingEnabled = false;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductEntityConfiguration());
        }
    }
}
