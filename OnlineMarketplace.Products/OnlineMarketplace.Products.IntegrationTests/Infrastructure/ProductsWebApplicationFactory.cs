using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using Xunit;

namespace OnlineMarketplace.Products.IntegrationTests.Infrastructure
{
    public class ProductsWebApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
    {
        private const string environmentName = "Testing";

        public DatabaseFixture DatabaseFixture { get; } = new DatabaseFixture();

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment(environmentName);

            builder.ConfigureServices(services =>
            {
                services.ConfigureDatabase(DatabaseFixture.ConnectionString);
            });
        }

        public async Task InitializeAsync()
        {
            await DatabaseFixture.InitializeAsync();
        }

        async Task IAsyncLifetime.DisposeAsync()
        {
            await DatabaseFixture.DisposeAsync();
            await base.DisposeAsync();
        }
    }
}
