namespace Application.Eventos.Queries.DetalleEvento;

public class DetalleEventoDto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string? Descripcion { get; set; }
    public DateTime FechaHora { get; set; }
    public string Ubicacion { get; set; } = string.Empty;
    public int CapacidadMaxima { get; set; }
    public List<AsistenteDto> Asistentes { get; set; } = new();
}

public class AsistenteDto
{
    public string Nombre { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}
