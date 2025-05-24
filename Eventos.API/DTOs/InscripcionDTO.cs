namespace Eventos.API.DTOs
{
    public class InscripcionDTO
    {
        public int InscripcionId { get; set; }
        public int ParticipanteId { get; set; }
        public int EventoId { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string EstadoPago { get; set; }
        public decimal MontoTotal { get; set; }
    }
}
