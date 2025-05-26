using FluentValidation;

namespace Application.Eventos.Commands.EditarEvento;

public sealed class EditarEventoValidator : AbstractValidator<EditarEventoCommand>
{
    public EditarEventoValidator()
    {
        RuleFor(x => x.IdEvento).GreaterThan(0);
        RuleFor(x => x.NuevaUbicacion).NotEmpty().WithMessage("La ubicación es obligatoria.");
        RuleFor(x => x.NuevaCapacidadMaxima).GreaterThan(0).WithMessage("La capacidad debe ser mayor a 0.");
        RuleFor(x => x.NuevaFechaHora).GreaterThan(DateTime.UtcNow).WithMessage("La fecha debe ser futura.");
    }
}
