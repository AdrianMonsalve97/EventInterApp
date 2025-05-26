using Application.Eventos.Commands.CreateEvento;
using FluentValidation;

namespace Application.Eventos.Commands.CrearEvento;

public sealed class CrearEventoValidator : AbstractValidator<CreateEventoComman>
{
    public CrearEventoValidator()
    {
        RuleFor(x => x.Nombre)
            .NotEmpty().WithMessage("El nombre del evento es obligatorio.");

        RuleFor(x => x.Descripcion)
            .NotEmpty().WithMessage("La descripción es obligatoria.");

        RuleFor(x => x.FechaHora)
            .GreaterThan(DateTime.Now).WithMessage("La fecha del evento debe ser futura.");

        RuleFor(x => x.Ubicacion)
            .NotEmpty().WithMessage("La ubicación es obligatoria.");

        RuleFor(x => x.CapacidadMaxima)
            .GreaterThan(0).WithMessage("Debe tener una capacidad mayor a cero.");
    }
}
