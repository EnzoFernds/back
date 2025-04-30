public class Restaurant
{
    public int RestaurantId { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }

    public string OwnerId { get; set; }
    public virtual User Owner { get; set; }

    public virtual List<MenuItem> MenuItems { get; set; } = new List<MenuItem>();

    public virtual List<Order> Orders { get; set; } = new List<Order>();

    public virtual List<Review> Reviews { get; set; } = new List<Review>();
}
