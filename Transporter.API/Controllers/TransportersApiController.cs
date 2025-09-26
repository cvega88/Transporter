using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Transporter.API.Services;
using Transporter.Core;
using Transporter.Data;

namespace Transporter.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TransportersApiController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TransportersApiController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransporterClass>>> GetTransporters()
        {
            return await _context.Transporters.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TransporterClass>> GetTransporter(int id)
        {
            var transporter = await _context.Transporters.FindAsync(id);
            if (transporter == null)
                return NotFound();

            return transporter;
        }

        [HttpPost]
        public async Task<ActionResult<TransporterClass>> PostTransporter(TransporterClass transporter)
        {
            _context.Transporters.Add(transporter);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTransporter), new { id = transporter.Id }, transporter);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTransporter(string id, TransporterClass transporter)
        {
            if (id != transporter.Id)
                return BadRequest();

            _context.Entry(transporter).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransporter(int id)
        {
            var transporter = await _context.Transporters.FindAsync(id);
            if (transporter == null)
                return NotFound();

            _context.Transporters.Remove(transporter);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("with-products")]
        public async Task<ActionResult<object>> GetTransportersWithProducts(
            [FromServices] FakeStoreService fakeStoreService)
        {
            var transporters = await _context.Transporters.ToListAsync();
            var products = await fakeStoreService.GetProductsAsync();

            return Ok(new
            {
                Transporters = transporters,
                Products = products.Take(5) // Ejemplo: solo los primeros 5
            });
        }
    }
}