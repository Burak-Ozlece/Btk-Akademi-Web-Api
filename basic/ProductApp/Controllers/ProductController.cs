using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductApp.Models;

namespace ProductApp.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;

        public ProductController(ILogger<ProductController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public IActionResult GetAllProdcuts()
        {
            var product = new List<Product>()
            {
                 new Product{ Id = 1, ProductName="Computer"},
                 new Product{ Id = 2, ProductName="Keyboard"},
                 new Product{ Id = 3, ProductName="Mouse"}
            };
            _logger.LogInformation("GetAllProducts action has been called.");
            return Ok(product); 
        }
        [HttpPost]
        public IActionResult GetAllProdcuts([FromBody]Product product)
        {
            _logger.LogWarning("Product has been created");
            return StatusCode(201);
        }
    }
}
