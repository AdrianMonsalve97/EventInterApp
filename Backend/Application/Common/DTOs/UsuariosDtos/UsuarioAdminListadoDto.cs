namespace Application.Common.DTOs.UsuariosDtos;

public sealed record UsuarioAdminListadoDto(
    int Id,
    string Nombre,
    string NombreUsuario,
    string Email,
    string Rol,
    bool DebeCambiarPassword
);
