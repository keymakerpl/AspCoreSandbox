using AspCoreSandbox.Data.Entities;
using Infrastructure.Repository;

namespace AspCoreSandbox.Data.Repositories
{

    public class OrderItemsRepository : GenericRepository<OrderItem, StoreDbContext>, IOrderItemRepository
    {
        public OrderItemsRepository(StoreDbContext context) : base(context)
        {
        }
    }

    public interface IOrderItemRepository : IGenericRepository<OrderItem>
    {
    }
}
