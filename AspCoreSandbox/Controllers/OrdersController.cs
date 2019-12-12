using AspCoreSandbox.Data.Entities;
using AspCoreSandbox.Data.Repositories;
using AspCoreSandbox.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AspCoreSandbox.Controllers
{
    [Route("api/[Controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrdersController : Controller
    {
        private readonly IOrderRepository _repository;
        private readonly ILogger<OrdersController> _logger;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<StoreUser> _userManager;

        public OrdersController(
            IOrderRepository repository,
            ILogger<OrdersController> logger,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            UserManager<StoreUser> userManager)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        /// <summary>
        /// includeItems - Query strings /api/orders?includeItems=false
        /// </summary>
        /// <param name="includeItems"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200)] // metadane o zwracanych rezultatach
        [ProducesResponseType(400)]
        public async Task<IActionResult> Get(bool includeItems = true)
        {
            try
            {
                var userName = User.Identity.Name;

                var result = includeItems ? _repository.FindByInclude(u => u.User.UserName == userName, i => i.Items, u => u.User) : await _repository.FindByAsync(u => u.User.UserName == userName);
                return Ok(_mapper.Map<IEnumerable<Order>, IEnumerable<OrderViewModel>>(result));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed: {ex}");
                return StatusCode(500, "Error...");
            }
        }

        [HttpGet("{id:int}", Name = nameof(Get))]
        public IActionResult Get(int id)
        {
            try
            {
                var userName = User.Identity.Name;

                var order = _repository.FindByInclude(o => o.User.UserName == userName, o => o.Items).FirstOrDefault();
                if (order == null)
                {
                    _logger.LogInformation($"Product with id: {id} not found");
                    return NotFound();
                }

                return Ok(order);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exeption has been thrown durring product Get({id}) request. \n{ex.Message}");
                return StatusCode(500, "Something goes wrong... :(");
            }
        }

        [HttpPost()]
        public async Task<IActionResult> Post([FromBody] OrderViewModel model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest();
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                var newOrder = _mapper.Map<OrderViewModel, Order>(model);

                if (newOrder.OrderDate == DateTime.MinValue)
                {
                    newOrder.OrderDate = DateTime.Now;
                }

                var currentUser = await _userManager.FindByNameAsync(User.Identity.Name);
                newOrder.User = currentUser;

                _repository.Add(newOrder);

                if (!await _repository.SaveAsync())
                {
                    return StatusCode(500, "Something goes wrong... in Save method :(");
                }

                return Created($"/api/orders/{model.OrderId}", model);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exeption has been thrown durring Order create. \n{ex.Message}");
                return StatusCode(500, "Something goes wrong... :(");
            }

        }

    }
}
