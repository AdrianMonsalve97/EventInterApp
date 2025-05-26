using Application.Common.DTOs.InscripcionesDtos;
using Infraestructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Inscripciones.Queries.ListarInscripciones;

public sealed class ListarInscripcionesHandler : IRequestHandler<ListarInscripcionesQuery, List<InscripcionDto>>
{
    private readonly EventosDbContext _context;

    public ListarInscripcionesHandler(EventosDbContext context)
    {
        _context = context;
    }

    public async Task<List<InscripcionDto>> Handle(ListarInscripcionesQuery request, CancellationToken cancellationToken)
    {
        return await _context.Inscripciones
            .Where(i => i.EventoId == request.IdEvento)
            .Select(i => new InscripcionDto(
                i.UsuarioId,
                i.Usuario.Nombre,
                i.Usuario.NombreUsuario,
                i.Usuario.Email,
                i.FechaInscripcion
            ))
            .ToListAsync(cancellationToken);
    }
}
