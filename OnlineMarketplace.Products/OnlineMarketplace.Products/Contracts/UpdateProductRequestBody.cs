using Microsoft.AspNetCore.Mvc;
using OnlineMarketplace.Products.BL.Dto;

namespace OnlineMarketplace.Products.Api.Contracts
{
    public record UpdateProductRequestBody(
        string Name,
        string Description, 
        decimal Price, 
        IEnumerable<ProductAttributesDto> Attributes);
}
