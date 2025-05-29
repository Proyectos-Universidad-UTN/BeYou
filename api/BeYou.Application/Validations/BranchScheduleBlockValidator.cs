using BeYou.Domain.Models;
using FluentValidation;

namespace BeYou.Application.Validations;

public class BranchScheduleBlockValidator : AbstractValidator<BranchScheduleBlock>
{
    public BranchScheduleBlockValidator()
    {
        RuleFor(m => m.BranchScheduleId)
                .NotEmpty().WithMessage("Debe especificar el horario de la sucursal");

        RuleFor(x => x.StartHour)
        .NotEmpty().WithMessage("La hora de inicio es requerida")
        .NotNull().WithMessage("La hora de inicio no puede ser nula");

        RuleFor(x => x.EndHour)
            .NotEmpty().WithMessage("La hora de fin es requerida")
            .NotNull().WithMessage("La hora de fin no puede ser nula")
            .GreaterThan(x => x.StartHour)
            .WithMessage("La hora de fin debe ser mayor que la hora de inicio");
    }
}