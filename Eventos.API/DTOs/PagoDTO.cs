namespace Eventos.API.DTOs
{
    public class PagoDTO
    {
        public int PagoId { get; set; }
        public int InscripcionId { get; set; }
        public string MedioPago { get; set; }
        public decimal Monto { get; set; }
        public DateTime FechaPago { get; set; }
    }
}
