using Moq;
using OnlineMarketplace.Products.BL.Contracts.Queries;
using OnlineMarketplace.Products.BL.Handlers.Query;
using OnlineMarketplace.Products.DAL.Models;
using OnlineMarketplace.Products.DAL.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace OnlineMarketplace.Products.Tests.BusinessLogic.Handlers
{
    public class GetProductsBySellerQueryHandlerTests
    {
        private readonly Mock<IProductRepository> _repositoryMock;
        private readonly GetProductsBySellerQueryHandler _handler;

        public GetProductsBySellerQueryHandlerTests()
        {
            _repositoryMock = new Mock<IProductRepository>();
            _handler = new GetProductsBySellerQueryHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldCallGetProductsBySellerAsync()
        {
            // Arrange
            var sellerId = 1;
            IEnumerable<Product> products = Enumerable
                .Range(1, 5).Select(i => new Product
                    {
                        Id = i,
                        Name = $"Name{i}"
                    })
                .Cast<Product>();

            _repositoryMock
                .Setup(m => m.GetProductsBySellerAsync(It.IsAny<int>()))
                .ReturnsAsync(products);

            // Act
            await _handler.Handle(new GetProductsBySellerQuery(sellerId), CancellationToken.None);

            // Assert
            _repositoryMock.Verify(m => m.GetProductsBySellerAsync(sellerId), Times.Once());
        }
    }
}
