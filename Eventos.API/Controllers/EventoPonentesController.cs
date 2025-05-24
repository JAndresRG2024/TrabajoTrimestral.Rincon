using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Eventos.API.DTOs;
using Eventos.Models;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Eventos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventoPonentesController : ControllerBase
    {
        private readonly EventosAPIContext _context;
        public EventoPonentesController(EventosAPIContext context) => _context = context;

        // GET: api/EventoPonentes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventoPonenteDTO>>> GetEventoPonentes()
        {
            return await _context.EventoPonentes
                .Select(ep => new EventoPonenteDTO
                {
                    EventoId = ep.EventoId,
                    PonenteId = ep.PonenteId
                })
                .ToListAsync();
        }

        // GET: api/EventoPonentes/5/3
        [HttpGet("{eventoId}/{ponenteId}")]
        public async Task<ActionResult<EventoPonenteDTO>> GetEventoPonente(int eventoId, int ponenteId)
        {
            var eventoPonente = await _context.EventoPonentes
                .Where(ep => ep.EventoId == eventoId && ep.PonenteId == ponenteId)
                .Select(ep => new EventoPonenteDTO
                {
                    EventoId = ep.EventoId,
                    PonenteId = ep.PonenteId
                })
                .FirstOrDefaultAsync();

            if (eventoPonente == null)
                return NotFound();

            return eventoPonente;
        }

        // POST: api/EventoPonentes
        [HttpPost]
        public async Task<ActionResult<EventoPonenteDTO>> CreateEventoPonente(EventoPonenteDTO dto)
        {
            var evento = await _context.Eventos.FindAsync(dto.EventoId);
            var ponente = await _context.Ponentes.FindAsync(dto.PonenteId);

            if (evento == null || ponente == null)
                return BadRequest(new { error = "Evento o Ponente no encontrado." });

            var existe = await _context.EventoPonentes
                .AnyAsync(ep => ep.EventoId == dto.EventoId && ep.PonenteId == dto.PonenteId);

            if (existe)
                return Conflict(new { error = "La relación ya existe." });

            var eventoPonente = new EventoPonente
            {
                EventoId = dto.EventoId,
                PonenteId = dto.PonenteId
            };

            _context.EventoPonentes.Add(eventoPonente);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEventoPonente), new { eventoId = dto.EventoId, ponenteId = dto.PonenteId }, dto);
        }

        // PUT: api/EventoPonentes/5/3
        [HttpPut("{eventoId}/{ponenteId}")]
        public async Task<IActionResult> UpdateEventoPonente(int eventoId, int ponenteId, EventoPonenteDTO dto)
        {
            if (eventoId != dto.EventoId || ponenteId != dto.PonenteId)
                return BadRequest();

            var eventoPonente = await _context.EventoPonentes
                .FirstOrDefaultAsync(ep => ep.EventoId == eventoId && ep.PonenteId == ponenteId);

            if (eventoPonente == null)
                return NotFound();

            // No hay más campos que actualizar, pero aquí podrías agregar lógica si tu modelo crece.

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/EventoPonentes/5/3
        [HttpDelete("{eventoId}/{ponenteId}")]
        public async Task<IActionResult> DeleteEventoPonente(int eventoId, int ponenteId)
        {
            var eventoPonente = await _context.EventoPonentes
                .FirstOrDefaultAsync(ep => ep.EventoId == eventoId && ep.PonenteId == ponenteId);

            if (eventoPonente == null)
                return NotFound();

            _context.EventoPonentes.Remove(eventoPonente);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
