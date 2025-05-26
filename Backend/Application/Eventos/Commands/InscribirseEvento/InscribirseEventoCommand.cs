using MediatR;
using Shared.Contracts.Genericos;

namespace Application.Eventos.Commands.InscribirseEvento;

public sealed record InscribirseEventoCommand(
    int IdEvento,
    int IdUsuario
) : IRequest<RespuestaGeneral<string>>;
