using MediatR;
using OnlineMarketplace.Products.BL.Contracts.Commands;
using OnlineMarketplace.Products.BL.Dto;
using OnlineMarketplace.Products.BL.Mappers;
using OnlineMarketplace.Products.DAL;
using OnlineMarketplace.Products.DAL.Repositories;
using System.ComponentModel.DataAnnotations;

namespace OnlineMarketplace.Products.BL.Handlers.Command
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductDto>
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ProductDto> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetProductByIdAsync(request.Id);
            
            if (product is null)
            {
                throw new ValidationException($"Product: {request.Id} not found");
            }

            product.Update(
                request.Name,
                request.Description,
                request.Price,
                request.Attributes.ToProductAttributesEnumerable().ToList());

            _productRepository.UpdateProduct(product);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return product.ToProductDto();
        }
    }
}
