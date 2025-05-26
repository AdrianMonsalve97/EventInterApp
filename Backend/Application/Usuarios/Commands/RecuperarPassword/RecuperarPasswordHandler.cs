using Domain.Entities;
using Infraestructure.Mailing;
using Infraestructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Contracts.Genericos;
using Shared.Helpers;
using Shared.Helppers;

namespace Application.Usuarios.Commands.RecuperarPassword;

public sealed class RecuperarPasswordHandler : IRequestHandler<RecuperarPasswordCommand, RespuestaGeneral<string>>
{
    private readonly EventosDbContext _context;
    private readonly IEmailService _emailService;

    public RecuperarPasswordHandler(EventosDbContext context, IEmailService emailService)
    {
        _context = context;
        _emailService = emailService;
    }

    public async Task<RespuestaGeneral<string>> Handle(RecuperarPasswordCommand request, CancellationToken cancellationToken)
    {
        Usuario? usuario = await _context.Usuarios
            .FirstOrDefaultAsync(u => u.NombreUsuario == request.NombreUsuario, cancellationToken);

        if (usuario is null)
            return RespuestaHelper.Error<string>("El usuario no existe.");

        string nuevaPassword = PasswordHelper.GenerarContrasenaSegura();
        PasswordHasher.CrearPasswordHash(nuevaPassword, out byte[] hash, out byte[] salt);

        usuario.PasswordHash = hash;
        usuario.PasswordSalt = salt;
        usuario.DebeCambiarPassword = true;

        await _context.SaveChangesAsync(cancellationToken);

        await _emailService.EnviarCorreoAsync(
            usuario.Email,
            "Recuperación de contraseña",
            $"<p>Hola <strong>{usuario.Nombre}</strong>,</p>" +
            $"<p>Tu nueva contraseña temporal es: <strong>{nuevaPassword}</strong></p>" +
            $"<p>Por seguridad, deberás cambiarla al iniciar sesión.</p>"
        );

        return RespuestaHelper.Exito("Se ha enviado una nueva contraseña temporal al correo registrado.");
    }
}
