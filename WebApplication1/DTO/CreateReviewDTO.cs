public class CreateReviewDTO
{
    public int UserId { get; set; }
    public int RestaurantId { get; set; }
    public int Rating { get; set; } // 1 à 5 étoiles
    public string Commentaire { get; set; }
}
