public class UpdateOrderDTO
{
    public int OrderId { get; set; }
    public OrderStatus Status { get; set; }

    public Order ToOrder()
    {
        return new Order
        {
            OrderId = this.OrderId,
            Status = this.Status,
        };
    }
}
