using Microsoft.AspNetCore.Mvc;
using Api_v2.Properties.Data;
using Api_v2.Properties.Models;
using Microsoft.AspNetCore.Authorization;


[Route("api/[controller]")]
[ApiController]
public class CartController : ControllerBase
{
    private readonly AppDbContext _context;

    public CartController(AppDbContext context)
    {
        _context = context;
    }

    [Authorize]
    [HttpGet("{userId}")]
    public IActionResult GetCart(int userId)
    {
        var cartItems = _context.Cart.Where(c => c.UserId == userId).ToList();
        return Ok(cartItems);
    }
    
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> AddToCart(Cart cart)
    {
        _context.Cart.Add(cart);
        await _context.SaveChangesAsync();
        return Ok("Product has been added to your cart");
    }
    
    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoveFromCart(int id)
    {
        var cartItem = await _context.Cart.FindAsync(id);
        if (cartItem == null)
            return NotFound("Product doesn't exist");

        _context.Cart.Remove(cartItem);
        await _context.SaveChangesAsync();
        return Ok("Product has been removed from your cart");
    }
}