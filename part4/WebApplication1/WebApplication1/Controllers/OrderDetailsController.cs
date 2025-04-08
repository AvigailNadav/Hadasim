using Microsoft.AspNetCore.Mvc;
using Repository.Entities;
using Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StoreProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailsController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IService<OrderDetails> _service;
        public OrderDetailsController(IService<OrderDetails> service, IConfiguration config)
        {
            this._service = service;
            this._config = config;
        }
        // GET: api/<ValuesController>
        [HttpGet]
        public async Task<IEnumerable<OrderDetails>> Get()
        {
            var allOrdersDetails = await _service.getAllAsync();
            return allOrdersDetails;
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDetails>> GetById(int id)
        {
            var allOrdersDetails = await _service.getAllAsync();
            if (allOrdersDetails == null)
            {
                return NotFound("Failed to get details");
            }
            var orderDetails = allOrdersDetails.FirstOrDefault(p => p.OrderDetailsId == id);
            if (orderDetails == null)
            {
                return NotFound("The order wasn't found");
            }
            return Ok(orderDetails);
        }
        // POST api/<ValuesController>
        [HttpPost]
        public async Task<ActionResult<OrderDetails>> Post([FromBody] OrderDetails order)
        {
            if (order == null)
            {
                return BadRequest("The order is null");
            }
            await _service.addAsync(order);
            return Ok(order);
        }
    }
}
