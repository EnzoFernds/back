using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Services;
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

        private readonly OrderService _orderService;

        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public ActionResult CreateOrder([FromBody] CreateOrderDTO dto)
        {
            var order = new Order
            {
                ClientId = dto.UserId,
                OrderDate = DateTime.Now,
                Status = OrderStatus.EnCours,
                OrderItems = dto.Items.Select(item => new OrderItem
                {
                    MenuItemId = item.MenuItemId,
                    Quantity = item.Quantity
                }).ToList()
            };

            _orderService.CreateOrder(order);
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
