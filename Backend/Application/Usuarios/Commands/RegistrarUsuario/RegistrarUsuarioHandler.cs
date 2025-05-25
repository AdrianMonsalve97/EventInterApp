using Azure.Core;
using Domain.Entities;
using Domain.Enums;
using Infraestructure.Mailing;
using Infraestructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Contracts.Genericos;
using Shared.Helpers;
using Shared.Helppers;

namespace Application.Usuarios.Commands.RegistrarUsuario;

/// <summary>
/// Handler para registrar un nuevo usuario con contraseña encriptada y validación de existencia.
/// </summary>

public sealed class RegistrarUsuarioHandler : IRequestHandler<RegistrarUsuarioCommand, RespuestaGeneral<string>>
{
    private readonly EventosDbContext _context;
    private readonly IEmailService _emailService;


    public RegistrarUsuarioHandler(EventosDbContext context, IEmailService emailService)
    {
        _context = context;
        _emailService = emailService;
    }

    /// <summary>
    /// Maneja la solicitud de registrar un nuevo usuario.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<RespuestaGeneral<string>> Handle(RegistrarUsuarioCommand request, CancellationToken cancellationToken)
    {
        if (await UsuarioYaExiste(request.Id, request.NombreUsuario, cancellationToken))
        {
            return CrearError("Ya existe un usuario con ese ID o nombre de usuario.");
        }

        RolUsuario rolAsignado = ObtenerRolConValidacion(request.Rol);

        if (!await PuedeAsignarRol(request.Usuario, rolAsignado, cancellationToken))

        {
            return CrearError("Solo un Administrador puede asignar roles Expositor o Gestionador.");
        }

        string password = PasswordHelper.GenerarContrasenaSegura();
        PasswordHasher.CrearPasswordHash(password, out byte[] hash, out byte[] salt);


        Usuario usuario = ConstruirUsuario(request, rolAsignado, hash, salt);

        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync(cancellationToken);

        await _emailService.EnviarCorreoAsync(
            usuario.Email,
            "Bienvenido a la plataforma de eventos",
            $"<p>Hola <strong>{usuario.Nombre}</strong>,</p>" +
            $"<p>Tu usuario ha sido creado exitosamente.</p>" +
            $"<p>Tu Usuario para iniciar sesion en la plataforma es: {usuario.NombreUsuario}</p>" +
            $"<p>Tu contraseña temporal es: <strong>{password}</strong></p>" +
            $"<p>Por seguridad, deberás cambiarla al iniciar sesión.</p>" +
            $"<p>este es link para ingresar a la aplicación http://localhost:4200.</p>" +
            $"<p>Saludos & gratas sorpresas en tus eventos </p>" 
        );

        return CrearExito("Usuario registrado correctamente.");
    }

    /// <summary>
    /// Verifica si el nombre de usuario o el ID ya existen en la base de datos.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="nombreUsuario"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    private async Task<bool> UsuarioYaExiste(int id, string nombreUsuario, CancellationToken cancellationToken)
    {
        // Validar ID duplicado
        bool idYaExiste = await _context.Usuarios.AnyAsync(x => x.Id == id, cancellationToken);
        if (idYaExiste)
        {
            return true;
        }

        // Validar NombreUsuario duplicado
        bool nombreYaExiste = await _context.Usuarios.AnyAsync(x => x.NombreUsuario == nombreUsuario, cancellationToken);
        if (nombreYaExiste)
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// Convierte el texto del rol a un valor de enumeración RolUsuario.
    /// </summary>
    /// <param name="rolTexto"></param>
    /// <returns></returns>
    private RolUsuario ObtenerRolConValidacion(string rolTexto)
    {
        bool valido = Enum.TryParse<RolUsuario>(rolTexto, true, out RolUsuario rol);
        return valido ? rol : RolUsuario.Asistente;
    }

    /// <summary>
    /// Verifica si el usuario que intenta asignar un rol tiene permisos para hacerlo.
    /// </summary>
    /// <param name="idCreadorTexto"></param>
    /// <param name="rolAsignado"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    private async Task<bool> PuedeAsignarRol(string idCreadorTexto, RolUsuario rolAsignado, CancellationToken cancellationToken)
    {
        if (rolAsignado is RolUsuario.Asistente) return true;

        int idCreador = int.Parse(idCreadorTexto);

        Usuario? creador = await _context.Usuarios.FindAsync(new object[] { idCreador }, cancellationToken);

        return creador is not null && creador.Rol == RolUsuario.Administrador;
    }

    /// <summary>
    /// Construye un nuevo objeto Usuario a partir de la información proporcionada en el comando.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="rol"></param>
    /// <param name="hash"></param>
    /// <param name="salt"></param>
    /// <returns></returns>
    private Usuario ConstruirUsuario(RegistrarUsuarioCommand request, RolUsuario rol, byte[] hash, byte[] salt)
    {
        return new Usuario
        {
            Id = request.Id,
            NombreUsuario = request.NombreUsuario,
            Nombre = request.Nombre,
            Email = request.Email,
            PasswordHash = hash,
            PasswordSalt = salt,
            Rol = rol,
            DebeCambiarPassword = true 
        };
    }

    /// <summary>
    /// Crea una respuesta de error con el mensaje proporcionado.
    /// </summary>
    /// <param name="mensaje"></param>
    /// <returns></returns>
    private RespuestaGeneral<string> CrearError(string mensaje)
    {
        return new RespuestaGeneral<string>(true, string.Empty, mensaje);
    }

    /// <summary>
    /// Crea una respuesta de éxito con el mensaje proporcionado.
    /// </summary>
    /// <param name="mensaje"></param>
    /// <returns></returns>

    private RespuestaGeneral<string> CrearExito(string mensaje)
    {
        return new RespuestaGeneral<string>(false, "OK", mensaje);
    }

}
