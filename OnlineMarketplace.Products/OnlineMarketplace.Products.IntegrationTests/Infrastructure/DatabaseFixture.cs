using Microsoft.EntityFrameworkCore;
using OnlineMarketplace.Products.DAL.Models;
using System.Linq;
using System.Threading.Tasks;
using Testcontainers.MsSql;
using Xunit;

namespace OnlineMarketplace.Products.IntegrationTests.Infrastructure
{
    public class DatabaseFixture : IAsyncLifetime
    {
        private MsSqlContainer _container;

        public string ConnectionString { get; private set; }

        public ProductsTestsContext ProductsContext { get; private set; }

        public DatabaseFixture()
        {
            _container = new MsSqlBuilder()
                .WithExposedPort(1433)
                .Build();
        }

        public async Task DisposeAsync()
        {
            await ProductsContext.DisposeAsync();
            await _container.StopAsync();
        }

        public async Task InitializeAsync()
        {
            await _container.StartAsync();

            ConnectionString = _container.GetConnectionString();

            var productsContextOptions = new DbContextOptionsBuilder<ProductsTestsContext>()
                .UseSqlServer(ConnectionString)
                .Options;

            ProductsContext = new ProductsTestsContext(productsContextOptions);

            await Seed();
        }

        public async Task Reseed()
        {
            await CleanupProducts();
            await SetupProducts();
        }

        private async Task Seed()
        {
            ProductsContext.Database.EnsureCreated();
            await SetupProducts();
        }

        private async Task SetupProducts()
        {
            var products = Enumerable
                .Range(0, 5)
                .Select(i =>
                {
                    return new Product
                    {
                        Name = $"Name{i}",
                        Description = $"Description{i}",
                        Price = 100 * i,
                        SellerId = i,
                        Attributes = Enumerable.Empty<ProductAttributes>().ToList()
                    };
                })
                .ToList();

            await ProductsContext.Products.AddRangeAsync(products);
            await ProductsContext.SaveChangesAsync();
        }

        private async Task CleanupProducts()
        {
            await ProductsContext.Products.ExecuteDeleteAsync();
            await ProductsContext.SaveChangesAsync();
        }
    }
}
