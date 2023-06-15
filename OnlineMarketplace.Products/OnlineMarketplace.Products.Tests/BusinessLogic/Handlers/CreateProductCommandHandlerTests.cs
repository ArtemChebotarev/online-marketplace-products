using Moq;
using OnlineMarketplace.Products.BL.Contracts.Commands;
using OnlineMarketplace.Products.BL.Dto;
using OnlineMarketplace.Products.BL.Handlers.Command;
using OnlineMarketplace.Products.DAL;
using OnlineMarketplace.Products.DAL.Models;
using OnlineMarketplace.Products.DAL.Repositories;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace OnlineMarketplace.Products.Tests.BusinessLogic.Handlers
{
    public class CreateProductCommandHandlerTests
    {
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly CreateProductCommandHandler _handler;

        public CreateProductCommandHandlerTests()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _handler = new CreateProductCommandHandler(_productRepositoryMock.Object, _unitOfWorkMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenSellerIdIsInvalid()
        {
            var sellerId = 0;

            await Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(new CreateProductCommand(
                "Name",
                "Description",
                100,
                sellerId,
                Enumerable.Empty<ProductAttributesDto>()), CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldCallAddProduct()
        {
            var command = new CreateProductCommand(
                "Name",
                "Description",
                100,
                1,
                Enumerable.Empty<ProductAttributesDto>());

            await _handler.Handle(command, CancellationToken.None);

            _productRepositoryMock.Verify(
                m => m.AddProduct(It.Is<Product>(
                    p => p.Name == command.Name && 
                         p.Description == command.Description)),
                Times.Once);
            _unitOfWorkMock.Verify(m => m.SaveChangesAsync(CancellationToken.None), Times.Once);
        }
    }
}
