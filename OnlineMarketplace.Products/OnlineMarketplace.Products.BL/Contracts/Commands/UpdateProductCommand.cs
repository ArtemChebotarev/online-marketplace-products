using MediatR;
using OnlineMarketplace.Products.BL.Dto;

namespace OnlineMarketplace.Products.BL.Contracts.Commands
{
    public record UpdateProductCommand(
        int Id,
        string Name, 
        string Description, 
        decimal Price, 
        IEnumerable<ProductAttributesDto> Attributes) : IRequest<ProductDto>;
}
