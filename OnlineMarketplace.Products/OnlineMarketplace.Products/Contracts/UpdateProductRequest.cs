using Microsoft.AspNetCore.Mvc;
using OnlineMarketplace.Products.BL.Dto;

namespace OnlineMarketplace.Products.Api.Contracts
{
    public record UpdateProductRequest(
        [FromRoute] int Id,
        [FromBody] UpdateProductRequestBody Product);
}
