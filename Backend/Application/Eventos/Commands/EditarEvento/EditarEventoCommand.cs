using MediatR;
using Shared.Contracts.Genericos;

namespace Application.Eventos.Commands.EditarEvento;

public sealed record EditarEventoCommand : IRequest<RespuestaGeneral<string>>
{
    public int IdEvento { get; init; }
    public DateTime NuevaFechaHora { get; init; }
    public string NuevaUbicacion { get; init; } = string.Empty;
    public int NuevaCapacidadMaxima { get; init; }
    public int IdUsuario { get; init; }
}
