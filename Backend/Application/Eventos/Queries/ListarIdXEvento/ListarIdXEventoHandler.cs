using Application.Common.DTOs.EventosDtos;
using Infraestructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Eventos.Queries.ListarIdXEvento
{
    public sealed class ListarIdXEventoHandler : IRequestHandler<ListarIdXEventoQuery, List<EventoIdNombreDto>>
    {
        private readonly EventosDbContext _context;

        public ListarIdXEventoHandler(EventosDbContext context)
        {
            _context = context;
        }

        public async Task<List<EventoIdNombreDto>> Handle(ListarIdXEventoQuery request, CancellationToken cancellationToken)
        {
            return await _context.Eventos
                .Select(e => new EventoIdNombreDto(e.Id, e.Nombre))
                .ToListAsync(cancellationToken);
        }
    }
}
