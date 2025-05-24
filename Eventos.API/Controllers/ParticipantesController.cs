using Eventos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Eventos.API.DTOs;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Eventos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParticipantesController : ControllerBase
    {
        private readonly EventosAPIContext _context;
        public ParticipantesController(EventosAPIContext context) => _context = context;

        // GET: api/Participante
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ParticipanteDTO>>> GetParticipantes()
        {
            return await _context.Participantes
                .Select(p => new ParticipanteDTO
                {
                    ParticipanteId = p.ParticipanteId,
                    Cedula = p.Cedula,
                    Nombre = p.Nombre,
                    Correo = p.Correo,
                    Telefono = p.Telefono
                })
                .ToListAsync();
        }

        // GET: api/Participante/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ParticipanteDTO>> GetParticipante(int id)
        {
            var participante = await _context.Participantes
                .Where(p => p.ParticipanteId == id)
                .Select(p => new ParticipanteDTO
                {
                    ParticipanteId = p.ParticipanteId,
                    Cedula = p.Cedula,
                    Nombre = p.Nombre,
                    Correo = p.Correo,
                    Telefono = p.Telefono
                })
                .FirstOrDefaultAsync();

            if (participante == null)
                return NotFound();

            return participante;
        }

        // POST: api/Participante
        [HttpPost]
        public async Task<ActionResult<ParticipanteDTO>> CreateParticipante(ParticipanteDTO dto)
        {
            var participante = new Participante
            {
                Cedula = dto.Cedula,
                Nombre = dto.Nombre,
                Correo = dto.Correo,
                Telefono = dto.Telefono
            };

            _context.Participantes.Add(participante);
            await _context.SaveChangesAsync();

            dto.ParticipanteId = participante.ParticipanteId;
            return CreatedAtAction(nameof(GetParticipante), new { id = participante.ParticipanteId }, dto);
        }

        // PUT: api/Participante/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateParticipante(int id, ParticipanteDTO dto)
        {
            if (id != dto.ParticipanteId)
                return BadRequest();

            var participante = await _context.Participantes.FindAsync(id);
            if (participante == null)
                return NotFound();

            participante.Cedula = dto.Cedula;
            participante.Nombre = dto.Nombre;
            participante.Correo = dto.Correo;
            participante.Telefono = dto.Telefono;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParticipante(int id)
        {
            if (await _context.Participantes.FindAsync(id) is Participante participante)
            {
                _context.Participantes.Remove(participante);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            return NotFound();
        }
    }
}
