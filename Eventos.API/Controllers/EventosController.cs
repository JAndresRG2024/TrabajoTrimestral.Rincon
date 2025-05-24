using Eventos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Eventos.API.DTOs;

namespace Eventos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventosController : ControllerBase
    {
        private readonly EventosAPIContext _context;
        public EventosController(EventosAPIContext context) => _context = context;

        // GET: api/Evento
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventoDTO>>> GetEventos()
        {
            return await _context.Eventos
                .Select(e => new EventoDTO
                {
                    EventoId = e.EventoId,
                    Nombre = e.Nombre,
                    Fecha = e.Fecha,
                    Lugar = e.Lugar,
                    Tipo = e.Tipo
                })
                .ToListAsync();
        }

        // GET: api/Evento/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EventoDTO>> GetEvento(int id)
        {
            var evento = await _context.Eventos
                .Where(e => e.EventoId == id)
                .Select(e => new EventoDTO
                {
                    EventoId = e.EventoId,
                    Nombre = e.Nombre,
                    Fecha = e.Fecha,
                    Lugar = e.Lugar,
                    Tipo = e.Tipo
                })
                .FirstOrDefaultAsync();

            if (evento == null)
                return NotFound();

            return evento;
        }

        // POST: api/Evento
        [HttpPost]
        public async Task<ActionResult<EventoDTO>> CreateEvento(EventoDTO dto)
        {
            var evento = new Evento
            {
                Nombre = dto.Nombre,
                Fecha = dto.Fecha,
                Lugar = dto.Lugar,
                Tipo = dto.Tipo
            };

            _context.Eventos.Add(evento);
            await _context.SaveChangesAsync();

            dto.EventoId = evento.EventoId;
            return CreatedAtAction(nameof(GetEvento), new { id = evento.EventoId }, dto);
        }

        // PUT: api/Evento/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEvento(int id, EventoDTO dto)
        {
            if (id != dto.EventoId)
                return BadRequest();

            var evento = await _context.Eventos.FindAsync(id);
            if (evento == null)
                return NotFound();

            evento.Nombre = dto.Nombre;
            evento.Fecha = dto.Fecha;
            evento.Lugar = dto.Lugar;
            evento.Tipo = dto.Tipo;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Evento/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvento(int id)
        {
            var evento = await _context.Eventos.FindAsync(id);
            if (evento == null)
                return NotFound();

            _context.Eventos.Remove(evento);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}