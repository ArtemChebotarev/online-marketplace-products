using OnlineMarketplace.Products.BL.Dto;
using OnlineMarketplace.Products.DAL.Models;

namespace OnlineMarketplace.Products.BL.Mappers
{
    public static class ProductDtoToProductMapper
    {
        public static IEnumerable<ProductAttributes> ToProductAttributesEnumerable(this IEnumerable<ProductAttributesDto> productAttributes)
        {
            foreach (var attr in productAttributes)
            {
                yield return ToProductAttribute(attr);
            }
        }

        private static ProductAttributes ToProductAttribute(ProductAttributesDto attr)
        {
            return new ProductAttributes
            {
                Key = attr.Key,
                Value = attr.Value,
            };
        }
    }
}
