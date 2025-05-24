using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventos.Models
{
    public class Pago
    {
        [Key]
        public int PagoId { get; set; }

        [ForeignKey("Inscripcion")]
        public int InscripcionId { get; set; }
        public Inscripcion Inscripcion { get; set; }

        [Required]
        [MaxLength(30)]
        public string MedioPago { get; set; }    // Tarjeta, Transferencia, Efectivo, etc.

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Monto { get; set; }

        [Required]
        public DateTime FechaPago { get; set; } = DateTime.Now;
    }
}
