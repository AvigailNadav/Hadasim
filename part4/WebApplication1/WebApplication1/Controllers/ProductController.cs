using Microsoft.AspNetCore.Mvc;
using Repository.Entities;
using Repository.Entities.DTO;
using Service;
using Service.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StoreProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IService<Product> _service;
        private readonly IService<Supplier> _supplier;
        public ProductController(IService<Product> service,IService<Supplier> supplier, IConfiguration config)
        {
            this._service = service;
            this._supplier = supplier;
            this._config = config;
        }
        // GET: api/<ProductController>
        [HttpGet]
        public async Task<IEnumerable<Product>> Get()
        {
            var allProducts = await _service.getAllAsync();
            return allProducts;
        }

        // GET api/<ProductController>/5
        [HttpGet("id/{id}")]
        public async Task<ActionResult<Product>> GetById(int id)
        {
            var allProducts = await _service.getAllAsync();
            if (allProducts == null)
            {
                return NotFound("Failed to get products");
            }
            var product = allProducts.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound("The product wasn't found");
            }
            return Ok(product);
        }
        // GET api/<ValuesController>/5
        [HttpGet("name/{name}")]
        public async Task<ActionResult<Product>> GetByName(string name)
        {
            var allProducts = await _service.getAllAsync();
            if (allProducts == null)
            {
                return NotFound("Failed to get products");
            }
            var product = allProducts.FirstOrDefault(p => p.Name == name);
            if (product == null)
            {
                return NotFound("The product wasn't found");
            }
            return Ok(product);
        }
        [HttpGet("bysupplier/{supplierId}")]
        public async Task<ActionResult<List<Product>>> GetProductsBySupplierId(int supplierId)
        {
            var products = await _service.getAllAsync();
            var supplierProducts = products.Where(p => p.SupplierId == supplierId).ToList();

            if (!supplierProducts.Any())
            {
                return NotFound("No products found for this supplier.");
            }
            return Ok(supplierProducts);
        }
        // POST api/<ProductController>
        [HttpPost]
        public async Task<ActionResult<Product>> Post([FromBody] ProductDto productDto)
        {
            if (productDto == null)
            {
                return BadRequest("The product is null");
            }
            var supplier = await _supplier.getByIdAsync(productDto.SupplierId);
            if (supplier == null)
            {
                return BadRequest("Supplier not found.");
            }
            var product = new Product
            {
                Name = productDto.Name,
                MinPurchase = productDto.MinPurchase,
                Price = productDto.Price,
                SupplierId = productDto.SupplierId,
                Supplier = supplier
            };
            await _service.addAsync(product);
            return Ok(product);
        }
    }
}
