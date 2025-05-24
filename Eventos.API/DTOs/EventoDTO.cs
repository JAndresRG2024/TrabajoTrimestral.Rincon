namespace Eventos.API.DTOs
{
    public class EventoDTO
    {
        public int EventoId { get; set; }
        public string Nombre { get; set; }
        public DateTime Fecha { get; set; }
        public string Lugar { get; set; }
        public string Tipo { get; set; }
    }
}