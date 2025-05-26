using Infraestructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Contracts.Genericos;
using Shared.Helpers;
using Shared.Helppers;

namespace Application.Inscripciones.Commands.EliminarInscripcion;

public sealed class EliminarInscripcionHandler : IRequestHandler<EliminarInscripcionCommand, RespuestaGeneral<string>>
{
    private readonly EventosDbContext _context;

    public EliminarInscripcionHandler(EventosDbContext context)
    {
        _context = context;
    }

    public async Task<RespuestaGeneral<string>> Handle(EliminarInscripcionCommand request, CancellationToken cancellationToken)
    {
        var inscripcion = await _context.Inscripciones
            .FirstOrDefaultAsync(i => i.EventoId == request.IdEvento && i.UsuarioId == request.IdUsuario, cancellationToken);

        if (inscripcion is null)
            return RespuestaHelper.Error<string>("No estás inscrito en este evento.");

        _context.Inscripciones.Remove(inscripcion);
        await _context.SaveChangesAsync(cancellationToken);

        return RespuestaHelper.Exito("Inscripción cancelada correctamente.");
    }
}
