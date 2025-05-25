using Domain.Enums;
using Domain.ValueObjects;

namespace Domain.Entities;

/// <summary>
/// Representa a un usuario del sistema.
/// </summary>
public class Usuario
{
    /// <summary>
    /// Identificador del usuario, corresponde al número de documento.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Tipo de documento (CC, CE, NIT, etc).
    /// </summary>
    public TipoDocumento TipoDocumento { get; set; } = TipoDocumento.Cedula;

    /// <summary>
    /// Nombre visible del usuario.
    /// </summary>
    public string NombreUsuario { get; set; } = string.Empty;
    /// <summary>
    /// Nombre completo del usuario.
    /// </summary>
    public string Nombre { get; set; } = string.Empty;

    /// <summary>
    /// Correo electrónico único del usuario.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Hash de la contraseña encriptada.
    /// </summary>
    public byte[] PasswordHash { get; set; } = Array.Empty<byte>();

    /// <summary>
    /// Salt usado para generar el hash.
    /// </summary>
    public byte[] PasswordSalt { get; set; } = Array.Empty<byte>();

    /// <summary>
    /// Indica si el usuario debe cambiar su contraseña al iniciar sesión por primera vez.
    /// </summary>
    public bool DebeCambiarPassword { get; set; } = true;

    /// <summary>
    /// Rol que tiene el usuario dentro del sistema (asistente, expositor, etc).
    /// </summary>
    public RolUsuario Rol { get; set; } = RolUsuario.Asistente;

    /// <summary>
    /// Eventos creados por el usuario.
    /// </summary>
    public List<Evento> EventosCreados { get; set; } = new();

    /// <summary>
    /// Eventos a los que el usuario está inscrito.
    /// </summary>
    public List<Inscripcion> EventosInscritos { get; set; } = new();
}
