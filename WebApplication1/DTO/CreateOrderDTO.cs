using System.Collections.Generic;

public class CreateOrderDTO
{
    public int UserId { get; set; }
    public int RestaurantId { get; set; }
    public List<CreateOrderItemDTO> Items { get; set; }
}


