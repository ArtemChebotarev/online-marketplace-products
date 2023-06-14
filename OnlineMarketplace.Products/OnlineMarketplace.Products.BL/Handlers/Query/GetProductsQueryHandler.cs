using MediatR;
using OnlineMarketplace.Products.BL.Contracts.Queries;
using OnlineMarketplace.Products.BL.Dto;
using OnlineMarketplace.Products.BL.Mappers;
using OnlineMarketplace.Products.DAL.Repositories;

namespace OnlineMarketplace.Products.BL.Handlers.Query
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, IEnumerable<ProductDto>>
    {
        private readonly IProductRepository _productRepository;

        public GetProductsQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetAllProductsAsync();

            return products.ToProductDtoEnumerable();
        }
    }
}
