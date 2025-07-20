namespace Api_v2.Properties.Models;

public class Order
{
    public int OrderId { get; set; }
    public int UserId { get; set; }
    public DateTime OrderDate { get; set; }
    public List<OrderItems> OrderItems { get; set; } = new List<OrderItems>();
    
    public User User { get; set; }
}