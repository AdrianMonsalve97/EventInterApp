using Application.Common.DTOs.EventosDtos;
using Infraestructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Eventos.Queries.ListarPorUsuario;

public sealed class ListarEventosPorUsuarioHandler : IRequestHandler<ListarEventosPorUsuarioQuery, List<EventoDisponibleDto>>
{
    private readonly EventosDbContext _context;

    public ListarEventosPorUsuarioHandler(EventosDbContext context)
    {
        _context = context;
    }

    public async Task<List<EventoDisponibleDto>> Handle(ListarEventosPorUsuarioQuery request, CancellationToken cancellationToken)
    {
        var eventos = await _context.Inscripciones
            .Where(i => i.UsuarioId == request.IdUsuario)
            .Include(i => i.Evento)
            .Select(i => new EventoDisponibleDto(
                i.Evento.Id,
                i.Evento.Nombre,
                i.Evento.Descripcion,
                i.Evento.FechaHora,
                i.Evento.Ubicacion,
                i.Evento.CapacidadMaxima,
                _context.Inscripciones.Count(x => x.EventoId == i.EventoId),
                true
            ))
            .ToListAsync(cancellationToken);

        return eventos;
    }
}
