namespace Api_v2.Properties.Models;

public class User
{
    public User(int id, string login, string password, string email, List<Order> orders)
    {
        Id = id;
        
        Login = login;
        
        Password = password;
        
        Email = email;
        
        Orders = orders;
    }

    public User()
    {
        //инициализация по дефолту
    }

    public int Id { get; set; }
    
    public required string Login { get; set; }
    
    public required string Password { get; set; }
    
    public required string Email { get; set; }
    
    public List<Order> Orders { get; set; }
}