using OnlineMarketplace.Products.BL.Dto;
using OnlineMarketplace.Products.DAL.Models;

namespace OnlineMarketplace.Products.BL.Mappers
{
    public static class ProductToProductDtoMapper
    {
        public static ProductDto ToProductDto(this Product product)
        {
            return new ProductDto(
                product.Id,
                product.Name,
                product.Description,
                product.Price / 100,
                product.Attributes.ToProductAttributesDtoEnumerable(),
                product.SellerId
            );
        }

        public static IEnumerable<ProductDto> ToProductDtoEnumerable(this IEnumerable<Product> products)
        {
            foreach ( var product in products )
            {
                yield return ToProductDto(product);
            }
        }

        public static ProductAttributesDto ToProductAttributesDto(this ProductAttributes productAttribute) 
        {
            return new ProductAttributesDto(productAttribute.Key, productAttribute.Value);
        }

        public static IEnumerable<ProductAttributesDto> ToProductAttributesDtoEnumerable(
            this IEnumerable<ProductAttributes> productAttributes)
        {
            foreach (var productAttribute in productAttributes)
            {
                yield return ToProductAttributesDto(productAttribute);
            }
        }
    }
}
