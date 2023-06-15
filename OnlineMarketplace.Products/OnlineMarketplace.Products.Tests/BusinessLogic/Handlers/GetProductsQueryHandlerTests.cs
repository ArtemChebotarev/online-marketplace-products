using Moq;
using OnlineMarketplace.Products.BL.Contracts.Queries;
using OnlineMarketplace.Products.BL.Handlers.Query;
using OnlineMarketplace.Products.DAL.Models;
using OnlineMarketplace.Products.DAL.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using Xunit;

namespace OnlineMarketplace.Products.Tests.BusinessLogic.Handlers
{
    public class GetProductsQueryHandlerTests
    {
        private readonly Mock<IProductRepository> _repositoryMock;
        private readonly GetProductsQueryHandler _handler;

        public GetProductsQueryHandlerTests()
        {
            _repositoryMock = new Mock<IProductRepository>();
            _handler = new GetProductsQueryHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldCallGetProductsBySellerAsync()
        {
            // Arrange
            IEnumerable<Product> products = Enumerable
                .Range(1, 5).Select(i => new Product
                {
                    Id = i,
                    Name = $"Name{i}"
                })
                .Cast<Product>();

            _repositoryMock
                .Setup(m => m.GetAllProductsAsync())
                .ReturnsAsync(products);

            // Act
            await _handler.Handle(new GetProductsQuery(), CancellationToken.None);

            // Assert
            _repositoryMock.Verify(m => m.GetAllProductsAsync(), Times.Once());
        }
    }
}
