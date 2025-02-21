using System;
using System.Linq;
using System.Collections.Generic;

namespace RestaurantManagement
{
    public class OrderItemRepository : Repository<OrderItem>
    {
        public List<OrderItem> GetByOrderId(int orderId)
        {
            return _context.Where(oi => oi.OrderId == orderId).ToList();
        }
    }
}
