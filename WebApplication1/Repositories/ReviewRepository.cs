using System;
using System.Linq;
using System.Collections.Generic;

namespace RestaurantManagement
{
    public class ReviewRepository : Repository<Review>
    {
        public List<Review> GetByRestaurantId(int restaurantId)
        {
            return _context.Where(r => r.RestaurantId == restaurantId).ToList();
        }

        public List<Review> GetByUserId(string userId)
        {
            return _context.Where(r => r.UserId == userId).ToList();
        }
    }
}
