using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventos.Models
{
    public class Certificado
    {
        [Key]
        public int CertificadoId { get; set; }

        [ForeignKey("Inscripcion")]
        public int InscripcionId { get; set; }
        public Inscripcion Inscripcion { get; set; }

        [Required]
        public DateTime FechaEmision { get; set; } = DateTime.Now;

        [Required]
        [MaxLength(200)]
        public string RutaArchivo { get; set; }

        [Required]
        [MaxLength(20)]
        public string Estado { get; set; } // Emitido, Revocado
    }
}
