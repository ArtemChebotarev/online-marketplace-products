using Microsoft.EntityFrameworkCore;
using OnlineMarketplace.Products.DAL.Models;

namespace OnlineMarketplace.Products.DAL.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductsContext _context;

        public ProductRepository(ProductsContext context)
        {
            _context = context;
        }

        public Product AddProduct(Product product)
        {
            _context.Products.Add(product);
            return product;
        }

        public bool DeleteProduct(Product product)
        {
            var result = _context.Products.Remove(product);
            return true;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _context.Products
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsBySellerAsync(int sellerId)
        {
            return await _context.Products
                .Where(p =>  p.SellerId == sellerId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public Product UpdateProduct(Product product)
        {
            _context.Entry(product).State = EntityState.Modified;
            return product;
        }
    }
}
