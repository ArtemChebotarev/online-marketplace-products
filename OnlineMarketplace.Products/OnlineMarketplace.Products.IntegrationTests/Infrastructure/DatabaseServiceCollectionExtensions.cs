using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OnlineMarketplace.Products.DAL;
using System.Data.Common;
using System.Linq;

namespace OnlineMarketplace.Products.IntegrationTests.Infrastructure
{
    public static class DatabaseServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureDatabase(this IServiceCollection services, string connectionString)
        {
            var dbContextDescriptor = services
                    .SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<ProductsContext>));

            if (dbContextDescriptor is not null)
            {
                services.Remove(dbContextDescriptor);
            }

            var dbConnectionDescriptor = services
                .SingleOrDefault(d => d.ServiceType == typeof(DbConnection));

            if (dbConnectionDescriptor is not null)
            {
                services.Remove(dbConnectionDescriptor);
            }

            services.AddDbContext<ProductsContext>(options => options.UseSqlServer(connectionString));

            var serviceProvider = services.BuildServiceProvider();

            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ProductsContext>();

            context.Database.EnsureCreated();

            return services;
        }
    }
}
