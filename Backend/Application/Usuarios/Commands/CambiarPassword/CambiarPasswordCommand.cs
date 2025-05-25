using MediatR;
using Shared.Contracts.Genericos;

namespace Application.Usuarios.Commands.CambiarPassword;

public sealed record CambiarPasswordCommand(
    int IdUsuario,
    string PasswordActual,
    string PasswordNueva
) : IRequest<RespuestaGeneral<string>>;
