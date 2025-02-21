public class MenuItem
{
    public int MenuItemId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string Category { get; set; }
    public bool IsAvailable { get; set; } = true;

    public int RestaurantId { get; set; }
    public virtual Restaurant Restaurant { get; set; }
}
