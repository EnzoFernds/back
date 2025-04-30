public enum OrderStatus
{
    EnCours,
    Prête,
    Livrée,
    Annulée
}

public class Order
{
    public int OrderId { get; set; }
    public string ClientId { get; set; }  // Clé étrangère
    public virtual User Client { get; set; }

    public int RestaurantId { get; set; } // Clé étrangère
    public virtual Restaurant Restaurant { get; set; }

    public DateTime OrderDate { get; set; } = DateTime.Now;
    public OrderStatus Status { get; set; } = OrderStatus.EnCours;
    public decimal TotalAmount { get; set; }

    // Relation : Une commande a plusieurs plats commandés
    public virtual List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
