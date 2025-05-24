namespace Eventos.API.DTOs
{
    public class CertificadoDTO
    {
        public int CertificadoId { get; set; }
        public int InscripcionId { get; set; }
        public DateTime FechaEmision { get; set; }
        public string RutaArchivo { get; set; }
        public string Estado { get; set; }
    }
}