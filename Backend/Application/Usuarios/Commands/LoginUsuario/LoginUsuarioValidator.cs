using FluentValidation;

namespace Application.Usuarios.Commands.LoginUsuario;

public sealed class LoginUsuarioValidator : AbstractValidator<LoginUsuarioCommand>
{
    public LoginUsuarioValidator()
    {
        RuleFor(x => x.NombreUsuario)
            .NotEmpty().WithMessage("Debe ingresar el nombre de usuario.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Debe ingresar la contraseña.");
    }
}
