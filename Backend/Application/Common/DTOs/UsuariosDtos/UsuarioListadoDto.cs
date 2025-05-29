namespace Application.Common.DTOs.UsuariosDtos;

public sealed record UsuarioListadoDto(
    int Id,
    string Nombre,
    string NombreUsuario,
    string Email,
    string Rol,
    int IdEvento
);
