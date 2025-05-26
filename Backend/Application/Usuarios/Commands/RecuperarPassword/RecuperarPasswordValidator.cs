using FluentValidation;

namespace Application.Usuarios.Commands.RecuperarPassword;

public sealed class RecuperarPasswordValidator : AbstractValidator<RecuperarPasswordCommand>
{
    public RecuperarPasswordValidator()
    {
        RuleFor(x => x.NombreUsuario)
            .NotEmpty().WithMessage("Debe ingresar el nombre de usuario.");
    }
}
