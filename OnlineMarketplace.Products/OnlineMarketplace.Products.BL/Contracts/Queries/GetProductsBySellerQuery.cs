using MediatR;
using OnlineMarketplace.Products.BL.Dto;

namespace OnlineMarketplace.Products.BL.Contracts.Queries
{
    public record GetProductsBySellerQuery(int SellerId) : IRequest<IEnumerable<ProductDto>>;
}
