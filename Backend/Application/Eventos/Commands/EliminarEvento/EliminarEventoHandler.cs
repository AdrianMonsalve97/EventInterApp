using Domain.Entities;
using Infraestructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Contracts.Genericos;
using Shared.Helpers;
using Shared.Helppers;

namespace Application.Eventos.Commands.EliminarEvento;

public sealed class EliminarEventoHandler : IRequestHandler<EliminarEventoCommand, RespuestaGeneral<string>>
{
    private readonly EventosDbContext _context;

    public EliminarEventoHandler(EventosDbContext context)
    {
        _context = context;
    }

    public async Task<RespuestaGeneral<string>> Handle(EliminarEventoCommand request, CancellationToken cancellationToken)
    {
        Evento? evento = await _context.Eventos
            .FirstOrDefaultAsync(e => e.Id == request.idEvento, cancellationToken);

        if (evento is null)
            return RespuestaHelper.Error<string>("El evento no existe.");

        Usuario? solicitante = await _context.Usuarios
            .FirstOrDefaultAsync(u => u.Id == request.idUsuario, cancellationToken);

        if (solicitante is null)
            return RespuestaHelper.Error<string>("Usuario no encontrado.");

        bool esCreador = evento.IdCreador == request.idUsuario;
        bool esAdmin = solicitante.Rol == Domain.Enums.RolUsuario.Administrador;

        if (!esCreador && !esAdmin)
            return RespuestaHelper.Error<string>("Solo el creador o un administrador pueden eliminar este evento.");


        bool tieneInscritos = await _context.Inscripciones
            .AnyAsync(i => i.EventoId == request.idEvento, cancellationToken);

        if (tieneInscritos)
            return RespuestaHelper.Error<string>("No se puede eliminar un evento con asistentes inscritos.");

        _context.Eventos.Remove(evento);
        await _context.SaveChangesAsync(cancellationToken);

        return RespuestaHelper.Exito("Evento eliminado correctamente.");
    }
}
