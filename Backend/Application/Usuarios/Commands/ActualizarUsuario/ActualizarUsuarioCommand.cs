using MediatR;
using Shared.Contracts.Genericos;

namespace Application.Usuarios.Commands.ActualizarUsuario;

public sealed record ActualizarUsuarioCommand(
    int IdUsuarioModificado,
    string Nombre,
    string Email,
    string Rol,
    int IdSolicitante
) : IRequest<RespuestaGeneral<string>>;
