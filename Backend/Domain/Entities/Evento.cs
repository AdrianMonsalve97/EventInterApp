namespace Domain.Entities;

public class Evento
{
    public int Id { get; set; }
    required public string Nombre { get; set; }
    required public string Descripcion { get; set; }
    public DateTime FechaHora { get; set; }
    required public string Ubicacion { get; set; }
    public int CapacidadMaxima { get; set; }
    public ICollection<Inscripcion> Inscripciones { get; set; } = new List<Inscripcion>();
}
