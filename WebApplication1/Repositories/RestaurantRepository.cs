using System;
using System.Linq;
using System.Collections.Generic;

namespace RestaurantManagement
{
    public class RestaurantRepository : Repository<Restaurant>
    {
        public List<Restaurant> GetByOwnerId(int ownerId)
        {
            return _context.Where(r => r.OwnerId == ownerId).ToList();
        }
    }
}
