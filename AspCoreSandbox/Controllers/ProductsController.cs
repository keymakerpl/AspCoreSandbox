using AspCoreSandbox.Data.Entities;
using AspCoreSandbox.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspCoreSandbox.Controllers
{
    [Route("api/[Controller]")]
    [ApiController] //metadane dla np. narzędzi do dokumentacji
    [Produces("application/json")]
    public class ProductsController :  Controller
    {
        private readonly IProductRepository _repository;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IProductRepository repository, ILogger<ProductsController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(200)] // metadane o zwracanych rezultatach
        [ProducesResponseType(400)]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await _repository.GetAllAsync());
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed: {ex}");
                return StatusCode(500, "Error...");
            }
        }
    }
}
