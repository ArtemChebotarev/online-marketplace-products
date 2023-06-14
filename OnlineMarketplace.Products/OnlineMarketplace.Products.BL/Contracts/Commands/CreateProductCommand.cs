using MediatR;
using OnlineMarketplace.Products.BL.Dto;

namespace OnlineMarketplace.Products.BL.Contracts.Commands
{
    public record CreateProductCommand(
        string Name, 
        string Description, 
        decimal Price, 
        int SellerId,
        IEnumerable<ProductAttributesDto> Attributes) : IRequest<ProductDto>;
}
