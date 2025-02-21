using System;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantManagement
{
    public abstract class Repository<T> where T : class
    {
        protected List<T> _context = new List<T>();

        public virtual void Add(T entity)
        {
            _context.Add(entity);
        }

        public virtual T Get(int id)
        {
            return _context.FirstOrDefault(e => ((dynamic)e).Id == id);
        }

        public virtual List<T> GetAll()
        {
            return _context;
        }

        public virtual void Update(T entity)
        {
            var index = _context.FindIndex(e => ((dynamic)e).Id == ((dynamic)entity).Id);
            if (index != -1)
            {
                _context[index] = entity;
            }
        }

        public virtual void Delete(int id)
        {
            var entity = Get(id);
            if (entity != null)
            {
                _context.Remove(entity);
            }
        }
    }
}
