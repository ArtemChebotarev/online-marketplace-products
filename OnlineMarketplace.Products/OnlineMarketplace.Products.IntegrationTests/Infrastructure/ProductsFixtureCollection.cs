using Xunit;

namespace OnlineMarketplace.Products.IntegrationTests.Infrastructure
{
    [CollectionDefinition(nameof(ProductsFixtureCollection))]
    public class ProductsFixtureCollection : ICollectionFixture<ProductsWebApplicationFactory>
    {
    }
}
