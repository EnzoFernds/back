using System;
using System.Linq;
using System.Collections.Generic;

namespace RestaurantManagement
{
    public class RestaurantRepository : Repository<Restaurant>
    {
        public List<Restaurant> GetByOwnerId(string ownerId)
        {
            return _context.Where(r => r.OwnerId == ownerId).ToList();
        }

        public List<Restaurant> GetAll()
        {
            return _context.ToList();
        }
    }
}
