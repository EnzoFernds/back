public class OrderReadDTO
{
    public int OrderId { get; set; }
    public string ClientName { get; set; }
    public decimal TotalAmount { get; set; }
    public DateTime OrderDate { get; set; }
    public string Status { get; set; }
}
