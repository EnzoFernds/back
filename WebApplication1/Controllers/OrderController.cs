using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace RestaurantManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderRepository _orderRepository;

        public OrderController(OrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [HttpGet]
        public ActionResult<List<Order>> GetAllOrders()
        {
            return Ok(_orderRepository.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<Order> GetOrderById(int id)
        {
            var order = _orderRepository.Get(id);
            if (order == null)
                return NotFound();
            return Ok(order);
        }

        [HttpPost]
        public ActionResult CreateOrder(Order order)
        {
            _orderRepository.Add(order);
            return CreatedAtAction(nameof(GetOrderById), new { id = order.OrderId }, order);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateOrder(int id, Order order)
        {
            if (id != order.OrderId)
                return BadRequest();
            _orderRepository.Update(order);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteOrder(int id)
        {
            var order = _orderRepository.Get(id);
            if (order == null)
                return NotFound();
            _orderRepository.Delete(id);
            return NoContent();
        }
    }
}
