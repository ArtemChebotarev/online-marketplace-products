using Moq;
using OnlineMarketplace.Products.BL.Contracts.Commands;
using OnlineMarketplace.Products.BL.Dto;
using OnlineMarketplace.Products.BL.Handlers.Command;
using OnlineMarketplace.Products.DAL;
using OnlineMarketplace.Products.DAL.Models;
using OnlineMarketplace.Products.DAL.Repositories;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace OnlineMarketplace.Products.Tests.BusinessLogic.Handlers
{
    public class DeleteProductCommandHandlerTests
    {
        private readonly Mock<IProductRepository> _repositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly DeleteProductCommandHandler _handler;

        public DeleteProductCommandHandlerTests()
        {
            _repositoryMock = new Mock<IProductRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _handler = new DeleteProductCommandHandler(_unitOfWorkMock.Object, _repositoryMock.Object);
        }

        [Fact]
        public async Task Handler_ShouldReturnFalse_WhenProductNotFound()
        {
            var id = 0;

            _repositoryMock
                .Setup(x => x.GetProductByIdAsync(It.Is<int>(i => i == id)))
                .ReturnsAsync((Product)null!);

            var result = await _handler.Handle(new DeleteProductCommand(id), CancellationToken.None);

            Assert.False(result);
        }

        [Fact]
        // Checking the way how the VerifyAll method works
        public async Task Handler_ShouldCallDeleteProduct_WhenProductExists()
        {
            var id = 1;
            var initialProduct = new Product
            {
                Id = id,
                Name = "Name",
                Description = "Description",
                Price = 100,
                SellerId = 1
            };

            _repositoryMock.Setup(m => m.GetProductByIdAsync(It.Is<int>(i => i == id)))
                .ReturnsAsync(initialProduct);

            _repositoryMock.Setup(m => m.DeleteProduct(initialProduct))
                .Returns(true);

            _unitOfWorkMock.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()));


            await _handler.Handle(new DeleteProductCommand(id), CancellationToken.None);

            _repositoryMock.VerifyAll();
            _unitOfWorkMock.VerifyAll();
        }
    }
}
