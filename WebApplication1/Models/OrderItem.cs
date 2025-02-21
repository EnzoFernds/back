public class OrderItem
{
    public int OrderItemId { get; set; }

    // Clé étrangère : La commande à laquelle appartient cet article
    public int OrderId { get; set; }
    public virtual Order Order { get; set; }

    // Clé étrangère : Le plat commandé
    public int MenuItemId { get; set; }
    public virtual MenuItem MenuItem { get; set; }

    public int Quantity { get; set; }
    public decimal SubTotal { get; set; } // (Prix du plat * Quantité)
}
