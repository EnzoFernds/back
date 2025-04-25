using System.ComponentModel.DataAnnotations;

public class UpdateReviewDTO
{
    [Required]
    public int ReviewId { get; set; }

    [Range(1, 5, ErrorMessage = "La note doit être entre 1 et 5.")]
    public int Rating { get; set; }

    [MaxLength(1000)]
    public string Commentaire { get; set; }

    public Review ToEntity(Review existing)
    {
        existing.Rating = this.Rating;
        existing.Comment = this.Commentaire;
        // on ne touche pas à UserId ni RestaurantId
        return existing;
    }
}
