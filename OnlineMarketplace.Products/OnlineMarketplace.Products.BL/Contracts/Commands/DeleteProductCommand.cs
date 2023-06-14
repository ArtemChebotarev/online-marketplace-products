using MediatR;

namespace OnlineMarketplace.Products.BL.Contracts.Commands
{
    public record DeleteProductCommand (int Id) : IRequest<bool>;
}
