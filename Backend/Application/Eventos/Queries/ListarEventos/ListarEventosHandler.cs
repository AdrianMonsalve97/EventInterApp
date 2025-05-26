using Application.Common.DTOs.EventosDtos;
using AutoMapper;
using Domain.Entities;
using Infraestructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Eventos.Queries.ListarEventos;

public sealed class ListarEventosHandler : IRequestHandler<ListarEventosQuery, List<EventoDisponibleDto>>
{
    private readonly EventosDbContext _context;
    private readonly IMapper _mapper;

    public ListarEventosHandler(EventosDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<EventoDisponibleDto>> Handle(ListarEventosQuery request, CancellationToken cancellationToken)
    {
        var eventos = await _context.Eventos
            .Include(e => e.Inscripciones)
            .ToListAsync(cancellationToken);

        List<EventoDisponibleDto> dtoList = _mapper.Map<List<EventoDisponibleDto>>(eventos);


        for (int i = 0; i < eventos.Count; i++)
        {
            bool estaInscrito = eventos[i].Inscripciones.Any(i => i.UsuarioId == request.UsuarioId);
            dtoList[i] = dtoList[i] with { EstaInscrito = estaInscrito };
        }

        return dtoList;
    }


}
