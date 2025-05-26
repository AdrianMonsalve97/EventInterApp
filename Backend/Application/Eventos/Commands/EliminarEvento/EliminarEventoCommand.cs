using MediatR;
using Shared.Contracts.Genericos;

namespace Application.Eventos.Commands.EliminarEvento;

public sealed record EliminarEventoCommand(
    int EventoId,
    int UsuarioId
) : IRequest<RespuestaGeneral<string>>;
