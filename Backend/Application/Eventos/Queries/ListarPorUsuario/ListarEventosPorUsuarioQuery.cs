using Application.Common.DTOs.EventosDtos;
using MediatR;

namespace Application.Eventos.Queries.ListarPorUsuario;

public sealed record ListarEventosPorUsuarioQuery(int IdUsuario) : IRequest<List<EventoDisponibleDto>>;
