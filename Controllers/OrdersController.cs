using EShopApi.DTOs.Orders;
using EShopApi.Models;
using EShopApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace EShopApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _service;

        public OrdersController(IOrderService service)
        {
            _service = service;
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var order = await _service.GetByIdAsync(id);

            return order == null ? NotFound() : Ok(order);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var Orders = await _service.GetAllAsync();

            return Ok(Orders);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OrderCreateDTO order)
        {
            if (order == null)
            {
                return BadRequest();
            }

            var createdOrder = await _service.CreateAsync(order);

            return CreatedAtAction(nameof(Get), new { id = createdOrder.Id }, createdOrder);
        }


        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var order = await _service.GetByIdAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            await _service.DeleteAsync(id);

            return NoContent();
        }
    }
}