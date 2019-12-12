using AspCoreSandbox.Data.Entities;
using AspCoreSandbox.Data.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspCoreSandbox.ViewModels
{
    [Route("/api/orders/{orderid}/items")]
    public class OrderItemsController : Controller
    {
        private readonly IOrderRepository _repository;
        private readonly ILogger<OrderItemsRepository> _logger;
        private readonly IMapper _mapper;

        public OrderItemsController(IOrderRepository repository, ILogger<OrderItemsRepository> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int orderId)
        {
            var order = await _repository.GetByIdAsync(orderId);
            if (order != null) 
                return Ok(_mapper.Map<IEnumerable<OrderItem>, IEnumerable<OrderItemViewModel>>(order.Items));
            
            return NotFound();
        }

        [HttpGet("{orderItemId}")]
        public async Task<IActionResult> Get(int orderId, int orderItemId)
        {
            var order = await _repository.GetByIdAsync(orderId);
            if (order != null)
            {
                var item = order.Items.FirstOrDefault(i => i.Id == orderItemId);
                if(item != null) 
                    return Ok(_mapper.Map<OrderItem, OrderItemViewModel>(item));
            }
            return NotFound();
        }
    }
}
