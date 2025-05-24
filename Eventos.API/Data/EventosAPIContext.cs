using Eventos.Models;
using Microsoft.EntityFrameworkCore;

public class EventosAPIContext : DbContext
{
    public EventosAPIContext(DbContextOptions<EventosAPIContext> options)
        : base(options) { }

    public DbSet<Evento> Eventos { get; set; }
    public DbSet<Sesion> Sesiones { get; set; }
    public DbSet<Ponente> Ponentes { get; set; }
    public DbSet<EventoPonente> EventoPonentes { get; set; }
    public DbSet<Participante> Participantes { get; set; }
    public DbSet<Inscripcion> Inscripciones { get; set; }
    public DbSet<Pago> Pagos { get; set; }
    public DbSet<Asistencia> Asistencias { get; set; }
    public DbSet<Certificado> Certificados { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Asistencia>()
            .HasKey(a => new { a.ParticipanteId, a.SesionId });
        modelBuilder.Entity<EventoPonente>()
        .HasKey(ep => new { ep.EventoId, ep.PonenteId });
    }
}