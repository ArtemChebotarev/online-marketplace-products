using FluentAssertions;
using Moq;
using OnlineMarketplace.Products.BL.Contracts.Commands;
using OnlineMarketplace.Products.BL.Dto;
using OnlineMarketplace.Products.BL.Handlers.Command;
using OnlineMarketplace.Products.DAL;
using OnlineMarketplace.Products.DAL.Models;
using OnlineMarketplace.Products.DAL.Repositories;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace OnlineMarketplace.Products.Tests.BusinessLogic.Handlers
{
    public class UpdateProductCommandHandlerTests
    {
        private readonly Mock<IProductRepository> _repositoryMock;
        private readonly UpdateProductCommandHandler _handler;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public UpdateProductCommandHandlerTests()
        {
            _repositoryMock = new Mock<IProductRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _handler = new UpdateProductCommandHandler(_repositoryMock.Object, _unitOfWorkMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldThrowValidationException_WhenProductNotFound()
        {
            var id = 0;
            _repositoryMock.Setup(m => m.GetProductByIdAsync(It.Is<int>(i => i == id)))
                .ReturnsAsync((Product)null!);

            var action = () => _handler.Handle(
                    new UpdateProductCommand(
                        id,
                        "Name",
                        "Description",
                        100,
                        Enumerable.Empty<ProductAttributesDto>()),
                    CancellationToken.None);

            await action.Should().ThrowAsync<ValidationException>();
        }

        [Fact]
        public async Task Handle_ShouldCallUpdateProduct_WhenProductExists()
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
            var updateCommand = new UpdateProductCommand(
                id, 
                "Name", 
                "Description2", 
                150, 
                Enumerable.Empty<ProductAttributesDto>());

            _repositoryMock.Setup(m => m.GetProductByIdAsync(It.Is<int>(i => i == id)))
                .ReturnsAsync(initialProduct);

            await _handler.Handle(updateCommand, CancellationToken.None);

            _repositoryMock.Verify(m => m.UpdateProduct(initialProduct), Times.Once());
            _unitOfWorkMock.Verify(m => m.SaveChangesAsync(CancellationToken.None), Times.Once());
        }
    }
}
