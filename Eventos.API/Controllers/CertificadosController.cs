using Eventos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Eventos.API.DTOs;

namespace Eventos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CertificadosController
        : ControllerBase
    {
        private readonly EventosAPIContext _context;
        public CertificadosController(EventosAPIContext context) => _context = context;

        // GET: api/Certificado
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CertificadoDTO>>> GetCertificados()
        {
            return await _context.Certificados
                .Select(c => new CertificadoDTO
                {
                    CertificadoId = c.CertificadoId,
                    InscripcionId = c.InscripcionId,
                    FechaEmision = c.FechaEmision,
                    RutaArchivo = c.RutaArchivo,
                    Estado = c.Estado
                })
                .ToListAsync();
        }

        // GET: api/Certificado/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CertificadoDTO>> GetCertificado(int id)
        {
            var certificado = await _context.Certificados
                .Where(c => c.CertificadoId == id)
                .Select(c => new CertificadoDTO
                {
                    CertificadoId = c.CertificadoId,
                    InscripcionId = c.InscripcionId,
                    FechaEmision = c.FechaEmision,
                    RutaArchivo = c.RutaArchivo,
                    Estado = c.Estado
                })
                .FirstOrDefaultAsync();

            if (certificado == null)
                return NotFound();

            return certificado;
        }

        // POST: api/Certificado
        [HttpPost]
        public async Task<ActionResult<CertificadoDTO>> CreateCertificado(CertificadoDTO dto)
        {
            var inscripcion = await _context.Inscripciones.FindAsync(dto.InscripcionId);
            if (inscripcion == null)
                return BadRequest(new { error = "Inscripción no encontrada." });

            var certificado = new Certificado
            {
                InscripcionId = dto.InscripcionId,
                FechaEmision = dto.FechaEmision,
                RutaArchivo = dto.RutaArchivo,
                Estado = dto.Estado
            };

            _context.Certificados.Add(certificado);
            await _context.SaveChangesAsync();

            dto.CertificadoId = certificado.CertificadoId;
            return CreatedAtAction(nameof(GetCertificado), new { id = certificado.CertificadoId }, dto);
        }

        // PUT: api/Certificado/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCertificado(int id, CertificadoDTO dto)
        {
            if (id != dto.CertificadoId)
                return BadRequest();

            var certificado = await _context.Certificados.FindAsync(id);
            if (certificado == null)
                return NotFound();

            var inscripcion = await _context.Inscripciones.FindAsync(dto.InscripcionId);
            if (inscripcion == null)
                return BadRequest(new { error = "Inscripción no encontrada." });

            certificado.InscripcionId = dto.InscripcionId;
            certificado.FechaEmision = dto.FechaEmision;
            certificado.RutaArchivo = dto.RutaArchivo;
            certificado.Estado = dto.Estado;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Certificado/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCertificado(int id)
        {
            var certificado = await _context.Certificados.FindAsync(id);
            if (certificado == null)
                return NotFound();

            _context.Certificados.Remove(certificado);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
