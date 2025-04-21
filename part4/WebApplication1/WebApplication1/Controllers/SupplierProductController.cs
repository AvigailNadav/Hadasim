using Microsoft.AspNetCore.Mvc;
using Repository.Entities;
using Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StoreProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierProductController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IService<SupplierProduct> _service;
        public SupplierProductController(IService<SupplierProduct> service, IConfiguration config)
        {
            this._service = service;
            this._config = config;
        }
        // GET: api/<SupplierProductController>
        [HttpGet]
        public async Task<IEnumerable<SupplierProduct>> Get()
        {
            var allSuppliersProducts = await _service.getAllAsync();
            return allSuppliersProducts;
        }

        // GET api/<SupplierProductController>/5
        [HttpGet("id/{id}")]
        public async Task<ActionResult<SupplierProduct>> GetById(int id)
        {
            var allSuppliersProducts = await _service.getAllAsync();
            if (allSuppliersProducts == null)
            {
                return NotFound("Failed to get products");
            }
            var product = allSuppliersProducts.FirstOrDefault(p => p.SupplierProductId == id);
            if (product == null)
            {
                return NotFound("The product wasn't found");
            }
            return Ok(product);
        }

        // POST api/<SupplierProductController>
        [HttpPost]
        public async Task<ActionResult<SupplierProduct>> Post([FromBody] SupplierProduct supplierProduct)
        {
            if (supplierProduct == null)
            {
                return BadRequest("The product is null");
            }
            await _service.addAsync(supplierProduct);
            return Ok(supplierProduct);
        }
    }
}
