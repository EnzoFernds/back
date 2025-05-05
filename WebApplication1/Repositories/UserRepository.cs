using System;
using System.Linq;
using System.Collections.Generic;

namespace RestaurantManagement
{
    public class UserRepository : Repository<User>
    {
        public User GetByEmail(string email)
        {
            return _context.FirstOrDefault(u => u.Email == email);
        }
        public User Get(string id)
        {
            return _context.FirstOrDefault(u => u.Id == id);
        }

    }
}
