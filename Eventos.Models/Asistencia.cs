using Eventos.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eventos.Models
{
    public class Asistencia
    {
        [ForeignKey("Participante")]
        public int ParticipanteId { get; set; }
        public Participante Participante { get; set; }

        [ForeignKey("Sesion")]
        public int SesionId { get; set; }
        public Sesion Sesion { get; set; }

        public bool Asistio { get; set; } = false;
    }
}