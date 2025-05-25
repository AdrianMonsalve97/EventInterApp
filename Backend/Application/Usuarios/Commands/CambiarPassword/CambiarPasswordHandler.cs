using Domain.Entities;
using Infraestructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Contracts.Genericos;
using Shared.Helpers;
using Shared.Helppers;

namespace Application.Usuarios.Commands.CambiarPassword;

public sealed class CambiarPasswordHandler : IRequestHandler<CambiarPasswordCommand, RespuestaGeneral<string>>
{
    private readonly EventosDbContext _context;

    public CambiarPasswordHandler(EventosDbContext context)
    {
        _context = context;
    }

    public async Task<RespuestaGeneral<string>> Handle(CambiarPasswordCommand request, CancellationToken cancellationToken)
    {
        Usuario? usuario = await _context.Usuarios
            .FirstOrDefaultAsync(u => u.Id == request.IdUsuario, cancellationToken);

        if (usuario is null)
        {
            return RespuestaHelper.Error<string>("El usuario no existe.");
        }

        bool claveValida = PasswordHasher.VerificarPassword(
            request.PasswordActual,
            usuario.PasswordHash,
            usuario.PasswordSalt
        );

        if (!claveValida)
        {
            return RespuestaHelper.Error<string>("La contraseña actual no es válida.");
        }

        PasswordHasher.CrearPasswordHash(request.PasswordNueva, out byte[] nuevoHash, out byte[] nuevoSalt);

        usuario.PasswordHash = nuevoHash;
        usuario.PasswordSalt = nuevoSalt;
        usuario.DebeCambiarPassword = false;

        _context.Usuarios.Update(usuario);
        await _context.SaveChangesAsync(cancellationToken);

        return RespuestaHelper.Exito("Contraseña actualizada correctamente.");
    }
}
