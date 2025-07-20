using Microsoft.AspNetCore.Mvc;
using Api_v2.Properties.Data;
using Api_v2.Properties.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Api_v2.Properties.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicineController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MedicineController(AppDbContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetMedicines()
        {
            var medicines = await _context.Medicines.ToListAsync();
            return Ok(medicines);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMedicine(int id)
        {
            var medicine = await _context.Medicines.FindAsync(id);
            if (medicine == null)
                return NotFound("Medicine has not been found.");
            return Ok(medicine);
        }

        [HttpPost]
        public async Task<IActionResult> AddMedicine(Medicine medicine)
        {
            _context.Medicines.Add(medicine);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetMedicine), new { id = medicine.MedicineId }, medicine);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMedicine(int id, Medicine medicine)
        {
            if (id != medicine.MedicineId)
                return BadRequest("Medicine Ids do not match.");

            _context.Entry(medicine).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Medicines.Any(m => m.MedicineId == id))
                    return NotFound("Medicine has not been found.");
                throw;
            }

            return NoContent();
        }
        
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedicine(int id)
        {
            var medicine = await _context.Medicines.FindAsync(id);
            if (medicine == null)
                return NotFound("Medicine has not been found");

            _context.Medicines.Remove(medicine);
            await _context.SaveChangesAsync();

            return Ok("Medicine has been deleted.");
        }
    }
}
