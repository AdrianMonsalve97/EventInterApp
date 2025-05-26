using MediatR;
using Shared.Contracts.Genericos;

namespace Application.Inscripciones.Commands.EliminarInscripcion;

public sealed record EliminarInscripcionCommand(
    int IdEvento,
    int IdUsuario
) : IRequest<RespuestaGeneral<string>>;
