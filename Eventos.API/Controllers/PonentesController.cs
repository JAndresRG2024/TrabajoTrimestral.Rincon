using Eventos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Eventos.API.DTOs;

namespace Eventos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PonentesController : ControllerBase
    {
        private readonly EventosAPIContext _context;
        public PonentesController(EventosAPIContext context) => _context = context;

        // GET: api/Ponente
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PonenteDTO>>> GetPonentes()
        {
            return await _context.Ponentes
                .Select(p => new PonenteDTO
                {
                    PonenteId = p.PonenteId,
                    Nombre = p.Nombre,
                    Correo = p.Correo,
                    Telefono = p.Telefono
                })
                .ToListAsync();
        }

        // GET: api/Ponente/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PonenteDTO>> GetPonente(int id)
        {
            var ponente = await _context.Ponentes
                .Where(p => p.PonenteId == id)
                .Select(p => new PonenteDTO
                {
                    PonenteId = p.PonenteId,
                    Nombre = p.Nombre,
                    Correo = p.Correo,
                    Telefono = p.Telefono
                })
                .FirstOrDefaultAsync();

            if (ponente == null)
                return NotFound();

            return ponente;
        }

        // POST: api/Ponente
        [HttpPost]
        public async Task<ActionResult<PonenteDTO>> CreatePonente(PonenteDTO dto)
        {
            var ponente = new Ponente
            {
                Nombre = dto.Nombre,
                Correo = dto.Correo,
                Telefono = dto.Telefono
            };

            _context.Ponentes.Add(ponente);
            await _context.SaveChangesAsync();

            dto.PonenteId = ponente.PonenteId;
            return CreatedAtAction(nameof(GetPonente), new { id = ponente.PonenteId }, dto);
        }

        // PUT: api/Ponente/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePonente(int id, PonenteDTO dto)
        {
            if (id != dto.PonenteId)
                return BadRequest();

            var ponente = await _context.Ponentes.FindAsync(id);
            if (ponente == null)
                return NotFound();

            ponente.Nombre = dto.Nombre;
            ponente.Correo = dto.Correo;
            ponente.Telefono = dto.Telefono;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Ponente/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePonente(int id)
        {
            var ponente = await _context.Ponentes.FindAsync(id);
            if (ponente == null)
                return NotFound();

            _context.Ponentes.Remove(ponente);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
