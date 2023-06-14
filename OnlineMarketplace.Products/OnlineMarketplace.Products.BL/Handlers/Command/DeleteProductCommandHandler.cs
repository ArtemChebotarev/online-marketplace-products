using MediatR;
using OnlineMarketplace.Products.BL.Contracts.Commands;
using OnlineMarketplace.Products.DAL;
using OnlineMarketplace.Products.DAL.Repositories;

namespace OnlineMarketplace.Products.BL.Handlers.Command
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, bool>
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteProductCommandHandler(IUnitOfWork unitOfWork, IProductRepository productRepository)
        {
            _unitOfWork = unitOfWork;
            _productRepository = productRepository;
        }


        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetProductByIdAsync(request.Id);

            if (product == null)
            {
                return false;
            }

            var result = _productRepository.DeleteProduct(product);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return result;
        }
    }
}
