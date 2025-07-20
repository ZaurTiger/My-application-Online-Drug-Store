namespace Api_v2.Properties.Models;

public class Cart
{
    public int Id { get; set; }
    
    public int UserId { get; set; }
    
    public int ItemId { get; set; }
    
    public int Quantity { get; set; }
    
    public User User { get; set; }
    
    public Medicine Item { get; set; }
}