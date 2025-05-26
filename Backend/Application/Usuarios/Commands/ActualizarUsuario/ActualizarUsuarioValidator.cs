using FluentValidation;

namespace Application.Usuarios.Commands.ActualizarUsuario;

public sealed class ActualizarUsuarioValidator : AbstractValidator<ActualizarUsuarioCommand>
{
    public ActualizarUsuarioValidator()
    {
        RuleFor(x => x.IdUsuarioModificado)
            .GreaterThan(0).WithMessage("Debe especificar un usuario válido.");

        RuleFor(x => x.Nombre)
            .NotEmpty().WithMessage("El nombre es obligatorio.")
            .MaximumLength(100);

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("El email es obligatorio.")
            .EmailAddress().WithMessage("Formato de email no válido.");

        RuleFor(x => x.Rol)
            .NotEmpty().WithMessage("El rol es obligatorio.");

        RuleFor(x => x.IdSolicitante)
            .GreaterThan(0).WithMessage("Debe especificar el solicitante.");
    }
}
