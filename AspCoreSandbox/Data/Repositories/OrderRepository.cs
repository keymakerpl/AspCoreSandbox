using System.Collections.Generic;
using System.Threading.Tasks;
using AspCoreSandbox.Data.Entities;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AspCoreSandbox.Data.Repositories
{
    public class OrderRepository : GenericRepository<Order, StoreDbContext>, IOrderRepository
    {
        private readonly StoreDbContext _context;
        private readonly ILogger<OrderRepository> _logger;

        public OrderRepository(StoreDbContext context, ILogger<OrderRepository> logger) : base(context)
        {
            _context = context;
            _logger = logger;
        }

        public override async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _context.Set<Order>()
                                    .Include(o => o.Items)
                                    .ThenInclude(p => p.Product)
                                    .ToListAsync();
        }
    }

    public interface IOrderRepository : IGenericRepository<Order>
    {
    }    
}
