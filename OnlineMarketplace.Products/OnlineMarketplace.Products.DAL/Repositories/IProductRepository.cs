using OnlineMarketplace.Products.DAL.Models;

namespace OnlineMarketplace.Products.DAL.Repositories
{
    public interface IProductRepository
    {
        Task<Product?> GetProductByIdAsync(int id);
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<IEnumerable<Product>> GetProductsBySellerAsync(int sellerId);
        Product AddProduct(Product product);
        Product UpdateProduct(Product product);
        bool DeleteProduct(Product product);

    }
}
