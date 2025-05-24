using Eventos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Eventos.API.DTOs;

namespace Eventos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SesionesController : ControllerBase
    {
        private readonly EventosAPIContext _context;
        public SesionesController(EventosAPIContext context) => _context = context;

        // GET: api/Sesion
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SesionDTO>>> GetSesiones()
        {
            return await _context.Sesiones
                .Select(s => new SesionDTO
                {
                    SesionId = s.SesionId,
                    EventoId = s.EventoId,
                    Nombre = s.Nombre,
                    HorarioInicio = s.HorarioInicio,
                    HorarioFin = s.HorarioFin,
                    SalaAsignada = s.SalaAsignada
                })
                .ToListAsync();
        }

        // GET: api/Sesion/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SesionDTO>> GetSesion(int id)
        {
            var sesion = await _context.Sesiones
                .Where(s => s.SesionId == id)
                .Select(s => new SesionDTO
                {
                    SesionId = s.SesionId,
                    EventoId = s.EventoId,
                    Nombre = s.Nombre,
                    HorarioInicio = s.HorarioInicio,
                    HorarioFin = s.HorarioFin,
                    SalaAsignada = s.SalaAsignada
                })
                .FirstOrDefaultAsync();

            if (sesion == null)
                return NotFound();

            return sesion;
        }

        // POST: api/Sesion
        [HttpPost]
        public async Task<ActionResult<SesionDTO>> CreateSesion(SesionDTO dto)
        {
            var evento = await _context.Eventos.FindAsync(dto.EventoId);
            if (evento == null)
                return BadRequest(new { error = "Evento no encontrado." });

            var sesion = new Sesion
            {
                EventoId = dto.EventoId,
                Nombre = dto.Nombre,
                HorarioInicio = dto.HorarioInicio,
                HorarioFin = dto.HorarioFin,
                SalaAsignada = dto.SalaAsignada
            };

            _context.Sesiones.Add(sesion);
            await _context.SaveChangesAsync();

            dto.SesionId = sesion.SesionId;
            return CreatedAtAction(nameof(GetSesion), new { id = sesion.SesionId }, dto);
        }

        // PUT: api/Sesion/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSesion(int id, SesionDTO dto)
        {
            if (id != dto.SesionId)
                return BadRequest();

            var sesion = await _context.Sesiones.FindAsync(id);
            if (sesion == null)
                return NotFound();

            var evento = await _context.Eventos.FindAsync(dto.EventoId);
            if (evento == null)
                return BadRequest(new { error = "Evento no encontrado." });

            sesion.EventoId = dto.EventoId;
            sesion.Nombre = dto.Nombre;
            sesion.HorarioInicio = dto.HorarioInicio;
            sesion.HorarioFin = dto.HorarioFin;
            sesion.SalaAsignada = dto.SalaAsignada;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Sesion/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSesion(int id)
        {
            var sesion = await _context.Sesiones.FindAsync(id);
            if (sesion == null)
                return NotFound();

            _context.Sesiones.Remove(sesion);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
