using Eventos.Models;
using Eventos.API.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace Eventos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InscripcionesController : ControllerBase
    {
        private readonly EventosAPIContext _context;

        public InscripcionesController(EventosAPIContext context)
        {
            _context = context;
        }

        // GET: api/Inscripciones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InscripcionDTO>>> GetInscripciones()
        {
            var inscripciones = await _context.Inscripciones
                .Select(i => new InscripcionDTO
                {
                    InscripcionId = i.InscripcionId,
                    ParticipanteId = i.ParticipanteId,
                    EventoId = i.EventoId,
                    FechaRegistro = i.FechaRegistro,
                    EstadoPago = i.EstadoPago,
                    MontoTotal = i.MontoTotal
                })
                .ToListAsync();

            return inscripciones;
        }

        // GET: api/Inscripciones/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InscripcionDTO>> GetInscripcion(int id)
        {
            var inscripcion = await _context.Inscripciones
                .Where(i => i.InscripcionId == id)
                .Select(i => new InscripcionDTO
                {
                    ParticipanteId = i.ParticipanteId,
                    EventoId = i.EventoId,
                    FechaRegistro = i.FechaRegistro,
                    EstadoPago = i.EstadoPago,
                    MontoTotal = i.MontoTotal
                })
                .FirstOrDefaultAsync();

            if (inscripcion == null)
                return NotFound();

            return inscripcion;
        }

        // POST: api/Inscripciones
        [HttpPost]
        public async Task<ActionResult<InscripcionDTO>> CreateInscripcion(InscripcionDTO dto)
        {
            var participante = await _context.Participantes.FindAsync(dto.ParticipanteId);
            var evento = await _context.Eventos.FindAsync(dto.EventoId);

            if (participante == null || evento == null)
            {
                return BadRequest(new
                {
                    error = "Participante o Evento no encontrado."
                });
            }

            var inscripcion = new Inscripcion
            {
                ParticipanteId = dto.ParticipanteId,
                EventoId = dto.EventoId,
                FechaRegistro = dto.FechaRegistro.Kind switch
                {
                    DateTimeKind.Unspecified => DateTime.SpecifyKind(dto.FechaRegistro, DateTimeKind.Utc),
                    DateTimeKind.Local => dto.FechaRegistro.ToUniversalTime(),
                    _ => dto.FechaRegistro
                },
                EstadoPago = dto.EstadoPago,
                MontoTotal = (int)dto.MontoTotal
            };

            _context.Inscripciones.Add(inscripcion);
            await _context.SaveChangesAsync();

            // Devolver el DTO creado
            var resultDto = new InscripcionDTO
            {
                ParticipanteId = inscripcion.ParticipanteId,
                EventoId = inscripcion.EventoId,
                FechaRegistro = inscripcion.FechaRegistro,
                EstadoPago = inscripcion.EstadoPago,
                MontoTotal = inscripcion.MontoTotal
            };

            return CreatedAtAction(nameof(GetInscripcion), new { id = inscripcion.InscripcionId }, resultDto);
        }

        // PUT: api/Inscripciones/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInscripcion(int id, InscripcionDTO dto)
        {
            // Buscar la inscripción existente
            var inscripcion = await _context.Inscripciones.FindAsync(id);
            if (inscripcion == null)
                return NotFound();

            // Validar existencia de participante y evento
            var participante = await _context.Participantes.FindAsync(dto.ParticipanteId);
            var evento = await _context.Eventos.FindAsync(dto.EventoId);
            if (participante == null || evento == null)
                return BadRequest(new { error = "Participante o Evento no encontrado." });

            // Actualizar los campos
            inscripcion.ParticipanteId = dto.ParticipanteId;
            inscripcion.EventoId = dto.EventoId;
            inscripcion.FechaRegistro = dto.FechaRegistro.Kind switch
            {
                DateTimeKind.Unspecified => DateTime.SpecifyKind(dto.FechaRegistro, DateTimeKind.Utc),
                DateTimeKind.Local => dto.FechaRegistro.ToUniversalTime(),
                _ => dto.FechaRegistro
            };
            inscripcion.EstadoPago = dto.EstadoPago;
            inscripcion.MontoTotal = (int)dto.MontoTotal;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InscripcionExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/Inscripciones/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInscripcion(int id)
        {
            var inscripcion = await _context.Inscripciones.FindAsync(id);
            if (inscripcion == null)
                return NotFound();

            _context.Inscripciones.Remove(inscripcion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InscripcionExists(int id)
        {
            return _context.Inscripciones.Any(e => e.InscripcionId == id);
        }
    }
}
