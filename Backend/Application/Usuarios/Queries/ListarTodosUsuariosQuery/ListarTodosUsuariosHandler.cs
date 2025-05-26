using Application.Common.DTOs.UsuariosDtos;
using Domain.Entities;
using Domain.Enums;
using Infraestructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Usuarios.Queries.ListarTodosUsuarios;

public sealed class ListarTodosUsuariosHandler : IRequestHandler<ListarTodosUsuariosQuery, List<UsuarioAdminListadoDto>>
{
    private readonly EventosDbContext _context;

    public ListarTodosUsuariosHandler(EventosDbContext context)
    {
        _context = context;
    }

    public async Task<List<UsuarioAdminListadoDto>> Handle(ListarTodosUsuariosQuery request, CancellationToken cancellationToken)
    {
        Usuario? solicitante = await _context.Usuarios.FindAsync(new object[] { request.IdSolicitante }, cancellationToken);
        if (solicitante is null || solicitante.Rol != RolUsuario.Administrador)
            return [];

        return await _context.Usuarios
            .Select(u => new UsuarioAdminListadoDto(
                u.Id,
                u.Nombre,
                u.NombreUsuario,
                u.Email,
                u.Rol.ToString(),
                u.DebeCambiarPassword
            ))
            .ToListAsync(cancellationToken);
    }
}
