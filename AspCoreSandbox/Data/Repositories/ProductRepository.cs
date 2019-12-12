using AspCoreSandbox.Data.Entities;
using Infrastructure.Repository;
using Microsoft.Extensions.Logging;

namespace AspCoreSandbox.Data.Repositories
{
    public class ProductRepository : GenericRepository<Product, StoreDbContext>, IProductRepository
    {
        private readonly StoreDbContext _context;
        private readonly ILogger<ProductRepository> _logger;

        public ProductRepository(StoreDbContext context, ILogger<ProductRepository> logger) : base(context)
        {
            _context = context;
            _logger = logger;
        }
    }

    public interface IProductRepository : IGenericRepository<Product>
    {
    }
}
