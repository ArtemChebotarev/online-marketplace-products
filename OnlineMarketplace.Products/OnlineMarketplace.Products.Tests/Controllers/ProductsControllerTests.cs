using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OnlineMarketplace.Products.Api.Contracts;
using OnlineMarketplace.Products.Api.Controllers;
using OnlineMarketplace.Products.BL.Contracts.Commands;
using OnlineMarketplace.Products.BL.Contracts.Queries;
using OnlineMarketplace.Products.BL.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace OnlineMarketplace.Products.Tests.Controllers
{
    public class ProductsControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly ProductsController _controller;

        public ProductsControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new ProductsController(_mediatorMock.Object);
        }

        [Fact]
        public async Task GetAll_ShouldSendWithGetProductsBySellerQuery_WhenSellerIdSpecified()
        {
            var sellerId = 1;

            await _controller.GetAll(sellerId);

            _mediatorMock.Verify(
                m => m.Send(
                    It.Is<GetProductsBySellerQuery>(it => it.SellerId == sellerId),
                    It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [Fact]
        public async Task GetAll_ShouldReturnProductDtos_WhenSellerIdSpecified()
        {
            var sellerId = 1;
            var procuts = Enumerable.Range(1, 3).Select(i => GetProductDto(i)).Cast<ProductDto>();

            _mediatorMock
                .Setup(
                    m => m.Send(
                        It.Is<GetProductsBySellerQuery>(it => it.SellerId == sellerId),
                        It.IsAny<CancellationToken>()))
                .ReturnsAsync(procuts);

            var result = await _controller.GetAll(sellerId);

            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var model = okResult.Value.Should().BeAssignableTo<IEnumerable<ProductDto>>().Subject;
            model.Should().HaveCount(3);
        }

        private ProductDto GetProductDto(int i)
        {
            return new ProductDto(
                i,
                $"Name{i}",
                $"Description{i}",
                100,
                Enumerable.Empty<ProductAttributesDto>(),
                1);
        }

        [Fact]
        public async Task GetAll_ShouldSendWithGetProductsQuery_WhenSellerIdIsNull()
        {
            int? sellerId = null;

            await _controller.GetAll(sellerId);

            _mediatorMock.Verify(
                m => m.Send(
                    It.IsAny<GetProductsQuery>(),
                    It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [Fact]
        public async Task GetAll_ShouldReturnProductDtos_WhenSellerIdIsNull()
        {
            int? sellerId = null;
            var procuts = Enumerable.Range(1, 3).Select(i => GetProductDto(i)).Cast<ProductDto>();

            _mediatorMock
                .Setup(
                    m => m.Send(
                        It.IsAny<GetProductsQuery>(),
                        It.IsAny<CancellationToken>()))
                .ReturnsAsync(procuts);

            var result = await _controller.GetAll(sellerId);

            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var model = okResult.Value.Should().BeAssignableTo<IEnumerable<ProductDto>>().Subject;
            model.Should().HaveCount(3);
        }

        [Fact]
        public async Task Get_ShouldReturnNotFound_WhenReturnedNull()
        {
            var id = 0;

            _mediatorMock
                .Setup(m => m.Send(It.Is<GetProductByIdQuery>(it => it.Id == id), It.IsAny<CancellationToken>()))
                .ReturnsAsync((ProductDto)null!);

            var result = await _controller.Get(id);

            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task Get_ShouldReturnProduct_WhenProductFound()
        {
            var id = 1;
            var productDto = new ProductDto(id, "Name", "Description", 100, Enumerable.Empty<ProductAttributesDto>(), 1);

            _mediatorMock
                .Setup(m => m.Send(It.Is<GetProductByIdQuery>(it => it.Id == id), It.IsAny<CancellationToken>()))
                .ReturnsAsync(productDto);

            var result = await _controller.Get(id);

            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var model = okResult.Value.Should().BeAssignableTo<ProductDto>().Subject;
            model.Should().BeEquivalentTo(productDto);
        }

        [Fact]
        public async Task Put_ShouldSendWithCorretRequest()
        {
            var updateRequestBody = new UpdateProductRequestBody("Name", "Description", 100, Enumerable.Empty<ProductAttributesDto>());
            var request = new UpdateProductRequest(1, updateRequestBody);

            await _controller.Put(request);

            _mediatorMock.Verify(
                m => m.Send(It.Is<UpdateProductCommand>(
                    it => it.Name == request.Product.Name &&
                    it.Description == request.Product.Description &&
                    it.Id == request.Id &&
                    it.Price == request.Product.Price &&
                    it.Attributes == request.Product.Attributes), It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [Fact]
        public async Task Put_ShouldReturnCreatedProduct()
        {
            var updateRequestBody = new UpdateProductRequestBody("Name", "Description", 100, Enumerable.Empty<ProductAttributesDto>());
            var request = new UpdateProductRequest(1, updateRequestBody);
            var productDto = new ProductDto(1, "Name", "Description", 100, Enumerable.Empty<ProductAttributesDto>(), 1);

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<UpdateProductCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(productDto);

            var result = await _controller.Put(request);

            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var model = okResult.Value.Should().BeAssignableTo<ProductDto>().Subject;
            model.Should().BeEquivalentTo(productDto);
        }

        [Fact]
        public async Task Post_ShouldSendWithCorretRequest()
        {
            var request = new CreateProductRequest("Name", "Description", 100, 1, Enumerable.Empty<ProductAttributesDto>());

            await _controller.Post(request);

            _mediatorMock.Verify(
                m => m.Send(It.Is<CreateProductCommand>(
                    it => it.Name == request.Name &&
                    it.Description == request.Description &&
                    it.SellerId == request.SellerId &&
                    it.Price == request.Price &&
                    it.Attributes == request.Attributes), It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [Fact]
        public async Task Post_ShouldReturnCreatedProduct()
        {
            var request = new CreateProductRequest("Name", "Description", 100, 1, Enumerable.Empty<ProductAttributesDto>());
            var productDto = new ProductDto(1, "Name", "Description", 100, Enumerable.Empty<ProductAttributesDto>(), 1);

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<CreateProductCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(productDto);

            var result = await _controller.Post(request);

            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var model = okResult.Value.Should().BeAssignableTo<ProductDto>().Subject;
            model.Should().BeEquivalentTo(productDto);
        }

        [Fact]
        public async Task Delete_ShouldReturnNotFound_WhenFalseReturned()
        {
            var id = 0;

            _mediatorMock
                .Setup(m => m.Send(It.Is<DeleteProductCommand>(c => c.Id == id), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            var result = await _controller.Delete(id);

            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task Delete_ShouldReturnOk_WhenTrueReturned()
        {
            var id = 1;

            _mediatorMock
                .Setup(m => m.Send(It.Is<DeleteProductCommand>(c => c.Id == id), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            var result = await _controller.Delete(id);

            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().BeEquivalentTo($"Product: {id} successfully deleted");
        }
    }
}
