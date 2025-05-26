using Domain.Entities;
using Domain.Enums;
using Infraestructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Contracts.Genericos;
using Shared.Helpers;
using Shared.Helppers;

namespace Application.Usuarios.Commands.ActualizarUsuario;

public sealed class ActualizarUsuarioHandler : IRequestHandler<ActualizarUsuarioCommand, RespuestaGeneral<string>>
{
    private readonly EventosDbContext _context;

    public ActualizarUsuarioHandler(EventosDbContext context)
    {
        _context = context;
    }

    public async Task<RespuestaGeneral<string>> Handle(ActualizarUsuarioCommand request, CancellationToken cancellationToken)
    {
        Usuario? admin = await _context.Usuarios.FindAsync(new object[] { request.IdSolicitante }, cancellationToken);
        if (admin is null || admin.Rol != RolUsuario.Administrador)
            return RespuestaHelper.Error<string>("Solo un administrador puede actualizar usuarios.");

        Usuario? usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == request.IdUsuarioModificado, cancellationToken);
        if (usuario is null)
            return RespuestaHelper.Error<string>("Usuario no encontrado.");

        bool rolValido = Enum.TryParse<RolUsuario>(request.Rol, true, out RolUsuario nuevoRol);
        if (!rolValido)
            return RespuestaHelper.Error<string>("Rol especificado no es válido.");

        usuario.Nombre = request.Nombre;
        usuario.Email = request.Email;
        usuario.Rol = nuevoRol;

        await _context.SaveChangesAsync(cancellationToken);
        return RespuestaHelper.Exito("Usuario actualizado correctamente.");
    }
}
