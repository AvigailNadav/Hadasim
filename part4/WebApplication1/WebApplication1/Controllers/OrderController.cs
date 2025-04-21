using Microsoft.AspNetCore.Mvc;
using Repository.Entities;
using Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StoreProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IOrderService<Order> _service;
        public OrderController(IOrderService<Order> service, IConfiguration config)
        {
            this._service = service;
            this._config = config;
        }
        // GET: api/<OrderController>
        [HttpGet]
        public async Task<IEnumerable<Order>> Get()
        {
            var allOrders = await _service.getAllAsync();
            return allOrders;
        }

        // GET api/<OrderController>/5
        [HttpGet("id/{id}")]
        public async Task<ActionResult<Order>> GetById(int id)
        {
            var allOrders = await _service.getAllAsync();
            if (allOrders == null)
            {
                return NotFound("Failed to get orders");
            }
            var order = allOrders.FirstOrDefault(p => p.Id == id);
            if (order == null)
            {
                return NotFound("The order wasn't found");
            }
            return Ok(order);
        }

        // POST api/<OrderController>
        [HttpPost]
        public async Task<ActionResult<Order>> Post([FromBody] Order order)
        {
            if (order == null)
            {
                return BadRequest("The order is null");
            }
            await _service.addAsync(order);
            return Ok(order);
        }

        // PUT api/<OrderController>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrderStatus(int id, [FromBody] string newStatus)
        {
            var order = await _service.getByIdAsync(id);

            if (order == null)
            {
                return NotFound("ההזמנה לא נמצאה");
            }

            order.Status = newStatus;

            try
            {
                await _service.updateAsync(order);
                return Ok("הסטטוס עודכן בהצלחה");
            }
            catch (Exception ex)
            {
                return BadRequest($"שגיאה בעדכון ההזמנה: {ex.Message}");
            }
        }
    }
}

