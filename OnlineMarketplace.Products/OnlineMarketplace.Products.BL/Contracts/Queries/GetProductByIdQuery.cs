using MediatR;
using OnlineMarketplace.Products.BL.Dto;

namespace OnlineMarketplace.Products.BL.Contracts.Queries
{
    public record GetProductByIdQuery(int Id) : IRequest<ProductDto?>;
}
