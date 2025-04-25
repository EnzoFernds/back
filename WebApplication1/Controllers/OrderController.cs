using Microsoft.AspNetCore.Mvc;
using WebApplication1.Service;     // pour OrderService

namespace RestaurantManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _orderService;

        // Injection de OrderService
        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("{id}")]
        public ActionResult<Order> GetOrderById(int id)
        {
            var order = _orderService.GetOrderById(id);
            if (order == null)
                return NotFound();
            return Ok(order);
        }

        [HttpPost]
        public ActionResult<Order> CreateOrder([FromBody] CreateOrderDTO dto)
        {
            var order = _orderService.CreateOrder(dto);
            return CreatedAtAction(nameof(GetOrderById),
                                   new { id = order.OrderId },
                                   order);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateOrder(int id, [FromBody] UpdateOrderDTO dto)
        {
            if (id != dto.OrderId)
                return BadRequest("L’ID de l’URL doit correspondre à dto.OrderId.");

            try
            {
                _orderService.UpdateOrder(id, dto.ToOrder());
                return NoContent();
            }
            catch (KeyNotFoundException knf)
            {
                return NotFound(knf.Message);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteOrder(int id)
        {
            try
            {
                _orderService.DeleteOrder(id);
                return NoContent();
            }
            catch (KeyNotFoundException knf)
            {
                return NotFound(knf.Message);
            }
        }
    }
}
