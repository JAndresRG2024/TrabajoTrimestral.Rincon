using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventos.Models
{
    public class Inscripcion
    {
        [Key]
        public int InscripcionId { get; set; }

        [ForeignKey("Participante")]
        public int ParticipanteId { get; set; }
        public Participante Participante { get; set; }

        [ForeignKey("Evento")]
        public int EventoId { get; set; }
        public Evento Evento { get; set; }

        [Required]
        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        [Required]
        [MaxLength(20)]
        public string EstadoPago { get; set; }   // Pendiente, Pagado, Cancelado
        public int MontoTotal { get; set; } = 0;

        public ICollection<Pago> Pagos { get; set; } = new List<Pago>();
        public ICollection<Certificado> Certificados { get; set; } = new List<Certificado>();
    }
}
