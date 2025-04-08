using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Repository.Entities;
using Repository.Entities.DTO;
using Service;
using Service.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StoreProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IService<Supplier> _service;
        private readonly IJwtTokenService _jwtTokenService;
        public SupplierController(IService<Supplier> service, IConfiguration config, IJwtTokenService jwtTokenService)
        {
            this._service = service;
            this._config = config;
            _jwtTokenService = jwtTokenService;
        }

        // GET api/<SuplierController>
        [HttpGet]
        [HttpGet("getAllSuppliers")]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<Supplier>>> GetAllSuppliers()
        {
            var suppliers = await _service.getAllAsync();
            var supplierList = suppliers.Where(u => u.Role == "Supplier").ToList();
            if (!supplierList.Any())
            {
                return NotFound("No suppliers found.");
            }
            return Ok(supplierList);
        }

        // GET api/<UserController>/5
        [HttpGet("id/{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult<Supplier>> GetById(int id)
        {
            var user = await _service.getByIdAsync(id);
            if (user == null)
            {
                return NotFound("Their is no user with this id");
            }
            return Ok(user);
        }

        // GET api/<UserController>/5
        [HttpGet("name/{name}")]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult<Supplier>> GetByName(string name)
        {
            var user = await _service.getByNameAsync(name);
            if (user == null)
            {
                return NotFound("Their is no user with this name");
            }
            return Ok(user);
        }
        // POST api/<SuplierController>
        [HttpPost("registerSupplier")]
        public async Task<ActionResult> RegisterSupplier([FromBody] SupplierDto value)
        {
            var existingSuppliers = await _service.getAllAsync();
            var existingSupplier = existingSuppliers.FirstOrDefault(s => s.PhoneNumber == value.PhoneNumber);
            if (existingSupplier != null)
            {
                return BadRequest("User with this phone number exists already");
            }
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(value.Password);
            var newSupplier = new Supplier
            {
                Name = value.Name,
                PhoneNumber = value.PhoneNumber,
                Password = hashedPassword,
                CompanyName = value.CompanyName,
                Role = "Supplier",
                Products = new List<Product>()
            };

            await _service.addAsync(newSupplier);
            var token = _jwtTokenService.GenerateJwtToken(newSupplier);
            return Ok(new { message = "Supplier registered successfully", token });
        }
        [HttpPost("login")]
        public async Task<IActionResult> LogIn([FromBody] SupplierLogInDto supplierLogIn)
        {
            var existingUsers = await _service.getAllAsync();
            var supplier = existingUsers.FirstOrDefault(s => s.PhoneNumber == supplierLogIn.PhoneNumber);
            if (supplier == null)
            {
                return BadRequest("Supplier not found");
            }

            if (!BCrypt.Net.BCrypt.Verify(supplierLogIn.Password, supplier.Password))
            {
                return BadRequest("The password is incorrect");
            }

            var token = _jwtTokenService.GenerateJwtToken(supplier);

            return Ok(new { message = "Login successful", token });
        }
    }
}
