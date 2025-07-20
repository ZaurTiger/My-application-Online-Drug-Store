using Microsoft.AspNetCore.Mvc;
using Api_v2.Properties.Data;
using Api_v2.Properties.Models;
using Microsoft.EntityFrameworkCore;

namespace Api_v2.Properties.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OrderController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var orders = await _context.Orders.ToListAsync();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder(int id)
        {
            var order = await _context.Orders.Include(o => o.OrderItems).ThenInclude(oi => oi.Item).FirstOrDefaultAsync(o => o.OrderId == id);
            if (order == null)
                return NotFound("Oder has not been found");
            return Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetOrder), new { id = order.OrderId }, order);
        }

        [HttpPost("{orderId}/items")]
        public async Task<IActionResult> AddItemsToOrder(int orderId, List<OrderItems> items)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null)
                return NotFound("Order has not been found");

            foreach (var item in items)
            {
                item.OrderId = orderId; 
                _context.OrderItems.Add(item);
            }

            await _context.SaveChangesAsync();
            return Ok("Product has been added to order");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
                return NotFound("Order has not been found");

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return Ok("Order has been deleted");
        }
    }
}
