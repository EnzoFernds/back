using System.ComponentModel.DataAnnotations;

public class CreateReviewDTO
{
    [Required]
    public string UserId { get; set; }

    [Required]
    public int RestaurantId { get; set; }

    [Required]
    [Range(1, 5, ErrorMessage = "La note doit être entre 1 et 5.")]
    public int Rating { get; set; }

    [MaxLength(1000)]
    public string Commentaire { get; set; }
}
