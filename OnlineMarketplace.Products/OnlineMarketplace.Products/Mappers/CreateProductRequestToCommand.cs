using OnlineMarketplace.Products.Api.Contracts;
using OnlineMarketplace.Products.BL.Contracts.Commands;

namespace OnlineMarketplace.Products.Api.Mappers
{
    public static class CreateProductRequestToCommand
    {
        public static CreateProductCommand ToCommand(this CreateProductRequest request)
        {
            return new CreateProductCommand(
                request.Name,
                request.Description,
                request.Price,
                request.SellerId,
                request.Attributes);
        }
    }
}
