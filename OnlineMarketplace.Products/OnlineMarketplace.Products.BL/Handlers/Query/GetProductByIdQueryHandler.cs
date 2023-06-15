using MediatR;
using OnlineMarketplace.Products.BL.Contracts.Queries;
using OnlineMarketplace.Products.BL.Dto;
using OnlineMarketplace.Products.BL.Mappers;
using OnlineMarketplace.Products.DAL.Repositories;

namespace OnlineMarketplace.Products.BL.Handlers.Query
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDto?>
    {
        private readonly IProductRepository _productRepository;

        public GetProductByIdQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductDto?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetProductByIdAsync(request.Id);

            return product?.ToProductDto();
        }
    }
}
