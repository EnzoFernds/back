using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace RestaurantManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        private readonly OrderItemRepository _orderItemRepository;

        public OrderItemController(OrderItemRepository orderItemRepository)
        {
            _orderItemRepository = orderItemRepository;
        }

        [HttpGet]
        public ActionResult<List<OrderItem>> GetAllOrderItems()
        {
            return Ok(_orderItemRepository.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<OrderItem> GetOrderItemById(int id)
        {
            var orderItem = _orderItemRepository.Get(id);
            if (orderItem == null)
                return NotFound();
            return Ok(orderItem);
        }

        [HttpPost]
        public ActionResult CreateOrderItem(OrderItem orderItem)
        {
            _orderItemRepository.Add(orderItem);
            return CreatedAtAction(nameof(GetOrderItemById), new { id = orderItem.OrderItemId }, orderItem);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateOrderItem(int id, OrderItem orderItem)
        {
            if (id != orderItem.OrderItemId)
                return BadRequest();
            _orderItemRepository.Update(orderItem);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteOrderItem(int id)
        {
            var orderItem = _orderItemRepository.Get(id);
            if (orderItem == null)
                return NotFound();
            _orderItemRepository.Delete(id);
            return NoContent();
        }
    }
}

