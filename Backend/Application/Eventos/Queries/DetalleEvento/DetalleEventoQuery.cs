using MediatR;

namespace Application.Eventos.Queries.DetalleEvento;

public record DetalleEventoQuery(int IdEvento) : IRequest<DetalleEventoDto>;
