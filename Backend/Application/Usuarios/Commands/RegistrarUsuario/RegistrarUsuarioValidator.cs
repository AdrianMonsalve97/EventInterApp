using Application.Usuarios.Commands.RegistrarUsuario;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Usuarios.Comands.RegistrarUsuario
{
    public sealed class RegistrarUsuarioValidator : AbstractValidator<RegistrarUsuarioCommand>
    {
        public RegistrarUsuarioValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("El ID es obligatorio.")
                .GreaterThan(0).WithMessage("Recuerde es su numero de cedula");
            RuleFor(x => x.NombreUsuario)
                .NotEmpty().WithMessage("El nombre de usuario es obligatorio.")
                .Length(3, 20).WithMessage("El nombre de usuario debe tener entre 3 y 20 caracteres.");
            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre es obligatorio.")
                .Length(2, 50).WithMessage("El nombre debe tener entre 2 y 50 caracteres.");
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("El email es obligatorio.")
                .EmailAddress().WithMessage("El email no es válido.");
            RuleFor(x => x.Rol)
                .NotEmpty().WithMessage("El rol es obligatorio.");
            RuleFor(x => x.Usuario).NotEmpty().When(x => x.Usuario is not null);

        }
    }
}
