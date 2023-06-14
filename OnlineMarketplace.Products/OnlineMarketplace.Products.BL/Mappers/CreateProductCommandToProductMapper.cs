using OnlineMarketplace.Products.BL.Contracts.Commands;
using OnlineMarketplace.Products.DAL.Models;

namespace OnlineMarketplace.Products.BL.Mappers
{
    public static class CreateProductCommandToProductMapper
    {
        public static Product ToProduct(this CreateProductCommand product)
        {
            return new Product
            {
                Name = product.Name,
                Description = product.Description,
                Price = (long)(product.Price * 100),
                SellerId = product.SellerId,
                Attributes = product.Attributes.ToProductAttributesEnumerable().ToList()
            };
        }
    }
}
