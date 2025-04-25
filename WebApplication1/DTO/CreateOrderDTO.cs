using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class CreateOrderDTO
{
    [Required]
    public string UserId { get; set; }

    [Required]
    public int RestaurantId { get; set; }

    [Required, MinLength(1)]
    public List<CreateOrderItemDTO> Items { get; set; }
}
