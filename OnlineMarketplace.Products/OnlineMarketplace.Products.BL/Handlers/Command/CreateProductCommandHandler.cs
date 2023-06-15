using MediatR;
using OnlineMarketplace.Products.BL.Contracts.Commands;
using OnlineMarketplace.Products.BL.Dto;
using OnlineMarketplace.Products.BL.Mappers;
using OnlineMarketplace.Products.DAL;
using OnlineMarketplace.Products.DAL.Repositories;
using System.ComponentModel.DataAnnotations;

namespace OnlineMarketplace.Products.BL.Handlers.Command
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductDto>
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ProductDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            if (request.SellerId <= 0)
            {
                throw new ValidationException("SellerId should be greater that 0");
            }

            var product = request.ToProduct();
            _productRepository.AddProduct(product);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return product.ToProductDto();
        }
    }
}
