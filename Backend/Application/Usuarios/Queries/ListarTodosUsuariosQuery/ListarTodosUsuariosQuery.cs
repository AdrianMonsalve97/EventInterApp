using Application.Common.DTOs.UsuariosDtos;
using MediatR;

namespace Application.Usuarios.Queries.ListarTodosUsuarios;

public sealed record ListarTodosUsuariosQuery(int IdSolicitante) : IRequest<List<UsuarioAdminListadoDto>>;
