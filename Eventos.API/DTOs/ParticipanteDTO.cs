namespace Eventos.API.DTOs
{
    public class ParticipanteDTO
    {
        public int ParticipanteId { get; set; }
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
    }
}
