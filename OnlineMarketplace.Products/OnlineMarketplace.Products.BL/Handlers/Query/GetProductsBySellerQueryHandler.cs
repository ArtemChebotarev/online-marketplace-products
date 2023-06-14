using MediatR;
using OnlineMarketplace.Products.BL.Contracts.Queries;
using OnlineMarketplace.Products.BL.Dto;
using OnlineMarketplace.Products.BL.Mappers;
using OnlineMarketplace.Products.DAL.Repositories;

namespace OnlineMarketplace.Products.BL.Handlers.Query
{
    public class GetProductsBySellerQueryHandler : IRequestHandler<GetProductsBySellerQuery, IEnumerable<ProductDto>>
    {
        private readonly IProductRepository _productRepository;

        public GetProductsBySellerQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<ProductDto>> Handle(GetProductsBySellerQuery request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetProductsBySellerAsync(request.SellerId);

            return products.ToProductDtoEnumerable();
        }
    }
}
