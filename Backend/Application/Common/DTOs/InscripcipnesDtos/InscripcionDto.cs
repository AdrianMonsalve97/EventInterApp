namespace Application.Common.DTOs.InscripcionesDtos;

public sealed record InscripcionDto(
    int IdUsuario,
    string Nombre,
    string NombreUsuario,
    string Email,
    DateTime FechaInscripcion
);
