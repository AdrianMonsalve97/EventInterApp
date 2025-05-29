using Application.Common.DTOs.UsuariosDtos;
using Domain.Entities;
using Infraestructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Usuarios.Queries.ListarAsistentes;

public sealed class ListarAsistentesHandler : IRequestHandler<ListarAsistentesQuery, List<UsuarioListadoDto>>
{
    private readonly EventosDbContext _context;

    public ListarAsistentesHandler(EventosDbContext context)
    {
        _context = context;
    }

    public async Task<List<UsuarioListadoDto>> Handle(ListarAsistentesQuery request, CancellationToken cancellationToken)
    {
        var asistentes = await _context.Inscripciones
            .Where(i => i.EventoId == request.IdEvento)
            .Include(i => i.Usuario)
            .Select(i => new UsuarioListadoDto(
                i.Usuario.Id,
                i.Usuario.Nombre,
                i.Usuario.NombreUsuario,
                i.Usuario.Email,
                i.Usuario.Rol.ToString(),
                i.EventoId
            ))
            .ToListAsync(cancellationToken);

        return asistentes;
    }
}
