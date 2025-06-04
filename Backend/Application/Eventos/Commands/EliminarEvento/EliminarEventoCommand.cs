using MediatR;
using Shared.Contracts.Genericos;

namespace Application.Eventos.Commands.EliminarEvento;

public sealed record EliminarEventoCommand(
    int idEvento,
    int idUsuario
) : IRequest<RespuestaGeneral<string>>;
