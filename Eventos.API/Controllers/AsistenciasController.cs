using Eventos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Eventos.API.DTOs;

namespace Eventos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AsistenciasController : ControllerBase
    {
        private readonly EventosAPIContext _context;
        public AsistenciasController(EventosAPIContext context) => _context = context;

        // GET: api/Asistencia
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AsistenciaDTO>>> GetAsistencias()
        {
            return await _context.Asistencias
                .Select(a => new AsistenciaDTO
                {
                    ParticipanteId = a.ParticipanteId,
                    SesionId = a.SesionId,
                    Asistio = a.Asistio
                })
                .ToListAsync();
        }

        // GET: api/Asistencia/5/3
        [HttpGet("{participanteId}/{sesionId}")]
        public async Task<ActionResult<AsistenciaDTO>> GetAsistencia(int participanteId, int sesionId)
        {
            var asistencia = await _context.Asistencias
                .Where(a => a.ParticipanteId == participanteId && a.SesionId == sesionId)
                .Select(a => new AsistenciaDTO
                {
                    ParticipanteId = a.ParticipanteId,
                    SesionId = a.SesionId,
                    Asistio = a.Asistio
                })
                .FirstOrDefaultAsync();

            if (asistencia == null)
                return NotFound();

            return asistencia;
        }

        // POST: api/Asistencia
        [HttpPost]
        public async Task<ActionResult<AsistenciaDTO>> CreateAsistencia(AsistenciaDTO dto)
        {
            var participante = await _context.Participantes.FindAsync(dto.ParticipanteId);
            var sesion = await _context.Sesiones.FindAsync(dto.SesionId);

            if (participante == null || sesion == null)
                return BadRequest(new { error = "Participante o Sesión no encontrada." });

            var asistencia = new Asistencia
            {
                ParticipanteId = dto.ParticipanteId,
                SesionId = dto.SesionId,
                Asistio = dto.Asistio
            };

            _context.Asistencias.Add(asistencia);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAsistencia), new { participanteId = dto.ParticipanteId, sesionId = dto.SesionId }, dto);
        }

        // PUT: api/Asistencia/5/3
        [HttpPut("{participanteId}/{sesionId}")]
        public async Task<IActionResult> UpdateAsistencia(int participanteId, int sesionId, AsistenciaDTO dto)
        {
            if (participanteId != dto.ParticipanteId || sesionId != dto.SesionId)
                return BadRequest();

            var asistencia = await _context.Asistencias
                .FirstOrDefaultAsync(a => a.ParticipanteId == participanteId && a.SesionId == sesionId);

            if (asistencia == null)
                return NotFound();

            asistencia.Asistio = dto.Asistio;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Asistencia/5/3
        [HttpDelete("{participanteId}/{sesionId}")]
        public async Task<IActionResult> DeleteAsistencia(int participanteId, int sesionId)
        {
            var asistencia = await _context.Asistencias
                .FirstOrDefaultAsync(a => a.ParticipanteId == participanteId && a.SesionId == sesionId);

            if (asistencia == null)
                return NotFound();

            _context.Asistencias.Remove(asistencia);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
