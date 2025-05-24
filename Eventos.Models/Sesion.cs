using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventos.Models
{
    public class Sesion
    {
        [Key]
        public int SesionId { get; set; }

        [ForeignKey("Evento")]
        public int EventoId { get; set; }
        public Evento Evento { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; }

        [Required]
        public DateTime HorarioInicio { get; set; }

        [Required]
        public DateTime HorarioFin { get; set; }

        [Required]
        [MaxLength(50)]
        public string SalaAsignada { get; set; }

        public ICollection<Asistencia> Asistencias { get; set; } = new List<Asistencia>();
    }
}
