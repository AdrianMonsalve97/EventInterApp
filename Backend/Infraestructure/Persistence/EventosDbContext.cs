using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infraestructure.Persistence
{
    public class EventosDbContext : DbContext
    {
        public EventosDbContext(DbContextOptions<EventosDbContext> options)
        : base(options)
        {
        }
        public DbSet<Evento> Eventos => Set<Evento>();
        public DbSet<Usuario> Usuarios => Set<Usuario>();
        public DbSet<Inscripcion> Inscripciones => Set<Inscripcion>();



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Evento>(entity =>
            {
                entity.ToTable("Eventos");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Nombre)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(e => e.Descripcion)
                      .HasMaxLength(500);

                entity.Property(e => e.Ubicacion)
                      .HasMaxLength(200);

                entity.Property(e => e.CapacidadMaxima)
                      .IsRequired();

                entity.Property(e => e.FechaHora)
                      .IsRequired();
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(u => u.Id);
                entity.Property(u => u.Id).ValueGeneratedNever();
                entity.Property(u => u.NombreUsuario).IsRequired().HasMaxLength(100);
                entity.Property(u => u.Email).IsRequired().HasMaxLength(150);
                entity.Property(u => u.Nombre).IsRequired().HasMaxLength(100);
                entity.Property(u => u.Nombre).IsRequired().HasMaxLength(100);
                entity.Property(u => u.TipoDocumento)
                    .HasConversion(
                     v => v.Codigo,     
                     v => TipoDocumento.FromCodigo(v))
                    .HasMaxLength(5);
                entity.Property(u => u.PasswordHash).IsRequired();
                entity.Property(u => u.PasswordSalt).IsRequired();
                entity.Property(u => u.DebeCambiarPassword).IsRequired();
                entity.Property(u => u.Rol).IsRequired();
            });

            modelBuilder.Entity<Inscripcion>(entity =>
            {
                entity.ToTable("Inscripciones");

                entity.HasKey(i => i.Id);

                entity.Property(i => i.FechaInscripcion)
                      .IsRequired();

                entity.HasOne(i => i.Usuario)
                      .WithMany(u => u.EventosInscritos)
                      .HasForeignKey(i => i.UsuarioId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(i => i.Evento)
                      .WithMany(e => e.Inscripciones)
                      .HasForeignKey(i => i.EventoId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

        }
    }
}
