using FluentAssertions;
using OnlineMarketplace.Products.DAL.Models;
using System.Collections.Generic;
using Xunit;

namespace OnlineMarketplace.Products.Tests.Domain
{
    public class ProductTests
    {
        [Fact]
        public void Update_ChangesProductState()
        {
            var product = new Product
            {
                Id = 1,
                Name = "Test",
                Description = "Test",
                Price = 100,
                SellerId = 1,
                Attributes = new List<ProductAttributes>()
                {
                    new ProductAttributes
                    {
                        Id = 1,
                        Key = "Key",
                        Value = "Value",
                        ProductId = 1
                    }
                }
            };

            var newName = "New test";
            var newPrice = (decimal)150.2;
            var newAttributes = new List<ProductAttributes>
            {
                new ProductAttributes
                {
                    Key = "Key1",
                    Value = "Value1",
                },
                new ProductAttributes
                {
                    Key = "Key2",
                    Value = "Value2",
                }
            };

            product.Update(newName, product.Description, newPrice, newAttributes);

            var expectedProduct = new Product
            {
                Id = 1,
                Name = newName,
                Price = 15020,
                Description = "Test",
                SellerId = 1,
                Attributes = newAttributes
            };

            product.Should().BeEquivalentTo(expectedProduct);
        }
    }
}
