namespace Api_v2.Properties.Models;

public class OrderItems
{
    public int Id { get; set; }
    
    public int OrderId { get; set; }
    
    public int ItemId { get; set; }
    
    public int Quantity { get; set; }
    
    public Medicine Item { get; set; }
    
    public Order Order { get; set; }
}