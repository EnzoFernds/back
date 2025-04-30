using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WebApplication1.Data;
using WebApplication1.DTO;

namespace RestaurantManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        private readonly OrderItemRepository _orderItemRepository;
        private readonly RestaurantContext _context;

        public OrderItemController(OrderItemRepository orderItemRepository, RestaurantContext context)
        {
            _orderItemRepository = orderItemRepository;
            _context = context;
        }

        [HttpGet("{id}")]
        public ActionResult<OrderItem> GetOrderItemById(int id)
        {
            var orderItem = _orderItemRepository.Get(id);
            if (orderItem == null)
                return NotFound();
            return Ok(orderItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderItem(int id, UpdateOrderItemDTO dto)
        {
            if (id != dto.OrderItemId)
                return BadRequest();

            var item = await _context.OrderItems.FindAsync(id);
            if (item == null)
                return NotFound();

            item.Quantity = dto.Quantity;
            item.SubTotal = item.Quantity * (await _context.MenuItems
                .Where(m => m.MenuItemId == item.MenuItemId)
                .Select(m => m.Price)
                .FirstOrDefaultAsync());

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderItem(int id)
        {
            var item = await _context.OrderItems.FindAsync(id);
            if (item == null)
                return NotFound();

            _context.OrderItems.Remove(item);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}

