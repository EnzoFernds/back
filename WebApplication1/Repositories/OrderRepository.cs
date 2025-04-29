using System;
using System.Linq;
using System.Collections.Generic;

namespace RestaurantManagement
{
    public class OrderRepository : Repository<Order>
    {
        public List<Order> GetByClientId(string clientId)
        {
            return _context.Where(o => o.ClientId == clientId).ToList();
        }

        public List<Order> GetByRestaurantId(int restaurantId)
        {
            return _context.Where(o => o.RestaurantId == restaurantId).ToList();
        }
    }
}
