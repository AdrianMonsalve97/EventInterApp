using Application.Common.DTOs.UsuariosDtos;
using MediatR;

namespace Application.Usuarios.Queries.ListarAsistentes;

public sealed record ListarAsistentesQuery(int IdEvento) : IRequest<List<UsuarioListadoDto>>;
