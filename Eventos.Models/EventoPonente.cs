using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eventos.Models
{
    public class EventoPonente
    {
        [ForeignKey("Evento")]
        public int EventoId { get; set; }
        public Evento Evento { get; set; }

        [ForeignKey("Ponente")]
        public int PonenteId { get; set; }
        public Ponente Ponente { get; set; }
    }
}