public class Review
{
    public int ReviewId { get; set; }

    public string UserId { get; set; }
    public virtual User User { get; set; }

    public int? RestaurantId { get; set; }
    public virtual Restaurant Restaurant { get; set; }

    public int Rating { get; set; } // Note entre 1 et 5
    public string Comment { get; set; }
    public DateTime DatePosted { get; set; } = DateTime.Now;
}
