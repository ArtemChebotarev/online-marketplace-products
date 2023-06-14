namespace OnlineMarketplace.Products.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ProductsContext _context;

        public UnitOfWork(ProductsContext context)
        {
            _context = context;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync();
        }
    }
}
