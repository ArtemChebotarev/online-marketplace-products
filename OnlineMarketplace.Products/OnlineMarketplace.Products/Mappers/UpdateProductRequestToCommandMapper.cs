using OnlineMarketplace.Products.Api.Contracts;
using OnlineMarketplace.Products.BL.Contracts.Commands;

namespace OnlineMarketplace.Products.Api.Mappers
{
    public static class UpdateProductRequestToCommandMapper
    {
        public static UpdateProductCommand ToCommand(this UpdateProductRequest request)
        {
            return new UpdateProductCommand(
                request.Id,
                request.Product.Name,
                request.Product.Description,
                request.Product.Price,
                request.Product.Attributes);
        }
    }
}
