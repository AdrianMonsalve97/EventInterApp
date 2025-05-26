using MediatR;
using Shared.Contracts.Genericos;

namespace Application.Usuarios.Commands.RecuperarPassword;

public sealed record RecuperarPasswordCommand(string NombreUsuario) : IRequest<RespuestaGeneral<string>>;
