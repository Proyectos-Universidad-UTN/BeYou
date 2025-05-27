using BeYou.Domain.Models;
using FluentValidation;

namespace BeYou.Application.Validations;

public class BranchScheduleValidator : AbstractValidator<BranchSchedule>
{
    public BranchScheduleValidator()
    {
        RuleFor(m => m.ScheduleId)
           .NotEmpty().WithMessage("Debe especificar el horario");

        RuleFor(m => m.BranchId)
            .NotEmpty().WithMessage("Debe especificar la sucursal");
    }
}
