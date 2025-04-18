using WebApplication1.Data;

public class MenuItemRepository
{
    private readonly RestaurantContext _context;

    public MenuItemRepository(RestaurantContext context)
    {
        _context = context;
    }

    public void Add(MenuItem menuItem)
    {
        _context.MenuItems.Add(menuItem);
        _context.SaveChanges(); // ← très important
    }

    public List<MenuItem> GetAll() => _context.MenuItems.ToList();

    public MenuItem GetByName(string name, int restaurantId)
    {
        return _context.MenuItems.FirstOrDefault(m => m.Name == name && m.RestaurantId == restaurantId);
    }

    public MenuItem Get(int id) => _context.MenuItems.FirstOrDefault(m => m.MenuItemId == id);

    public void Update(MenuItem menuItem)
    {
        var existing = _context.MenuItems.FirstOrDefault(m => m.MenuItemId == menuItem.MenuItemId);

        if (existing == null)
            throw new Exception("MenuItem non trouvé");

        existing.Name = menuItem.Name;
        existing.Description = menuItem.Description;
        existing.Price = menuItem.Price;
        existing.Category = menuItem.Category;
        existing.IsAvailable = menuItem.IsAvailable;

        // ⚠️ Ne surtout pas toucher à RestaurantId ou Restaurant ici

        _context.Entry(existing).Property(x => x.RestaurantId).IsModified = false;
        _context.Entry(existing).Reference(x => x.Restaurant).IsModified = false;

        _context.SaveChanges();
    }


    public MenuItem? GetById(int id)
    {
        return _context.MenuItems.FirstOrDefault(m => m.MenuItemId == id);
    }


    public void Delete(int id)
    {
        var item = Get(id);
        if (item != null)
        {
            _context.MenuItems.Remove(item);
            _context.SaveChanges();
        }
    }
}
