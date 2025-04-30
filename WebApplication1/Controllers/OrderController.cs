using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WebApplication1.Data;

namespace RestaurantManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderRepository _orderRepository;
        private readonly RestaurantContext _context;

        public OrderController(OrderRepository orderRepository, RestaurantContext context)
        {
            _orderRepository = orderRepository;
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<Order>> GetAllOrders()
        {
            return Ok(_orderRepository.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.OrderId == id);

            if (order == null)
                return NotFound();

            return order;
        }

        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(CreateOrderDTO dto)
        {
            var user = await _context.Users.FindAsync(dto.UserId);
            if (user == null)
                return NotFound($"Utilisateur {dto.UserId} introuvable.");

            var restaurant = await _context.Restaurants.FindAsync(dto.RestaurantId);
            if (restaurant == null)
                return NotFound($"Restaurant {dto.RestaurantId} introuvable.");

            var order = new Order
            {
                ClientId = dto.UserId,
                RestaurantId = dto.RestaurantId,
                OrderDate = DateTime.UtcNow,
                Status = OrderStatus.EnCours,
                TotalAmount = 0,
                OrderItems = new List<OrderItem>()
            };

            foreach (var item in dto.Items)
            {
                var menuItem = await _context.MenuItems.FindAsync(item.MenuItemId);
                if (menuItem == null)
                    return NotFound($"MenuItem {item.MenuItemId} introuvable.");

                var orderItem = new OrderItem
                {
                    MenuItemId = item.MenuItemId,
                    Quantity = item.Quantity,
                    SubTotal = item.Quantity * menuItem.Price
                };

                order.TotalAmount += orderItem.SubTotal;
                order.OrderItems.Add(orderItem);
            }

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOrder), new { id = order.OrderId }, order);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, UpdateOrderDTO dto)
        {
            if (id != dto.OrderId)
                return BadRequest();

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
                return NotFound();

            order.Status = (OrderStatus)dto.Status;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.OrderId == id);

            if (order == null)
                return NotFound();

            _context.OrderItems.RemoveRange(order.OrderItems);
            _context.Orders.Remove(order);

            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}
