using FluentAssertions;
using Newtonsoft.Json;
using OnlineMarketplace.Products.Api.Contracts;
using OnlineMarketplace.Products.BL.Dto;
using OnlineMarketplace.Products.BL.Mappers;
using OnlineMarketplace.Products.DAL.Models;
using OnlineMarketplace.Products.IntegrationTests.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace OnlineMarketplace.Products.IntegrationTests
{
    [Collection(nameof(ProductsFixtureCollection))]
    public class ProductsControllerTests
    {
        private readonly HttpClient _httpClient;
        private readonly ProductsWebApplicationFactory _factory;
        private readonly ProductsTestsContext _productsContext;

        public ProductsControllerTests(
            ProductsWebApplicationFactory factory)
        {
            _factory = factory;
            _httpClient = factory.CreateClient();
            _productsContext = factory.DatabaseFixture.ProductsContext;
        }

        [Fact]
        public async Task GetAll_ReturnsAllProducts_WhenNoSellerIdSpecified()
        {
            // Arrange
            await _factory.DatabaseFixture.Reseed();

            // Act
            var response = await _httpClient.GetAsync("api/products");

            // Assert
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var products = JsonConvert.DeserializeObject<List<ProductDto>>(content);

            products!.Count.Should().Be(5);  
        }

        [Fact]
        public async Task GetAll_ReturnsOnlyProductsAssignedToASeller_WhenSellerIdIsSpecified()
        {
            // Arrange
            await _factory.DatabaseFixture.Reseed();
            var sellerId = 1;

            // Act
            var response = await _httpClient.GetAsync($"api/products?sellerId={sellerId}");

            // Assert
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var products = JsonConvert.DeserializeObject<List<ProductDto>>(content);

            products!.Count.Should().Be(1);
            products.First().SellerId.Should().Be(sellerId);
        }

        [Fact]
        public async Task Post_NewProductShouldBeCreated()
        {
            // Arragne
            var request = new CreateProductRequest("Name", "Description", 100, 1, Enumerable.Empty<ProductAttributesDto>());
            var expectedResult = new Product
            {
                Name = request.Name,
                Description = request.Description,
                Price = (long)(request.Price * 100),
                SellerId = request.SellerId,
                Attributes = request.Attributes.ToProductAttributesEnumerable().ToList()
            };

            var requestContent = JsonContent.Create(request);

            // Act
            var response = await _httpClient.PostAsync($"api/products", requestContent);

            // Assert
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            var returnedProduct = JsonConvert.DeserializeObject<ProductDto>(responseContent);

            expectedResult.Id = returnedProduct!.Id;

            var createdProduct = _productsContext.Products.First(p => p.Id == returnedProduct!.Id);
            createdProduct.Should().BeEquivalentTo(expectedResult);
        }
    }
}
