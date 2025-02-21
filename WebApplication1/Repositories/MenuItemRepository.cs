using System;
using System.Linq;
using System.Collections.Generic;

namespace RestaurantManagement
{
    public class MenuItemRepository : Repository<MenuItem>
    {
        public List<MenuItem> GetByRestaurantId(int restaurantId)
        {
            return _context.Where(m => m.RestaurantId == restaurantId).ToList();
        }

        public MenuItem GetByName(string name)
        {
            return _context.FirstOrDefault(m => m.Name == name);
        }
    }
}
