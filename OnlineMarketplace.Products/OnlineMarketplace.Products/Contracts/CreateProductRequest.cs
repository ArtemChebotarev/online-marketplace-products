using OnlineMarketplace.Products.BL.Dto;

namespace OnlineMarketplace.Products.Api.Contracts
{
    public record CreateProductRequest(string Name,
        string Description,
        decimal Price,
        int SellerId,
        IEnumerable<ProductAttributesDto> Attributes);
}
