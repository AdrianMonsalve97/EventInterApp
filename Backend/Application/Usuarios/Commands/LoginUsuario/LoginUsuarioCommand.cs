using MediatR;
using Shared.Contracts.Genericos;
using Shared.Contracts.Autenticacion;

namespace Application.Usuarios.Commands.LoginUsuario;

public sealed record LoginUsuarioCommand(
    string NombreUsuario,
    string Password
) : IRequest<RespuestaGeneral<JwtResponseDto>>;
