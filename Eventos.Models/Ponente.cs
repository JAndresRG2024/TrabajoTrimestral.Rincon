using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventos.Models
{
    public class Ponente
    {
        [Key]
        public int PonenteId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; }

        [MaxLength(100)]
        public string Correo { get; set; }

        [MaxLength(20)]
        public string Telefono { get; set; }

        // Relación M-N con Eventos
        public ICollection<EventoPonente> EventoPonentes { get; set; } = new List<EventoPonente>();
    }
}
