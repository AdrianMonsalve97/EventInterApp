namespace Application.Common.DTOs.EventosDtos;
public record EventoDisponibleDto(
    int Id,
    string Nombre,
    string Descripcion,
    DateTime FechaHora,
    string Ubicacion,
    int CapacidadMaxima,
    int CantidadInscritos,
    bool EstaInscrito
);
