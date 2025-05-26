using Application.Common.DTOs.InscripcionesDtos;
using MediatR;

namespace Application.Inscripciones.Queries.ListarInscripciones;

public sealed record ListarInscripcionesQuery(int IdEvento) : IRequest<List<InscripcionDto>>;
