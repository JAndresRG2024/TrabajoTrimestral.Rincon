using Eventos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Eventos.API.DTOs;

namespace Eventos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagosController : ControllerBase
    {
        private readonly EventosAPIContext _context;
        public PagosController(EventosAPIContext context) => _context = context;

        // GET: api/Pago
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PagoDTO>>> GetPagos()
        {
            return await _context.Pagos
                .Select(p => new PagoDTO
                {
                    PagoId = p.PagoId,
                    InscripcionId = p.InscripcionId,
                    MedioPago = p.MedioPago,
                    Monto = p.Monto,
                    FechaPago = p.FechaPago
                })
                .ToListAsync();
        }

        // GET: api/Pago/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PagoDTO>> GetPago(int id)
        {
            var pago = await _context.Pagos
                .Where(p => p.PagoId == id)
                .Select(p => new PagoDTO
                {
                    PagoId = p.PagoId,
                    InscripcionId = p.InscripcionId,
                    MedioPago = p.MedioPago,
                    Monto = p.Monto,
                    FechaPago = p.FechaPago
                })
                .FirstOrDefaultAsync();

            if (pago == null)
                return NotFound();

            return pago;
        }

        // POST: api/Pago
        [HttpPost]
        public async Task<ActionResult<PagoDTO>> CreatePago(PagoDTO dto)
        {
            var inscripcion = await _context.Inscripciones.FindAsync(dto.InscripcionId);
            if (inscripcion == null)
                return BadRequest(new { error = "Inscripción no encontrada." });

            var pago = new Pago
            {
                InscripcionId = dto.InscripcionId,
                MedioPago = dto.MedioPago,
                Monto = dto.Monto,
                FechaPago = dto.FechaPago
            };

            _context.Pagos.Add(pago);
            await _context.SaveChangesAsync();

            dto.PagoId = pago.PagoId;
            return CreatedAtAction(nameof(GetPago), new { id = pago.PagoId }, dto);
        }

        // PUT: api/Pago/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePago(int id, PagoDTO dto)
        {
            if (id != dto.PagoId)
                return BadRequest();

            var pago = await _context.Pagos.FindAsync(id);
            if (pago == null)
                return NotFound();

            var inscripcion = await _context.Inscripciones.FindAsync(dto.InscripcionId);
            if (inscripcion == null)
                return BadRequest(new { error = "Inscripción no encontrada." });

            pago.InscripcionId = dto.InscripcionId;
            pago.MedioPago = dto.MedioPago;
            pago.Monto = dto.Monto;
            pago.FechaPago = dto.FechaPago;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Pago/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePago(int id)
        {
            var pago = await _context.Pagos.FindAsync(id);
            if (pago == null)
                return NotFound();

            _context.Pagos.Remove(pago);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
