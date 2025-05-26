namespace Domain.Entities;

public class Evento
{
    public int Id { get; set; }
    public required string Nombre { get; set; }
    public required string Descripcion { get; set; }
    public DateTime FechaHora { get; set; }
    public required string Ubicacion { get; set; }
    public int CapacidadMaxima { get; set; }
    public ICollection<Inscripcion> Inscripciones { get; set; }
    public int IdCreador { get; set; } 
}
