using Application.Common.DTOs.EventosDtos;
using MediatR;

namespace Application.Eventos.Queries.ListarEventos;

public sealed record ListarEventosQuery(int UsuarioId) : IRequest<List<EventoDisponibleDto>>;
