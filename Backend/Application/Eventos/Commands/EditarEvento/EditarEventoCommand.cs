using MediatR;
using Shared.Contracts.Genericos;

namespace Application.Eventos.Commands.EditarEvento;

public sealed record EditarEventoCommand(
    int IdEvento,
    DateTime NuevaFechaHora,
    string NuevaUbicacion,
    int NuevaCapacidadMaxima,
    int IdUsuario
) : IRequest<RespuestaGeneral<string>>;
