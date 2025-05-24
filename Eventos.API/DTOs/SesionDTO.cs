namespace Eventos.API.DTOs
{
    public class SesionDTO
    {
        public int SesionId { get; set; }
        public int EventoId { get; set; }
        public string Nombre { get; set; }
        public DateTime HorarioInicio { get; set; }
        public DateTime HorarioFin { get; set; }
        public string SalaAsignada { get; set; }
    }
}