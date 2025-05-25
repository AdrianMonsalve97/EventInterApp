using MediatR;
using Shared.Contracts;
using Shared.Contracts.Genericos;

namespace Application.Usuarios.Commands.RegistrarUsuario;

public sealed record RegistrarUsuarioCommand(
    int Id,
    string NombreUsuario,
    string Nombre,
    string Email,
    string Rol,
    string Usuario
) : IRequest<RespuestaGeneral<string>>;
