using FluentValidation;
using Shared.Helpers;
using Shared.Helppers;

namespace Application.Usuarios.Commands.CambiarPassword;

public sealed class CambiarPasswordValidator : AbstractValidator<CambiarPasswordCommand>
{
    public CambiarPasswordValidator()
    {
        RuleFor(x => x.IdUsuario)
            .GreaterThan(0).WithMessage("El ID del usuario es obligatorio.");

        RuleFor(x => x.PasswordActual)
            .NotEmpty().WithMessage("Debe ingresar su contraseña actual.");

        RuleFor(x => x.PasswordNueva)
            .Must(PasswordHelper.EsValida)
            .WithMessage("La nueva contraseña debe tener mínimo 8 caracteres, incluir letras, números, un carácter especial y no contener espacios.");
    }
}
