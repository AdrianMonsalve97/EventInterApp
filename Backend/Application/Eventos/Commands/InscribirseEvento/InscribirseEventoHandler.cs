using Domain.Entities;
using Infraestructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Contracts.Genericos;
using Shared.Helpers;
using Shared.Helppers;

namespace Application.Eventos.Commands.InscribirseEvento;

public sealed class InscribirseEventoHandler : IRequestHandler<InscribirseEventoCommand, RespuestaGeneral<string>>
{
    private readonly EventosDbContext _context;

    public InscribirseEventoHandler(EventosDbContext context)
    {
        _context = context;
    }

    public async Task<RespuestaGeneral<string>> Handle(InscribirseEventoCommand request, CancellationToken cancellationToken)
    {
        var todosLosEventos = await _context.Eventos.ToListAsync(cancellationToken);

        Evento? evento = await _context.Eventos
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(e => e.Id == request.IdEvento, cancellationToken);


        if (evento is null)
            return RespuestaHelper.Error<string>("El evento no existe.");

        if (evento.IdCreador == request.IdUsuario)
            return RespuestaHelper.Error<string>("No puedes inscribirte a tu propio evento.");

        bool yaInscrito = await _context.Inscripciones
            .AnyAsync(i => i.EventoId == request.IdEvento && i.UsuarioId == request.IdUsuario, cancellationToken);

        if (yaInscrito)
            return RespuestaHelper.Error<string>("Ya estás inscrito en este evento.");

        int totalInscritos = await _context.Inscripciones
            .CountAsync(i => i.EventoId == request.IdEvento, cancellationToken);

        if (totalInscritos >= evento.CapacidadMaxima)
            return RespuestaHelper.Error<string>("El evento ya alcanzó su capacidad máxima.");

        int eventosUsuario = await _context.Inscripciones
            .CountAsync(i => i.UsuarioId == request.IdUsuario, cancellationToken);

        if (eventosUsuario >= 3)
            return RespuestaHelper.Error<string>("Ya estás inscrito en el máximo de 3 eventos permitidos.");

        Inscripcion nueva = new Inscripcion
        {
            UsuarioId = request.IdUsuario,
            EventoId = request.IdEvento,
            FechaInscripcion = DateTime.UtcNow
        };

        _context.Inscripciones.Add(nueva);
        await _context.SaveChangesAsync(cancellationToken);

        return RespuestaHelper.Exito("Inscripción realizada correctamente.");
    }
}
