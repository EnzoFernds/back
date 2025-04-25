using System.ComponentModel.DataAnnotations;

public class CreateOrderItemDTO
{
    [Required]
    public int MenuItemId { get; set; }

    [Required, Range(1, int.MaxValue)]
    public int Quantity { get; set; }
}
