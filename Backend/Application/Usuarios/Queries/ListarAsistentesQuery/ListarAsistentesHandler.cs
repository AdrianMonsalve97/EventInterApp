using Application.Common.DTOs.UsuariosDtos;
using Domain.Entities;
using Domain.Enums;
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
        Usuario? solicitante = await _context.Usuarios.FindAsync(new object[] { request.IdSolicitante }, cancellationToken);
        if (solicitante is null) return [];

        IQueryable<Usuario> query = solicitante.Rol == RolUsuario.Administrador
            ? _context.Usuarios
            : _context.Usuarios.Where(u => u.Rol == RolUsuario.Asistente);

        return await query
            .Select(u => new UsuarioListadoDto(
                u.Id,
                u.Nombre,
                u.NombreUsuario,
                u.Email,
                u.Rol.ToString()
            ))
            .ToListAsync(cancellationToken);
    }
}
