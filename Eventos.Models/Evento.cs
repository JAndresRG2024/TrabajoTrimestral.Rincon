using System.ComponentModel.DataAnnotations;

namespace Eventos.Models
{
    public class Evento
    {
        [Key]
        public int EventoId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        [Required]
        [MaxLength(100)]
        public string Lugar { get; set; }

        [Required]
        [MaxLength(20)]
        public string Tipo { get; set; }    // Taller, Conferencia, Webinar, etc.

        // Navegación
        public ICollection<Sesion> Sesiones { get; set; } = new List<Sesion>();
        public ICollection<Inscripcion> Inscripciones { get; set; } = new List<Inscripcion>();
        public ICollection<EventoPonente> EventoPonentes { get; set; } = new List<EventoPonente>();
    }
}
