using Domain.Entities;
using Infraestructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Contracts.Genericos;
using Shared.Helpers;
using Shared.Helppers;

namespace Application.Eventos.Commands.EditarEvento;

public sealed class EditarEventoHandler : IRequestHandler<EditarEventoCommand, RespuestaGeneral<string>>
{
    private readonly EventosDbContext _context;

    public EditarEventoHandler(EventosDbContext context)
    {
        _context = context;
    }

    public async Task<RespuestaGeneral<string>> Handle(EditarEventoCommand request, CancellationToken cancellationToken)
    {
        Evento? evento = await _context.Eventos
            .FirstOrDefaultAsync(e => e.Id == request.IdEvento, cancellationToken);

        if (evento is null)
            return RespuestaHelper.Error<string>("El evento no existe.");

        if (evento.IdCreador != request.IdUsuario)
            return RespuestaHelper.Error<string>("Solo el creador puede editar este evento.");

        evento.FechaHora = request.NuevaFechaHora;
        evento.Ubicacion = request.NuevaUbicacion;
        evento.CapacidadMaxima = request.NuevaCapacidadMaxima;

        await _context.SaveChangesAsync(cancellationToken);

        return RespuestaHelper.Exito("Evento actualizado correctamente.");
    }
}
