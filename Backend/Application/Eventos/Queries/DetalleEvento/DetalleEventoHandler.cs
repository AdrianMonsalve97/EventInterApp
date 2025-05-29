
using AutoMapper;
using Domain.Entities;
using Infraestructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Eventos.Queries.DetalleEvento;

public class DetalleEventoQueryHandler : IRequestHandler<DetalleEventoQuery, DetalleEventoDto>
{
    private readonly EventosDbContext _context;
    private readonly IMapper _mapper;

    public DetalleEventoQueryHandler(EventosDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<DetalleEventoDto> Handle(DetalleEventoQuery request, CancellationToken cancellationToken)
    {
        Evento? evento = await _context.Eventos
            .Include(e => e.Inscripciones)
                .ThenInclude(i => i.Usuario)
            .FirstOrDefaultAsync(e => e.Id == request.IdEvento, cancellationToken);

        if (evento is null)
            throw new KeyNotFoundException("Evento no encontrado");

        return new DetalleEventoDto
        {
            Id = evento.Id,
            Nombre = evento.Nombre,
            Descripcion = evento.Descripcion,
            FechaHora = evento.FechaHora,
            Ubicacion = evento.Ubicacion,
            CapacidadMaxima = evento.CapacidadMaxima,
            Asistentes = evento.Inscripciones.Select(i => new AsistenteDto
            {
                Nombre = i.Usuario.Nombre,
                Email = i.Usuario.Email
            }).ToList()
        };
    }
}
